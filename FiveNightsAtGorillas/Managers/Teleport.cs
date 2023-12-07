using GorillaLocomotion;
using UnityEngine;
using System.Collections;
using FiveNightsAtGorillas.Managers.Refrences;

namespace FiveNightsAtGorillas.Managers.TeleportScript
{
    public class Teleport : MonoBehaviour
    {
        public static Teleport Data;

        void Awake() { Data = this; }

        public IEnumerator TeleportPlayer(Vector3 destinationPosition, Vector3 destinationRot, bool killVelocity = true)
        {
            GorillaTagger.Instance.bodyCollider.enabled = false;
            GorillaTagger.Instance.headCollider.enabled = false;
            Player.Instance.locomotionEnabledLayers.value = 0;
            GorillaTagger.Instance.mainCamera.transform.position = destinationPosition;
            GorillaTagger.Instance.mainCamera.transform.rotation = Quaternion.Euler(destinationRot);
            if (killVelocity) { GorillaTagger.Instance.rigidbody.velocity = new Vector3(0, 0, 0); }
            yield return new WaitForSeconds(0.1f);
            GorillaTagger.Instance.bodyCollider.enabled = true;
            GorillaTagger.Instance.headCollider.enabled = true;
            Player.Instance.locomotionEnabledLayers.value = RefrenceManager.Data.DefaultMask;
        }
    }
}