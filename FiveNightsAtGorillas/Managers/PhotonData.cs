using Photon.Pun;

namespace FiveNightsAtGorillas.Managers.NetworkedData
{
    public class PhotonData : MonoBehaviourPunCallbacks
    {
        public enum Key
        {
            RightDoor = 1,
            LeftDoor = 2,
            Gorilla = 3,
            Bob = 4,
            Mingus = 5,
            Dingus = 6,
            Close = 7,
            Open = 8
        }

        public static PhotonData instance;

        void Awake() { instance = this; }
    }
}