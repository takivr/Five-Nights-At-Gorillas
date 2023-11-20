using UnityEngine;

namespace FNAGUpdater.Button.Update
{
    public class ButtonUpdate : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.name == "RightHandTriggerCollider" || other.name == "LeftHandTriggerCollider")
            {
                StartCoroutine(Updater.Instance.GetNewVersion());
            }
        }
    }
}