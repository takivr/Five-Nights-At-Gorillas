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
                        FNAG.Data.StartGame(Night.ToString(), RefrenceManager.Data.GD.text.ToString(), RefrenceManager.Data.MD.text.ToString(), RefrenceManager.Data.BD.text.ToString(), RefrenceManager.Data.DD.text.ToString());
                    }
                    else if (PhotonNetwork.InRoom)
                    {
                        object[] value = new object[] { Night, RefrenceManager.Data.GD.text.ToString(), RefrenceManager.Data.MD.text.ToString(), RefrenceManager.Data.BD.text.ToString(), RefrenceManager.Data.DD.text.ToString() };
                        RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                        PhotonNetwork.RaiseEvent(PhotonData.RightDoor, value, options, SendOptions.SendReliable);
                    }
            }
        }

        public void OnEvent(EventData photonEvent)
        {
            object[] receivedData = (object[])photonEvent.CustomData;
            string NightNumber = receivedData[0].ToString();
            string GD = receivedData[1].ToString();
            string MD = receivedData[2].ToString();
            string BD = receivedData[3].ToString();
            string DD = receivedData[4].ToString();
            switch (photonEvent.Code)
            {
                case PhotonData.StartNight:
                    FNAG.Data.StartGame(NightNumber, GD, MD, BD, DD);
                    break;
            }
        }
    }
}