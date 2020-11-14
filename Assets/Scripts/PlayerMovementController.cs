using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Space(5)]
    [SerializeField] private CharacterController characterController;
    public Camera cachedPlayerCamera = null;
    [SerializeField] Transform groundCheck;
    public LayerMask groundLayerMask;

    [Space(5)]
    [SerializeField] private Main_Input m_controls = null;

    [Header("Customizable")]
    [Space(5)]
    [Range(1.1f, 20f)] public float speed = 1.1f;
    [Range(1f, 14f)] public float rotateSpeed = 1;
    [Space(5)]
    public Vector3 desiredGravity = new Vector3(0, -9.81f, 0);

    [Header("Other Debug")]
    [Space(5)]
    public bool isGrounded = false;
    private float xRotation = 0f;

    private void Awake() { m_controls = new Main_Input(); }

    private void OnEnable() { m_controls.Enable(); }

    void Start() { Cursor.lockState = CursorLockMode.Locked; }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, .2f, groundLayerMask);
        var moveDirection = m_controls.Player.Move.ReadValue<Vector2>() * speed;
        var look = m_controls.Player.Look.ReadValue<Vector2>();

        var desiredVelocity = (!isGrounded ? desiredGravity * Time.deltaTime : characterController.velocity.y < 0 ? Vector3.down * 2 : Vector3.zero)
            + (transform.forward * moveDirection.y + transform.right * moveDirection.x) * Time.deltaTime;

        characterController.Move(desiredVelocity);

        if (look.sqrMagnitude < 0.01)
            return;

        xRotation -= look.y * rotateSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        var scaledRotateSpeed = look * rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * scaledRotateSpeed.x);

        //cachedPlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        cachedPlayerCamera.transform.localRotation = Quaternion.Slerp(cachedPlayerCamera.transform.localRotation, Quaternion.Euler(xRotation, 0, 0),
            Time.deltaTime * 9.5f);
        Debug.Log(characterController.velocity);
    }

    private void OnDisable() { m_controls.Disable(); }

    private void OnDestroy() { m_controls.Dispose(); }

    private void OnValidate() { if (characterController == null) { characterController = GetComponent<CharacterController>(); } }

}
