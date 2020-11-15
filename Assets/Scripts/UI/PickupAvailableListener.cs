using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAvailableListener : MonoBehaviour
{
    [SerializeField] PlayerMovementController playerMovementController = null;
    [SerializeField] TMPro.TextMeshProUGUI textMesh = null;
    //[SerializeField] private int totalCharacters = 0;


    private void Awake()
    {
        if (null == playerMovementController)
        {
            playerMovementController = FindObjectOfType<PlayerMovementController>();
        }

        if (playerMovementController) { playerMovementController.onCollectibleOverlap += OnCollectibleOverlap; }
    }

    private void Start() { textMesh.maxVisibleCharacters = 0; }

    private void OnCollectibleOverlap(bool isOverlapping)
    {
        if (isOverlapping)
        {
            StartCoroutine(DisplayText());
        }
        else
        {
            StopCoroutine(DisplayText());
            textMesh.maxVisibleCharacters = 0;
        }

    }

    IEnumerator DisplayText()
    {
        while (textMesh.maxVisibleCharacters <= textMesh.text.Length)
        {
            textMesh.maxVisibleCharacters++;

            yield return new WaitForSeconds(.045f);
        }
    }

}
