using Mirror;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private PlayerInputHandler inputs;

    float xRotation = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputs = GetComponent<PlayerInputHandler>();
    }

    public override void OnStartLocalPlayer()
    {
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        Camera.main.transform.SetParent(transform.GetChild(0));
        Camera.main.transform.localPosition = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        MovePlayer();
        Look();
    }

    // Déplacement

    private void MovePlayer()
    {
        Vector3 move = (transform.forward * inputs.MoveInput.y)
             + (transform.right * inputs.MoveInput.x);
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    // Rotation caméra + joueur
    private void Look()
    {
        float mouseX = inputs.LookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = inputs.LookInput.y * mouseSensitivity * Time.deltaTime;

        // Rotation verticale (caméra)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotation horizontale (joueur)
        transform.Rotate(Vector3.up * mouseX);
    }
}
