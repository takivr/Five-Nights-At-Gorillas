using UnityEngine;

namespace FNAGUpdater.Button.Ignore
{
    public class ButtonIgnore : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.name == "RightHandTriggerCollider" || other.name == "LeftHandTriggerCollider")
            {
                Updater.Instance.Notif.SetActive(false);
            }
        }
    }
}