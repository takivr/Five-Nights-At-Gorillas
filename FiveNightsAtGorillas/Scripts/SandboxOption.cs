using FiveNightsAtGorillas.Managers;
using Photon.Pun;
using UnityEngine;

namespace FiveNightsAtGorillas.Other {
    public class SandboxOption : MonoBehaviour {
        public bool IsBrightOffice;
        public bool IsInfinitePower;
        public bool IsAutoCloseDoor;
        public bool IsAutoSwitchCamera;
        public bool IsShorterNight;
        public bool IsSlowPower;
        public bool IsFastPower;
        public bool IsNoCamera;
        public bool IsPitchBlack;
        public bool IsNoLights;
        public bool IsLimitedPower;

        void Awake() { gameObject.layer = 18; }

        void OnTriggerEnter(Collider other) {
            if (other.name == "LeftHandTriggerCollider") {
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }
            else if (other.name == "RightHandTriggerCollider") {
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tapHapticDuration);
            }

            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider") {
                    if (IsBrightOffice) { SandboxValues.Data.SwitchValue("BrightOffice"); }
                    else if (IsInfinitePower) { SandboxValues.Data.SwitchValue("InfinitePower"); }
                    else if (IsAutoCloseDoor) { SandboxValues.Data.SwitchValue("AutoCloseDoor"); }
                    else if (IsAutoSwitchCamera) { SandboxValues.Data.SwitchValue("AutoSwitchCamera"); }
                    else if (IsShorterNight) { SandboxValues.Data.SwitchValue("ShorterNight"); }
                    else if (IsSlowPower) { SandboxValues.Data.SwitchValue("SlowPower"); }
                    else if (IsFastPower) { SandboxValues.Data.SwitchValue("FastPower"); }
                    else if (IsNoCamera) { SandboxValues.Data.SwitchValue("NoCamera"); }
                    else if (IsPitchBlack) { SandboxValues.Data.SwitchValue("PitchBlack"); }
                    else if (IsNoLights) { SandboxValues.Data.SwitchValue("NoLights"); }
                    else if (IsLimitedPower) { SandboxValues.Data.SwitchValue("LimitedPower"); }
            }
        }
    }
}