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
    private bool isObjectOverlapped = false;
    private Transform collectibleObj;

    [Header("Unlockables")]
    [Space(6.5f)]
    public bool isTorchUnlocked = false;
    public bool isKeyGrabbed = false;
    public bool isHammerGrabbed = false;
    public bool isLampGrabbed = false;
    public bool isDiscoBallGrabbed = false;
    public bool isBloodyDressGrabbed = false;
    public bool isHatGrabbed = false;

    //Event
    public Action<bool> onCollectibleOverlap;

    private void Awake()
    {
        m_controls = new Main_Input();

        m_controls.Player.Fire.started += OnClickStarted;
        m_controls.Player.Fire.performed += OnClickPerformed;

        m_controls.Player.Grab.performed += Grab_performed;
    }

    private void Grab_performed(InputAction.CallbackContext ctx)
    {
        if (isObjectOverlapped && collectibleObj)
        {
            //We're just going to compare it with the name of the Collectible for simplicity and avoiding extra systems
            if (collectibleObj.gameObject.name == "Key")
            {
                this.isKeyGrabbed = true;
            }
            else if (collectibleObj.gameObject.name == "Hammer")
            {
                this.isHammerGrabbed = true;
            }
            else if (collectibleObj.gameObject.name == "Lamp")
            {
                this.isLampGrabbed = true;
            }
            else if (collectibleObj.gameObject.name == "DiscoBall")
            {
                this.isDiscoBallGrabbed = true;
            }
            else if (collectibleObj.gameObject.name == "Bloody Dress")
            {
                this.isBloodyDressGrabbed = true;
            }
            else if (collectibleObj.gameObject.name == "Hat")
            {
                this.isHatGrabbed = true;
            }

            Destroy(collectibleObj.gameObject, Time.deltaTime);

            onCollectibleOverlap?.Invoke(false);
            collectibleObj = null;
            isObjectOverlapped = false;
        }
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

    private void Start()
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            onCollectibleOverlap?.Invoke(true);

            isObjectOverlapped = true;
            collectibleObj = other.transform;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            onCollectibleOverlap?.Invoke(false);
            collectibleObj = null;
            isObjectOverlapped = false;
        }
    }

    private void OnDisable() { m_controls.Disable(); }

    private void OnDestroy() { m_controls.Dispose(); }

    private void OnValidate()
    {
        if (characterController == null) { characterController = GetComponent<CharacterController>(); }

        if (m_animator != null && torchPointLight == null) { torchPointLight = m_animator.gameObject.GetComponentInChildren<Light>(); }
    }

}
