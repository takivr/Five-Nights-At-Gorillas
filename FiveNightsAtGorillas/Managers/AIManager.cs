using ExitGames.Client.Photon;
using FiveNightsAtGorillas.Managers.NetworkedData;
using FiveNightsAtGorillas.Managers.Refrences;
using Photon.Pun;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using FiveNightsAtGorillas.Managers.DoorAndLight;
using Photon.Realtime;
using FiveNightsAtGorillas.Other.PlayerDetecter;
using FiveNightsAtGorillas.Managers.Cameras;

namespace FiveNightsAtGorillas.Managers.AI
{
    public class AIManager : MonoBehaviourPunCallbacks
    {
        public string CamPos { get; set; }
        public string AIName { get; set; }
        public bool AllowedToRun { get; private set; }
        public int Difficulty { get; set; }

        void Awake()
        {
            PhotonNetwork.AddCallbackTarget(this); 
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        public void StopAI() { AllowedToRun = false; }
        public void StartAI()
        {
            if(Difficulty != 0) { AllowedToRun = true; }
            if (PlayersInRound.Data.PlayersPlaying <= 1 && AllowedToRun)
            {
                StartCoroutine(GorillaLocalDelay());
                StartCoroutine(MingusLocalDelay());
                StartCoroutine(BobLocalDelay());
                StartCoroutine(DingusLocalDelay());
            }
            else if (PlayersInRound.Data.PlayersPlaying > 1 && AllowedToRun)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    StartCoroutine(GorillaOnlineDelay());
                    StartCoroutine(MingusOnlineDelay());
                    StartCoroutine(BobOnlineDelay());
                    StartCoroutine(DingusOnlineDelay());
                }
            }
        }

