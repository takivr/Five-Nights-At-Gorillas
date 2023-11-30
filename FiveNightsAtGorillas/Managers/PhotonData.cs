using UnityEngine;

namespace FiveNightsAtGorillas.Managers.NetworkedData
{
    public class PhotonData : MonoBehaviour
    {
        public const byte RightDoor = 1;
        public const byte LeftDoor = 2;
        public const byte Gorilla = 3;
        public const byte Bob = 4;
        public const byte Mingus = 5;
        public const byte Dingus = 6;
        public const byte Close = 7;
        public const byte Open = 8;
        public const byte StartNight = 9;

        public static PhotonData Data;

        void Awake() { Data = this; }
    }
}