using BepInEx;
using FiveNightsAtGorillas.Managers;
using FiveNightsAtGorillas.Other;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Video;
using Random = UnityEngine.Random;

namespace FiveNightsAtGorillas
{
    [ModdedGamemode("fnag", "FNAG", Utilla.Models.BaseGamemode.Casual)]
    [BepInDependency("org.legoandmars.gorillatag.utilla")]
    [BepInPlugin(FNAGInfo.GUID, FNAGInfo.Name, FNAGInfo.Version)]
    public class FNAG : BaseUnityPlugin
    {
        public int Version { get; private set; } = 105;

        public static FNAG Data;
        public bool RoundCurrentlyRunning;
        public bool LocalPlayingRound;
        public bool InCustomRoom { get; private set; }
        public int CurrentPage { get; private set; } = 1;
        public bool TestMode { get; private set; } = false;
        public bool HasUpdater;
        public bool GameRunning { get; private set; }

        void Start() { Events.GameInitialized += OnGameInitialized; Data = this; }

        void OnEnable() { HarmonyPatches.ApplyHarmonyPatches(); }
        void OnDisable() { Data.enabled = true; HarmonyPatches.RemoveHarmonyPatches(); } //Sorry! I can't take the time to figure out how I'm going to disable this properly
        public void SkyColorFullBlack() { RenderSettings.ambientSkyColor = Color.black; }
        public void SkyColorGameBlack() { RenderSettings.ambientSkyColor = RefrenceManager.Data.GameSkyColor.color; }
        public void SkyColorWhite() { RenderSettings.ambientSkyColor = Color.white; }

        void OnGameInitialized(object sender, EventArgs e)
        {
            var bundle = LoadAssetBundle("FiveNightsAtGorillas.Assets.fnag");
            var map = bundle.LoadAsset<GameObject>("FNAG MAP");
            var jumpscare = bundle.LoadAsset<GameObject>("Jumpscares");
            var menu = bundle.LoadAsset<GameObject>("Menu");

            GameObject.Find("BepInEx_Manager").AddComponent<RefrenceManager>();

            RefrenceManager.Data.Menu = Instantiate(menu);
            RefrenceManager.Data.FNAGMAP = Instantiate(map);
            RefrenceManager.Data.Jumpscares = Instantiate(jumpscare);

            RefrenceManager.Data.SetRefrences();

            SetupHitSounds();
            SetupComps();
            SetupEnemys();
            SetupMenu();
            StartCoroutine(GetInfoStuff());

            if (TestMode)
            {
                RefrenceManager.Data.Menu.SetActive(true);
                RefrenceManager.Data.FNAGMAP.SetActive(true);
            }
        }

