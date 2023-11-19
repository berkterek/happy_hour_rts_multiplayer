using HappyHourRts.Abstracts.Controllers;
using HappyHourRts.Abstracts.Inputs;
using UnityEngine;

namespace HappyHourRts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] LayerMask _layerMask;
        
        Camera _camera;

        IClickableController _selectedClickableController;
        
        public IInputReader IInputReader { get; private set; }

        [Zenject.Inject]
        public void Constructor(IInputReader inputReader)
        {
            IInputReader = inputReader;
        }

        void Awake()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            SelectUnSelectSoldier();
        }

        private void SelectUnSelectSoldier()
        {
            if (IInputReader.IsTouchDown)
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
                                return;
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
        }
    }    
}