using ExitGames.Client.Photon;
using FiveNightsAtGorillas.Managers.AI;
using FiveNightsAtGorillas.Managers.Refrences;
using FiveNightsAtGorillas.Managers.TimePower;
using Photon.Pun;
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
            if (photonEvent.CustomData != null)
            {
                object[] receivedData = (object[])photonEvent.CustomData;
                byte NightNumber = (byte)receivedData[0];
                byte GD = (byte)receivedData[1];
                byte MD = (byte)receivedData[2];
                byte BD = (byte)receivedData[3];
                byte DD = (byte)receivedData[4];
                switch (photonEvent.Code)
                {
                    case StartNight:
                        StartGame(NightNumber, GD.ToString(), MD.ToString(), BD.ToString(), DD.ToString());
                        break;
                }
            }
        }

        public void OnlineStartGame(string Night)
        {
            object[] value = new object[] { Night, RefrenceManager.Data.GD.text, RefrenceManager.Data.MD.text, RefrenceManager.Data.BD.text, RefrenceManager.Data.DD.text };

            RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(RightDoor, value, options, SendOptions.SendReliable);
        }

        public void StartGame(byte Night, string GD, string MD, string BD, string DD)
        {
            #region StartGame
            if (Night == 1)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 0;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 2;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 2;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 0;
            }
            if (Night == 2)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 0;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 2;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 3;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 1;
            }
            if (Night == 3)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 1;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 5;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 4;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 2;
            }
            if (Night == 4)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 2;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 7;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 3;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 6;
            }
            if (Night == 5)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 5;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 7;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 6;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 6;
            }
            if (Night == 6)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = 8;
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = 12;
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = 10;
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = 16;
            }
            if (Night == 7)
            {
                RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().Difficulty = int.Parse(GD);
                RefrenceManager.Data.mingusParent.GetComponent<AIManager>().Difficulty = int.Parse(MD);
                RefrenceManager.Data.bobParent.GetComponent<AIManager>().Difficulty = int.Parse(BD);
                RefrenceManager.Data.dingusParent.GetComponent<AIManager>().Difficulty = int.Parse(DD);
            }
            RefrenceManager.Data.MenuRoundRunning.SetActive(true);
            RefrenceManager.Data.MenuWarning.SetActive(false);
            RefrenceManager.Data.MenuIgnoreButton.SetActive(false);
            RefrenceManager.Data.MenuSelects.SetActive(false);
            RefrenceManager.Data.MenuScrollLeft.SetActive(false);
            RefrenceManager.Data.MenuScrollRight.SetActive(false);
            TimePowerManager.Data.StartEverything();
            RefrenceManager.Data.gorillaParent.GetComponent<AIManager>().StartAI();
            RefrenceManager.Data.mingusParent.GetComponent<AIManager>().StartAI();
            RefrenceManager.Data.bobParent.GetComponent<AIManager>().StartAI();
            RefrenceManager.Data.dingusParent.GetComponent<AIManager>().StartAI();
            FNAG.Data.SkyColorGameBlack();
            FNAG.Data.TeleportPlayerToGame();
            #endregion
        }
    }
}