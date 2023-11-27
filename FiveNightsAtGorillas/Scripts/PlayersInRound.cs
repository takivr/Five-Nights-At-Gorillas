using System.EnterpriseServices.Internal;
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