using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Settings.Input
{
    [CreateAssetMenu(fileName = "InputReaderObj",  menuName = "Scriptable Objects/Input Reader", order = 0)]
    public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
    {
        private InputSystem_Actions actions;
        public UnityAction ShootEvent;

        private void OnEnable()
        {
            if (actions == null)
            {
                actions = new InputSystem_Actions();
                actions.Player.SetCallbacks(this);
            }
            
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                ShootEvent?.Invoke();
            }
        }
    }
}
