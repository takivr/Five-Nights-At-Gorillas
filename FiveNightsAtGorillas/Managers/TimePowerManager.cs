using UnityEngine;
using System.Collections;
using FiveNightsAtGorillas.Managers.DoorAndLight;
using FiveNightsAtGorillas.Managers.Refrences;

namespace FiveNightsAtGorillas.Managers.TimePower
{
    public class TimePowerManager : MonoBehaviour
    {
        public static TimePowerManager Data;
        public int CurrentPower { get; private set; } = 100;
        public int CurrentPowerDrainTime { get; private set; } = 15;
        public string CurrentTime { get; private set; } = "12AM";
        public bool AllowedToRun { get; private set; } = false;

        void Awake() { Data = this; }
        
        public void StopEverything()
        {
            AllowedToRun = false;
            CurrentTime = "12AM";
            CurrentPower = 100;
            RefreshText();
        }

        public void StartEverything()
        {
            AllowedToRun = true;
            CurrentTime = "12AM";
            CurrentPower = 100;
            RefreshText();
            StartCoroutine(PowerDelay());
            StartCoroutine(TimeDelay());
        }

        public void RefreshDrainTime()
        {
            if(DoorManager.Data.RightDoorOpen && !DoorManager.Data.LeftDoorOpen)
            {
                CurrentPowerDrainTime = 9;
                return;
            }
            else if(!DoorManager.Data.RightDoorOpen && DoorManager.Data.LeftDoorOpen)
            {
                CurrentPowerDrainTime = 9;
                return;
            }
            else if(DoorManager.Data.RightDoorOpen && DoorManager.Data.LeftDoorOpen)
            {
                CurrentPowerDrainTime = 15;
                return;
            }
            else if(!DoorManager.Data.RightDoorOpen && !DoorManager.Data.LeftDoorOpen)
            {
                CurrentPowerDrainTime = 3;
            }
        }

        void RefreshText()
        {
            RefrenceManager.Data.CurrentPower.text = CurrentPower.ToString();
            RefrenceManager.Data.CurrentTime.text = CurrentTime;
        }

        IEnumerator PowerDelay()
        {
            yield return new WaitForSeconds(CurrentPowerDrainTime);
            if (AllowedToRun)
            {
                CurrentPower--;
                if(CurrentPower < 0)
                {
                    FNAG.Data.Poweroutage();
                }
                StartCoroutine(PowerDelay());
                RefreshText();
            }
        }

        IEnumerator TimeDelay()
        {
            yield return new WaitForSeconds(90);
            if (AllowedToRun)
            {
                if (CurrentTime == "12AM") { CurrentTime = "1AM"; }
                else if (CurrentTime == "1AM") { CurrentTime = "2AM"; }
                else if (CurrentTime == "2AM") { CurrentTime = "3AM"; }
                else if (CurrentTime == "3AM") { CurrentTime = "4AM"; }
                else if (CurrentTime == "4AM") { CurrentTime = "5AM"; }
                else if (CurrentTime == "5AM") { CurrentTime = "6AM"; FNAG.Data.SixAM(); }
                StartCoroutine(TimeDelay());
                RefreshText();
            }
        }
    }
}