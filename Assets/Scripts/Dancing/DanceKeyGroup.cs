using UnityEngine;

public class DanceKeyGroup : MonoBehaviour {
    [SerializeField] private SpriteRenderer[] keys = null;

    public void SetKeySprite(int num, Sprite sprite) {
        keys[num].sprite = sprite;
    }
}
