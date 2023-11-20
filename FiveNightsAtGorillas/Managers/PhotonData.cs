using Photon.Pun;
using Photon.Realtime;

namespace FiveNightsAtGorillas.Managers.NetworkedData
{
    public class PhotonData : MonoBehaviourPunCallbacks
    {
        public enum RaiseEventValues
        {
            ChangeDoorState = 0,
            ChangeLightState = 1,
            SwitchCamera = 2,
            ChangeGorillaState = 3,
            ChangeBobState = 4,
            ChangeMingusState = 5,
            ChangeDingusState = 6
        }

        public static PhotonData instance;
        public Player CurrentMasterClient;
        public ExitGames.Client.Photon.Hashtable RoomProperties = new ExitGames.Client.Photon.Hashtable();

        void Awake() { instance = this; }
        public override void OnMasterClientSwitched(Player newMasterClient) { CurrentMasterClient = newMasterClient; }
    }
}