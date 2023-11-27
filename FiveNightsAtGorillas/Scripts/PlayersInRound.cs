using FiveNightsAtGorillas.Managers.Refrences;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.PlayerDetecter
{
    public class PlayersInRound : MonoBehaviour
    {
        public static PlayersInRound Data;
        public int PlayersPlaying { get; private set; }

        void Awake() { Data = this; }

        void OnTriggerEnter(Collider other)
        {
            if(other.name == "head_end")
            {
                PlayersPlaying++;
                if(PlayersPlaying > 0)
                {
                    RefrenceManager.Data.MenuRoundRunning.SetActive(true);
                    RefrenceManager.Data.MenuWarning.SetActive(false);
                    RefrenceManager.Data.MenuIgnoreButton.SetActive(false);
                    RefrenceManager.Data.MenuSelects.SetActive(false);
                    RefrenceManager.Data.MenuScrollLeft.SetActive(false);
                    RefrenceManager.Data.MenuScrollRight.SetActive(false);
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.name == "head_end")
            {
                PlayersPlaying--;
                if(PlayersPlaying == 0) { FNAG.Data.StopGame(); }
            }
        }
    }
}