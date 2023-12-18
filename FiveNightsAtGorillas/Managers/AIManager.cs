using Photon.Pun;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace FiveNightsAtGorillas.Managers
{
    public class AIManager : MonoBehaviourPun
    {
        public string CamPos { get; set; }
        public string AIName { get; set; }
        public bool AllowedToRun { get; private set; }
        public int Difficulty { get; set; }
        public bool AllowedToMove { get; private set; } = false;

        public void StopAI() { AllowedToRun = false; AllowedToMove = false; if (AIName != "dingus") { CamPos = "Cam11"; } else { CamPos = "Stage1"; } }
        public void StartAI()
        {
            AllowedToRun = true;
            StartCoroutine(AllowedToMoveDelay());
        }

        IEnumerator AllowedToMoveDelay()
        {
            int baseTime = 52 - Difficulty * 2;

            AllowedToMove = false;
            yield return new WaitForSeconds(baseTime > 2 ? baseTime : 2);
            AllowedToMove = true;

            if (AllowedToRun) { RestartAI(); }
        }

        #region Unused
        /*
            AllowedToMove = false;
            if(Difficulty == 1) { yield return new WaitForSeconds(50); }
            else if(Difficulty == 2) { yield return new WaitForSeconds(45); }
            else if(Difficulty == 3) { yield return new WaitForSeconds(40); }
            else if(Difficulty == 4) { yield return new WaitForSeconds(35); }
            else if(Difficulty == 5) { yield return new WaitForSeconds(32); }
            else if(Difficulty == 6) { yield return new WaitForSeconds(30); }
            else if(Difficulty == 7) { yield return new WaitForSeconds(28); }
            else if(Difficulty == 8) { yield return new WaitForSeconds(26); }
            else if(Difficulty == 9) { yield return new WaitForSeconds(24); }
            else if(Difficulty == 10) { yield return new WaitForSeconds(22); }
            else if(Difficulty == 11) { yield return new WaitForSeconds(20); }
            else if(Difficulty == 12) { yield return new WaitForSeconds(18); }
            else if(Difficulty == 13) { yield return new WaitForSeconds(16); }
            else if(Difficulty == 14) { yield return new WaitForSeconds(14); }
            else if(Difficulty == 15) { yield return new WaitForSeconds(12); }
            else if(Difficulty == 16) { yield return new WaitForSeconds(10); }
            else if(Difficulty == 17) { yield return new WaitForSeconds(9); }
            else if(Difficulty == 18) { yield return new WaitForSeconds(8); }
            else if(Difficulty == 19) { yield return new WaitForSeconds(7); }
            else if(Difficulty == 20) { yield return new WaitForSeconds(6); }
            AllowedToMove = true;
            if (AllowedToRun) { RestartAI(); }
         */
        #endregion

        void RestartAI()
        {
            if (AllowedToRun && AllowedToMove)
            {
                if (AIName == "gorilla" && Difficulty != 0 && AllowedToRun && AllowedToMove) { GorillaLocalDelay(); }
                if (AIName == "mingus" && Difficulty != 0 && AllowedToRun && AllowedToMove) { MingusLocalDelay(); }
                if (AIName == "bob" && Difficulty != 0 && AllowedToRun && AllowedToMove) { BobLocalDelay(); }
                if (AIName == "dingus" && Difficulty != 0 && AllowedToRun && AllowedToMove) { DingusLocalDelay(); }
                CameraManager.Data.RefreshCamera();
            }
        }

        #region EnemyMove
        void GorillaLocalDelay()
        {
            if (CamPos == "Cam3") { if (DoorManager.Data.RightDoorOpen) { FNAG.Data.Jumpscare(); } else { MoveGorilla("Cam10"); } return; }
            else if (CamPos == "Cam4") { int random = Random.Range(1, 3); if (random == 1) { MoveGorilla("Cam3"); } else { MoveGorilla("Cam10"); } return; }
            else if (CamPos == "Cam10") { int random = Random.Range(1, 3); if (random == 1) { MoveGorilla("Cam5"); } else { MoveGorilla("Cam4"); } return; }
            else if (CamPos == "Cam5") { MoveGorilla("Cam10"); return; }
            else if (CamPos == "Cam11") { MoveGorilla("Cam10"); return; }
        }

        void MingusLocalDelay()
        {
            if (CamPos == "Cam2") { if (DoorManager.Data.LeftDoorOpen) { FNAG.Data.Jumpscare(); } else { MoveMingus("Cam10"); } return; }
            else if (CamPos == "Cam10") { int random = Random.Range(1, 3); if (random == 1) { MoveMingus("Cam9"); } else { MoveMingus("Cam1"); } return; }
            else if (CamPos == "Cam1") { int random = Random.Range(1, 3); if (random == 1) { MoveMingus("Cam7"); } else { MoveMingus("Cam2"); } return; }
            else if (CamPos == "Cam7") { MoveMingus("Cam1"); return; }
            else if (CamPos == "Cam9") { MoveMingus("Cam10"); return; }
            else if (CamPos == "Cam11") { MoveMingus("Cam10"); return; }
        }

        void BobLocalDelay()
        {
            if (CamPos == "Cam3") { if (DoorManager.Data.RightDoorOpen) { FNAG.Data.Jumpscare(); } else { MoveBob("Cam10"); } return; }
            else if (CamPos == "Cam4") { MoveBob("Cam3"); return; }
            else if (CamPos == "Cam10") { int random = Random.Range(1, 3); if (random == 1) { MoveBob("Cam6"); } else { MoveBob("Cam4"); } return; }
            else if (CamPos == "Cam6") { int random = Random.Range(1, 3); if (random == 1) { MoveBob("Cam10"); } else { MoveBob("Cam4"); } return; }
            else if (CamPos == "Cam11") { MoveBob("Cam10"); return; }
        }

        void DingusLocalDelay()
        {
            if (CamPos == "Stage6") { FNAG.Data.DingusRun(); MoveDingus("Stage1"); return; }
            else if (CamPos == "Stage5") { MoveDingus("Stage6"); return; }
            else if (CamPos == "Stage4") { MoveDingus("Stage5"); return; }
            else if (CamPos == "Stage3") { MoveDingus("Stage4"); return; }
            else if (CamPos == "Stage2") { MoveDingus("Stage3"); return; }
            else if (CamPos == "Stage1") { MoveDingus("Stage2"); return; }
        }
        #endregion

        public void ResetDingus()
        {
            CamPos = "Stage1";
            foreach (GameObject D in RefrenceManager.Data.dingus) { D.SetActive(false); }
            RefrenceManager.Data.dingus[0].SetActive(true);
        }

        void MoveGorilla(string NewCamPos)
        {
            if (AllowedToRun)
            {
                bool CanContinue = false;
                AIManager[] OtherAI = Resources.FindObjectsOfTypeAll<AIManager>();
                foreach(AIManager AI in OtherAI)
                {
                    if(AI.CamPos == NewCamPos)
                    {
                        CanContinue = false;
                    }
                    else
                    {
                        CanContinue = true;
                    }
                }
                AIManager ai = RefrenceManager.Data.gorillaParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos && CanContinue)
                {
                    if (ai.AIName == "gorilla")
                    {
                        if (ai.Difficulty != 0)
                        {
                            ai.CamPos = NewCamPos;
                            foreach (var gPOS in RefrenceManager.Data.gorilla)
                            {
                                gPOS.SetActive(false);
                            }
                            if (NewCamPos == "Cam11") { RefrenceManager.Data.gorilla[0].SetActive(true); }
                            else if (NewCamPos == "Cam10") { RefrenceManager.Data.gorilla[1].SetActive(true); }
                            else if (NewCamPos == "Cam5") { RefrenceManager.Data.gorilla[2].SetActive(true); }
                            else if (NewCamPos == "Cam4") { RefrenceManager.Data.gorilla[3].SetActive(true); }
                            else if (NewCamPos == "Cam3") { RefrenceManager.Data.gorilla[4].SetActive(true); }

                            if (SandboxValues.Data.AutoCloseDoor && CamPos == "Cam3")
                            {
                                if (DoorManager.Data.RightDoorOpen) { DoorManager.Data.UseLocalDoor(true); }
                            }
                        }
                    }
                }
            }
            StartCoroutine(AllowedToMoveDelay());
        }

        void MoveMingus(string NewCamPos)
        {
            if (AllowedToRun)
            {
                bool CanContinue = false;
                AIManager[] OtherAI = Resources.FindObjectsOfTypeAll<AIManager>();
                foreach (AIManager AI in OtherAI)
                {
                    if (AI.CamPos == NewCamPos)
                    {
                        CanContinue = false;
                    }
                    else
                    {
                        CanContinue = true;
                    }
                }
                AIManager ai = RefrenceManager.Data.mingusParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos && CanContinue)
                {
                    if (ai.AIName == "mingus")
                    {
                        if (ai.Difficulty != 0)
                        {
                            ai.CamPos = NewCamPos;
                            foreach (var gPOS in RefrenceManager.Data.mingus)
                            {
                                gPOS.SetActive(false);
                            }
                            if (NewCamPos == "Cam11") { RefrenceManager.Data.mingus[0].SetActive(true); }
                            else if (NewCamPos == "Cam10") { RefrenceManager.Data.mingus[1].SetActive(true); }
                            else if (NewCamPos == "Cam9") { RefrenceManager.Data.mingus[2].SetActive(true); }
                            else if (NewCamPos == "Cam7") { RefrenceManager.Data.mingus[3].SetActive(true); }
                            else if (NewCamPos == "Cam1") { RefrenceManager.Data.mingus[4].SetActive(true); }
                            else if (NewCamPos == "Cam2") { RefrenceManager.Data.mingus[5].SetActive(true); }

                            if(CamPos == "Cam2")
                            {
                                int random = Random.Range(1, 5);
                                if(random == 3)
                                {
                                    RefrenceManager.Data.AnimatronicFootStepRight.Play();
                                }
                            }

                            if (SandboxValues.Data.AutoCloseDoor && CamPos == "Cam2")
                            {
                                if (DoorManager.Data.LeftDoorOpen) { DoorManager.Data.UseLocalDoor(false); }
                            }
                        }
                    }
                }
            }
            StartCoroutine(AllowedToMoveDelay());
        }

        void MoveBob(string NewCamPos)
        {
            if (AllowedToRun)
            {
                bool CanContinue = false;
                AIManager[] OtherAI = Resources.FindObjectsOfTypeAll<AIManager>();
                foreach (AIManager AI in OtherAI)
                {
                    if (AI.CamPos == NewCamPos)
                    {
                        CanContinue = false;
                    }
                    else
                    {
                        CanContinue = true;
                    }
                }
                AIManager ai = RefrenceManager.Data.bobParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos && CanContinue)
                {
                    if (ai.AIName == "bob")
                    {
                        if (ai.Difficulty != 0)
                        {
                            ai.CamPos = NewCamPos;
                            foreach (var gPOS in RefrenceManager.Data.bob)
                            {
                                gPOS.SetActive(false);
                            }
                            if (NewCamPos == "Cam11") { RefrenceManager.Data.bob[0].SetActive(true); }
                            else if (NewCamPos == "Cam10") { RefrenceManager.Data.bob[1].SetActive(true); }
                            else if (NewCamPos == "Cam6") { RefrenceManager.Data.bob[2].SetActive(true); }
                            else if (NewCamPos == "Cam4") { RefrenceManager.Data.bob[3].SetActive(true); }
                            else if (NewCamPos == "Cam3") { RefrenceManager.Data.bob[4].SetActive(true); }

                            if (CamPos == "Cam3")
                            {
                                int random = Random.Range(1, 5);
                                if (random == 3)
                                {
                                    RefrenceManager.Data.AnimatronicFootStepLeft.Play();
                                }
                            }

                            if (SandboxValues.Data.AutoCloseDoor && CamPos == "Cam3")
                            {
                                if (DoorManager.Data.RightDoorOpen) { DoorManager.Data.UseLocalDoor(true); }
                            }
                        }
                    }
                }
            }
            StartCoroutine(AllowedToMoveDelay());
        }

        void MoveDingus(string NewCamPos)
        {
            if (AllowedToRun)
            {
                AIManager ai = RefrenceManager.Data.dingusParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos)
                {
                    if (ai.AIName == "dingus")
                    {
                        if (ai.Difficulty != 0)
                        {
                            ai.CamPos = NewCamPos;
                            foreach (var gPOS in RefrenceManager.Data.dingus)
                            {
                                gPOS.SetActive(false);
                            }
                            if (NewCamPos == "Stage1") { RefrenceManager.Data.dingus[0].SetActive(true); }
                            else if (NewCamPos == "Stage2") { RefrenceManager.Data.dingus[1].SetActive(true); }
                            else if (NewCamPos == "Stage3") { RefrenceManager.Data.dingus[2].SetActive(true); }
                            else if (NewCamPos == "Stage4") { RefrenceManager.Data.dingus[3].SetActive(true); }
                            else if (NewCamPos == "Stage5") { RefrenceManager.Data.dingus[4].SetActive(true); }
                            else if (NewCamPos == "Stage6") { RefrenceManager.Data.dingus[5].SetActive(true); }
                        }
                    }
                }
            }
            StartCoroutine(AllowedToMoveDelay());
        }
    }
}