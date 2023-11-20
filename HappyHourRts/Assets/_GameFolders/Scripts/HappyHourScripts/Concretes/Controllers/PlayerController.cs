using Fusion;
using HappyHourRts.Abstracts.Controllers;
using HappyHourRts.Abstracts.Inputs;
using HappyHourRts.Helpers;
using HappyHourRts.Networks;
using UnityEngine;

namespace HappyHourRts.Controllers
{
    public class PlayerController : NetworkBehaviour, IPlayerLeft, IPlayerJoined
    {
        [SerializeField] LayerMask _layerMask;
        [SerializeField] Camera _camera;
        [SerializeField] float _speed = 1f;
        [SerializeField] SpriteRenderer[] _spriteRenderers;
        [SerializeField] SoldierController[] _soldiers;

        IClickableController _selectedClickableController;
        Transform _cameraTransform;
        bool _isOwner;

        public IInputReader IInputReader { get; private set; }
        public static PlayerController Local { get; private set; }

        [Zenject.Inject]
        public void Constructor(IInputReader inputReader)
        {
            IInputReader = inputReader;
        }

        void OnValidate()
        {
            this.GetReference(ref _camera);
        }

        void Awake()
        {
            _cameraTransform = _camera.transform;
        }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                Local = this;
                _isOwner = true;
                
                Debug.Log("Local player spawned!");
            }
            else
            {
                Debug.Log("Remote player spawned!");
                _camera.gameObject.SetActive(false);
                _isOwner = false;
            }
        }

        void Update()
        {
            if (!_isOwner) return;
            CameraMovement();
        }

        public override void FixedUpdateNetwork()
        {
            if (!_isOwner) return;
            
            if (!GetInput(out NetworkInputData networkInputData)) return;

            bool isTouchDown = networkInputData.IsTouchDown;
            if (!SelectUnSelectSoldier(isTouchDown)) return;

            if (isTouchDown)
            {
                if (_selectedClickableController != null)
                {
                    var worldPosition = _camera.ScreenToWorldPoint(IInputReader.TouchPosition);
                    var raycastResult = Physics2D.Raycast(worldPosition, worldPosition, 100f, _layerMask);

                    if (raycastResult.collider != null)
                    {
                        if (raycastResult.collider.TryGetComponent(out IResourceController resourceController))
                        {
                            _selectedClickableController.SetResourceToSoldier(resourceController);
                        }
                    }
                    else
                    {
                        _selectedClickableController.SetTarget(worldPosition);
                    }
                }

                isTouchDown = false;
            }
        }

        private bool SelectUnSelectSoldier(bool isTouchDown)
        {
            if (isTouchDown)
            {
                var worldPosition = _camera.ScreenToWorldPoint(IInputReader.TouchPosition);
                var raycastResult = Physics2D.Raycast(worldPosition, worldPosition, 100f, _layerMask);

                if (raycastResult.collider != null)
                {
                    if (raycastResult.collider.TryGetComponent(out IClickableController clickableController))
                    {
                        if (clickableController.HasAuthority != Object.HasInputAuthority) return false;
                        
                        if (_selectedClickableController != null)
                        {
                            if (_selectedClickableController.Equals(clickableController))
                            {
                                _selectedClickableController.Unselect();
                                _selectedClickableController = null;
                                return false;
                            }

                            _selectedClickableController.Unselect();
                            _selectedClickableController = clickableController;
                            _selectedClickableController.Select();
                        }
                        else
                        {
                            _selectedClickableController = clickableController;
                            _selectedClickableController.Select();
                        }
                    }
                }
            }

            return _selectedClickableController != null;
        }

        private void CameraMovement()
        {
            if (IInputReader.IsTouch)
            {
                float deltaX = -IInputReader.DeltaPosition.x;
                float deltaY = -IInputReader.DeltaPosition.y;

                Vector2 newDeltaPosition = new Vector2(deltaX, deltaY);

                Vector3 cameraPosition = _cameraTransform.position;
                cameraPosition += _speed * Time.deltaTime * (Vector3)newDeltaPosition;

                _cameraTransform.position = cameraPosition;
            }
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.HasInputAuthority)
            {
                Runner.Despawn(Object);
            }
        }

        public void SetColor(Color color)
        {
            RPC_SetColorClientToServer(color);   
        }

        [Rpc(RpcSources.InputAuthority,RpcTargets.All)]
        void RPC_SetColorClientToServer(Color color)
        {
            RPC_SetColorServerToClient(color);
        }

        [Rpc(RpcSources.All, RpcTargets.InputAuthority)]
        void RPC_SetColorServerToClient(Color color)
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = color;
            }
        }

        public void PlayerJoined(PlayerRef player)
        {
            if (player == Object.HasInputAuthority)
            {
                foreach (var soldierController in _soldiers)
                {
                    soldierController.SetPlayerAuthority(player);
                }    
            }
        }
    }
}