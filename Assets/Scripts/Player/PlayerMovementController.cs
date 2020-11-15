using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovementController : MonoBehaviour
{
    [Space(5)]
    [SerializeField] private CharacterController characterController;
    public Camera cachedPlayerCamera = null;
    [SerializeField] Transform groundCheck;
    [SerializeField] Animator m_animator = null;
    [SerializeField] Light torchPointLight = null;

    [Space(5)]
    [HideInInspector] public Main_Input m_controls = null;

    [Header("Customizable")]
    [Space(5)]
    [Range(1.1f, 20f)] public float speed = 1.1f;
    [Range(1f, 14f)] public float rotateSpeed = 1;

    [Space(5)]
    public Vector3 desiredGravity = new Vector3(0, -9.81f, 0);
    public LayerMask groundLayerMask;

    [Header("Other Debug")]
    [Space(10)]
    public bool isGrounded = false;
    public bool isLeftButtonPressed = false;
    private float xRotation = 0f;

    [Header("Unlockables")]
    [Space(6.5f)]
    public bool isTorchUnlocked = false;

    private void Awake()
    {
        m_controls = new Main_Input();

        m_controls.Player.Fire.started += OnClickStarted;
        m_controls.Player.Fire.performed += OnClickPerformed;
    }

    private void OnClickStarted(InputAction.CallbackContext obj)
    {
        if (!isTorchUnlocked) { return; }

        isLeftButtonPressed = true;
        torchPointLight.enabled = true;
        m_animator.SetBool("IsButtonHeld", isLeftButtonPressed);
    }

    private void OnClickPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.interaction is SlowTapInteraction)
        {
            isLeftButtonPressed = false;
            torchPointLight.enabled = false;

            m_animator.SetBool("IsButtonHeld", isLeftButtonPressed);
        }

    }

    private void OnEnable() { m_controls.Enable(); }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.2f, groundLayerMask);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cachedPlayerCamera.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        var moveDirection = m_controls.Player.Move.ReadValue<Vector2>() * speed;

        MoveMethod(moveDirection);
    }

    private void MoveMethod(Vector2 moveDirection)
    {
        var desiredVelocity = (!isGrounded ? desiredGravity * Time.deltaTime : characterController.velocity.y < 0 ? Vector3.down * 2 : Vector3.zero)
            + (transform.forward * moveDirection.y + transform.right * moveDirection.x) * Time.deltaTime;

        characterController.Move(desiredVelocity);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rb = hit.collider.gameObject.GetComponent<Rigidbody>();
        if (rb) { rb.AddForce((transform.position - hit.collider.transform.position).normalized * 25); }
    }

    private void OnDisable() { m_controls.Disable(); }

    private void OnDestroy() { m_controls.Dispose(); }

    private void OnValidate()
    {
        if (characterController == null) { characterController = GetComponent<CharacterController>(); }

        if (m_animator != null && torchPointLight == null) { torchPointLight = m_animator.gameObject.GetComponentInChildren<Light>(); }
    }

}