        #region EnemySetup
        void SetupEnemys()
        {
            foreach (GameObject obj in RefrenceManager.Data.gorilla) { obj.SetActive(false); }
            foreach(GameObject obj in RefrenceManager.Data.mingus) { obj.SetActive(false); }
            foreach(GameObject obj in RefrenceManager.Data.dingus) { obj.SetActive(false); }
            foreach(GameObject obj in RefrenceManager.Data.bob) { obj.SetActive(false); }
            RefrenceManager.Data.gorilla[0].SetActive(true);
            RefrenceManager.Data.mingus[0].SetActive(true);
            RefrenceManager.Data.dingus[0].SetActive(true);
            RefrenceManager.Data.bob[0].SetActive(true);
            RefrenceManager.Data.Jumpscare.SetActive(false);
            RefrenceManager.Data.StaticScreen.transform.parent = GorillaTagger.Instance.mainCamera.transform;
            RefrenceManager.Data.StaticScreen.transform.localPosition = new Vector3(0, 0, 0.6f);
            RefrenceManager.Data.StaticScreen.SetActive(false);
            RefrenceManager.Data.FNAGMAP.transform.position = new Vector3(-102.0151f, 23.7944f, -65.1198f);
            RefrenceManager.Data.Jumpscares.transform.localRotation = Quaternion.Euler(0, 180, 0);
            RefrenceManager.Data.Jumpscare.transform.parent = GorillaTagger.Instance.mainCamera.transform;
            RefrenceManager.Data.Jumpscare.transform.localPosition = new Vector3(0, -0.4f, 0.8f);
            RefrenceManager.Data.StaticScreen.transform.localScale = new Vector3(2, 1.3f, 0.3234f);
        }
        #endregion
        #region SetupHitsounds
        void SetupHitSounds()
        {
            GameObject.Find($"{RefrenceManager.Data.FNAGMAP.name}/Office/Floor/Floor").AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;
            GameObject.Find($"{RefrenceManager.Data.FNAGMAP.name}/Office/Walls/Office Walls").AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;
            GameObject.Find($"{RefrenceManager.Data.FNAGMAP.name}/Office/Chair/Cylinder (1)").AddComponent<GorillaSurfaceOverride>().overrideIndex = 3;
            GameObject.Find($"{RefrenceManager.Data.FNAGMAP.name}/TheRest/Deco/Monitors").AddComponent<GorillaSurfaceOverride>().overrideIndex = 146;
            RefrenceManager.Data.CameraScreen.AddComponent<GorillaSurfaceOverride>().overrideIndex = 29;
            RefrenceManager.Data.CameraScreen.AddComponent<BoxCollider>();
        }
        #endregion
        #region SetupComps
        void SetupComps()
        {
            RefrenceManager.Data.LeftDoor.AddComponent<DoorButton>().isLeft = true;
            RefrenceManager.Data.RightDoor.AddComponent<DoorButton>().isLeft = false;
            RefrenceManager.Data.RightLight.AddComponent<LightButton>().isLeft = false;
            RefrenceManager.Data.LeftLight.AddComponent<LightButton>().isLeft = true;
            RefrenceManager.Data.ChainLoader.AddComponent<PhotonData>();
            RefrenceManager.Data.ChainLoader.AddComponent<DoorManager>();
            RefrenceManager.Data.ChainLoader.AddComponent<CameraManager>();
            RefrenceManager.Data.ChainLoader.AddComponent<TimePowerManager>();
            RefrenceManager.Data.ChainLoader.AddComponent<SandboxValues>();
            RefrenceManager.Data.gorillaParent.AddComponent<AIManager>().AIName = "gorilla";
            RefrenceManager.Data.mingusParent.AddComponent<AIManager>().AIName = "mingus";
            RefrenceManager.Data.bobParent.AddComponent<AIManager>().AIName = "bob";
            RefrenceManager.Data.dingusParent.AddComponent<AIManager>().AIName = "dingus";
            RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos = "Cam11";
            RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos = "Cam11";
            RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos = "Cam11";
            RefrenceManager.Data.dingusParent.GetComponent<AIManager>().CamPos = "Stage1";
            RefrenceManager.Data.NearGameTrigger.AddComponent<PlayersInRound>();
            RefrenceManager.Data.gorillaBoopTrigger.AddComponent<Boop_>();

            RefrenceManager.Data.Cam1.AddComponent<CameraButton>().CameraButtonTrigger = "Cam1";
            RefrenceManager.Data.Cam2.AddComponent<CameraButton>().CameraButtonTrigger = "Cam2";
            RefrenceManager.Data.Cam3.AddComponent<CameraButton>().CameraButtonTrigger = "Cam3";
            RefrenceManager.Data.Cam4.AddComponent<CameraButton>().CameraButtonTrigger = "Cam4";
            RefrenceManager.Data.Cam5.AddComponent<CameraButton>().CameraButtonTrigger = "Cam5";
            RefrenceManager.Data.Cam6.AddComponent<CameraButton>().CameraButtonTrigger = "Cam6";
            RefrenceManager.Data.Cam7.AddComponent<CameraButton>().CameraButtonTrigger = "Cam7";
            RefrenceManager.Data.Cam8.AddComponent<CameraButton>().CameraButtonTrigger = "Cam8";
            RefrenceManager.Data.Cam9.AddComponent<CameraButton>().CameraButtonTrigger = "Cam9";
            RefrenceManager.Data.Cam10.AddComponent<CameraButton>().CameraButtonTrigger = "Cam10";
            RefrenceManager.Data.Cam11.AddComponent<CameraButton>().CameraButtonTrigger = "Cam11";

            RefrenceManager.Data.MenuIgnoreButton.AddComponent<IgnoreWarning>();
            RefrenceManager.Data.MenuStartButton[0].AddComponent<StartNight>().Night = 1;
            RefrenceManager.Data.MenuStartButton[1].AddComponent<StartNight>().Night = 2;
            RefrenceManager.Data.MenuStartButton[2].AddComponent<StartNight>().Night = 3;
            RefrenceManager.Data.MenuStartButton[3].AddComponent<StartNight>().Night = 4;
            RefrenceManager.Data.MenuStartButton[4].AddComponent<StartNight>().Night = 5;
            RefrenceManager.Data.MenuStartButton[5].AddComponent<StartNight>().Night = 6;
            RefrenceManager.Data.MenuStartButton[6].AddComponent<StartNight>().Night = 7;
            RefrenceManager.Data.MenuScrollLeftButton.AddComponent<MenuScroll>().isRight = false;
            RefrenceManager.Data.MenuScrollRightButton.AddComponent<MenuScroll>().isRight = true;

            RefrenceManager.Data.MenuCNAddGorilla.AddComponent<CNAdd>().IsGorilla = true;
            RefrenceManager.Data.MenuCNAddMingus.AddComponent<CNAdd>().IsMingus = true;
            RefrenceManager.Data.MenuCNAddBob.AddComponent<CNAdd>().IsBob = true;
            RefrenceManager.Data.MenuCNAddDingus.AddComponent<CNAdd>().IsDingus = true;

            RefrenceManager.Data.MenuCNSubGorilla.AddComponent<CNSub>().IsGorilla = true;
            RefrenceManager.Data.MenuCNSubMingus.AddComponent<CNSub>().IsMingus = true;
            RefrenceManager.Data.MenuCNSubBob.AddComponent<CNSub>().IsBob = true;
            RefrenceManager.Data.MenuCNSubDingus.AddComponent<CNSub>().IsDingus = true;

            RefrenceManager.Data.SwitchPageRight.AddComponent<SwitchPage>().isSub = false;
            RefrenceManager.Data.SwitchPageLeft.AddComponent<SwitchPage>().isSub = true;

            RefrenceManager.Data.BrightOffice.AddComponent<SandboxOption>().IsBrightOffice = true;
            RefrenceManager.Data.AutoCloseDoor.AddComponent<SandboxOption>().IsAutoCloseDoor = true;
            RefrenceManager.Data.AutoSwitchCamera.AddComponent<SandboxOption>().IsAutoSwitchCamera = true;
            RefrenceManager.Data.ShorterNight.AddComponent<SandboxOption>().IsShorterNight = true;
            RefrenceManager.Data.SlowPower.AddComponent<SandboxOption>().IsSlowPower = true;
            RefrenceManager.Data.FastPower.AddComponent<SandboxOption>().IsFastPower = true;
            RefrenceManager.Data.NoCamera.AddComponent<SandboxOption>().IsNoCamera = true;
            RefrenceManager.Data.PitchBlack.AddComponent<SandboxOption>().IsPitchBlack = true;
            RefrenceManager.Data.NoLights.AddComponent<SandboxOption>().IsNoLights = true;
            RefrenceManager.Data.LimitedPower.AddComponent<SandboxOption>().IsLimitedPower = true;
            RefrenceManager.Data.InfinitePower.AddComponent<SandboxOption>().IsInfinitePower = true;
        }
        #endregion
        #region MenuSetup
        void SetupMenu()
        {
            RefrenceManager.Data.Menu.transform.position = new Vector3(-63.078f, 12.4836f, -82.3281f);
            RefrenceManager.Data.Menu.transform.localRotation = Quaternion.Euler(0, 90, 0);

            RefrenceManager.Data.NightOneSelect.SetActive(true);
            RefrenceManager.Data.NightTwoSelect.SetActive(false);
            RefrenceManager.Data.NightThreeSelect.SetActive(false);
            RefrenceManager.Data.NightFourSelect.SetActive(false);
            RefrenceManager.Data.NightFiveSelect.SetActive(false);
            RefrenceManager.Data.NightSixSelect.SetActive(false);
            RefrenceManager.Data.CustomNightSelect.SetActive(false);

            RefrenceManager.Data.MenuScrollLeft.SetActive(false);
            RefrenceManager.Data.MenuScrollRight.SetActive(true);
            RefrenceManager.Data.MenuWarning.SetActive(false);
            RefrenceManager.Data.MenuSelects.SetActive(true);
            RefrenceManager.Data.MenuRoundRunning.SetActive(false);

            RefrenceManager.Data.Menu.SetActive(false);
            RefrenceManager.Data.FNAGMAP.SetActive(false);
        }
        #endregion

        AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        public void ChangeCurrentPage(bool isSub)
        {
            foreach (GameObject obj in RefrenceManager.Data.MenuNights)
            {
                obj.SetActive(false);
            }

            if (isSub)
            {
                CurrentPage--;
                RefrenceManager.Data.MenuNights[CurrentPage].SetActive(true);
                if (RefrenceManager.Data.CustomNightSelect.activeSelf) { RefrenceManager.Data.MenuScrollRight.SetActive(false); RefrenceManager.Data.MenuScrollLeft.SetActive(true); } else { RefrenceManager.Data.MenuScrollRight.SetActive(true); RefrenceManager.Data.MenuScrollLeft.SetActive(true); }
                if (RefrenceManager.Data.NightOneSelect.activeSelf) { RefrenceManager.Data.MenuScrollRight.SetActive(true); RefrenceManager.Data.MenuScrollLeft.SetActive(false); } else { RefrenceManager.Data.MenuScrollRight.SetActive(true); RefrenceManager.Data.MenuScrollLeft.SetActive(true); }
            }
            else 
            { 
                CurrentPage++; RefrenceManager.Data.MenuNights[CurrentPage].SetActive(true);
                if (RefrenceManager.Data.NightOneSelect.activeSelf) { RefrenceManager.Data.MenuScrollRight.SetActive(true); RefrenceManager.Data.MenuScrollLeft.SetActive(false); } else { RefrenceManager.Data.MenuScrollRight.SetActive(true); RefrenceManager.Data.MenuScrollLeft.SetActive(true); }
                if (RefrenceManager.Data.CustomNightSelect.activeSelf) { RefrenceManager.Data.MenuScrollRight.SetActive(false); RefrenceManager.Data.MenuScrollLeft.SetActive(true); } else { RefrenceManager.Data.MenuScrollRight.SetActive(true); RefrenceManager.Data.MenuScrollLeft.SetActive(true); }
            }
        }

