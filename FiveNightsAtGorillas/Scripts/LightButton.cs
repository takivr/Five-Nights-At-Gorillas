using FiveNightsAtGorillas.Managers.DoorAndLight;
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
                if (isLeft) { DoorManager.Data.UseLight(false); }
                else if (!isLeft) { DoorManager.Data.UseLight(true); }
            }
        }
    }
}