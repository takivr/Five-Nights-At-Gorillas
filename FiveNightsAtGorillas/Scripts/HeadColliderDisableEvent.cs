using FiveNightsAtGorillas.Managers;
using UnityEngine;

namespace FiveNightsAtGorillas.Other {
    public class HeadColliderDisableEvent : MonoBehaviour {
        public bool alreadyInTrigger;
        bool IsFirst;

        void OnDisable() {
            Collider This = gameObject.GetComponent<Collider>();
            RefrenceManager.Data.NearGameTrigger.GetComponent<MPDetectTrigger>().OnTriggerExit(This);
        }

        void OnEnable() {
            if (IsFirst) {
                IsFirst = false;
                return;
            }
            else {
                Collider This = gameObject.GetComponent<Collider>();
                if (alreadyInTrigger)
                    RefrenceManager.Data.NearGameTrigger.GetComponent<MPDetectTrigger>().OnTriggerEnter(This);
                else
                    RefrenceManager.Data.NearGameTrigger.GetComponent<MPDetectTrigger>().OnTriggerExit(This);
            }
        }
    }
}