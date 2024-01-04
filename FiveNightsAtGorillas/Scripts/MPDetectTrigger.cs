using System.Collections.Generic;
using UnityEngine;

namespace FiveNightsAtGorillas.Other
{
    public class MPDetectTrigger : MonoBehaviour
    {
        private List<GameObject> triggeredObjects = new List<GameObject>();

        public void OnTriggerEnter(Collider other) {
            if (!other.gameObject.activeSelf) return;

            if (other.name == "head_end") {
                if (!triggeredObjects.Contains(other.gameObject))
                    triggeredObjects.Add(other.gameObject);

                if (!other.gameObject.GetComponent<HeadColliderDisableEvent>().alreadyInTrigger) {
                    other.gameObject.GetComponent<HeadColliderDisableEvent>().alreadyInTrigger = true;
                }
            }

            FNAG.Data.AmountOfPlayersPlaying = triggeredObjects.Count;
            FNAG.Data.triggeredObjects = triggeredObjects;
            FNAG.Data.Refresh();
        }

        public void OnTriggerExit(Collider other) {
            if (triggeredObjects.Contains(other.gameObject)) {
                triggeredObjects.Remove(other.gameObject);
            }

            if (other.name == "head_end") {
                if (other.gameObject.GetComponent<HeadColliderDisableEvent>().alreadyInTrigger) {
                    other.gameObject.GetComponent<HeadColliderDisableEvent>().alreadyInTrigger = false;
                }
            }

            FNAG.Data.AmountOfPlayersPlaying = triggeredObjects.Count;
            FNAG.Data.triggeredObjects = triggeredObjects;
            FNAG.Data.Refresh();
        }
    }
}