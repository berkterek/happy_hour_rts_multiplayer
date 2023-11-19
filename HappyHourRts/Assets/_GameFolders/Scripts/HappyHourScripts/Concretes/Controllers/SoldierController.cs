using HappyHourRts.Abstracts.Animations;
using HappyHourRts.Abstracts.Controllers;
using UnityEngine;

namespace HappyHourRts.Controllers
{
    public class SoldierController : MonoBehaviour,IClickableController
    {
        [SerializeField] SpriteRenderer _bodyRenderer;
        [SerializeField] SpriteRenderer _selectRenderer;
        [SerializeField] Transform _target;

        ISoldierAnimationService _soldierAnimationService;

        [Zenject.Inject]
        public void Constructor(ISoldierAnimationService soldierAnimationService)
        {
            _soldierAnimationService = soldierAnimationService;
        }

        void Update()
        {
            _soldierAnimationService.Tick();
        }

        void LateUpdate()
        {
            _soldierAnimationService.LateTick();
        }

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