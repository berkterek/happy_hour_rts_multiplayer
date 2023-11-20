using HappyHourRts.Abstracts.Controllers;
using HappyHourRts.Abstracts.Inputs;
using HappyHourRts.Helpers;
using UnityEngine;

namespace HappyHourRts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] LayerMask _layerMask;
        [SerializeField] Camera _camera;
        [SerializeField] float _speed = 1f;

        IClickableController _selectedClickableController;
        Transform _cameraTransform;
        
        public IInputReader IInputReader { get; private set; }

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

        void Update()
        {
            CameraMovement();
            
            bool isTouchDown = IInputReader.IsTouchDown;
            if (!SelectUnSelectSoldier(isTouchDown)) return;

            if (isTouchDown)
            {
                if (_selectedClickableController != null)
                {
                    var worldPosition = _camera.ScreenToWorldPoint(IInputReader.TouchPosition);
                    var raycastResult = Physics2D.Raycast(worldPosition, worldPosition, 100f,_layerMask);

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
    }    
}