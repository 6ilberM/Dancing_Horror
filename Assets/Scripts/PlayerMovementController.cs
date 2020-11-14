using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Space(5)]
    [SerializeField] private CharacterController characterController;
    public Camera cachedPlayerCamera = null;

    [Space(5)]
    [SerializeField] private Main_Input m_controls = null;

    [Header("Customizable")]
    [Space(5)]
    [Range(1.1f, 20f)] public float speed = 1.1f;
    [Range(1f, 14f)] public float rotateSpeed = 1;

    private Vector2 m_Rotation;
    private float xRotation = 0f;

    private void Awake()
    { m_controls = new Main_Input(); }

    private void OnEnable() { m_controls.Enable(); }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var moveDirection = m_controls.Player.Move.ReadValue<Vector2>() * speed;
        var look = m_controls.Player.Look.ReadValue<Vector2>();

        var desiredVelocity = ((Vector3.up * Physics.gravity.y)
            * Time.deltaTime
            + ((transform.forward * moveDirection.y) + (transform.right * moveDirection.x))
            * Time.deltaTime);

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
    }

    private void OnDisable() { m_controls.Disable(); }

    private void OnDestroy() { m_controls.Dispose(); }

    private void OnValidate() { if (characterController == null) { characterController = GetComponent<CharacterController>(); } }

}