        IEnumerator GetInfoStuff()
        {
            yield return new WaitForSeconds(10);
            RefrenceManager.Data.Version.text = Version.ToString();
            RefrenceManager.Data.HasUpdater.text = HasUpdater.ToString();
            RefrenceManager.Data.TestMode.text = TestMode.ToString();
            UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/MrBanana01/Five-Nights-At-Gorillas/master/News");
            yield return www.SendWebRequest();
            RefrenceManager.Data.RecentNews.text = www.downloadHandler.text;
        }

        [ModdedGamemodeJoin]
        void OnJoin(string gamemode) 
        { 
            InCustomRoom = true; 
            RefrenceManager.Data.Menu.SetActive(true);
            RefrenceManager.Data.FNAGMAP.SetActive(true);
        }

        [ModdedGamemodeLeave]
        void OnLeave(string gamemode) 
        {
            InCustomRoom = false;
            StopGame();
            RefrenceManager.Data.Menu.SetActive(false);
            RefrenceManager.Data.FNAGMAP.SetActive(false);
        }

        public void StopGame()
        {
            #region ResetAI
            foreach (GameObject ai in RefrenceManager.Data.gorilla) { ai.gameObject.SetActive(false); }
            foreach (GameObject ai in RefrenceManager.Data.mingus) { ai.gameObject.SetActive(false); }
            foreach (GameObject ai in RefrenceManager.Data.bob) { ai.gameObject.SetActive(false); }
            foreach (GameObject ai in RefrenceManager.Data.dingus) { ai.gameObject.SetActive(false); }
            RefrenceManager.Data.gorilla[0].SetActive(true);
            RefrenceManager.Data.mingus[0].SetActive(true);
            RefrenceManager.Data.bob[0].SetActive(true);
            RefrenceManager.Data.dingus[0].SetActive(true);
            AIManager[] AI = Resources.FindObjectsOfTypeAll<AIManager>();
            foreach(AIManager ai in AI) { ai.Difficulty = 0; ai.StopAI(); }
            #endregion
            #region ResetDoors
            DoorManager.Data.ResetDoors();
            #endregion
            CameraManager.Data.RefreshCamera();
            GameRunning = false;
        }

