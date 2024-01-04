using Photon.Pun;
using Photon.Realtime;

namespace FiveNightsAtGorillas.Managers {
    public class JoinedRoomChecker : MonoBehaviourPunCallbacks {
        public override void OnPlayerEnteredRoom(Player newPlayer) {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1) {
                if (int.Parse(RefrenceManager.Data.GD.text) > 20) {
                    RefrenceManager.Data.GD.text = "20";
                }
                if (int.Parse(RefrenceManager.Data.BD.text) > 20) {
                    RefrenceManager.Data.BD.text = "20";
                }
                if (int.Parse(RefrenceManager.Data.DD.text) > 20) {
                    RefrenceManager.Data.DD.text = "20";
                }
                if (int.Parse(RefrenceManager.Data.MD.text) > 20) {
                    RefrenceManager.Data.MD.text = "20";
                }
            }
        }
    }
}