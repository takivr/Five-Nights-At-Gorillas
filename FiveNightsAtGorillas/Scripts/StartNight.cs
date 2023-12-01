using ExitGames.Client.Photon;
using FiveNightsAtGorillas.Managers.NetworkedData;
using FiveNightsAtGorillas.Managers.Refrences;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.NightStart
{
    public class StartNight : MonoBehaviour, IOnEventCallback
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
                        FNAG.Data.StartGame(Night, int.Parse(RefrenceManager.Data.GD.text), int.Parse(RefrenceManager.Data.MD.text), int.Parse(RefrenceManager.Data.BD.text), int.Parse(RefrenceManager.Data.DD.text));
                    }
                    else if (PhotonNetwork.InRoom)
                    {
                        object[] value = new object[] { Night, int.Parse(RefrenceManager.Data.GD.text), int.Parse(RefrenceManager.Data.MD.text), int.Parse(RefrenceManager.Data.BD.text), int.Parse(RefrenceManager.Data.DD.text) };
                        RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                        PhotonNetwork.RaiseEvent(PhotonData.RightDoor, value, options, SendOptions.SendReliable);
                    }
            }
        }

        public void OnEvent(EventData photonEvent)
        {
            if(photonEvent.Code == PhotonData.StartNight)
            {
                object[] receivedData = (object[])photonEvent.CustomData;
                int NightNumber = int.Parse(receivedData[0].ToString());
                int GD = int.Parse(receivedData[1].ToString());
                int MD = int.Parse(receivedData[2].ToString());
                int BD = int.Parse(receivedData[3].ToString());
                int DD = int.Parse(receivedData[4].ToString());
                FNAG.Data.StartGame(NightNumber, GD, MD, BD, DD);
            }
        }
    }
}
