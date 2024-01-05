using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;
using System;

namespace FiveNightsAtGorillas.Managers {
    public class PhotonData : MonoBehaviourPunCallbacks, IOnEventCallback {
        public const byte RightDoor = 90;
        public const byte LeftDoor = 91;
        public enum DoorState { Open = 92, Close = 93 }
        public enum AI { Gorilla = 94, Mingus = 95, Bob = 96, Dingus = 97 }
        public const byte StartNight = 98;
        public const byte AIMove = 99;

        public static PhotonData Data;

        public bool CanUseMultiplayerDoor { get; private set; } = true;

        void Awake() { Data = this; PhotonNetwork.AddCallbackTarget(this); }

        /*
        object[] content = new object[] { null };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(Code, content, raiseEventOptions, SendOptions.SendReliable);
        */

        public void MultiplayerStartNight(byte night, byte GD, byte BD, byte MD, byte DD) {
            object[] content = new object[] { night, GD, BD, MD, DD };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(StartNight, content, raiseEventOptions, SendOptions.SendReliable);
        }

        public void UseRightDoorMultiplayer(bool CloseDoor) {
            if (CanUseMultiplayerDoor) {
                if (CloseDoor) {
                    object[] content = new object[] { DoorState.Close };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(RightDoor, content, raiseEventOptions, SendOptions.SendReliable);
                }
                else {
                    object[] content = new object[] { DoorState.Open };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(RightDoor, content, raiseEventOptions, SendOptions.SendReliable);
                }
            }
            else {
                RefrenceManager.Data.RightDoorFailSound.Play();
            }
        }

        public void UseLeftDoorMultiplayer(bool CloseDoor) {
            if (CanUseMultiplayerDoor) {
                if (CloseDoor) {
                    object[] content = new object[] { DoorState.Close };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(LeftDoor, content, raiseEventOptions, SendOptions.SendReliable);
                }
                else {
                    object[] content = new object[] { DoorState.Open };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(LeftDoor, content, raiseEventOptions, SendOptions.SendReliable);
                }
            }
            else {
                RefrenceManager.Data.LeftDoorFailSound.Play();
            }
        }

        public void OnEvent(EventData photonData) {
            if (FNAG.Data.InCustomRoom) {
                if (photonData.Code == 90 || photonData.Code == 91 || photonData.Code == 92 || photonData.Code == 93 || photonData.Code == 94 || photonData.Code == 95 || photonData.Code == 96 || photonData.Code == 97 || photonData.Code == 98 || photonData.Code == 99) {
                    object[] data = (object[])photonData.CustomData;

                    switch (photonData.Code) {
                        case StartNight:
                            SandboxValues.Data.ResetAllValues();
                            FNAG.Data.StartGame((byte)data[0], (byte)data[1], (byte)data[2], (byte)data[3], (byte)data[4]);
                            break;
                        case RightDoor:
                            if ((DoorState)data[0] == DoorState.Close && FNAG.Data.IsLocalRigInGame()) {
                                DoorManager.Data.UseMPDoor(true, true);
                                StartCoroutine(DoorDelay());
                            }
                            else if ((DoorState)data[0] == DoorState.Open && FNAG.Data.IsLocalRigInGame()) {
                                DoorManager.Data.UseMPDoor(true, false);
                                StartCoroutine(DoorDelay());
                            }
                            break;
                        case LeftDoor:
                            if ((DoorState)data[0] == DoorState.Close && FNAG.Data.IsLocalRigInGame()) {
                                DoorManager.Data.UseMPDoor(false, true);
                                StartCoroutine(DoorDelay());
                            }
                            else if ((DoorState)data[0] == DoorState.Open && FNAG.Data.IsLocalRigInGame()) {
                                DoorManager.Data.UseMPDoor(false, false);
                                StartCoroutine(DoorDelay());
                            }
                            break;
                        case AIMove:
                            if ((AI)data[0] == AI.Gorilla) {
                                if ((byte)data[2] == 0) {
                                    RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().MoveGorilla($"Cam{(byte)data[1]}");
                                    CameraManager.Data.RefreshCamera();
                                }
                                else if ((byte)data[2] == 1) {
                                    if (FNAG.Data.IsLocalRigInGame()) {
                                        FNAG.Data.Jumpscare();
                                        CameraManager.Data.RefreshCamera();
                                    }
                                    StartCoroutine(JumpscareMenuDelay());
                                }
                            }
                            else if ((AI)data[0] == AI.Mingus) {
                                if ((byte)data[2] == 0) {
                                    RefrenceManager.Data.mingusParent.GetComponent<AIManager>().MoveMingus($"Cam{(byte)data[1]}");
                                    CameraManager.Data.RefreshCamera();
                                }
                                else if ((byte)data[2] == 1) {
                                    if (FNAG.Data.IsLocalRigInGame()) {
                                        FNAG.Data.Jumpscare();
                                        CameraManager.Data.RefreshCamera();
                                    }
                                    FNAG.Data.AmountOfPlayersPlaying = 0;
                                }
                            }
                            else if ((AI)data[0] == AI.Bob) {
                                if ((byte)data[2] == 0) {
                                    RefrenceManager.Data.bobParent.GetComponent<AIManager>().MoveBob($"Cam{(byte)data[1]}");
                                    CameraManager.Data.RefreshCamera();
                                }
                                else if ((byte)data[2] == 1) {
                                    if (FNAG.Data.IsLocalRigInGame()) {
                                        FNAG.Data.Jumpscare();
                                        CameraManager.Data.RefreshCamera();
                                    }
                                    FNAG.Data.AmountOfPlayersPlaying = 0;
                                }
                            }
                            else if ((AI)data[0] == AI.Dingus) {
                                if ((byte)data[2] == 0) {
                                    RefrenceManager.Data.dingusParent.GetComponent<AIManager>().MoveDingus($"Stage{(byte)data[1]}");
                                    CameraManager.Data.RefreshCamera();
                                }
                                else if ((byte)data[2] == 1) {
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
