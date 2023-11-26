using BepInEx;
using FiveNightsAtGorillas.Managers.Refrences;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;
using System.Collections;
using FiveNightsAtGorillas.Patches.Teleport;
using FiveNightsAtGorillas.Other.Door;
using FiveNightsAtGorillas.Managers.AI;
using FiveNightsAtGorillas.Managers.DoorAndLight;
using FiveNightsAtGorillas.Managers.NetworkedData;
using FiveNightsAtGorillas.Other.Light;
using Photon.Pun;
using FiveNightsAtGorillas.Scripts;

namespace FiveNightsAtGorillas
{
    [ModdedGamemode("fnag", "FNAG", Utilla.Models.BaseGamemode.Casual)]
    [BepInDependency("org.legoandmars.gorillatag.utilla")]
    [BepInDependency("com.ahauntedarmy.gorillatag.tmploader")]
    [BepInPlugin(FNAGInfo.GUID, FNAGInfo.Name, FNAGInfo.Version)]
    public class FNAG : BaseUnityPlugin
    {
        public int Version { get; private set; } = 100;

        public static FNAG Data;
        public bool RoundCurrentlyRunning;
        public bool LocalPlayingRound;
        public bool IsPowerCurrentlyOut;
        public bool InCustomRoom { get; private set; }

        void Start() { Events.GameInitialized += OnGameInitialized; Data = this; }

        void OnEnable() { HarmonyPatches.ApplyHarmonyPatches(); }
        void OnDisable() { this.enabled = true; HarmonyPatches.RemoveHarmonyPatches(); } //Sorry! I can't take the time to figure out how I'm going to disable this properly
        void MakeFog() { RenderSettings.fog = true; RenderSettings.fogStartDistance = 7; RenderSettings.fogColor = Color.black; }
        void MakePoweroutFog() { RenderSettings.fog = true; RenderSettings.fogStartDistance = 0.3f; RenderSettings.fogColor = Color.black; }
        void RemoveFog() { RenderSettings.fog = false; }
        public void SkyColorFullBlack() { RenderSettings.ambientSkyColor = Color.black; MakeFog(); }
        public void SkyColorGameBlack() { RenderSettings.ambientSkyColor = RefrenceManager.Data.GameSkyColor.color; MakeFog(); }
        public void SkyColorWhite() { RenderSettings.ambientSkyColor = Color.white; RemoveFog(); }

        void OnGameInitialized(object sender, EventArgs e)
        {
            GameObject[] allObjs = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject obj in allObjs)
            {
                if(obj.name == "head_end")
                {
                    obj.AddComponent<BoxCollider>().isTrigger = true;
                    obj.layer = 10;
                    obj.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                }
            }

            var bundle = LoadAssetBundle("FiveNightsAtGorillas.Assets.fnag");
            var map = bundle.LoadAsset<GameObject>("FNAG MAP");
            var jumpscare = bundle.LoadAsset<GameObject>("Jumpscares");
            var menu = bundle.LoadAsset<GameObject>("Menu");

            GameObject.Find("BepInEx_Manager").AddComponent<RefrenceManager>();

            RefrenceManager.Data.Menu = Instantiate(menu);
            RefrenceManager.Data.FNAGMAP = Instantiate(map);
            RefrenceManager.Data.Jumpscares = Instantiate(jumpscare);

            RefrenceManager.Data.SetRefrences();

            GameObject.Find("FNAG MAP(Clone)/Office/Buttons").transform.localPosition = new Vector3(37.8108f, -12.4782f, -16.2527f);
            GameObject.Find("FNAG MAP(Clone)/Office/Doors").transform.localPosition = new Vector3(36.6835f, -12.4727f, -16.2383f);

            SetupComps();
            SetupEnemys();
            SetupMenu();
            SetupHitSounds();
        }

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
            RefrenceManager.Data.MenuScrollRight.SetActive(false);
            RefrenceManager.Data.MenuWarning.SetActive(true);
            RefrenceManager.Data.MenuSelects.SetActive(false);
            RefrenceManager.Data.MenuRoundRunning.SetActive(false);

