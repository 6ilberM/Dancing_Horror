using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAvailableListener : MonoBehaviour
{
    [SerializeField] PlayerMovementController playerMovementController = null;
    [SerializeField] TMPro.TextMeshProUGUI textMesh;
    [SerializeField] private int totalCharacters = 0;


    private void Awake()
    {
        if (null == playerMovementController)
        {
            playerMovementController = FindObjectOfType<PlayerMovementController>();
        }

        if (playerMovementController) { playerMovementController.onCollectibleOverlap += OnCollectibleOverlap; }
    }

    private void Start()
    {
        textMesh.maxVisibleCharacters = 0;
    }

    private void OnCollectibleOverlap(bool isOverlapping)
    {
        //textMesh.enabled = isOverlapping;
        if (isOverlapping)
        {
            StartCoroutine(TextAppear());
        }
        else
        {
            StopCoroutine(TextAppear());
            textMesh.maxVisibleCharacters = 0;
        }
    }

    IEnumerator TextAppear()
    {

        while (textMesh.maxVisibleCharacters <= textMesh.text.Length)
        {
            textMesh.maxVisibleCharacters++;

            yield return new WaitForSeconds(.1f);
        }
    }

}
