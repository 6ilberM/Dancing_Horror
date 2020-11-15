using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;

    [Space(10)]
    public PlayerMovementController player;
    private Vector3 startingRotation;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (!Application.isPlaying) { return; }
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null)
                    startingRotation = transform.localRotation.eulerAngles;

                Vector2 deltaInput = player.m_controls.Player.Look.ReadValue<Vector2>();

                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;

                startingRotation.y += -deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0);
            }
        }
    }
}
