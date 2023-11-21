using UnityEngine;
using System.Collections;
using Photon.Pun;
using FiveNightsAtGorillas.Managers.Refrences;
using Photon.Realtime;
using ExitGames.Client.Photon;
using FiveNightsAtGorillas.Managers.NetworkedData;

namespace FiveNightsAtGorillas.Managers.DoorAndLight
{
    public class DoorManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        public static DoorManager Data;
        public int ButtonTimerDelay { get; private set; } = 3;
        public bool CanUseLeftButton { get; private set; } = true;
        public bool CanUseRightButton { get; private set; } = true;
        public bool RightDoorOpen { get; private set; } = true;
        public bool LeftDoorOpen { get; private set; } = true;

        void Awake() { Data = this; }

        public void UseLocalDoor(bool isRight)
        {
            if(isRight)
            {
                if (RightDoorOpen)
                {
                    CloseOpenDoor(true, true);
                }
                else
                {
                    CloseOpenDoor(true, false);
                }
            }
            else
            {
                if(LeftDoorOpen)
                {
                    CloseOpenDoor(false, true);
                }
                else
                {
                    CloseOpenDoor(false, false);
                }
            }
        }

        public void UseOnlineDoor(bool isRight)
        {
            if (isRight)
            {
                if (RightDoorOpen)
                {
                    object[] value = new object[] { PhotonData.Key.RightDoor, PhotonData.Key.Close };
                    RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.Others };
                    PhotonNetwork.RaiseEvent((byte)PhotonData.Key.RightDoor, value, options, SendOptions.SendReliable);
                }
                else
                {
                    object[] value = new object[] { PhotonData.Key.RightDoor, PhotonData.Key.Open };
                    RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.Others };
                    PhotonNetwork.RaiseEvent((byte)PhotonData.Key.RightDoor, value, options, SendOptions.SendReliable);
                }
            }
            else
            {
                if (LeftDoorOpen)
                {
                    object[] value = new object[] { PhotonData.Key.LeftDoor, PhotonData.Key.Close };
                    RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.Others };
                    PhotonNetwork.RaiseEvent((byte)PhotonData.Key.LeftDoor, value, options, SendOptions.SendReliable);
                }
                else
                {
                    object[] value = new object[] { PhotonData.Key.LeftDoor, PhotonData.Key.Open };
                    RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.Others };
                    PhotonNetwork.RaiseEvent((byte)PhotonData.Key.LeftDoor, value, options, SendOptions.SendReliable);
                }
            }
        }

        public void OnEvent(EventData photonEvent)
        {
            object[] receivedData = (object[])photonEvent.CustomData;
            string Action = (string)receivedData[1];
            switch (photonEvent.Code)
            {
                case (byte)PhotonData.Key.RightDoor:
                    if (Action == PhotonData.Key.Close.ToString()) { CloseOpenDoor(true, true); }
                    if (Action == PhotonData.Key.Open.ToString()) { CloseOpenDoor(true, true); }
                    break;
                case (byte)PhotonData.Key.LeftDoor:
                    if (Action == PhotonData.Key.Close.ToString()) { CloseOpenDoor(false, true); }
                    if (Action == PhotonData.Key.Open.ToString()) { CloseOpenDoor(false, true); }
                    break;
            }
        }

        void CloseOpenDoor(bool isRight, bool isClose)
        {
            if(isRight)
            {
                if (isClose)
                {
                    RefrenceManager.Data.RightDoorAnimation.Play("Right Door Close");
                    RightDoorOpen = false;
                    StartCoroutine(ButtonDelay(true));
                }
                else
                {
                    RefrenceManager.Data.RightDoorAnimation.Play("Right Door OPen");
                    RightDoorOpen = true;
                    StartCoroutine(ButtonDelay(true));
                }
            }
            else
            {
                if (isClose)
                {
                    RefrenceManager.Data.LeftDoorAnimation.Play("Left Door Close");
                    LeftDoorOpen = false;
                    StartCoroutine(ButtonDelay(false));
                }
                else
                {
                    RefrenceManager.Data.LeftDoorAnimation.Play("Left Door Open");
                    LeftDoorOpen = true;
                    StartCoroutine(ButtonDelay(false));
                }
            }
        }

        IEnumerator ButtonDelay(bool isRight)
        {
            if (isRight) { CanUseRightButton = false; } else { CanUseLeftButton = false; }
            yield return new WaitForSeconds(ButtonTimerDelay);
            if (isRight) { CanUseRightButton = true; } else { CanUseLeftButton = true; }
        }
    }
}