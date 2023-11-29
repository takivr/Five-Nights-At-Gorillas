﻿using UnityEngine;
using System.Collections;
using Photon.Pun;
using FiveNightsAtGorillas.Managers.Refrences;
using Photon.Realtime;
using ExitGames.Client.Photon;
using FiveNightsAtGorillas.Managers.NetworkedData;
using FiveNightsAtGorillas.Managers.TimePower;

namespace FiveNightsAtGorillas.Managers.DoorAndLight
{
    public class DoorManager : MonoBehaviourPunCallbacks
    {
        public static DoorManager Data;
        public int ButtonTimerDelay { get; private set; } = 3;
        public float LightTimerDelay { get; private set; } = 0.5f;
        public bool CanUseLeftButton { get; private set; } = true;
        public bool CanUseRightButton { get; private set; } = true;
        public bool RightDoorOpen { get; private set; } = true;
        public bool LeftDoorOpen { get; private set; } = true;
        public bool RightLightOn { get; private set; }
        public bool LeftLightOn { get; private set; }
        public bool CanUseLeftLight { get; private set; }
        public bool CanUseRightLight { get; private set; }

        void Awake() { Data = this; PhotonNetwork.AddCallbackTarget(this); PhotonNetwork.NetworkingClient.EventReceived += OnEvent; }

        //R door opened: 2.272
        //R door closed: 0.772

        //L door opened: 2.35
        //L door closed: 0.75

        public void ResetDoors()
        {
            RightDoorOpen = true;
            LeftDoorOpen = true;
            CloseOpenDoor(true, false, 2.272f);
            CloseOpenDoor(false, false, 2.35f);
            RightLightOn = false;
            LeftLightOn = false;
            RefrenceManager.Data.LeftDoorVoid.SetActive(true);
            RefrenceManager.Data.LeftLightSound.Stop();
            RefrenceManager.Data.RightDoorVoid.SetActive(true);
            RefrenceManager.Data.RightLightSound.Stop();
        }

        public void UseLocalDoor(bool isRight)
        {
            if (isRight)
            {
                if (RightDoorOpen)
                {
                    CloseOpenDoor(true, true, 0.772f);
                }
                else
                {
                    CloseOpenDoor(true, false, 2.272f);
                }
            }
            else
            {
                if (LeftDoorOpen)
                {
                    CloseOpenDoor(false, true, 0.75f);
                }
                else
                {
                    CloseOpenDoor(false, false, 2.35f);
                }
            }
        }

        public void UseLight(bool isRight)
        {
            if (isRight)
            {
                if (RightLightOn)
                {
                    RefrenceManager.Data.RightDoorVoid.SetActive(true);
                    RefrenceManager.Data.RightLightSound.Stop();
                    RightLightOn = false;
                }
                else
                {
                    RefrenceManager.Data.RightDoorVoid.SetActive(false);
                    RefrenceManager.Data.RightLightSound.Play();
                    RightLightOn = true;
                }
            }
            else
            {
                if (LeftLightOn)
                {
                    RefrenceManager.Data.LeftDoorVoid.SetActive(true);
                    RefrenceManager.Data.LeftLightSound.Stop();
                    LeftLightOn = false;
                }
                else
                {
                    RefrenceManager.Data.LeftDoorVoid.SetActive(false);
                    RefrenceManager.Data.LeftLightSound.Play();
                    LeftLightOn = true;
                }
            }
        }

        IEnumerator LightUsedDelay()
        {
            yield return new WaitForSeconds(7);

            if (RightLightOn)
            {
                UseLight(true);
                CanUseRightLight = false;
                CanUseLeftLight = false;
                RefrenceManager.Data.RightDoorFailSound.Play();
            }

            if (LeftLightOn)
            {
                UseLight(false);
                CanUseRightLight = false;
                CanUseLeftLight = false;
                RefrenceManager.Data.LeftDoorFailSound.Play();
            }
        }

        IEnumerator LightDelay()
        {
            yield return new WaitForSeconds(40);
            CanUseRightLight = true;
            CanUseLeftLight = true;
        }

