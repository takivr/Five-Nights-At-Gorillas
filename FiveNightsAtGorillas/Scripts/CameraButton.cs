using FiveNightsAtGorillas.Managers.Cameras;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.Camera
{
    public class CameraButton : MonoBehaviour
    {
        public string CameraButtonTrigger;

        void Awake() { gameObject.layer = 18; }

        void OnTriggerEnter(Collider other)
        {
            if(other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                CameraManager.Data.ChangeCamera(CameraButtonTrigger);
            }
        }
    }
}