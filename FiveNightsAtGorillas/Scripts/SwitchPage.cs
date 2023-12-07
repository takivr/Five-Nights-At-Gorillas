using FiveNightsAtGorillas.Managers.Refrences;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.SwitchPage
{
    public class SwitchPage : MonoBehaviour
    {
        public bool isSub;

        void Awake() { gameObject.layer = 18; }

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                if (other.name == "LeftHandTriggerCollider")
                {
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
                }
                else if (other.name == "RightHandTriggerCollider")
                {
                    GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
                }

                if (isSub)
                {
                    if(RefrenceManager.Data.RecentNews.pageToDisplay != 0)
                    {
                        RefrenceManager.Data.RecentNews.pageToDisplay--;
                    }
                }
                else
                {
                    RefrenceManager.Data.RecentNews.pageToDisplay++;
                }
            }
        }
    }
}