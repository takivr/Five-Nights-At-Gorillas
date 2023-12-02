using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;

namespace FiveNightsAtGorillas.Managers.Teleport
{
    public class Teleport : GorillaTriggerBox
    {
        public static Teleport Data;
        // Code from devs minecraft mod: https://github.com/developer9998/DevMinecraftMod/blob/main/DevMinecraftMod/Scripts/MinecraftQuitBox.cs

        void Awake() { Data = this; }

        public void TeleportPlayer(Vector3 location)
        {
            Vector3 target = location;

            Traverse.Create(Player.Instance).Field("lastPosition").SetValue(target);
            Traverse.Create(Player.Instance).Field("lastLeftHandPosition").SetValue(target);
            Traverse.Create(Player.Instance).Field("lastRightHandPosition").SetValue(target);
            Traverse.Create(Player.Instance).Field("lastHeadPosition").SetValue(target);

            Player.Instance.leftControllerTransform.position = target;
            Player.Instance.rightControllerTransform.position = target;
            Player.Instance.bodyCollider.attachedRigidbody.transform.position = target;

            Player.Instance.GetComponent<Rigidbody>().position = target;
            Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}