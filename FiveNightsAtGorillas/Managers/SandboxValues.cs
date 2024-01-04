using UnityEngine;

namespace FiveNightsAtGorillas.Managers
{
    public class SandboxValues : MonoBehaviour
    {
        public static SandboxValues Data;
        void Awake() { Data = this; }

        public bool BrightOffice { get; private set; }
        public bool InfinitePower { get; private set; }
        public bool AutoCloseDoor { get; private set; }
        public bool AutoSwitchCamera { get; private set; }
        public bool ShorterNight { get; private set; }
        public bool SlowPower { get; private set; }

        public bool FastPower { get; private set; }
        public bool NoCamera { get; private set; }
        public bool PitchBlack { get; private set; }
        public bool NoLights { get; private set; }
        public bool LimitedPower { get; private set; }

        public void SwitchValue(string value) {
            if(value == "BrightOffice") {
                if (BrightOffice) { BrightOffice = false; RefrenceManager.Data.BrightOfficeOn.text = "False"; RefrenceManager.Data.BrightOfficeOn.color = Color.red; } else { BrightOffice = true; RefrenceManager.Data.BrightOfficeOn.text = "True"; RefrenceManager.Data.BrightOfficeOn.color = Color.green; }
                return;
            }
            else if (value == "InfinitePower") {
                if (InfinitePower) { InfinitePower = false; RefrenceManager.Data.InfinitePowerOn.text = "False"; RefrenceManager.Data.InfinitePowerOn.color = Color.red; } else { InfinitePower = true; RefrenceManager.Data.InfinitePowerOn.text = "True"; RefrenceManager.Data.InfinitePowerOn.color = Color.green; }
                return;
            }
            else if (value == "AutoCloseDoor") {
                if (AutoCloseDoor) { AutoCloseDoor = false; RefrenceManager.Data.AutoCloseDoorOn.text = "False"; RefrenceManager.Data.AutoCloseDoorOn.color = Color.red; } else { AutoCloseDoor = true; RefrenceManager.Data.AutoCloseDoorOn.text = "True"; RefrenceManager.Data.AutoCloseDoorOn.color = Color.green; }
                return;
            }
            else if (value == "AutoSwitchCamera") {
                if (AutoSwitchCamera) { AutoSwitchCamera = false; RefrenceManager.Data.AutoSwitchCameraOn.text = "False"; RefrenceManager.Data.AutoSwitchCameraOn.color = Color.red; } else { AutoSwitchCamera = true; RefrenceManager.Data.AutoSwitchCameraOn.text = "True"; RefrenceManager.Data.AutoSwitchCameraOn.color = Color.green; }
                return;
            }
            else if (value == "ShorterNight") {
                if (ShorterNight) { ShorterNight = false; RefrenceManager.Data.ShorterNightOn.text = "False"; RefrenceManager.Data.ShorterNightOn.color = Color.red; } else { ShorterNight = true; RefrenceManager.Data.ShorterNightOn.text = "True"; RefrenceManager.Data.ShorterNightOn.color = Color.green; }
                return;
            }
            else if (value == "SlowPower") {
                if (SlowPower) { SlowPower = false; RefrenceManager.Data.SlowPowerOn.text = "False"; RefrenceManager.Data.SlowPowerOn.color = Color.red; } else { SlowPower = true; RefrenceManager.Data.SlowPowerOn.text = "True"; RefrenceManager.Data.SlowPowerOn.color = Color.green; }
                return;
            }
            else if (value == "FastPower") {
                if (FastPower) { FastPower = false; RefrenceManager.Data.FastPowerOn.text = "False"; RefrenceManager.Data.FastPowerOn.color = Color.red; } else { FastPower = true; RefrenceManager.Data.FastPowerOn.text = "True"; RefrenceManager.Data.FastPowerOn.color = Color.green; }
                return;
            }
            else if (value == "NoCamera") {
                if (NoCamera) { NoCamera = false; RefrenceManager.Data.NoCameraOn.text = "False"; RefrenceManager.Data.NoCameraOn.color = Color.red; } else { NoCamera = true; RefrenceManager.Data.NoCameraOn.text = "True"; RefrenceManager.Data.NoCameraOn.color = Color.green; }
                return;
            }
            else if (value == "PitchBlack") {
                if (PitchBlack) { PitchBlack = false; RefrenceManager.Data.PitchBlackOn.text = "False"; RefrenceManager.Data.PitchBlackOn.color = Color.red; } else { PitchBlack = true; RefrenceManager.Data.PitchBlackOn.text = "True"; RefrenceManager.Data.PitchBlackOn.color = Color.green; }
                return;
            }
            else if (value == "NoLights") {
                if (NoLights) { NoLights = false; RefrenceManager.Data.NoLightsOn.text = "False"; RefrenceManager.Data.NoLightsOn.color = Color.red; } else { NoLights = true; RefrenceManager.Data.NoLightsOn.text = "True"; RefrenceManager.Data.NoLightsOn.color = Color.green; }
                return;
            }
            else if (value == "LimitedPower") {
                if (LimitedPower) { LimitedPower = false; RefrenceManager.Data.LimitedPowerOn.text = "False"; RefrenceManager.Data.LimitedPowerOn.color = Color.red; } else { LimitedPower = true; RefrenceManager.Data.LimitedPowerOn.text = "True"; RefrenceManager.Data.LimitedPowerOn.color = Color.green; }
                return;
            }
        }

        public void ResetAllValues() {
            BrightOffice = false;
            InfinitePower = false;
            AutoCloseDoor = false;
            AutoSwitchCamera = false;
            ShorterNight = false;
            SlowPower = false;
            FastPower = false;
            NoCamera = false;
            PitchBlack = false;
            NoLights = false;
            LimitedPower = false;

            RefrenceManager.Data.LimitedPowerOn.text = "False"; RefrenceManager.Data.LimitedPowerOn.color = Color.red;
            RefrenceManager.Data.NoLightsOn.text = "False"; RefrenceManager.Data.NoLightsOn.color = Color.red;
            RefrenceManager.Data.PitchBlackOn.text = "False"; RefrenceManager.Data.PitchBlackOn.color = Color.red;
            RefrenceManager.Data.NoCameraOn.text = "False"; RefrenceManager.Data.NoCameraOn.color = Color.red;
            RefrenceManager.Data.FastPowerOn.text = "False"; RefrenceManager.Data.FastPowerOn.color = Color.red;
            RefrenceManager.Data.SlowPowerOn.text = "False"; RefrenceManager.Data.SlowPowerOn.color = Color.red;
            RefrenceManager.Data.ShorterNightOn.text = "False"; RefrenceManager.Data.ShorterNightOn.color = Color.red;
            RefrenceManager.Data.AutoSwitchCameraOn.text = "False"; RefrenceManager.Data.AutoSwitchCameraOn.color = Color.red;
            RefrenceManager.Data.AutoCloseDoorOn.text = "False"; RefrenceManager.Data.AutoCloseDoorOn.color = Color.red;
            RefrenceManager.Data.InfinitePowerOn.text = "False"; RefrenceManager.Data.InfinitePowerOn.color = Color.red;
            RefrenceManager.Data.BrightOfficeOn.text = "False"; RefrenceManager.Data.BrightOfficeOn.color = Color.red;
        }
    }
}