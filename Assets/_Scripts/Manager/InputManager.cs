using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace UnityTutorial.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;

        public Vector2 Move {get; private set;}
        public Vector2 Look {get; private set;}
        public bool Run {get; private set;}
        public bool Jump {get; private set;}
        public bool Crouch {get; private set;}
        public bool Attack {get; private set;}
        public bool Melee {get; private set;}
        public bool Aim {get; private set;}

        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _jumpAction;
        private InputAction _crouchAction;
        private InputAction _attackAction;
        private InputAction _meleeAction;
        private InputAction _aimAction;

        private void Awake() {
            HideCursor();
            _currentMap = PlayerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction  = _currentMap.FindAction("Run");
            _jumpAction = _currentMap.FindAction("Jump");
            _crouchAction = _currentMap.FindAction("Crouch");
            _attackAction = _currentMap.FindAction("Attack");
            _meleeAction = _currentMap.FindAction("Melee");
            _aimAction = _currentMap.FindAction("Aim");


            _moveAction.performed += onMove;
            _lookAction.performed += onLook;
            _runAction.performed += onRun;
            _jumpAction.performed += onJump;
            _crouchAction.started += onCrouch;
            _attackAction.started += onAttack;
            _meleeAction.started += onMelee;
            _aimAction.started += onAim;

            _moveAction.canceled += onMove;
            _lookAction.canceled += onLook;
            _runAction.canceled += onRun;
            _jumpAction.canceled += onJump;
            _crouchAction.canceled += onCrouch;
            _attackAction.canceled += onAttack;
            _aimAction.canceled += onAim;
        }

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void onMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }
        private void onLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }
        private void onRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }
        private void onJump(InputAction.CallbackContext context)
        {
            Jump = context.ReadValueAsButton();
        }
        private void onCrouch(InputAction.CallbackContext context)
        {
            Crouch = context.ReadValueAsButton();
        }

        private void onAttack(InputAction.CallbackContext context)
        {
            Attack = context.ReadValueAsButton();
        }

        private void onMelee(InputAction.CallbackContext context)
        {
            Melee = context.ReadValueAsButton();
        }

        private void onAim(InputAction.CallbackContext context)
        {
            Aim = context.ReadValueAsButton();
        }

        private void OnEnable() {
            _currentMap.Enable();
        }

        private void OnDisable() {
            _currentMap.Disable();
        }
        
    }
}
