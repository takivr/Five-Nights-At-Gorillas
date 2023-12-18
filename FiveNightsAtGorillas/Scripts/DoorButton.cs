using FiveNightsAtGorillas.Managers;
using UnityEngine;

namespace FiveNightsAtGorillas.Other
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
            if (other.name == "LeftHandTriggerCollider")
            {
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }
            else if (other.name == "RightHandTriggerCollider")
            {
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }

            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                if (isLeft)
                {
                        if (DoorManager.Data.CanUseLeftButton || !DoorManager.Data.LeftJammed)
                        {
                            DoorManager.Data.UseLocalDoor(false);
                        }
                        else
                        {
                            RefrenceManager.Data.LeftDoorFailSound.Play();
                        }
                }
                else
                {
                        if (DoorManager.Data.CanUseRightButton || !DoorManager.Data.RightJammed)
                        {
                            DoorManager.Data.UseLocalDoor(true);
                        }
                        else
                        {
                            RefrenceManager.Data.RightDoorFailSound.Play();
                        }
                }
            }
        }
    }
}