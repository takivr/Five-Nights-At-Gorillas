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
                if (isLeft)
                {
                    if (amountOfPlayersInRoom > 1)
                    {
                        if (DoorManager.Data.CanUseLeftButton)
                        {
                            DoorManager.Data.UseOnlineDoor(false);
                        }
                    }
                    else
                    {
                        if (DoorManager.Data.CanUseLeftButton)
                        {
                            DoorManager.Data.UseLocalDoor(false);
                        }
                    }
                }
                else
                {
                    if (amountOfPlayersInRoom > 1)
                    {
                        if (DoorManager.Data.CanUseRightButton)
                        {
                            DoorManager.Data.UseOnlineDoor(true);
                        }
                    }
                    else
                    {
                        if (DoorManager.Data.CanUseRightButton)
                        {
                            DoorManager.Data.UseLocalDoor(true);
                        }
                    }
                }
            }
        }
    }
}