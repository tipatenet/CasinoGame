using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerInputHandler : NetworkBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool JumpPressed { get; private set; }

    private PlayerInputActions inputActions;

    public override void OnStartLocalPlayer()
    {
        inputActions = new PlayerInputActions();

        //Move
        inputActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        //Look
        inputActions.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => LookInput = Vector2.zero;

        //Interact
        inputActions.Player.Interact.performed += ctx => InteractPressed = true;
        inputActions.Player.Interact.canceled += ctx => InteractPressed = false;

        //Jump
        inputActions.Player.Jump.performed += ctx => JumpPressed = true;
        inputActions.Player.Jump.canceled += ctx => JumpPressed = false;

        // Active la map Player
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        if (isLocalPlayer && inputActions != null)
            inputActions.Player.Disable();
    }
}