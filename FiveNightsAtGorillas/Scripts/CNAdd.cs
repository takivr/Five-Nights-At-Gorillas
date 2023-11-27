using FiveNightsAtGorillas.Managers.Refrences;
using UnityEngine;

namespace FiveNightsAtGorillas.Other.CustomNightAdd
{
    public class CNAdd : MonoBehaviour
    {
        public bool IsGorilla;
        public bool IsMingus;
        public bool IsBob;
        public bool IsDingus;

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "LeftHandTriggerCollider" || other.name == "RightHandTriggerCollider")
            {
                if (IsGorilla)
                {
                    if(RefrenceManager.Data.GD.text != "20") 
                    {
                        int value = int.Parse(RefrenceManager.Data.GD.text);
                        value += 1;
                        RefrenceManager.Data.GD.text = value.ToString();
                    }
                }
                else if(IsMingus)
                {
                    if (RefrenceManager.Data.MD.text != "20")
                    {
                        int value = int.Parse(RefrenceManager.Data.GD.text);
                        value += 1;
                        RefrenceManager.Data.MD.text = value.ToString();
                    }
                }
                else if(IsBob)
                {
                    if (RefrenceManager.Data.BD.text != "20")
                    {
                        int value = int.Parse(RefrenceManager.Data.GD.text);
                        value += 1;
                        RefrenceManager.Data.BD.text = value.ToString();
                    }
                }
                else if (IsDingus)
                {
                    if (RefrenceManager.Data.DD.text != "20")
                    {
                        int value = int.Parse(RefrenceManager.Data.GD.text);
                        value += 1;
                        RefrenceManager.Data.DD.text = value.ToString();
                    }
                }
            }
        }
    }
}