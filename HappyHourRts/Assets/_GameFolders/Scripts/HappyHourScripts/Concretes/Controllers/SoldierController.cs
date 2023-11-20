using HappyHourRts.Abstracts.Animations;
using HappyHourRts.Abstracts.Controllers;
using HappyHourRts.Helpers;
using UnityEngine;

namespace HappyHourRts.Controllers
{
    public class SoldierController : MonoBehaviour,IClickableController
    {
        [SerializeField] SpriteRenderer _bodyRenderer;
        [SerializeField] SpriteRenderer _selectRenderer;
        [SerializeField] Transform _target;
        [SerializeField] Transform _transform;
        [SerializeField] float _collectedResourceDistance = 0.5f;
        [SerializeField] float _maxCollectedTime = 3f;
        [SerializeField] int _collectedValue = 1;

        ISoldierAnimationService _soldierAnimationService;
        IResourceController _resourceController;
        float _currentCollectedTime = 0f;

        [Zenject.Inject]
        public void Constructor(ISoldierAnimationService soldierAnimationService)
        {
            _soldierAnimationService = soldierAnimationService;
            _soldierAnimationService.SetClickableController(this);
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);
        }

        void Update()
        {
            _soldierAnimationService.Tick();
        
            if (_resourceController == null) return;
            
            if (Vector2.Distance(_transform.position, _target.position) > _collectedResourceDistance) return;
        
            _currentCollectedTime += Time.deltaTime;
            _soldierAnimationService.IsResourceCollecting(true);
            if (_currentCollectedTime > _maxCollectedTime)
            {
                _currentCollectedTime = 0f;
                _resourceController.SoldierCollectingResource(_collectedValue);
                if (!_resourceController.CanDestroy)
                {
                    Destroy(_resourceController.transform.gameObject);
                    _resourceController = null;
                    _soldierAnimationService.IsResourceCollecting(false);
                }
            }
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

        public void SetResourceToSoldier(IResourceController resourceController)
        {
            _resourceController = resourceController;
            _target.position = _resourceController.Target.position;
        }
    }    
}