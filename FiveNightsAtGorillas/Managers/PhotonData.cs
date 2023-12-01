using UnityEngine;

namespace FiveNightsAtGorillas.Managers.NetworkedData
{
    public class PhotonData : MonoBehaviour
    {
        public const byte RightDoor = 90;
        public const byte LeftDoor = 91;
        public const byte Gorilla = 92;
        public const byte Bob = 93;
        public const byte Mingus = 94;
        public const byte Dingus = 95;
        public const byte Close = 96;
        public const byte Open = 97;
        public const byte StartNight = 98;

        public static PhotonData Data;

        void Awake() { Data = this; }
    }
}