        public void UseOnlineDoor(bool isRight)
        {
            if (isRight)
            {
                if (RightDoorOpen)
                {
                    object[] value = new object[] { PhotonData.Key.Close, "0.772" };
                    RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent((byte)PhotonData.Key.RightDoor, value, options, SendOptions.SendReliable);
                }
                else
                {
                    object[] value = new object[] { PhotonData.Key.Open, "2.272" };
                    RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent((byte)PhotonData.Key.RightDoor, value, options, SendOptions.SendReliable);
                }
            }
            else
            {
                if (LeftDoorOpen)
                {
                    object[] value = new object[] { PhotonData.Key.Close, "0.75" };
                    RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent((byte)PhotonData.Key.LeftDoor, value, options, SendOptions.SendReliable);
                }
                else
                {
                    object[] value = new object[] { PhotonData.Key.Open, "2.35" };
                    RaiseEventOptions options = new RaiseEventOptions() { CachingOption = EventCaching.DoNotCache, Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent((byte)PhotonData.Key.LeftDoor, value, options, SendOptions.SendReliable);
                }
            }
            TimePowerManager.Data.RefreshDrainTime();
        }

        public void OnEvent(EventData photonEvent)
        {
            object[] receivedData = (object[])photonEvent.CustomData;
            string Action = receivedData[0].ToString();
            string v = receivedData[1].ToString();
            float y = float.Parse(v);
            switch (photonEvent.Code)
            {
                case (byte)PhotonData.Key.RightDoor:
                    if (Action == PhotonData.Key.Close.ToString()) { CloseOpenDoor(true, true, y); }
                    else if (Action == PhotonData.Key.Open.ToString()) { CloseOpenDoor(true, true, y); }
                    break;
                case (byte)PhotonData.Key.LeftDoor:
                    if (Action == PhotonData.Key.Close.ToString()) { CloseOpenDoor(false, true, y); }
                    else if (Action == PhotonData.Key.Open.ToString()) { CloseOpenDoor(false, true, y); }
                    break;
            }
        }

        void CloseOpenDoor(bool isRight, bool isClose, float yLevel)
        {
            if (isRight)
            {
                if (isClose)
                {
                    RightDoorOpen = false;
                    float x = RefrenceManager.Data.RightDoorObject.transform.localPosition.x;
                    float z = RefrenceManager.Data.RightDoorObject.transform.localPosition.z;
                    RefrenceManager.Data.RightDoorObject.transform.localPosition = new Vector3(x, yLevel, z);
                    PlayDoorSound(true);
                }
                else
                {
                    RightDoorOpen = true;
                    float x = RefrenceManager.Data.RightDoorObject.transform.localPosition.x;
                    float z = RefrenceManager.Data.RightDoorObject.transform.localPosition.z;
                    RefrenceManager.Data.RightDoorObject.transform.localPosition = new Vector3(x, yLevel, z);
                    PlayDoorSound(true);
                }
            }
            else
            {
                if (isClose)
                {
                    LeftDoorOpen = false;
                    float x = RefrenceManager.Data.LeftDoorObject.transform.localPosition.x;
                    float z = RefrenceManager.Data.LeftDoorObject.transform.localPosition.z;
                    RefrenceManager.Data.LeftDoorObject.transform.localPosition = new Vector3(x, yLevel, z);
                    PlayDoorSound(false);
                }
                else
                {
                    LeftDoorOpen = true;
                    float x = RefrenceManager.Data.LeftDoorObject.transform.localPosition.x;
                    float z = RefrenceManager.Data.LeftDoorObject.transform.localPosition.z;
                    RefrenceManager.Data.LeftDoorObject.transform.localPosition = new Vector3(x, yLevel, z);
                    PlayDoorSound(false);
                }
            }
            TimePowerManager.Data.RefreshDrainTime();
        }

        IEnumerator ButtonDelay(bool isRight)
        {
            if (isRight) { CanUseRightButton = false; } else { CanUseLeftButton = false; }
            yield return new WaitForSeconds(ButtonTimerDelay);
            if (isRight) { CanUseRightButton = true; } else { CanUseLeftButton = true; }
        }

        IEnumerator LightDelay(bool isRight)
        {
            if (isRight) { CanUseRightButton = false; } else { CanUseLeftButton = false; }
            yield return new WaitForSeconds(ButtonTimerDelay);
            if (isRight) { CanUseRightButton = true; } else { CanUseLeftButton = true; }
        }

        //I hope this works lol
        object PlayDoorSound(bool isRight)
        {
            if (isRight) { RefrenceManager.Data.RightDoorSound.Play(); StartCoroutine(ButtonDelay(true)); }
            else { RefrenceManager.Data.LeftDoorSound.Play(); StartCoroutine(ButtonDelay(false)); }
            return this;
        }
    }
}