        public void StartGame(int Night, string GD, string MD, string BD, string DD)
        {
            #region StartGame
            Vector3 Back = new Vector3(-103.5589f, 24.4809f, -66.4852f); Teleport.TeleportPlayer(Back, 90, true);
            if (Night == 1)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 0;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 2;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 2;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 0;
            }
            else if (Night == 2)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 0;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 2;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 3;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 1;
            }
            else if (Night == 3)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 1;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 5;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 4;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 2;
            }
            else if (Night == 4)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 2;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 7;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 3;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 6;
            }
            else if (Night == 5)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 5;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 7;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 6;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 6;
            }
            else if (Night == 6)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 8;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 12;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 10;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 16;
            }
            else if (Night == 7)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = int.Parse(GD);
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = int.Parse(MD);
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = int.Parse(BD);
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = int.Parse(DD);
            }
            TimePowerManager.Data.StartEverything();
            RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().StartAI();
            RefrenceManager.Data.mingusParent.GetComponent<AIManager>().StartAI();
            RefrenceManager.Data.bobParent.GetComponent<AIManager>().StartAI();
            RefrenceManager.Data.dingusParent.GetComponent<AIManager>().StartAI();
            GameRunning = true;
            StartCoroutine(SpookyFootsteps());
            if (!SandboxValues.Data.BrightOffice) { SkyColorGameBlack(); }
            if (SandboxValues.Data.PitchBlack) { SkyColorFullBlack(); }
            if (SandboxValues.Data.NoCamera) { RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam11Nothing; }
            #endregion
        }

        IEnumerator SpookyFootsteps()
        {
            if (GameRunning)
            {
                yield return new WaitForSeconds(90);
                int random = Random.Range(1, 10);
                if (random == 6)
                {
                    RefrenceManager.Data.AnimatronicFootStepLeft.Play();
                }
                else if (random == 7) { RefrenceManager.Data.AnimatronicFootStepRight.Play(); }
                StartCoroutine(SpookyFootsteps());
            }
        }

        public void TeleportPlayerBack()
        { 
            Vector3 Back = new Vector3(-66.3163f, 12.9148f, -82.4704f); Teleport.TeleportPlayer(Back, 90, true);
        }

        public void TeleportPlayerToBox()
        {
            Teleport.TeleportPlayer(RefrenceManager.Data.BlackBoxTeleport.transform.position, 90, true);
        }

        public void Jumpscare()
        {
            RefrenceManager.Data.Jumpscare.SetActive(true);
            RefrenceManager.Data.JumpscareAnimation.Play("Jumpscare");
            RefrenceManager.Data.JumpscareSound.Play();
            StartCoroutine(JumpscareDelay());
            StopGame();
        }

        public void Poweroutage()
        {
            if (!DoorManager.Data.RightDoorOpen)
            {
                DoorManager.Data.UseLocalDoor(true);
            }
            if (!DoorManager.Data.LeftDoorOpen)
            {
                DoorManager.Data.UseLocalDoor(false);
            }
            if (DoorManager.Data.LeftLightOn)
            {
                DoorManager.Data.UseLight(false);
            }
            if (DoorManager.Data.RightLightOn)
            {
                DoorManager.Data.UseLight(true);
            }
            StopGame();
            DoorManager.Data.PowerOutage();
            RefrenceManager.Data.Poweroutage.Play();
            TimePowerManager.Data.StopOnlyPower();
            SkyColorFullBlack();
            StartCoroutine(PoweroutageDelay());
        }

        public void SixAM()
        {
            StopGame();
            RefrenceManager.Data.SixAMSound.Play();
            RefrenceManager.Data.Poweroutage.Stop();
            TeleportPlayerToBox();
            RefrenceManager.Data.SixAM.GetComponent<VideoPlayer>().Play();
            StartCoroutine(SixAMDelay());
        }

        public void DingusRun()
        {
            if (SandboxValues.Data.AutoCloseDoor) { if (DoorManager.Data.LeftDoorOpen) { DoorManager.Data.UseLocalDoor(false); } }
            RefrenceManager.Data.DingusRunning.Play();
            foreach (GameObject D in RefrenceManager.Data.dingus)
            {
                D.SetActive(false);
            }
            StartCoroutine(DingusRunDelay());
        }

        IEnumerator SixAMDelay()
        {
            TimePowerManager.Data.StopEverything();
            yield return new WaitForSeconds(10);
            TeleportPlayerBack();
            SkyColorWhite();
        }

        IEnumerator PoweroutageDelay()
        {
            yield return new WaitForSeconds(67.8f);
            Jumpscare();
        }

        IEnumerator DingusRunDelay()
        {
            yield return new WaitForSeconds(2);
            if (DoorManager.Data.LeftDoorOpen)
            {
                Jumpscare();
            }
            else
            {
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().ResetDingus();
                RefrenceManager.Data.DingusScrapingSound.Play();
                TimePowerManager.Data.DingusThing();
            }
        }

        IEnumerator JumpscareDelay()
        {
            yield return new WaitForSeconds(1.5f);
            TeleportPlayerToBox();
            RefrenceManager.Data.Jumpscare.SetActive(false);
            RefrenceManager.Data.JumpscareSound.Stop();
            RefrenceManager.Data.StaticScreen.SetActive(true);
            RefrenceManager.Data.StaticScreen.GetComponent<VideoPlayer>().Play();
            yield return new WaitForSeconds(7);
            RefrenceManager.Data.StaticScreen.SetActive(false);
            TeleportPlayerBack();
            TimePowerManager.Data.StopEverything();
            SkyColorWhite();
            RefrenceManager.Data.Jumpscare.SetActive(false);
            RefrenceManager.Data.JumpscareAnimation.StopPlayback();
            RefrenceManager.Data.JumpscareSound.Stop();
            SkyColorWhite();
        }
    }
}