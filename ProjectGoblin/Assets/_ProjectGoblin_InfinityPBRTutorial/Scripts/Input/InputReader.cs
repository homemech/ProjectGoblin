using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]

public class InputReader : ScriptableObject, Player25DGameInput.IGameplayActions 
{
    // Unity Open Projects - UOP1

    // Gameplay
    public event UnityAction jumpEvent;
    public event UnityAction interactEvent;
    public event UnityAction<float> moveEvent;

    private Player25DGameInput player25DGameInput;

    private void OnEnable()
    {
        if (player25DGameInput == null)
        {
            player25DGameInput = new Player25DGameInput();
            player25DGameInput.Gameplay.SetCallbacks(this);
        }
        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (moveEvent != null)
        {
            moveEvent.Invoke(context.ReadValue<float>());
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (interactEvent != null
            && context.phase == InputActionPhase.Performed)
            interactEvent.Invoke();
    }

    public void EnableGameplayInput()
    {
        player25DGameInput.Enable();
    }

    public void DisableAllInput()
    {
        player25DGameInput.Disable();
    }
}
