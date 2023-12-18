using FiveNightsAtGorillas.Managers;
using UnityEngine;

namespace FiveNightsAtGorillas.Other
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
                RefrenceManager.Data.NightOneSelect.SetActive(true);
                RefrenceManager.Data.NightTwoSelect.SetActive(false);
                RefrenceManager.Data.NightThreeSelect.SetActive(false);
                RefrenceManager.Data.NightFourSelect.SetActive(false);
                RefrenceManager.Data.NightFiveSelect.SetActive(false);
                RefrenceManager.Data.NightSixSelect.SetActive(false);
                RefrenceManager.Data.CustomNightSelect.SetActive(false);
                RefrenceManager.Data.MenuRoundRunning.SetActive(false);
            }
        }
    }
}