using HappyHourRts.Abstracts.Controllers;
using UnityEngine;

namespace HappyHourRts.Controllers
{
    public class SoldierController : MonoBehaviour,IClickableController
    {
        public void Select()
        {
            Debug.Log("Select");
        }

        public void Unselect()
        {
            Debug.Log("Unselect");
        }
    }    
}