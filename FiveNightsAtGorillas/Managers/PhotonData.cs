using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;

namespace FiveNightsAtGorillas.Managers {
    public class PhotonData : MonoBehaviourPunCallbacks, IOnEventCallback {
        public const byte FNAGcode = 98; //This is used as the code and all the rest are just values that get put in the objcet to identify which is which
        public const byte RightDoor = 1;
        public const byte LeftDoor = 2;
        public enum DoorState { Open = 3, Close = 4 }
        public enum AI { Gorilla = 5, Mingus = 6, Bob = 7, Dingus = 8 }
        public const byte StartNight = 9;
        public const byte AIMove = 10;

        public static PhotonData Data;

        public bool CanUseMultiplayerDoor { get; private set; } = true;

        void Awake() { Data = this; PhotonNetwork.AddCallbackTarget(this); }

        /*
        object[] content = new object[] { null };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(Code, content, raiseEventOptions, SendOptions.SendReliable);
        */

        public void MultiplayerStartNight(byte night, byte GD, byte BD, byte MD, byte DD) {
            string BrightOffice = SandboxValues.Data.BrightOffice.ToString().Substring(0, 1)[0].ToString();
            string InfinitePower = SandboxValues.Data.InfinitePower.ToString().Substring(0, 1)[0].ToString();
            string AutoCloseDoor = SandboxValues.Data.AutoCloseDoor.ToString().Substring(0, 1)[0].ToString();
            string AutoSwitchCamera = SandboxValues.Data.AutoSwitchCamera.ToString().Substring(0, 1)[0].ToString();
            string ShorterNight = SandboxValues.Data.ShorterNight.ToString().Substring(0, 1)[0].ToString();
            string SlowPower = SandboxValues.Data.SlowPower.ToString().Substring(0, 1)[0].ToString();
            string FastPower = SandboxValues.Data.FastPower.ToString().Substring(0, 1)[0].ToString();
            string NoCamera = SandboxValues.Data.NoCamera.ToString().Substring(0, 1)[0].ToString();
            string PitchBlack = SandboxValues.Data.PitchBlack.ToString().Substring(0, 1)[0].ToString();
            string NoLights = SandboxValues.Data.NoLights.ToString().Substring(0, 1)[0].ToString();
            string LimitedPower = SandboxValues.Data.LimitedPower.ToString().Substring(0, 1)[0].ToString();

            //I don't even care if there's a better way to do this. This entire project already looks messy anyway.

            object[] content = new object[] { StartNight, night, GD, BD, MD, DD, BrightOffice, InfinitePower, AutoCloseDoor, AutoSwitchCamera, ShorterNight, SlowPower, FastPower, NoCamera, PitchBlack, NoLights, LimitedPower };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(FNAGcode, content, raiseEventOptions, SendOptions.SendReliable);
        }

        public void UseRightDoorMultiplayer(bool CloseDoor) {
            if (CanUseMultiplayerDoor) {
                if (CloseDoor) {
                    object[] content = new object[] { RightDoor, DoorState.Close };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(FNAGcode, content, raiseEventOptions, SendOptions.SendReliable);
                }
                else {
                    object[] content = new object[] { RightDoor, DoorState.Open };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(FNAGcode, content, raiseEventOptions, SendOptions.SendReliable);
                }
            }
            else {
                RefrenceManager.Data.RightDoorFailSound.Play();
            }
        }

        public void UseLeftDoorMultiplayer(bool CloseDoor) {
            if (CanUseMultiplayerDoor) {
                if (CloseDoor) {
                    object[] content = new object[] { LeftDoor, DoorState.Close };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(FNAGcode, content, raiseEventOptions, SendOptions.SendReliable);
                }
                else {
                    object[] content = new object[] { LeftDoor, DoorState.Open };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(FNAGcode, content, raiseEventOptions, SendOptions.SendReliable);
                }
            }
            else {
                RefrenceManager.Data.LeftDoorFailSound.Play();
            }
        }

