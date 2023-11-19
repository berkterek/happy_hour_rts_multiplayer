using HappyHourRts.Abstracts.Controllers;
using UnityEngine;

namespace HappyHourRts.Controllers
{
    public class SoldierController : MonoBehaviour,IClickableController
    {
        [SerializeField] SpriteRenderer _bodyRenderer;
        [SerializeField] SpriteRenderer _selectRenderer;
        [SerializeField] Transform _target;

        public void Select()
        {
            _selectRenderer.enabled = true;
        }

        public void Unselect()
        {
            _selectRenderer.enabled = false;
        }

        public void SetTarget(Vector3 position)
        {
            _target.position = position;
        }
    }    
}