using Photon.Pun;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Net;

namespace FiveNightsAtGorillas.Managers {
    public class AIManager : MonoBehaviourPun {

        public string CamPos { get; set; }
        public string AIName { get; set; }
        public bool AllowedToRun { get; private set; }
        public int Difficulty { get; set; }
        public bool AllowedToMove { get; private set; } = false;

        public void StopAI() { AllowedToRun = false; AllowedToMove = false; if (AIName != "dingus") { CamPos = "Cam11"; } else { CamPos = "Stage1"; } }
        public void StartAI() {
            if(Difficulty > 0) {
                AllowedToRun = true;
                StartCoroutine(AllowedToMoveDelay());
            }
        }

        IEnumerator AllowedToMoveDelay() {
            int baseTime = 52 - Difficulty * 2;

            AllowedToMove = false;
            yield return new WaitForSeconds(baseTime);
            AllowedToMove = true;

            RestartAI();
        }

        void RestartAI() {
            if(FNAG.Data.AmountOfPlayersPlaying > 1) {
                if (PhotonNetwork.LocalPlayer.IsMasterClient) {
                    if (AIName == "gorilla" && Difficulty > 0) {
                        byte CamToMove = 11;
                        byte Jumpscare;

                        if (CamPos == "Cam3") {
                            if (DoorManager.Data.RightDoorOpen) {
                                Jumpscare = 1;

                                object[] content = new object[] { PhotonData.AI.Gorilla, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                            else {
                                CamToMove = 10;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Gorilla, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                        }
                        else if (CamPos == "Cam4") {
                            CamToMove = 3;
                            Jumpscare = 0;

                            object[] content = new object[] { PhotonData.AI.Gorilla, CamToMove, Jumpscare };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Cam10") {
                            int random = Random.Range(1, 3);
                            if (random == 1) {
                                CamToMove = 5;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Gorilla, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                            else {
                                CamToMove = 4;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Gorilla, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                        }
                        else if (CamPos == "Cam5") {
                            CamToMove = 10;
                            Jumpscare = 0;

                            object[] content = new object[] { PhotonData.AI.Gorilla, CamToMove, Jumpscare };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Cam11") {
                            CamToMove = 10;
                            Jumpscare = 0;

                            object[] content = new object[] { PhotonData.AI.Gorilla, CamToMove, Jumpscare };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                    }

                    if (AIName == "mingus" && Difficulty > 0) {
                        byte CamToMove = 11;
                        byte Jumpscare;

                        if (CamPos == "Cam2") {
                            if (DoorManager.Data.LeftDoorOpen) {
                                Jumpscare = 1;

                                object[] content = new object[] { PhotonData.AI.Mingus, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                            else {
                                CamToMove = 10;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Mingus, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                        }
                        else if (CamPos == "Cam10") {
                            int random = Random.Range(1, 3);
                            if (random == 1) {
                                CamToMove = 9;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Mingus, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                            else {
                                CamToMove = 1;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Mingus, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                        }
                        else if (CamPos == "Cam1") {
                            int random = Random.Range(1, 3);
                            if (random == 1) {
                                CamToMove = 7;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Mingus, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                            else {
                                CamToMove = 2;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Mingus, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                        }
                        else if (CamPos == "Cam7") {
                            CamToMove = 1;
                            Jumpscare = 0;

                            object[] content = new object[] { PhotonData.AI.Mingus, CamToMove, Jumpscare };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Cam9") {
                            CamToMove = 10;
                            Jumpscare = 0;

                            object[] content = new object[] { PhotonData.AI.Mingus, CamToMove, Jumpscare };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Cam11") {
                            CamToMove = 10;
                            Jumpscare = 0;

                            object[] content = new object[] { PhotonData.AI.Mingus, CamToMove, Jumpscare };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                    }

                    if (AIName == "bob" && Difficulty > 0) {
                        byte CamToMove = 11;
                        byte Jumpscare;

                        if (CamPos == "Cam3") {
                            if (DoorManager.Data.RightDoorOpen) {
                                Jumpscare = 1;

                                object[] content = new object[] { PhotonData.AI.Bob, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                            else {
                                CamToMove = 10;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Bob, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                        }
                        else if (CamPos == "Cam4") {
                            CamToMove = 3;
                            Jumpscare = 0;

                            object[] content = new object[] { PhotonData.AI.Bob, CamToMove, Jumpscare };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Cam10") {
                            int random = Random.Range(1, 3);
                            if (random == 1) {
                                CamToMove = 6;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Bob, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                            else {
                                CamToMove = 4;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Bob, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                        }
                        else if (CamPos == "Cam6") {
                            int random = Random.Range(1, 3);
                            if (random == 1) {
                                CamToMove = 10;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Bob, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                            else {
                                CamToMove = 4;
                                Jumpscare = 0;

                                object[] content = new object[] { PhotonData.AI.Bob, CamToMove, Jumpscare };
                                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                                PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                                return;
                            }
                        }
                        else if (CamPos == "Cam11") {
                            CamToMove = 10;
                            Jumpscare = 0;

                            object[] content = new object[] { PhotonData.AI.Bob, CamToMove, Jumpscare };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                    }
                    else if (AIName == "dingus" && Difficulty > 0) {
                        byte StageToMove;
                        byte Run;

                        if (CamPos == "Stage6") {
                            Run = 1;
                            StageToMove = 1;

                            object[] content = new object[] { PhotonData.AI.Dingus, StageToMove, Run };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Stage5") {
                            Run = 0;
                            StageToMove = 6;

                            object[] content = new object[] { PhotonData.AI.Dingus, StageToMove, Run };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Stage4") {
                            Run = 0;
                            StageToMove = 5;

                            object[] content = new object[] { PhotonData.AI.Dingus, StageToMove, Run };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Stage3") {
                            Run = 0;
                            StageToMove = 4;

                            object[] content = new object[] { PhotonData.AI.Dingus, StageToMove, Run };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Stage2") {
                            Run = 0;
                            StageToMove = 3;

                            object[] content = new object[] { PhotonData.AI.Dingus, StageToMove, Run };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                        else if (CamPos == "Stage1") {
                            Run = 0;
                            StageToMove = 2;

                            object[] content = new object[] { PhotonData.AI.Dingus, StageToMove, Run };
                            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                            PhotonNetwork.RaiseEvent(PhotonData.AIMove, content, raiseEventOptions, SendOptions.SendReliable);
                            return;
                        }
                    }
                }
            }
            else {
                if (AIName == "gorilla" && Difficulty > 0) { GorillaLocalDelay(); }
                if (AIName == "mingus" && Difficulty > 0) { MingusLocalDelay(); }
                if (AIName == "bob" && Difficulty > 0) { BobLocalDelay(); }
                if (AIName == "dingus" && Difficulty > 0) { DingusLocalDelay(); }
                CameraManager.Data.RefreshCamera();
            }
        }

        #region EnemyMove
        void GorillaLocalDelay() {
            if (CamPos == "Cam3") { if (DoorManager.Data.RightDoorOpen) { FNAG.Data.Jumpscare(); } else { MoveGorilla("Cam10"); } return; }
            else if (CamPos == "Cam4") { int random = Random.Range(1, 3); if (random == 1) { MoveGorilla("Cam3"); } else { MoveGorilla("Cam10"); } return; }
            else if (CamPos == "Cam10") { int random = Random.Range(1, 3); if (random == 1) { MoveGorilla("Cam5"); } else { MoveGorilla("Cam4"); } return; }
            else if (CamPos == "Cam5") { MoveGorilla("Cam10"); return; }
            else if (CamPos == "Cam11") { MoveGorilla("Cam10"); return; }
        }

        void MingusLocalDelay() {
            if (CamPos == "Cam2") { if (DoorManager.Data.LeftDoorOpen) { FNAG.Data.Jumpscare(); } else { MoveMingus("Cam10"); } return; }
            else if (CamPos == "Cam10") { int random = Random.Range(1, 3); if (random == 1) { MoveMingus("Cam9"); } else { MoveMingus("Cam1"); } return; }
            else if (CamPos == "Cam1") { int random = Random.Range(1, 3); if (random == 1) { MoveMingus("Cam7"); } else { MoveMingus("Cam2"); } return; }
            else if (CamPos == "Cam7") { MoveMingus("Cam1"); return; }
            else if (CamPos == "Cam9") { MoveMingus("Cam10"); return; }
            else if (CamPos == "Cam11") { MoveMingus("Cam10"); return; }
        }

        void BobLocalDelay() {
            if (CamPos == "Cam3") { if (DoorManager.Data.RightDoorOpen) { FNAG.Data.Jumpscare(); } else { MoveBob("Cam10"); } return; }
            else if (CamPos == "Cam4") { MoveBob("Cam3"); return; }
            else if (CamPos == "Cam10") { int random = Random.Range(1, 3); if (random == 1) { MoveBob("Cam6"); } else { MoveBob("Cam4"); } return; }
            else if (CamPos == "Cam6") { int random = Random.Range(1, 3); if (random == 1) { MoveBob("Cam10"); } else { MoveBob("Cam4"); } return; }
            else if (CamPos == "Cam11") { MoveBob("Cam10"); return; }
        }

        void DingusLocalDelay() {
            if (CamPos == "Stage6") { FNAG.Data.DingusRun(); MoveDingus("Stage1"); return; }
            else if (CamPos == "Stage5") { MoveDingus("Stage6"); return; }
            else if (CamPos == "Stage4") { MoveDingus("Stage5"); return; }
            else if (CamPos == "Stage3") { MoveDingus("Stage4"); return; }
            else if (CamPos == "Stage2") { MoveDingus("Stage3"); return; }
            else if (CamPos == "Stage1") { MoveDingus("Stage2"); return; }
        }
        #endregion

        public void ResetDingus() {
            CamPos = "Stage1";
            foreach (GameObject D in RefrenceManager.Data.dingus) { D.SetActive(false); }
            RefrenceManager.Data.dingus[0].SetActive(true);
            StartAI();
        }

        public void MoveGorilla(string NewCamPos) {
            if (AllowedToRun) {
                AIManager[] OtherAI = Resources.FindObjectsOfTypeAll<AIManager>();
                bool CanContinue = true;
                bool cam11or10 = NewCamPos == "Cam11" || NewCamPos == "Cam10";
                if (!cam11or10) {
                    foreach (AIManager ai1 in OtherAI) {
                        if (ai1.CamPos == NewCamPos) {
                            CanContinue = false;
                            break;
                        }
                    }
                }
                else if (cam11or10) {
                    CanContinue = true;
                }

                AIManager ai = RefrenceManager.Data.gorillaParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos && CanContinue) {
                    if (ai.AIName == "gorilla") {
                        if (ai.Difficulty > 0) {
                            ai.CamPos = NewCamPos;
                            foreach (var gPOS in RefrenceManager.Data.gorilla) {
                                gPOS.SetActive(false);
                            }
                            if (NewCamPos == "Cam11") { RefrenceManager.Data.gorilla[0].SetActive(true); }
                            else if (NewCamPos == "Cam10") { RefrenceManager.Data.gorilla[1].SetActive(true); }
                            else if (NewCamPos == "Cam5") { RefrenceManager.Data.gorilla[2].SetActive(true); }
                            else if (NewCamPos == "Cam4") { RefrenceManager.Data.gorilla[3].SetActive(true); }
                            else if (NewCamPos == "Cam3") { RefrenceManager.Data.gorilla[4].SetActive(true); }

                            if (SandboxValues.Data.AutoCloseDoor && CamPos == "Cam3") {
                                if (DoorManager.Data.RightDoorOpen) { DoorManager.Data.UseLocalDoor(true); }
                            }
                        }
                    }
                }
            }
            StartCoroutine(AllowedToMoveDelay());
        }

        public void MoveMingus(string NewCamPos) {
            if (AllowedToRun) {
                AIManager[] OtherAI = Resources.FindObjectsOfTypeAll<AIManager>();
                bool CanContinue = true;
                bool cam11or10 = NewCamPos == "Cam11" || NewCamPos == "Cam10";
                if (!cam11or10) {
                    foreach (AIManager ai1 in OtherAI) {
                        if (ai1.CamPos == NewCamPos) {
                            CanContinue = false;
                            break;
                        }
                    }
                }
                else if (cam11or10) {
                    CanContinue = true;
                }
                AIManager ai = RefrenceManager.Data.mingusParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos && CanContinue) {
                    if (ai.AIName == "mingus") {
                        if (ai.Difficulty > 0) {
                            ai.CamPos = NewCamPos;
                            foreach (var gPOS in RefrenceManager.Data.mingus) {
                                gPOS.SetActive(false);
                            }
                            if (NewCamPos == "Cam11") { RefrenceManager.Data.mingus[0].SetActive(true); }
                            else if (NewCamPos == "Cam10") { RefrenceManager.Data.mingus[1].SetActive(true); }
                            else if (NewCamPos == "Cam9") { RefrenceManager.Data.mingus[2].SetActive(true); }
                            else if (NewCamPos == "Cam7") { RefrenceManager.Data.mingus[3].SetActive(true); }
                            else if (NewCamPos == "Cam1") { RefrenceManager.Data.mingus[4].SetActive(true); }
                            else if (NewCamPos == "Cam2") { RefrenceManager.Data.mingus[5].SetActive(true); }

                            if (SandboxValues.Data.AutoCloseDoor && CamPos == "Cam2") {
                                if (DoorManager.Data.LeftDoorOpen) { DoorManager.Data.UseLocalDoor(false); }
                            }
                        }
                    }
                }
            }
            StartCoroutine(AllowedToMoveDelay());
        }

        public void MoveBob(string NewCamPos) {
            if (AllowedToRun) {
                AIManager[] OtherAI = Resources.FindObjectsOfTypeAll<AIManager>();
                bool CanContinue = true;
                bool cam11or10 = NewCamPos == "Cam11" || NewCamPos == "Cam10";
                if (!cam11or10) {
                    foreach (AIManager ai1 in OtherAI) {
                        if (ai1.CamPos == NewCamPos) {
                            CanContinue = false;
                            break;
                        }
                    }
                }
                else if (cam11or10) {
                    CanContinue = true;
                }
                AIManager ai = RefrenceManager.Data.bobParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos && CanContinue) {
                    if (ai.AIName == "bob") {
                        if (ai.Difficulty > 0) {
                            ai.CamPos = NewCamPos;
                            foreach (var gPOS in RefrenceManager.Data.bob) {
                                gPOS.SetActive(false);
                            }
                            if (NewCamPos == "Cam11") { RefrenceManager.Data.bob[0].SetActive(true); }
                            else if (NewCamPos == "Cam10") { RefrenceManager.Data.bob[1].SetActive(true); }
                            else if (NewCamPos == "Cam6") { RefrenceManager.Data.bob[2].SetActive(true); }
                            else if (NewCamPos == "Cam4") { RefrenceManager.Data.bob[3].SetActive(true); }
                            else if (NewCamPos == "Cam3") { RefrenceManager.Data.bob[4].SetActive(true); }

                            if (SandboxValues.Data.AutoCloseDoor && CamPos == "Cam3") {
                                if (DoorManager.Data.RightDoorOpen) { DoorManager.Data.UseLocalDoor(true); }
                            }
                        }
                    }
                }
            }
            StartCoroutine(AllowedToMoveDelay());
        }

        public void MoveDingus(string NewCamPos) {
            if (AllowedToRun) {
                AIManager ai = RefrenceManager.Data.dingusParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos) {
                    if (ai.AIName == "dingus") {
                        if (ai.Difficulty > 0) {
                            ai.CamPos = NewCamPos;
                            foreach (var gPOS in RefrenceManager.Data.dingus) {
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