        public void OnEvent(EventData photonData) {
            if (FNAG.Data.InCustomRoom) {
                if (photonData.Code == FNAGcode) {
                    object[] data = (object[])photonData.CustomData;

                    switch (data[0]) {
                        case StartNight:
                            SandboxValues.Data.ResetAllValues();

                            if (data[6].ToString().ToLower() == "t")
                                SandboxValues.Data.BrightOffice = true;
                            if (data[7].ToString().ToLower() == "t")
                                SandboxValues.Data.InfinitePower = true;
                            if (data[8].ToString().ToLower() == "t")
                                SandboxValues.Data.AutoCloseDoor = true;
                            if (data[9].ToString().ToLower() == "t")
                                SandboxValues.Data.AutoSwitchCamera = true;
                            if (data[10].ToString().ToLower() == "t")
                                SandboxValues.Data.ShorterNight = true;
                            if (data[11].ToString().ToLower() == "t")
                                SandboxValues.Data.SlowPower = true;
                            if (data[12].ToString().ToLower() == "t")
                                SandboxValues.Data.FastPower = true;
                            if (data[13].ToString().ToLower() == "t")
                                SandboxValues.Data.NoCamera = true;
                            if (data[14].ToString().ToLower() == "t")
                                SandboxValues.Data.PitchBlack = true;
                            if (data[15].ToString().ToLower() == "t")
                                SandboxValues.Data.NoLights = true;
                            if (data[16].ToString().ToLower() == "t")
                                SandboxValues.Data.LimitedPower = true;

                            FNAG.Data.StartGame((byte)data[1], (byte)data[2], (byte)data[3], (byte)data[4], (byte)data[5]);
                            break;
                        case RightDoor:
                            if ((DoorState)data[1] == DoorState.Close && FNAG.Data.IsLocalRigInGame()) {
                                DoorManager.Data.UseMPDoor(true, true);
                                StartCoroutine(DoorDelay());
                            }
                            else if ((DoorState)data[1] == DoorState.Open && FNAG.Data.IsLocalRigInGame()) {
                                DoorManager.Data.UseMPDoor(true, false);
                                StartCoroutine(DoorDelay());
                            }
                            break;
                        case LeftDoor:
                            if ((DoorState)data[1] == DoorState.Close && FNAG.Data.IsLocalRigInGame()) {
                                DoorManager.Data.UseMPDoor(false, true);
                                StartCoroutine(DoorDelay());
                            }
                            else if ((DoorState)data[1] == DoorState.Open && FNAG.Data.IsLocalRigInGame()) {
                                DoorManager.Data.UseMPDoor(false, false);
                                StartCoroutine(DoorDelay());
                            }
                            break;
                        case AIMove:
                            if ((AI)data[1] == AI.Gorilla) {
                                if ((byte)data[3] == 0) {
                                    RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().MoveGorilla($"Cam{(byte)data[1]}");
                                    CameraManager.Data.RefreshCamera();
                                }
                                else if ((byte)data[3] == 1) {
                                    if (FNAG.Data.IsLocalRigInGame()) {
                                        FNAG.Data.Jumpscare();
                                        CameraManager.Data.RefreshCamera();
                                    }
                                    StartCoroutine(JumpscareMenuDelay());
                                }
                            }
                            else if ((AI)data[1] == AI.Mingus) {
                                if ((byte)data[3] == 0) {
                                    RefrenceManager.Data.mingusParent.GetComponent<AIManager>().MoveMingus($"Cam{(byte)data[1]}");
                                    CameraManager.Data.RefreshCamera();
                                }
                                else if ((byte)data[3] == 1) {
                                    if (FNAG.Data.IsLocalRigInGame()) {
                                        FNAG.Data.Jumpscare();
                                        CameraManager.Data.RefreshCamera();
                                    }
                                    FNAG.Data.AmountOfPlayersPlaying = 0;
                                }
                            }
                            else if ((AI)data[1] == AI.Bob) {
                                if ((byte)data[3] == 0) {
                                    RefrenceManager.Data.bobParent.GetComponent<AIManager>().MoveBob($"Cam{(byte)data[1]}");
                                    CameraManager.Data.RefreshCamera();
                                }
                                else if ((byte)data[3] == 1) {
                                    if (FNAG.Data.IsLocalRigInGame()) {
                                        FNAG.Data.Jumpscare();
                                        CameraManager.Data.RefreshCamera();
                                    }
                                    FNAG.Data.AmountOfPlayersPlaying = 0;
                                }
                            }
                            else if ((AI)data[1] == AI.Dingus) {
                                if ((byte)data[3] == 0) {
                                    RefrenceManager.Data.dingusParent.GetComponent<AIManager>().MoveDingus($"Stage{(byte)data[1]}");
                                    CameraManager.Data.RefreshCamera();
                                }
                                else if ((byte)data[3] == 1) {
                                    if (FNAG.Data.IsLocalRigInGame()) {
                                        FNAG.Data.DingusRun();
                                        CameraManager.Data.RefreshCamera();
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        IEnumerator DoorDelay() {
            CanUseMultiplayerDoor = false;
            yield return new WaitForSeconds(5);
            CanUseMultiplayerDoor = true;
        }

        IEnumerator JumpscareMenuDelay() {
            yield return new WaitForSeconds(9);
            FNAG.Data.AmountOfPlayersPlaying = 0;
            FNAG.Data.CurrentPage = 1;
            FNAG.Data.Refresh();
        }
    }
}
