using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader",  menuName = "Scriptable Objects/Input Reader", order = 0)]
public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    public void OnMove(InputAction.CallbackContext context)
    {
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
    }
}
