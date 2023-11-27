using UnityEngine;
using System.Collections;
using FiveNightsAtGorillas.Managers.DoorAndLight;

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
        }

        public void StartEverything()
        {
            AllowedToRun = true;
            CurrentTime = "12AM";
            CurrentPower = 100;
            StartCoroutine(PowerDelay());
            StartCoroutine(TimeDelay());
        }

        public void RefreshDrainTime()
        {
            if (DoorManager.Data.RightDoorOpen) { CurrentPowerDrainTime -= 5; } else { CurrentPowerDrainTime += 5; }
            if (DoorManager.Data.LeftDoorOpen) { CurrentPowerDrainTime -= 5; } else { CurrentPowerDrainTime += 5; }
        }

        IEnumerator PowerDelay()
        {
            yield return new WaitForSeconds(CurrentPowerDrainTime);
            if (AllowedToRun)
            {
                CurrentPower = -1;
                if(CurrentPower < 0)
                {
                    FNAG.Data.Poweroutage();
                }
                StartCoroutine(PowerDelay());
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
            }
        }
    }
}