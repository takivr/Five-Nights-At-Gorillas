using FiveNightsAtGorillas.Managers.Refrences;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.Scroll
{
    public class MenuScroll : MonoBehaviour
    {
        public bool isRight;

        void Awake() { gameObject.layer = 18; }

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                if (isRight)
                {
                    if (RefrenceManager.Data.NightOneSelect.activeSelf) //Set the end part to "== true" if it's not working
                    {
                        RefrenceManager.Data.NightOneSelect.SetActive(false);
                        RefrenceManager.Data.NightTwoSelect.SetActive(true);
                        RefrenceManager.Data.MenuScrollLeft.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightTwoSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightTwoSelect.SetActive(false);
                        RefrenceManager.Data.NightThreeSelect.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightThreeSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightThreeSelect.SetActive(false);
                        RefrenceManager.Data.NightFourSelect.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightFourSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightFourSelect.SetActive(false);
                        RefrenceManager.Data.NightFiveSelect.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightFiveSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightFiveSelect.SetActive(false);
                        RefrenceManager.Data.NightSixSelect.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightSixSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightSixSelect.SetActive(false);
                        RefrenceManager.Data.CustomNightSelect.SetActive(true);
                        RefrenceManager.Data.MenuScrollRight.SetActive(false);
                    }
                }
                else
                {
                    if (RefrenceManager.Data.CustomNightSelect.activeSelf)
                    {
                        RefrenceManager.Data.CustomNightSelect.SetActive(false);
                        RefrenceManager.Data.NightSixSelect.SetActive(true);
                        RefrenceManager.Data.MenuScrollRight.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightSixSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightSixSelect.SetActive(false);
                        RefrenceManager.Data.NightFiveSelect.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightFiveSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightFiveSelect.SetActive(false);
                        RefrenceManager.Data.NightFourSelect.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightFourSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightFourSelect.SetActive(false);
                        RefrenceManager.Data.NightThreeSelect.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightThreeSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightThreeSelect.SetActive(false);
                        RefrenceManager.Data.NightTwoSelect.SetActive(true);
                    }
                    else if (RefrenceManager.Data.NightTwoSelect.activeSelf)
                    {
                        RefrenceManager.Data.NightTwoSelect.SetActive(false);
                        RefrenceManager.Data.NightOneSelect.SetActive(true);
                        RefrenceManager.Data.MenuScrollLeft.SetActive(false);
                    }
                }
            }
        }
    }
}