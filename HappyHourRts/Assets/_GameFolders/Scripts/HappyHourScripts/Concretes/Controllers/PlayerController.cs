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

        IClickableController _selectedClickableController;
        
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

        void Update()
        {
            bool isTouchDown = IInputReader.IsTouchDown;
            if (!SelectUnSelectSoldier(isTouchDown)) return;

            if (isTouchDown)
            {
                if (_selectedClickableController != null)
                {
                    var worldPosition = _camera.ScreenToWorldPoint(IInputReader.TouchPosition);
                    _selectedClickableController.SetTarget(worldPosition);
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
    }    
}