        void RestartAI()
        {
            CameraManager.Data.RefreshCamera();
            if (PlayersInRound.Data.PlayersPlaying <= 1 && AllowedToRun)
            {
                if (AIName == "gorilla" && Difficulty != 0) { StartCoroutine(GorillaLocalDelay()); }
                if (AIName == "mingus" && Difficulty != 0) { StartCoroutine(MingusLocalDelay()); }
                if (AIName == "bob" && Difficulty != 0) { StartCoroutine(BobLocalDelay()); }
                if (AIName == "dingus" && Difficulty != 0) { StartCoroutine(DingusLocalDelay()); }
            }
            else if (PlayersInRound.Data.PlayersPlaying > 1 && AllowedToRun)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    if (AIName == "gorilla" && Difficulty != 0) { StartCoroutine(GorillaOnlineDelay()); }
                    if (AIName == "mingus" && Difficulty != 0) { StartCoroutine(MingusOnlineDelay()); }
                    if (AIName == "bob" && Difficulty != 0) { StartCoroutine(BobOnlineDelay()); }
                    if (AIName == "dingus" && Difficulty != 0) { StartCoroutine(DingusOnlineDelay()); }
                }
            }
        }

        #region EnemyMove
        IEnumerator GorillaLocalDelay()
        {
            if (Difficulty != 0)
            {
                if (Difficulty == 1) { yield return new WaitForSeconds(50); }
                else if (Difficulty == 2) { yield return new WaitForSeconds(47); }
                else if (Difficulty == 3) { yield return new WaitForSeconds(44); }
                else if (Difficulty == 4) { yield return new WaitForSeconds(41); }
                else if (Difficulty == 5) { yield return new WaitForSeconds(37); }
                else if (Difficulty == 6) { yield return new WaitForSeconds(34); }
                else if (Difficulty == 7) { yield return new WaitForSeconds(31); }
                else if (Difficulty == 8) { yield return new WaitForSeconds(27); }
                else if (Difficulty == 9) { yield return new WaitForSeconds(25); }
                else if (Difficulty == 10) { yield return new WaitForSeconds(24); }
                else if (Difficulty == 11) { yield return new WaitForSeconds(23); }
                else if (Difficulty == 12) { yield return new WaitForSeconds(22); }
                else if (Difficulty == 13) { yield return new WaitForSeconds(21); }
                else if (Difficulty == 14) { yield return new WaitForSeconds(17); }
                else if (Difficulty == 15) { yield return new WaitForSeconds(14); }
                else if (Difficulty == 16) { yield return new WaitForSeconds(13); }
                else if (Difficulty == 17) { yield return new WaitForSeconds(12); }
                else if (Difficulty == 18) { yield return new WaitForSeconds(11); }
                else if (Difficulty == 19) { yield return new WaitForSeconds(10); }
                else if (Difficulty == 20) { yield return new WaitForSeconds(7); }
                if (CamPos == "Cam11") { MoveGorilla("Cam10"); yield return this; }
                else if (CamPos == "Cam10") { int random = Random.Range(1, 3); if (random == 1) { MoveGorilla("Cam5"); } else { MoveGorilla("Cam4"); } yield return this; }
                else if (CamPos == "Cam4") { int random = Random.Range(1, 3); if (random == 1) { MoveGorilla("Cam3"); } else { MoveGorilla("Cam10"); } yield return this; }
                else if (CamPos == "Cam5") { MoveGorilla("Cam10"); yield return this; }
                else if (CamPos == "Cam3") { if (DoorManager.Data.RightDoorOpen) { FNAG.Data.Jumpscare(); } yield return this; }
            }
        }

        IEnumerator MingusLocalDelay()
        {
            if (Difficulty != 0)
            {
                if (Difficulty == 1) { yield return new WaitForSeconds(50); }
                else if (Difficulty == 2) { yield return new WaitForSeconds(47); }
                else if (Difficulty == 3) { yield return new WaitForSeconds(44); }
                else if (Difficulty == 4) { yield return new WaitForSeconds(41); }
                else if (Difficulty == 5) { yield return new WaitForSeconds(37); }
                else if (Difficulty == 6) { yield return new WaitForSeconds(34); }
                else if (Difficulty == 7) { yield return new WaitForSeconds(31); }
                else if (Difficulty == 8) { yield return new WaitForSeconds(27); }
                else if (Difficulty == 9) { yield return new WaitForSeconds(25); }
                else if (Difficulty == 10) { yield return new WaitForSeconds(24); }
                else if (Difficulty == 11) { yield return new WaitForSeconds(23); }
                else if (Difficulty == 12) { yield return new WaitForSeconds(22); }
                else if (Difficulty == 13) { yield return new WaitForSeconds(21); }
                else if (Difficulty == 14) { yield return new WaitForSeconds(17); }
                else if (Difficulty == 15) { yield return new WaitForSeconds(14); }
                else if (Difficulty == 16) { yield return new WaitForSeconds(13); }
                else if (Difficulty == 17) { yield return new WaitForSeconds(12); }
                else if (Difficulty == 18) { yield return new WaitForSeconds(11); }
                else if (Difficulty == 19) { yield return new WaitForSeconds(10); }
                else if (Difficulty == 20) { yield return new WaitForSeconds(7); }
                if (CamPos == "Cam11") { MoveMingus("Cam10"); yield return this; }
                else if (CamPos == "Cam10") { int random = Random.Range(1, 3); if (random == 1) { MoveMingus("Cam9"); } else { MoveMingus("Cam1"); } yield return this; }
                else if (CamPos == "Cam1") { int random = Random.Range(1, 3); if (random == 1) { MoveMingus("Cam7"); } else { MoveMingus("Cam2"); } yield return this; }
                else if (CamPos == "Cam7") { MoveMingus("Cam1"); yield return this; }
                else if (CamPos == "Cam9") { MoveMingus("Cam10"); yield return this; }
                else if (CamPos == "Cam2") { if (DoorManager.Data.LeftDoorOpen) { FNAG.Data.Jumpscare(); } yield return this; }
            }
        }

        IEnumerator BobLocalDelay()
        {
            if (Difficulty != 0)
            {
                if (Difficulty == 1) { yield return new WaitForSeconds(50); }
                else if (Difficulty == 2) { yield return new WaitForSeconds(47); }
                else if (Difficulty == 3) { yield return new WaitForSeconds(44); }
                else if (Difficulty == 4) { yield return new WaitForSeconds(41); }
                else if (Difficulty == 5) { yield return new WaitForSeconds(37); }
                else if (Difficulty == 6) { yield return new WaitForSeconds(34); }
                else if (Difficulty == 7) { yield return new WaitForSeconds(31); }
                else if (Difficulty == 8) { yield return new WaitForSeconds(27); }
                else if (Difficulty == 9) { yield return new WaitForSeconds(25); }
                else if (Difficulty == 10) { yield return new WaitForSeconds(24); }
                else if (Difficulty == 11) { yield return new WaitForSeconds(23); }
                else if (Difficulty == 12) { yield return new WaitForSeconds(22); }
                else if (Difficulty == 13) { yield return new WaitForSeconds(21); }
                else if (Difficulty == 14) { yield return new WaitForSeconds(17); }
                else if (Difficulty == 15) { yield return new WaitForSeconds(14); }
                else if (Difficulty == 16) { yield return new WaitForSeconds(13); }
                else if (Difficulty == 17) { yield return new WaitForSeconds(12); }
                else if (Difficulty == 18) { yield return new WaitForSeconds(11); }
                else if (Difficulty == 19) { yield return new WaitForSeconds(10); }
                else if (Difficulty == 20) { yield return new WaitForSeconds(7); }
                if (CamPos == "Cam11") { MoveBob("Cam10"); yield return this; }
                else if (CamPos == "Cam10") { int random = Random.Range(1, 3); if (random == 1) { MoveBob("Cam6"); } else { MoveBob("Cam4"); } yield return this; }
                else if (CamPos == "Cam4") { MoveBob("Cam3"); yield return this; }
                else if (CamPos == "Cam6") { MoveBob("Cam10"); yield return this; }
                else if (CamPos == "Cam3") { if (DoorManager.Data.RightDoorOpen) { FNAG.Data.Jumpscare(); } yield return this; }
            }
        }

        IEnumerator DingusLocalDelay()
        {
            if (Difficulty != 0)
            {
                if (Difficulty == 1) { yield return new WaitForSeconds(50); }
                else if (Difficulty == 2) { yield return new WaitForSeconds(47); }
                else if (Difficulty == 3) { yield return new WaitForSeconds(44); }
                else if (Difficulty == 4) { yield return new WaitForSeconds(41); }
                else if (Difficulty == 5) { yield return new WaitForSeconds(37); }
                else if (Difficulty == 6) { yield return new WaitForSeconds(34); }
                else if (Difficulty == 7) { yield return new WaitForSeconds(31); }
                else if (Difficulty == 8) { yield return new WaitForSeconds(27); }
                else if (Difficulty == 9) { yield return new WaitForSeconds(25); }
                else if (Difficulty == 10) { yield return new WaitForSeconds(24); }
                else if (Difficulty == 11) { yield return new WaitForSeconds(23); }
                else if (Difficulty == 12) { yield return new WaitForSeconds(22); }
                else if (Difficulty == 13) { yield return new WaitForSeconds(21); }
                else if (Difficulty == 14) { yield return new WaitForSeconds(17); }
                else if (Difficulty == 15) { yield return new WaitForSeconds(14); }
                else if (Difficulty == 16) { yield return new WaitForSeconds(13); }
                else if (Difficulty == 17) { yield return new WaitForSeconds(12); }
                else if (Difficulty == 18) { yield return new WaitForSeconds(11); }
                else if (Difficulty == 19) { yield return new WaitForSeconds(10); }
                else if (Difficulty == 20) { yield return new WaitForSeconds(7); }
                if (CamPos == "Stage1") { MoveDingus("Stage2"); yield return this; }
                else if (CamPos == "Stage2") { MoveDingus("Stage3"); yield return this; }
                else if (CamPos == "Stage3") { MoveDingus("Stage4"); yield return this; }
                else if (CamPos == "Stage4") { MoveDingus("Stage5"); yield return this; }
                else if (CamPos == "Stage5") { MoveDingus("Stage6"); yield return this; }
                else if (CamPos == "Stage6") { FNAG.Data.DingusRun(); yield return this; }
            }
        }

        IEnumerator GorillaOnlineDelay()
        {
            if (Difficulty != 0)
            {
                if (Difficulty == 1) { yield return new WaitForSeconds(50); }
                else if (Difficulty == 2) { yield return new WaitForSeconds(47); }
                else if (Difficulty == 3) { yield return new WaitForSeconds(44); }
                else if (Difficulty == 4) { yield return new WaitForSeconds(41); }
                else if (Difficulty == 5) { yield return new WaitForSeconds(37); }
                else if (Difficulty == 6) { yield return new WaitForSeconds(34); }
                else if (Difficulty == 7) { yield return new WaitForSeconds(31); }
                else if (Difficulty == 8) { yield return new WaitForSeconds(27); }
                else if (Difficulty == 9) { yield return new WaitForSeconds(25); }
                else if (Difficulty == 10) { yield return new WaitForSeconds(24); }
                else if (Difficulty == 11) { yield return new WaitForSeconds(23); }
                else if (Difficulty == 12) { yield return new WaitForSeconds(22); }
                else if (Difficulty == 13) { yield return new WaitForSeconds(21); }
                else if (Difficulty == 14) { yield return new WaitForSeconds(17); }
                else if (Difficulty == 15) { yield return new WaitForSeconds(14); }
                else if (Difficulty == 16) { yield return new WaitForSeconds(13); }
                else if (Difficulty == 17) { yield return new WaitForSeconds(12); }
                else if (Difficulty == 18) { yield return new WaitForSeconds(11); }
                else if (Difficulty == 19) { yield return new WaitForSeconds(10); }
                else if (Difficulty == 20) { yield return new WaitForSeconds(7); }
                int randomvalue = Random.Range(1, 3);
                object[] value = new object[] { randomvalue };
                RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                PhotonNetwork.RaiseEvent((byte)PhotonData.Key.Gorilla, value, options, SendOptions.SendReliable);
            }
        }

        IEnumerator MingusOnlineDelay()
        {
            if (Difficulty != 0)
            {
                if (Difficulty == 1) { yield return new WaitForSeconds(50); }
                else if (Difficulty == 2) { yield return new WaitForSeconds(47); }
                else if (Difficulty == 3) { yield return new WaitForSeconds(44); }
                else if (Difficulty == 4) { yield return new WaitForSeconds(41); }
                else if (Difficulty == 5) { yield return new WaitForSeconds(37); }
                else if (Difficulty == 6) { yield return new WaitForSeconds(34); }
                else if (Difficulty == 7) { yield return new WaitForSeconds(31); }
                else if (Difficulty == 8) { yield return new WaitForSeconds(27); }
                else if (Difficulty == 9) { yield return new WaitForSeconds(25); }
                else if (Difficulty == 10) { yield return new WaitForSeconds(24); }
                else if (Difficulty == 11) { yield return new WaitForSeconds(23); }
                else if (Difficulty == 12) { yield return new WaitForSeconds(22); }
                else if (Difficulty == 13) { yield return new WaitForSeconds(21); }
                else if (Difficulty == 14) { yield return new WaitForSeconds(17); }
                else if (Difficulty == 15) { yield return new WaitForSeconds(14); }
                else if (Difficulty == 16) { yield return new WaitForSeconds(13); }
                else if (Difficulty == 17) { yield return new WaitForSeconds(12); }
                else if (Difficulty == 18) { yield return new WaitForSeconds(11); }
                else if (Difficulty == 19) { yield return new WaitForSeconds(10); }
                else if (Difficulty == 20) { yield return new WaitForSeconds(7); }
                int randomvalue = Random.Range(1, 3);
                object[] value = new object[] { randomvalue };
                RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                PhotonNetwork.RaiseEvent((byte)PhotonData.Key.Mingus, value, options, SendOptions.SendReliable);
            }
        }

        IEnumerator BobOnlineDelay()
        {
            if (Difficulty != 0)
            {
                if (Difficulty == 1) { yield return new WaitForSeconds(50); }
                else if (Difficulty == 2) { yield return new WaitForSeconds(47); }
                else if (Difficulty == 3) { yield return new WaitForSeconds(44); }
                else if (Difficulty == 4) { yield return new WaitForSeconds(41); }
                else if (Difficulty == 5) { yield return new WaitForSeconds(37); }
                else if (Difficulty == 6) { yield return new WaitForSeconds(34); }
                else if (Difficulty == 7) { yield return new WaitForSeconds(31); }
                else if (Difficulty == 8) { yield return new WaitForSeconds(27); }
                else if (Difficulty == 9) { yield return new WaitForSeconds(25); }
                else if (Difficulty == 10) { yield return new WaitForSeconds(24); }
                else if (Difficulty == 11) { yield return new WaitForSeconds(23); }
                else if (Difficulty == 12) { yield return new WaitForSeconds(22); }
                else if (Difficulty == 13) { yield return new WaitForSeconds(21); }
                else if (Difficulty == 14) { yield return new WaitForSeconds(17); }
                else if (Difficulty == 15) { yield return new WaitForSeconds(14); }
                else if (Difficulty == 16) { yield return new WaitForSeconds(13); }
                else if (Difficulty == 17) { yield return new WaitForSeconds(12); }
                else if (Difficulty == 18) { yield return new WaitForSeconds(11); }
                else if (Difficulty == 19) { yield return new WaitForSeconds(10); }
                else if (Difficulty == 20) { yield return new WaitForSeconds(7); }
                int randomvalue = Random.Range(1, 3);
                object[] value = new object[] { randomvalue };
                RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                PhotonNetwork.RaiseEvent((byte)PhotonData.Key.Bob, value, options, SendOptions.SendReliable);
            }
        }

        IEnumerator DingusOnlineDelay()
        {
            if (Difficulty != 0)
            {
                if (Difficulty == 1) { yield return new WaitForSeconds(50); }
                else if (Difficulty == 2) { yield return new WaitForSeconds(47); }
                else if (Difficulty == 3) { yield return new WaitForSeconds(44); }
                else if (Difficulty == 4) { yield return new WaitForSeconds(41); }
                else if (Difficulty == 5) { yield return new WaitForSeconds(37); }
                else if (Difficulty == 6) { yield return new WaitForSeconds(34); }
                else if (Difficulty == 7) { yield return new WaitForSeconds(31); }
                else if (Difficulty == 8) { yield return new WaitForSeconds(27); }
                else if (Difficulty == 9) { yield return new WaitForSeconds(25); }
                else if (Difficulty == 10) { yield return new WaitForSeconds(24); }
                else if (Difficulty == 11) { yield return new WaitForSeconds(23); }
                else if (Difficulty == 12) { yield return new WaitForSeconds(22); }
                else if (Difficulty == 13) { yield return new WaitForSeconds(21); }
                else if (Difficulty == 14) { yield return new WaitForSeconds(17); }
                else if (Difficulty == 15) { yield return new WaitForSeconds(14); }
                else if (Difficulty == 16) { yield return new WaitForSeconds(13); }
                else if (Difficulty == 17) { yield return new WaitForSeconds(12); }
                else if (Difficulty == 18) { yield return new WaitForSeconds(11); }
                else if (Difficulty == 19) { yield return new WaitForSeconds(10); }
                else if (Difficulty == 20) { yield return new WaitForSeconds(7); }
                RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                PhotonNetwork.RaiseEvent((byte)PhotonData.Key.Dingus, null, options, SendOptions.SendReliable);
            }
        }
        #endregion

        void OnEvent(EventData photonEvent)
        {
            object[] receivedData = (object[])photonEvent.CustomData;
            int random = (int)receivedData[0];
            switch (photonEvent.Code)
            {
                case (byte)PhotonData.Key.Gorilla:
                    if (CamPos == "Cam11") { MoveGorilla("Cam10"); return; }
                    else if (CamPos == "Cam10") { if (random == 1) { MoveGorilla("Cam5"); } else { MoveGorilla("Cam4"); } return; }
                    else if (CamPos == "Cam4") { if (random == 1) { MoveGorilla("Cam3"); } else { MoveGorilla("Cam10"); } return; }
                    else if (CamPos == "Cam5") { MoveGorilla("Cam10"); return; }
                    else if (CamPos == "Cam3") { if (DoorManager.Data.RightDoorOpen) { FNAG.Data.Jumpscare(); } return; }
                    break;
                case (byte)PhotonData.Key.Mingus:
                    if (CamPos == "Cam11") { MoveMingus("Cam10"); return; }
                    else if (CamPos == "Cam10") { if (random == 1) { MoveMingus("Cam9"); } else { MoveMingus("Cam1"); } return; }
                    else if (CamPos == "Cam7") { MoveMingus("Cam1"); return; }
                    else if (CamPos == "Cam9") { MoveMingus("Cam10"); return; }
                    else if (CamPos == "Cam1") { if (random == 1) { MoveMingus("Cam7"); } else { MoveMingus("Cam2"); } return; }
                    else if (CamPos == "Cam2") { if (DoorManager.Data.LeftDoorOpen) { FNAG.Data.Jumpscare(); } return; }
                    break;
                case (byte)PhotonData.Key.Bob:
                    if (CamPos == "Cam11") { MoveBob("Cam10"); return; }
                    else if (CamPos == "Cam10") { if (random == 1) { MoveBob("Cam6"); } else { MoveBob("Cam4"); } return; }
                    else if (CamPos == "Cam4") { MoveBob("Cam3"); return; }
                    else if (CamPos == "Cam6") { MoveBob("Cam10"); return; }
                    else if (CamPos == "Cam3") { if (DoorManager.Data.RightDoorOpen) { FNAG.Data.Jumpscare(); } return; }
                    break;
                case (byte)PhotonData.Key.Dingus:
                    if (CamPos == "Stage1") { MoveDingus("Stage2"); return; }
                    if (CamPos == "Stage2") { MoveDingus("Stage3"); return; }
                    if (CamPos == "Stage3") { MoveDingus("Stage4"); return; }
                    if (CamPos == "Stage4") { MoveDingus("Stage5"); return; }
                    if (CamPos == "Stage5") { MoveDingus("Stage6"); return; }
                    if (CamPos == "Stage6") { FNAG.Data.DingusRun(); return; }
                    break;
            }
        }

        public void ResetDingus()
        {
            if (gameObject.GetComponent<AIManager>().AIName == "dingus")
            {
                CamPos = "Stage1";
                foreach (GameObject D in RefrenceManager.Data.dingus) { D.SetActive(false); }
            }
        }

        void MoveGorilla(string NewCamPos)
        {
            if (AllowedToRun)
            {
                AIManager ai = RefrenceManager.Data.gorillaParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos)
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
                        }
                    }
                }
                RestartAI();
            }
        }

        void MoveMingus(string NewCamPos)
        {
            if (AllowedToRun)
            {
                AIManager ai = RefrenceManager.Data.mingusParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos)
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
                        }
                    }
                }
                RestartAI();
            }
        }

        void MoveBob(string NewCamPos)
        {
            if (AllowedToRun)
            {
                AIManager ai = RefrenceManager.Data.bobParent.GetComponent<AIManager>();
                if (ai.CamPos != NewCamPos)
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
                        }
                    }
                }
                RestartAI();
            }
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
                RestartAI();
            }
        }
    }
}