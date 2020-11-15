using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    [SerializeField] private PlayerMovementController player = null;
    [SerializeField] private GameObject openDoorText = null;

    void Update() {
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer < 9 && player.isKeyGrabbed) {
            //print("here");
            openDoorText.SetActive(true);
            if (Input.GetKey(KeyCode.E)) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("YouWin");
            }
        } else {
            openDoorText.SetActive(false);
        }
    }
}
