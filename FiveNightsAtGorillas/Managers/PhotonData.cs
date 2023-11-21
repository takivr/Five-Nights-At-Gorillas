using ExitGames.Client.Photon;
using FiveNightsAtGorillas.Managers.DoorAndLight;
using Photon.Pun;
using Photon.Realtime;

namespace FiveNightsAtGorillas.Managers.NetworkedData
{
    public class PhotonData : MonoBehaviourPunCallbacks
    {
        public enum Key
        {
            RightDoor = 0,
            LeftDoor = 1,
            GorillaCam = 3,
            BobCam = 4,
            Mingus = 5,
            Dingus = 6,
            Close = 7,
            Open = 8
        }

        public static PhotonData instance;

        void Awake() { instance = this; }
    }
}