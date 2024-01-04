using UnityEngine;
using FiveNightsAtGorillas.Managers;

namespace FiveNightsAtGorillas.Other {
    public class Boop_ : MonoBehaviour {
        void Awake() { gameObject.layer = 18; }

        void OnTriggerEnter(Collider other) {
            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider") {
                if (other.name == "LeftHandTriggerCollider") {
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
                }
                else if (other.name == "RightHandTriggerCollider") {
                    GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
                }

                RefrenceManager.Data.GorillaBoopSound.Play();
            }
        }
    }
}