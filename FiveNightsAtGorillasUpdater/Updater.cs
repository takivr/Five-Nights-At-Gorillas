using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using Utilla;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using FiveNightsAtGorillasUpdater;
using FNAGUpdater.Button.Ignore;
using FNAGUpdater.Button.Update;
using FiveNightsAtGorillas;

namespace FNAGUpdater
{
    [BepInDependency("org.legoandmars.gorillatag.utilla")]
    [BepInPlugin(UpdaterInfo.GUID, UpdaterInfo.Name, UpdaterInfo.Version)]
    public class Updater : BaseUnityPlugin
    {
        public static Updater Instance { get; private set; }
        TextMeshPro TitleText;
        TextMeshPro Message;
        public GameObject Notif;
        GameObject UpdateButton;
        void Start()
        {
            Events.GameInitialized += OnGameInitialized;
            Instance = this;
        }

        void OnEnable()
        {
           FNAGHarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            FNAGHarmonyPatches.RemoveHarmonyPatches();
        }

        void TestDownloadNewVersion()
        {
            StartCoroutine(GetNewVersion());
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            var bundle = LoadAssetBundle("FiveNightsAtGorillasUpdater.Assets.notif");
            var asset = bundle.LoadAsset<GameObject>("NotifObject");
            Notif = Instantiate(asset);
            Notif.name = "FNAGUpdaterNotif";
            Notif.transform.position = new Vector3(-64.6088f, 12.24f, -83.2306f);
            Notif.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);

            StartCoroutine(GetVersion());

            TitleText = GameObject.Find("Notif Title").GetComponent<TextMeshPro>();
            Message = GameObject.Find("Notif Text").GetComponent<TextMeshPro>();

            UpdateButton = GameObject.Find("Update Button");
            GameObject.Find("Ignore Button").AddComponent<ButtonIgnore>();
            GameObject.Find("Ignore Button").AddComponent<BoxCollider>().isTrigger = true;
            GameObject.Find("Ignore Button").layer = 18;

            UpdateButton.AddComponent<ButtonUpdate>();
            UpdateButton.AddComponent<BoxCollider>().isTrigger = true;
            UpdateButton.layer = 18;
            UpdateButton.SetActive(false);

            Notif.SetActive(false);
        }

        AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        IEnumerator GetVersion()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/MrBanana01/Five-Nights-At-Gorillas/main/ModVersion");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.Log("Error getting FNAG version: " + www.error);
                Notif.SetActive(true);
                TitleText.text = "ERROR!";
                Message.text = "Failed to get FiveNightsAtGorillas version from github server. Try restarting your game or tell the developer about this issue. If there is a new update then update the mod manually. (if this text box keeps appearing and is annoying, feel free to delete the \"FiveNightsAtGorillasUpdater\" until the issue has been fixed)";
            }
            else
            {
                string result = www.downloadHandler.text;
                int serverVersion = new int();
                int.TryParse(result, out serverVersion);
                Debug.Log("Found FNAG version: " + serverVersion);

                if (serverVersion > FNAG.Data.Version)
                {
                    Notif.SetActive(true);
                    UpdateButton.SetActive(true);
                    TitleText.text = "NEW VERSION AVAIBLE!";
                    Message.text = "A new version of FiveNightsAtGorillas is available. To update press the \"UPDATE\" button below to update the mod. This will need to close the game in order to be able to fully update";
                }

                if (serverVersion < FNAG.Data.Version)
                {
                    Notif.SetActive(true);
                    UpdateButton.SetActive(false);
                    TitleText.text = "ON NEWER VERSION!";
                    Message.text = "It appears you are on a newer version of FiveNightsAtGorillas, we won't do anything about it because we expect that you are the developer of the mod or a beta tester, have fun with the new features or bug fixes!";
                }
            }

            www.Dispose();
        }

        public IEnumerator GetNewVersion()
        {
            Debug.Log("Start of GetNewVersion");
            using (UnityWebRequest www = UnityWebRequest.Get("https://github.com/MrBanana01/Five-Nights-At-Gorillas/releases/download/1/FiveNightsAtGorillas.zip"))
            {
                Debug.Log("Downloading new version...");
                yield return www.Send();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Starting FNAG update");
                    if (Directory.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins"))
                    {
                        Debug.Log("Putting new version in steam folder");
                        string savePath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\FiveNightsAtGorillas.zip";
                        File.WriteAllBytes(savePath, www.downloadHandler.data);
                        Process proc = null;

                        string batDir = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\BepInEx\\plugins\\FiveNightsAtGorillasUpdater";
                        proc = new Process();
                        proc.StartInfo.WorkingDirectory = batDir;
                        proc.StartInfo.FileName = "FNAGUpdate-STEAM.bat";
                        proc.StartInfo.CreateNoWindow = false;
                        proc.Start();
                        proc.WaitForExit();
                    }
                    if (Directory.Exists("C:\\Program Files\\Oculus\\Software\\Software\\another-axiom-gorilla-tag\\BepInEx\\plugins"))
                    {
                        Debug.Log("Putting new version in oculus PC folder");
                        string savePath = "C:\\Program Files\\Oculus\\Software\\Software\\another-axiom-gorilla-tag\\BepInEx\\plugins\\FiveNightsAtGorillas.zip";
                        File.WriteAllBytes(savePath, www.downloadHandler.data);
                        Process proc = null;

                        string batDir = "C:\\Program Files\\Oculus\\Software\\Software\\another-axiom-gorilla-tag\\BepInEx\\plugins\\FiveNightsAtGorillasUpdater";
                        proc = new Process();
                        proc.StartInfo.WorkingDirectory = batDir;
                        proc.StartInfo.FileName = "FNAGUpdate-OCULUS.bat";
                        proc.StartInfo.CreateNoWindow = false;
                        proc.Start();
                        proc.WaitForExit();
                    }
                    else
                    {
                        Debug.Log("Could not find game folder to update");
                        Notif.SetActive(true);
                        UpdateButton.SetActive(false);
                        TitleText.text = "ERROR!";
                        Message.text = "Could not find the game folder to put the new FNAG version in, please dowload the new FNAG update manually";
                    }
                }
            }
        }
    }
}