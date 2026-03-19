using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    [SerializeField] private Vector3 heightCamPos = new Vector3(0, 1.5f, 0);

    private CharacterController controller;

    float xRotation = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void OnStartLocalPlayer()
    {
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = heightCamPos;

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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    // Rotation caméra + joueur
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation verticale (caméra)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotation horizontale (joueur)
        transform.Rotate(Vector3.up * mouseX);
    }
}
