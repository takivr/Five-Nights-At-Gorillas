using ExitGames.Client.Photon;
using FiveNightsAtGorillas.Managers.NetworkedData;
using FiveNightsAtGorillas.Managers.Refrences;
using Photon.Pun;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.NightStart
{
    public class StartNight : MonoBehaviour
    {
        public int Night;

        void Awake() { PhotonNetwork.AddCallbackTarget(this); gameObject.layer = 18; }

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                if(other.name == "LeftHandTriggerCollider")
                {
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
                }
                else if (other.name == "RightHandTriggerCollider")
                {
                    GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
                }

                if(RefrenceManager.Data.GD.text == "1" && RefrenceManager.Data.BD.text == "9" && RefrenceManager.Data.DD.text == "8" && RefrenceManager.Data.MD.text == "7")
                {
                    FNAG.Data.Jumpscare();
                    return;
                }
                if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom.PlayerCount <= 1)
                {
                    PhotonData.Data.StartGame((byte)Night, RefrenceManager.Data.GD.text.ToString(), RefrenceManager.Data.MD.text.ToString(), RefrenceManager.Data.BD.text.ToString(), RefrenceManager.Data.DD.text.ToString());
                }
                else if (PhotonNetwork.InRoom)
                {
                    PhotonData.Data.OnlineStartGame(Night.ToString());
                }
            }
        }
    }
}