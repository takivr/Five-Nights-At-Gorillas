using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace FiveNightsAtGorillas.Managers {
    public class DoorManager : MonoBehaviour {
        public static DoorManager Data;
        public float ButtonTimerDelay { get; private set; } = 2;
        public float LightTimerDelay { get; private set; } = 2;
        public bool CanUseLeftButton { get; private set; } = true;
        public bool CanUseRightButton { get; private set; } = true;
        public bool RightDoorOpen { get; private set; } = true;
        public bool LeftDoorOpen { get; private set; } = true;
        public bool RightLightOn { get; private set; }
        public bool LeftLightOn { get; private set; }
        public bool CanUseLeftLight { get; private set; } = true;
        public bool CanUseRightLight { get; private set; } = true;
        public bool PlayedRightAnimatronicAtDoor { get; private set; } = false;
        public bool PlayedLeftAnimatronicAtDoor { get; private set; } = false;

        void Awake() { Data = this; PhotonNetwork.AddCallbackTarget(this); }

        //R door opened: 2.272
        //R door closed: 0.772

        //L door opened: 2.35
        //L door closed: 0.75

        public void ResetDoors() {
            CanUseLeftButton = true;
            CanUseRightButton = true;
            CanUseLeftLight = true;
            CanUseRightLight = true;
            RefrenceManager.Data.RightDoor.GetComponent<Renderer>().material.color = Color.red;
            RefrenceManager.Data.LeftDoor.GetComponent<Renderer>().material.color = Color.red;
            if (!RightDoorOpen) {
                CloseOpenDoor(true, false, 2.272f);
            }
            if (!LeftDoorOpen) {
                CloseOpenDoor(false, false, 2.35f);
            }
            if (RightLightOn) {
                UseLight(true);
            }
            if (LeftLightOn) {
                UseLight(false);
            }
        }

        public void PowerOutage() {
            CanUseLeftButton = false;
            CanUseRightButton = false;
            CanUseLeftLight = false;
            CanUseRightLight = false;
        }

        public void UseLocalDoor(bool isRight) {
            if (isRight && CanUseRightButton) {
                if (RightDoorOpen) {
                    RefrenceManager.Data.RightDoor.GetComponent<Renderer>().material.color = Color.green;
                    CloseOpenDoor(true, true, 0.772f);
                    if (RightLightOn) {
                        UseLight(true);
                    }
                }
                else {
                    RefrenceManager.Data.RightDoor.GetComponent<Renderer>().material.color = Color.red;
                    CloseOpenDoor(true, false, 2.272f);
                    if (RightLightOn && RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam3") {
                        RefrenceManager.Data.AnimatronicAtDoor.Play(); StartCoroutine(AnimatronicAtDoorSoundDelay(true));
                    }
                }
            }
            else if(!isRight && CanUseLeftButton) {
                if (LeftDoorOpen) {
                    RefrenceManager.Data.LeftDoor.GetComponent<Renderer>().material.color = Color.green;
                    CloseOpenDoor(false, true, 0.75f);
                    if (LeftLightOn) {
                        UseLight(false);
                    }
                }
                else {
                    RefrenceManager.Data.LeftDoor.GetComponent<Renderer>().material.color = Color.red;
                    CloseOpenDoor(false, false, 2.35f);
                    if (LeftLightOn && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam2") {
                        RefrenceManager.Data.AnimatronicAtDoor.Play(); StartCoroutine(AnimatronicAtDoorSoundDelay(false));
                    }
                }
            }
        }

        public void UseMPDoor(bool isRight, bool Close) {
            if (isRight && CanUseRightButton) {
                if (Close) {
                    RefrenceManager.Data.RightDoor.GetComponent<Renderer>().material.color = Color.green;
                    CloseOpenDoor(true, true, 0.772f);
                    if (RightLightOn) {
                        UseLight(true);
                    }
                }
                else {
                    RefrenceManager.Data.RightDoor.GetComponent<Renderer>().material.color = Color.red;
                    CloseOpenDoor(true, false, 2.272f);
                    if (RightLightOn && RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam3") {
                        RefrenceManager.Data.AnimatronicAtDoor.Play(); StartCoroutine(AnimatronicAtDoorSoundDelay(true));
                    }
                }
            }
            else if(!isRight && CanUseLeftButton) {
                if (Close) {
                    RefrenceManager.Data.LeftDoor.GetComponent<Renderer>().material.color = Color.green;
                    CloseOpenDoor(false, true, 0.75f);
                    if (LeftLightOn) {
                        UseLight(false);
                    }
                }
                else {
                    RefrenceManager.Data.LeftDoor.GetComponent<Renderer>().material.color = Color.red;
                    CloseOpenDoor(false, false, 2.35f);
                    if (LeftLightOn && RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam2") {
                        RefrenceManager.Data.AnimatronicAtDoor.Play(); StartCoroutine(AnimatronicAtDoorSoundDelay(false));
                    }
                }
            }
        }

        public void UseLight(bool isRight) {
            if (isRight && CanUseRightLight) {
                if (RightLightOn) {
                    RefrenceManager.Data.RightDoorVoid.SetActive(true);
                    RefrenceManager.Data.RightLightSound.Stop();
                    RightLightOn = false;
                    StopCoroutine(LightUsedDelay());
                    return;
                }
                else {
                    RefrenceManager.Data.RightDoorVoid.SetActive(false);
                    RefrenceManager.Data.RightLightSound.Play();
                    RightLightOn = true;
                    StartCoroutine(LightUsedDelay());
                    if (!PlayedRightAnimatronicAtDoor) {
                        if (RefrenceManager.Data.bobParent.GetComponent<AIManager>().CamPos == "Cam3" && RightDoorOpen) { RefrenceManager.Data.AnimatronicAtDoor.Play(); StartCoroutine(AnimatronicAtDoorSoundDelay(true)); }
                    }
                    return;
                }
            }
            else if(!isRight && CanUseLeftLight) {
                if (LeftLightOn) {
                    RefrenceManager.Data.LeftDoorVoid.SetActive(true);
                    RefrenceManager.Data.LeftLightSound.Stop();
                    LeftLightOn = false;
                    StopCoroutine(LightUsedDelay());
                    return;
                }
                else {
                    RefrenceManager.Data.LeftDoorVoid.SetActive(false);
                    RefrenceManager.Data.LeftLightSound.Play();
                    LeftLightOn = true;
                    StartCoroutine(LightUsedDelay());
                    if (!PlayedLeftAnimatronicAtDoor) {
                        if (RefrenceManager.Data.mingusParent.GetComponent<AIManager>().CamPos == "Cam2" && LeftDoorOpen) { RefrenceManager.Data.AnimatronicAtDoor.Play(); StartCoroutine(AnimatronicAtDoorSoundDelay(false)); }
                    }
                    return;
                }
            }
        }

        IEnumerator LightUsedDelay() {
            yield return new WaitForSeconds(7);

            if (RightLightOn) {
                UseLight(true);
                CanUseRightLight = false;
                CanUseLeftLight = false;
                RefrenceManager.Data.RightDoorFailSound.Play();
                StartCoroutine(LightDelay());
            }

            if (LeftLightOn) {
                UseLight(false);
                CanUseRightLight = false;
                CanUseLeftLight = false;
                RefrenceManager.Data.LeftDoorFailSound.Play();
                StartCoroutine(LightDelay());
            }
        }

        IEnumerator AnimatronicAtDoorSoundDelay(bool right) {
            if (right) {
                PlayedRightAnimatronicAtDoor = true;
                yield return new WaitForSeconds(15);
                PlayedRightAnimatronicAtDoor = false;
            }
            else {
                PlayedLeftAnimatronicAtDoor = true;
                yield return new WaitForSeconds(15);
                PlayedLeftAnimatronicAtDoor = false;
            }
        }

        IEnumerator LightDelay() {
            yield return new WaitForSeconds(40);
            CanUseRightLight = true;
            CanUseLeftLight = true;
        }

        public void CloseOpenDoor(bool isRight, bool isClose, float yLevel) {
            if (isRight) {
                if (isClose) {
                    RightDoorOpen = false;
                    float x = RefrenceManager.Data.RightDoorObject.transform.localPosition.x;
                    float z = RefrenceManager.Data.RightDoorObject.transform.localPosition.z;
                    RefrenceManager.Data.RightDoorObject.transform.localPosition = new Vector3(x, yLevel, z);
                    PlayDoorSound(true);
                }
                else {
                    RightDoorOpen = true;
                    float x = RefrenceManager.Data.RightDoorObject.transform.localPosition.x;
                    float z = RefrenceManager.Data.RightDoorObject.transform.localPosition.z;
                    RefrenceManager.Data.RightDoorObject.transform.localPosition = new Vector3(x, yLevel, z);
                    PlayDoorSound(true);
                }
            }
            else {
                if (isClose) {
                    LeftDoorOpen = false;
                    float x = RefrenceManager.Data.LeftDoorObject.transform.localPosition.x;
                    float z = RefrenceManager.Data.LeftDoorObject.transform.localPosition.z;
                    RefrenceManager.Data.LeftDoorObject.transform.localPosition = new Vector3(x, yLevel, z);
                    PlayDoorSound(false);
                }
                else {
                    LeftDoorOpen = true;
                    float x = RefrenceManager.Data.LeftDoorObject.transform.localPosition.x;
                    float z = RefrenceManager.Data.LeftDoorObject.transform.localPosition.z;
                    RefrenceManager.Data.LeftDoorObject.transform.localPosition = new Vector3(x, yLevel, z);
                    PlayDoorSound(false);
                }
            }
            TimePowerManager.Data.RefreshDrainTime();
        }

        IEnumerator ButtonDelay(bool isRight) {
            if (isRight) { CanUseRightButton = false; } else { CanUseLeftButton = false; }
            yield return new WaitForSeconds(ButtonTimerDelay);
            if (isRight) { CanUseRightButton = true; } else { CanUseLeftButton = true; }
        }

        IEnumerator LightDelay(bool isRight) {
            if (isRight) { CanUseRightButton = false; } else { CanUseLeftButton = false; }
            yield return new WaitForSeconds(ButtonTimerDelay);
            if (isRight) { CanUseRightButton = true; } else { CanUseLeftButton = true; }
        }

        void PlayDoorSound(bool isRight) {
            if (isRight) { RefrenceManager.Data.RightDoorSound.Play(); if (FNAG.Data.AmountOfPlayersPlaying < 2) { StartCoroutine(ButtonDelay(true)); } }
            else { RefrenceManager.Data.LeftDoorSound.Play(); if (FNAG.Data.AmountOfPlayersPlaying < 2) { StartCoroutine(ButtonDelay(false)); } }
        }
    }
}