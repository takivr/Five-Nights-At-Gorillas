using FiveNightsAtGorillas.Managers.AI;
using FiveNightsAtGorillas.Managers.Refrences;
using Photon.Pun;
using UnityEngine;

namespace FiveNightsAtGorillas.Managers.Cameras
{
    public class CameraManager : MonoBehaviourPunCallbacks
    {
        public static CameraManager Data;
        public string CurrentCameraPos;

        void Awake() { Data = this; }

        public void RefreshCamera()
        {
            if (CurrentCameraPos == "Cam1") { ChangeCamera("Cam1"); }
            if (CurrentCameraPos == "Cam2") { ChangeCamera("Cam2"); }
            if (CurrentCameraPos == "Cam3") { ChangeCamera("Cam3"); }
            if (CurrentCameraPos == "Cam4") { ChangeCamera("Cam4"); }
            if (CurrentCameraPos == "Cam5") { ChangeCamera("Cam5"); }
            if (CurrentCameraPos == "Cam6") { ChangeCamera("Cam6"); }
            if (CurrentCameraPos == "Cam7") { ChangeCamera("Cam7"); }
            if (CurrentCameraPos == "Cam8") { ChangeCamera("Cam8"); }
            if (CurrentCameraPos == "Cam9") { ChangeCamera("Cam9"); }
            if (CurrentCameraPos == "Cam10") { ChangeCamera("Cam10"); }
            if (CurrentCameraPos == "Cam11") { ChangeCamera("Cam11"); }
        }

        public void ChangeCamera(string NewCameraName)
        {
            foreach (GameObject allCam in RefrenceManager.Data.CameraButtons)
            {
                allCam.GetComponent<Renderer>().material = RefrenceManager.Data.WhiteRefrence;
            }
            if (NewCameraName == "Cam1") { CurrentCameraPos = "Cam1"; RefrenceManager.Data.Cam1.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam2") { CurrentCameraPos = "Cam2"; RefrenceManager.Data.Cam2.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam3") { CurrentCameraPos = "Cam3"; RefrenceManager.Data.Cam3.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam4") { CurrentCameraPos = "Cam4"; RefrenceManager.Data.Cam4.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam5") { CurrentCameraPos = "Cam5"; RefrenceManager.Data.Cam5.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam6") { CurrentCameraPos = "Cam6"; RefrenceManager.Data.Cam7.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam7") { CurrentCameraPos = "Cam7"; RefrenceManager.Data.Cam6.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam8") { CurrentCameraPos = "Cam8"; RefrenceManager.Data.Cam8.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam9") { CurrentCameraPos = "Cam9"; RefrenceManager.Data.Cam9.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam10") { CurrentCameraPos = "Cam10"; RefrenceManager.Data.Cam10.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            if (NewCameraName == "Cam11") { CurrentCameraPos = "Cam11"; RefrenceManager.Data.Cam11.GetComponent<Renderer>().material = RefrenceManager.Data.RedRefrence; }
            AIManager aiM = RefrenceManager.Data.mingusParent.GetComponent<AIManager>();
            AIManager aiG = RefrenceManager.Data.gorillaParent.GetComponent<AIManager>();
            AIManager aiD = RefrenceManager.Data.dingusParent.GetComponent<AIManager>();
            AIManager aiB = RefrenceManager.Data.bobParent.GetComponent<AIManager>();
            if (NewCameraName == "Cam1")
            {
                if (aiM.CamPos == "Cam1")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam1Mingus;
                }
                else
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam1Nothing;
                }
            }
            if (NewCameraName == "Cam2")
            {
                if (aiM.CamPos == "Cam2")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam2Mingus;
                }
                else
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam2Nothing;
                }
            }
            if (NewCameraName == "Cam3")
            {
                if (aiB.CamPos == "Cam3")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam3Bob;
                }
                else if (aiG.CamPos == "Cam3")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam3Gorilla;
                }
                else
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam3Nothing;
                }
            }
            if (NewCameraName == "Cam4")
            {
                if (aiB.CamPos == "Cam4")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam4Bob;
                }
                else if (aiG.CamPos == "Cam4")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam4Gorilla;
                }
                else
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam4Nothing;
                }
            }
            if (NewCameraName == "Cam5")
            {
                RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam5Credits;
            }
            if (NewCameraName == "Cam6")
            {
                if (aiB.CamPos == "Cam6")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam6Bob;
                }
                else
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam6Nothing;
                }
            }
            if (NewCameraName == "Cam7")
            {
                if (aiM.CamPos == "Cam7")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam7Mingus;
                }
                else
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam7Nothing;
                }
            }
            if (NewCameraName == "Cam8")
            {
                if (aiD.CamPos == "Stage1" && aiD.AIName == "dingus")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus1;
                }
                else if (aiD.CamPos == "Stage2" && aiD.AIName == "dingus")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus2;
                }
                else if (aiD.CamPos == "Stage3" && aiD.AIName == "dingus")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus3;
                }
                else if (aiD.CamPos == "Stage4" && aiD.AIName == "dingus")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus4;
                }
                else if (aiD.CamPos == "Stage5" && aiD.AIName == "dingus")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus5;
                }
                else if (aiD.CamPos == "Stage6" && aiD.AIName == "dingus")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus6;
                }
            }
            if (NewCameraName == "Cam9")
            {
                if (aiM.CamPos == "Cam9")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam9Mingus;
                }
                else
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam9Nothing;
                }
            }
            if (NewCameraName == "Cam10")
            {
                if (RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam10")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam10Mingus;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam10")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam10Bob;
                }
                else if (RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos == "Cam10")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam10Gorilla;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam10" && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam10")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam10BobMingus;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam10" && RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos == "Cam10")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam10BobGorilla;
                }
                else if (RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos == "Cam10" && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam10")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam10GorillaMingus;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam10" && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam10" && RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos == "Cam10")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam10All;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos != "Cam10" && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos != "Cam10" && RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos != "Cam10")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam10Nothing;
                }
            }
            if (NewCameraName == "Cam11")
            {
                if (RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam11")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam11Mingus;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam11")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam11Bob;
                }
                else if (RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos == "Cam11")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam11Gorilla;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam11" && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam11")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam11MingusBob;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam11" && RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos == "Cam11")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam11GorillaBob;
                }
                else if (RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos == "Cam11" && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam11")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam11GorillaMingus;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam11" && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam11" && RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos == "Cam11")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam11All;
                }
                else if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos != "Cam11" && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos != "Cam11" && RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().CamPos != "Cam11")
                {
                    RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam11Nothing;
                }
            }
        }
    }
}