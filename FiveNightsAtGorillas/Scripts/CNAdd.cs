using FiveNightsAtGorillas.Managers;
using Photon.Pun;
using UnityEngine;

namespace FiveNightsAtGorillas.Other
{
    public class CNAdd : MonoBehaviour
    {
        public bool IsGorilla;
        public bool IsMingus;
        public bool IsBob;
        public bool IsDingus;

        void Awake() { gameObject.layer = 18; }

        void OnTriggerEnter(Collider other) {
            if (other.name == "LeftHandTriggerCollider") {
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }
            else if (other.name == "RightHandTriggerCollider") {
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }

            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider") {
                if (IsGorilla) {
                    if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
                        if (RefrenceManager.Data.GD.text != "20") {
                            int value = int.Parse(RefrenceManager.Data.GD.text);
                            value++;
                            RefrenceManager.Data.GD.text = value.ToString();
                        }
                    }
                    else if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom.PlayerCount <= 1) {
                        if (RefrenceManager.Data.GD.text != "25") {
                            int value = int.Parse(RefrenceManager.Data.GD.text);
                            value++;
                            RefrenceManager.Data.GD.text = value.ToString();
                        }
                    }
                }
                else if(IsMingus) {
                    if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
                        if (RefrenceManager.Data.MD.text != "20") {
                            int value = int.Parse(RefrenceManager.Data.MD.text);
                            value++;
                            RefrenceManager.Data.MD.text = value.ToString();
                        }
                    }
                    else if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom.PlayerCount <= 1) {
                        if (RefrenceManager.Data.MD.text != "25") {
                            int value = int.Parse(RefrenceManager.Data.MD.text);
                            value++;
                            RefrenceManager.Data.MD.text = value.ToString();
                        }
                    }
                }
                else if(IsBob) {
                    if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
                        if (RefrenceManager.Data.BD.text != "20") {
                            int value = int.Parse(RefrenceManager.Data.BD.text);
                            value++;
                            RefrenceManager.Data.BD.text = value.ToString();
                        }
                    }
                    else if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom.PlayerCount <= 1) {
                        if (RefrenceManager.Data.BD.text != "25") {
                            int value = int.Parse(RefrenceManager.Data.BD.text);
                            value++;
                            RefrenceManager.Data.BD.text = value.ToString();
                        }
                    }
                }
                else if (IsDingus) {
                    if(PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
                        if (RefrenceManager.Data.DD.text != "20") {
                            int value = int.Parse(RefrenceManager.Data.DD.text);
                            value++;
                            RefrenceManager.Data.DD.text = value.ToString();
                        }
                    }
                    else if(!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom.PlayerCount <= 1) {
                        if (RefrenceManager.Data.DD.text != "25") {
                            int value = int.Parse(RefrenceManager.Data.DD.text);
                            value++;
                            RefrenceManager.Data.DD.text = value.ToString();
                        }
                    }
                }
            }
        }
    }
}