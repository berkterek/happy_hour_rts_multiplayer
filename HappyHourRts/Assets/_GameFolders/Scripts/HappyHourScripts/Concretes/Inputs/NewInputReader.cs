using HappyHourRts.Abstracts.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HappyHourRts.Inputs
{
    public class NewInputReader : IInputReader
    {
        readonly GameInputActions _input;

        public Vector2 TouchPosition { get; private set;}
        public Vector2 DeltaPosition { get; private set; }
        public bool IsTouchDown => _input.Player.IsTouch.WasPressedThisFrame();
        public bool IsTouch { get; private set; }

        public NewInputReader()
        {
            _input = new GameInputActions();
            _input.Enable();

            _input.Player.IsTouch.performed += HandleOnIsTouch;
            _input.Player.IsTouch.canceled += HandleOnIsTouch;
            _input.Player.DeltaPosition.performed += HandleOnDeltaPosition;
            _input.Player.DeltaPosition.canceled += HandleOnDeltaPosition;
            _input.Player.TouchPosition.performed += HandleOnTouchPosition;
            _input.Player.TouchPosition.canceled += HandleOnTouchPosition;
        }

        ~NewInputReader()
        {
            _input.Disable();
            
            _input.Player.IsTouch.performed -= HandleOnIsTouch;
            _input.Player.IsTouch.canceled -= HandleOnIsTouch;
            _input.Player.DeltaPosition.performed -= HandleOnDeltaPosition;
            _input.Player.DeltaPosition.canceled -= HandleOnDeltaPosition;
            _input.Player.TouchPosition.performed -= HandleOnTouchPosition;
            _input.Player.TouchPosition.canceled -= HandleOnTouchPosition;
        }
        
        void HandleOnIsTouch(InputAction.CallbackContext context)
        {
            IsTouch = context.ReadValueAsButton();
        }
        
        void HandleOnDeltaPosition(InputAction.CallbackContext context)
        {
            DeltaPosition = context.ReadValue<Vector2>();
        }
        
        void HandleOnTouchPosition(InputAction.CallbackContext context)
        {
            TouchPosition = context.ReadValue<Vector2>();
        }
    }
}