using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class TintOnMouseOver : MonoBehaviour,
                                IPointerEnterHandler,
                                IPointerExitHandler,
                                IPointerClickHandler {

    [SerializeField] private Color originalColor = Color.red;
    [SerializeField] private Color tintColor = Color.white;
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private string sceneToMoveTo = "First_floor";

    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene(sceneToMoveTo);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        text.color = tintColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        text.color = originalColor;
    }
}
