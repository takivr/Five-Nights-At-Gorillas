using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

namespace FiveNightsAtGorillas.Managers.NetworkedData
{
    public class PhotonData : MonoBehaviour, IOnEventCallback
    {
        public const byte RightDoor = 90;
        public const byte LeftDoor = 91;
        public const byte Gorilla = 92;
        public const byte Bob = 93;
        public const byte Mingus = 94;
        public const byte Dingus = 95;
        public const byte Close = 96;
        public const byte Open = 97;
        public const byte StartNight = 98;

        public static PhotonData Data;

        void Awake() { Data = this; }

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
                case StartNight:
                    FNAG.Data.StartGame(NightNumber, GD, MD, BD, DD);
                    break;
            }
        }
    }
}