            RefrenceManager.Data.Menu.SetActive(false);
        }
        #endregion
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
            RefrenceManager.Data.SixAM.SetActive(false);
            RefrenceManager.Data.Jumpscare.SetActive(false);
            RefrenceManager.Data.FNAGMAP.transform.position = new Vector3(-102.0151f, 23.7944f, -65.1198f);
            RefrenceManager.Data.Jumpscares.transform.localRotation = Quaternion.Euler(0, 90, 0);
            RefrenceManager.Data.Jumpscare.transform.parent = GorillaTagger.Instance.mainCamera.transform;
            RefrenceManager.Data.SixAM.transform.parent = GorillaTagger.Instance.mainCamera.transform;
            RefrenceManager.Data.Jumpscare.transform.localPosition = new Vector3(0, -0.5f, 0.4f);
            RefrenceManager.Data.SixAM.transform.localPosition = new Vector3(0.2273f, 0, 0.1091f);
        }
        #endregion
        #region SetupHitsounds
        void SetupHitSounds()
        {
            GameObject.Find($"{RefrenceManager.Data.FNAGMAP.name}/Office/Floor/Floor").AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;
            GameObject.Find("Office Walls").AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;
            RefrenceManager.Data.CameraScreen.AddComponent<GorillaSurfaceOverride>().overrideIndex = 29;
            GameObject.Find($"{RefrenceManager.Data.FNAGMAP.name}/Office/Desk/Collider").AddComponent<GorillaSurfaceOverride>().overrideIndex = 146;
            RefrenceManager.Data.RightDoorVoid.AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;
            RefrenceManager.Data.LeftDoorVoid.AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;
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
            RefrenceManager.Data.gorillaParent.AddComponent<AIManager>().AIName = "gorilla";
            RefrenceManager.Data.mingusParent.AddComponent<AIManager>().AIName = "mingus";
            RefrenceManager.Data.bobParent.AddComponent<AIManager>().AIName = "bob";
            RefrenceManager.Data.dingusParent.AddComponent<AIManager>().AIName = "dingus";
            RefrenceManager.Data.NearGameTrigger.AddComponent<PlayersInRound>();
            RefrenceManager.Data.NearGameTrigger.layer = 18;
        }
        #endregion

        AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        [ModdedGamemodeJoin]
        void OnJoin(string gamemode) 
        { 
            InCustomRoom = true; 
            RefrenceManager.Data.Menu.SetActive(true);
        }

        [ModdedGamemodeLeave]
        void OnLeave(string gamemode) 
        {
            InCustomRoom = false;
            Reset();
            RefrenceManager.Data.Menu.SetActive(false); RefrenceManager.Data.MenuScrollLeft.SetActive(false);
            RefrenceManager.Data.MenuScrollRight.SetActive(false);
            RefrenceManager.Data.MenuWarning.SetActive(true);
            GameObject.Find("Selects").SetActive(false);
        }

        public void Reset()
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
        }

        public void TeleportPlayerBack() 
        { 
            Vector3 Back = new Vector3(-66.3163f, 12.9148f, -82.4704f); TeleportPatch.TeleportPlayer(Back, 90, true);
        }

        public void TeleportPlayerToGame() 
        { 
            Vector3 Back = new Vector3(-103.6259f, 24.8166f, -66.3371f); TeleportPatch.TeleportPlayer(Back, 90, true);
        }

        public void Jumpscare()
        {
            RefrenceManager.Data.Jumpscare.SetActive(true);
            RefrenceManager.Data.JumpscareAnimation.Play("Jumpscare");
            RefrenceManager.Data.JumpscareSound.Play();
            StartCoroutine(JumpscareDelay());
            Reset();
        }

        public void DingusRun()
        {
            RefrenceManager.Data.DingusRunning.Play();
            foreach (GameObject D in RefrenceManager.Data.dingus)
            {
                D.SetActive(false);
            }
            StartCoroutine(DingusRunDelay());
        }

        IEnumerator DingusRunDelay()
        {
            yield return new WaitForSeconds(5);
            if (DoorManager.Data.LeftDoorOpen)
            {
                Jumpscare();
            }
            else
            {
                AIManager.Data.ResetDingus();
            }
        }

        IEnumerator JumpscareDelay()
        {
            yield return new WaitForSeconds(3);
            TeleportPlayerBack();
            SkyColorWhite();
            RefrenceManager.Data.Jumpscare.SetActive(false);
            RefrenceManager.Data.JumpscareAnimation.StopPlayback();
            RefrenceManager.Data.JumpscareSound.Stop();
            SkyColorWhite();
        }
    }
}