using UnityEngine;

namespace FiveNightsAtGorillas.Other.NightStart
{
    public class StartNight : MonoBehaviour
    {
        public int Night;

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                FNAG.Data.StartGame(Night);
            }
        }
    }
}