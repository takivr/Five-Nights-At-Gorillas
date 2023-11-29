using FiveNightsAtGorillas.Managers.Refrences;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.CustomNightSub
{
    public class CNSub : MonoBehaviour
    {
        public bool IsGorilla;
        public bool IsMingus;
        public bool IsBob;
        public bool IsDingus;

        void Awake() { gameObject.layer = 18; }

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                if (IsGorilla)
                {
                    if (RefrenceManager.Data.GD.text != "0")
                    {
                        int value = int.Parse(RefrenceManager.Data.GD.text);
                        value--;
                        RefrenceManager.Data.GD.text = value.ToString();
                    }
                }
                else if (IsMingus)
                {
                    if (RefrenceManager.Data.MD.text != "0")
                    {
                        int value = int.Parse(RefrenceManager.Data.MD.text);
                        value--;
                        RefrenceManager.Data.MD.text = value.ToString();
                    }
                }
                else if (IsBob)
                {
                    if (RefrenceManager.Data.BD.text != "0")
                    {
                        int value = int.Parse(RefrenceManager.Data.BD.text);
                        value--;
                        RefrenceManager.Data.BD.text = value.ToString();
                    }
                }
                else if (IsDingus)
                {
                    if (RefrenceManager.Data.DD.text != "0")
                    {
                        int value = int.Parse(RefrenceManager.Data.DD.text);
                        value--;
                        RefrenceManager.Data.DD.text = value.ToString();
                    }
                }
            }
        }
    }
}