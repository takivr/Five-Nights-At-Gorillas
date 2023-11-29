using FiveNightsAtGorillas.Managers.DoorAndLight;
using FiveNightsAtGorillas.Managers.Refrences;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.Light
{
    public class LightButton : MonoBehaviour
    {
        public bool isLeft { get; set; }

        void Awake()
        {
            gameObject.layer = 18;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                if (isLeft) { if (DoorManager.Data.CanUseLeftLight) { DoorManager.Data.UseLight(false); } else { RefrenceManager.Data.LeftDoorFailSound.Play(); } }
                else if (!isLeft) { if (DoorManager.Data.CanUseRightLight) { DoorManager.Data.UseLight(true); } else { RefrenceManager.Data.RightDoorFailSound.Play(); } }
            }
        }
    }
}