using FiveNightsAtGorillas.Managers.DoorAndLight;
using Photon.Pun;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.Door
{
    public class DoorButton : MonoBehaviour
    {
        public bool isLeft { get; set; }

        void Awake()
        {
            gameObject.layer = 18;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                int amountOfPlayersInRoom = PhotonNetwork.PlayerList.Length;
                if (isLeft && amountOfPlayersInRoom <= 1 || !PhotonNetwork.InRoom) { DoorManager.Data.UseLocalDoor(false); }
                else if (isLeft && amountOfPlayersInRoom > 1 && PhotonNetwork.InRoom) { DoorManager.Data.UseOnlineDoor(false); }
                else if (!isLeft && amountOfPlayersInRoom <= 1 || !PhotonNetwork.InRoom) { DoorManager.Data.UseLocalDoor(true); }
                else if (!isLeft && amountOfPlayersInRoom > 1 && PhotonNetwork.InRoom) { DoorManager.Data.UseOnlineDoor(true); }
            }
        }
    }
}