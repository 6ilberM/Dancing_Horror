using UnityEngine;

public class PlayAgain : MonoBehaviour {
    [SerializeField] GameObject playAgainButton = null;

    public void DoPlayAgain() {
        playAgainButton.SetActive(true);
    }
}
