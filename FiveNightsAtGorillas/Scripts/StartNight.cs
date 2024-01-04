using FiveNightsAtGorillas.Managers;
using Photon.Pun;
using UnityEngine;

namespace FiveNightsAtGorillas.Other
{
    public class StartNight : MonoBehaviour
    {
        public byte Night;

        void Awake() { gameObject.layer = 18; }

        void OnTriggerEnter(Collider other) {
            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider") {
                if (other.name == "LeftHandTriggerCollider") {
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
                }
                else if (other.name == "RightHandTriggerCollider") {
                    GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
                }

                if (Night == 7 && RefrenceManager.Data.GD.text == "1" && RefrenceManager.Data.BD.text == "9" && RefrenceManager.Data.DD.text == "8" && RefrenceManager.Data.MD.text == "7") {
                    FNAG.Data.Jumpscare();
                    return;
                }
                else {
                    if(!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom.PlayerCount <= 1) {
                        FNAG.Data.StartGame(Night, (byte)int.Parse(RefrenceManager.Data.GD.text), (byte)int.Parse(RefrenceManager.Data.MD.text.ToString()), (byte)int.Parse(RefrenceManager.Data.BD.text.ToString()), (byte)int.Parse(RefrenceManager.Data.DD.text.ToString()));
                    }
                    else if(PhotonNetwork.CurrentRoom.PlayerCount > 1) {
                        PhotonData.Data.MultiplayerStartNight(Night, (byte)int.Parse(RefrenceManager.Data.GD.text), (byte)int.Parse(RefrenceManager.Data.MD.text.ToString()), (byte)int.Parse(RefrenceManager.Data.BD.text.ToString()), (byte)int.Parse(RefrenceManager.Data.DD.text.ToString()));
                    }
                }
            }
        }
    }
}