using FiveNightsAtGorillas.Managers;
using Photon.Pun;
using UnityEngine;

namespace FiveNightsAtGorillas.Other {
    public class DoorButton : MonoBehaviour {
        public bool isLeft { get; set; }

        void Awake() {
            gameObject.layer = 18;
        }

        void OnTriggerEnter(Collider other) {
            if (other.name == "LeftHandTriggerCollider") {
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }
            else if (other.name == "RightHandTriggerCollider") {
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }

            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider") {
                if (isLeft) {
                    if (DoorManager.Data.CanUseLeftButton) {
                        if (PhotonNetwork.InRoom && FNAG.Data.AmountOfPlayersPlaying > 1) {
                            if (DoorManager.Data.LeftDoorOpen) {
                                PhotonData.Data.UseLeftDoorMultiplayer(true);
                                return;
                            }
                            else {
                                PhotonData.Data.UseLeftDoorMultiplayer(false);
                                return;
                            }
                        }
                        else {
                            DoorManager.Data.UseLocalDoor(false);
                            return;
                        }
                    }
                    else {
                        RefrenceManager.Data.LeftDoorFailSound.Play();
                        return;
                    }
                }
                else {
                    if (DoorManager.Data.CanUseRightButton) {
                        if (PhotonNetwork.InRoom && FNAG.Data.AmountOfPlayersPlaying > 1) {
                            if (DoorManager.Data.RightDoorOpen) {
                                PhotonData.Data.UseRightDoorMultiplayer(true);
                                return;
                            }
                            else {
                                PhotonData.Data.UseRightDoorMultiplayer(false);
                                return;
                            }
                        }
                        else {
                            DoorManager.Data.UseLocalDoor(true);
                            return;
                        }
                    }
                    else {
                        RefrenceManager.Data.RightDoorFailSound.Play();
                        return;
                    }
                }
            }
        }
    }
}