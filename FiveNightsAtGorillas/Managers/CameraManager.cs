using FiveNightsAtGorillas.Managers.AI;
using FiveNightsAtGorillas.Managers.Refrences;
using Photon.Pun;
using UnityEngine;

namespace FiveNightsAtGorillas.Managers.Cameras
{
    public class CameraManager : MonoBehaviourPunCallbacks
    {
        public static CameraManager Data;

        void Awake() { Data = this; }

        public void ChangeCamera(string NewCameraName)
        {
            AIManager[] AllAI = Resources.FindObjectsOfTypeAll<AIManager>();
            if (NewCameraName == "Cam1")
            {
                foreach(AIManager ai in AllAI)
                {
                    if(ai.CamPos == "Cam1")
                    {
                        if(ai.AIName == "mingus")
                        {
                            RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam1Mingus;
                        }
                    }
                    else
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam1Nothing;
                    }
                }
            }
            if (NewCameraName == "Cam2")
            {
                foreach (AIManager ai in AllAI)
                {
                    if (ai.CamPos == "Cam2")
                    {
                        if (ai.AIName == "mingus")
                        {
                            RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam2Mingus;
                        }
                    }
                    else
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam2Nothing;
                    }
                }
            }
            if (NewCameraName == "Cam3")
            {
                foreach (AIManager ai in AllAI)
                {
                    if (ai.CamPos == "Cam3")
                    {
                        if (ai.AIName == "bob")
                        {
                            RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam3Bob;
                        }
                        else if(ai.AIName == "gorilla")
                        {
                            RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam3Gorilla;
                        }
                    }
                    else
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam3Nothing;
                    }
                }
            }
            if (NewCameraName == "Cam4")
            {
                foreach (AIManager ai in AllAI)
                {
                    if (ai.CamPos == "Cam4")
                    {
                        if (ai.AIName == "bob")
                        {
                            RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam4Bob;
                        }
                        else if (ai.AIName == "gorilla")
                        {
                            RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam4Gorilla;
                        }
                    }
                    else
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam4Nothing;
                    }
                }
            }
            if (NewCameraName == "Cam5")
            {
                RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam5Credits;
            }
            if (NewCameraName == "Cam6")
            {
                foreach (AIManager ai in AllAI)
                {
                    if (ai.CamPos == "Cam6")
                    {
                        if (ai.AIName == "bob")
                        {
                            RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam6Bob;
                        }
                    }
                    else
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam6Nothing;
                    }
                }
            }
            if (NewCameraName == "Cam7")
            {
                foreach (AIManager ai in AllAI)
                {
                    if (ai.CamPos == "Cam7")
                    {
                        if (ai.AIName == "mingus")
                        {
                            RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam7Mingus;
                        }
                    }
                    else
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam7Nothing;
                    }
                }
            }
            if (NewCameraName == "Cam8")
            {
                foreach (AIManager ai in AllAI)
                {
                    if (ai.CamPos == "Stage1" && ai.AIName == "dingus")
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus1;
                    }
                    else if (ai.CamPos == "Stage2" && ai.AIName == "dingus")
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus2;
                    }
                    else if (ai.CamPos == "Stage3" && ai.AIName == "dingus")
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus3;
                    }
                    else if (ai.CamPos == "Stage4" && ai.AIName == "dingus")
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus4;
                    }
                    else if (ai.CamPos == "Stage5" && ai.AIName == "dingus")
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus5;
                    }
                    else if (ai.CamPos == "Stage6" && ai.AIName == "dingus")
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam8Dingus6;
                    }
                }
            }
            if (NewCameraName == "Cam9")
            {
                foreach (AIManager ai in AllAI)
                {
                    if (ai.CamPos == "Cam9")
                    {
                        if (ai.AIName == "mingus")
                        {
                            RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam9Mingus;
                        }
                    }
                    else
                    {
                        RefrenceManager.Data.CameraScreen.GetComponent<Renderer>().material = RefrenceManager.Data.Cam9Nothing;
                    }
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