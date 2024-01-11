using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions playerInput;
    PlayerCombat playerCombat;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnEnable()
    {
        // Enable the input actions
        playerInput.Enable();

        // Subscribe to the desired action events
        playerInput.Player.ShootUp.performed += OnShootUp;
        playerInput.Player.ShootRight.performed += OnShootRight;
        playerInput.Player.ShootDown.performed += OnShootDown;
        playerInput.Player.ShootLeft.performed += OnShootLeft;
    }

    private void OnDisable()
    {
        // Unsubscribe from the action events
        playerInput.Player.ShootUp.performed -= OnShootUp;
        playerInput.Player.ShootRight.performed -= OnShootRight;
        playerInput.Player.ShootDown.performed -= OnShootDown;
        playerInput.Player.ShootLeft.performed -= OnShootLeft;

        // Disable the input actions
        playerInput.Disable();
    }

    private void OnShootUp(InputAction.CallbackContext context)
    {
        playerCombat.HandleAttack(Vector2.up);
    }

    private void OnShootRight(InputAction.CallbackContext context)
    {
        playerCombat.HandleAttack(Vector2.right);
    }

    private void OnShootDown(InputAction.CallbackContext context)
    {
        playerCombat.HandleAttack(Vector2.down);
    }

    private void OnShootLeft(InputAction.CallbackContext context)
    {
        playerCombat.HandleAttack(Vector2.left);
    }
}
