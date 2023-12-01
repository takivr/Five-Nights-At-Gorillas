using FiveNightsAtGorillas.Managers.Refrences;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.Ignore
{
    public class IgnoreWarning : MonoBehaviour
    {
        void Awake() { gameObject.layer = 18; }

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
                RefrenceManager.Data.MenuWarning.SetActive(false);
                RefrenceManager.Data.MenuIgnoreButton.SetActive(false);
                RefrenceManager.Data.MenuSelects.SetActive(true);
                RefrenceManager.Data.MenuScrollLeft.SetActive(false);
                RefrenceManager.Data.MenuScrollRight.SetActive(true);
            }
        }
    }
}