using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manequin : MonoBehaviour {

    [SerializeField] private PlayerMovementController player = null;

    [SerializeField] private GameObject putBallOnManequinText = null;
    [SerializeField] private GameObject putDressOnManequinText = null;

    public GameObject discoHead;
    public GameObject dress;
    public GameObject key;

    private bool wasDiscoHeadGrabbed = false;
    private bool wasDressgrabbed = false;

    void Update() {
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer < 5) {
            if (player.isDiscoBallGrabbed && !wasDiscoHeadGrabbed) {
                //print("here");
                putBallOnManequinText.SetActive(true);
                if (Input.GetKey(KeyCode.E)) {
                    discoHead.SetActive(true);
                    if (dress.activeSelf) {
                        key.SetActive(true);
                    }
                    wasDiscoHeadGrabbed = true;
                }
            } else if (player.isBloodyDressGrabbed && !wasDressgrabbed) {
                putDressOnManequinText.SetActive(true);
                if (Input.GetKey(KeyCode.E)) {
                    dress.SetActive(true);
                    if (discoHead.activeSelf) {
                        key.SetActive(true);
                    }
                    wasDressgrabbed = true;
                }
            }
        } else {
            putBallOnManequinText.SetActive(false);
            putDressOnManequinText.SetActive(false);
        }
    }
}
