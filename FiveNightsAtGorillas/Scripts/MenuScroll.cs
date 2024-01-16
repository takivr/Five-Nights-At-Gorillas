using UnityEngine;

namespace FiveNightsAtGorillas.Other {
    public class MenuScroll : MonoBehaviour {
        public bool isRight;

        void Awake() { gameObject.layer = 18; }

        void OnTriggerEnter(Collider other) {
            if (other.name == "LeftHandTriggerCollider") {
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }
            else if (other.name == "RightHandTriggerCollider") {
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }

            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider") {
                if (isRight) {
                    FNAG.Data.ChangeCurrentPage(false);
                }
                else {
                    FNAG.Data.ChangeCurrentPage(true);
                }
            }
        }
    }
}