using UnityEngine;
using System.Collections;
using Photon.Pun;
using FiveNightsAtGorillas.Managers.Refrences;
using Photon.Realtime;
using ExitGames.Client.Photon;
using FiveNightsAtGorillas.Managers.NetworkedData;

namespace FiveNightsAtGorillas.Managers.DoorAndLight
{
    public class DoorManager : MonoBehaviourPun
    {
        public static DoorManager Data;

        void Awake() { Data = this; }
    }
}