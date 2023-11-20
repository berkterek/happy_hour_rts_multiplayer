using Fusion;
using HappyHourRts.Abstracts.Animations;
using HappyHourRts.Abstracts.Controllers;
using HappyHourRts.Helpers;
using UnityEngine;

namespace HappyHourRts.Controllers
{
    public class SoldierController : NetworkBehaviour,IClickableController
    {
        [SerializeField] SpriteRenderer _selectRenderer;
        [SerializeField] Transform _target;
        [SerializeField] Transform _transform;
        [SerializeField] float _collectedResourceDistance = 0.5f;
        [SerializeField] float _maxCollectedTime = 3f;
        [SerializeField] int _collectedValue = 1;

        ISoldierAnimationService _soldierAnimationService;
        IResourceController _resourceController;
        float _currentCollectedTime = 0f;
        Vector3 _position;

        public bool HasAuthority => Object.HasInputAuthority;

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
            
            ResourceProcess();
        }

        void LateUpdate()
        {
            _soldierAnimationService.LateTick();
        }

        private void ResourceProcess()
        {
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
            RPC_SetTarget(position);
        }

        public void SetResourceToSoldier(IResourceController resourceController)
        {
            _resourceController = resourceController;
            RPC_SetTarget(_resourceController.Target.position);
        }
        
        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        void RPC_SetTarget(Vector3 position)
        {
            _target.position = position;
        }

        public void SetPlayerAuthority(PlayerRef playerRef)
        {
            Object.SetPlayerAlwaysInterested(playerRef,true);
            if (_target.TryGetComponent(out NetworkObject networkObject))
            {
                networkObject.SetPlayerAlwaysInterested(playerRef,true);
            }
        }
    }    
}