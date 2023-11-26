using FiveNightsAtGorillas.Managers.DoorAndLight;
using FiveNightsAtGorillas.Scripts;
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
                if (isLeft)
                {
                    if (PlayersInRound.Data.PlayersPlaying > 1)
                    {
                        if (DoorManager.Data.CanUseLeftButton)
                        {
                            DoorManager.Data.UseOnlineDoor(false);
                        }
                    }
                    else if(PlayersInRound.Data.PlayersPlaying <= 1)
                    {
                        if (DoorManager.Data.CanUseLeftButton)
                        {
                            DoorManager.Data.UseLocalDoor(false);
                        }
                    }
                }
                else
                {
                    if (PlayersInRound.Data.PlayersPlaying > 1)
                    {
                        if (DoorManager.Data.CanUseRightButton)
                        {
                            DoorManager.Data.UseOnlineDoor(true);
                        }
                    }
                    else if(PlayersInRound.Data.PlayersPlaying <= 1)
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