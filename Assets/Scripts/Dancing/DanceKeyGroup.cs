using UnityEngine;
using System.Collections;

public class DanceKeyGroup : MonoBehaviour {
    [SerializeField] private SpriteRenderer[] keys = null;

    [SerializeField]
    [Range(0.1f, 2)]
    private float keyShakeDuration = 0.2f;

    public int NumberOfKeys => keys.Length;

    public void SetKeySprite(int keyIndex, Sprite sprite) {
        keys[keyIndex].sprite = sprite;
    }

    public void ShakeKey(int keyIndex) {
        StartCoroutine(ShakeAnimation(keyIndex));
    }

    private IEnumerator ShakeAnimation(int keyIndex) {
        SpriteRenderer keySprite = keys[keyIndex];

        float animationEnd = Time.time + keyShakeDuration;
        float rotationAngle = 25;

        while (Time.time < animationEnd) {
            float remainingTime = (animationEnd - Time.time) / keyShakeDuration;
            var newRotation = new Vector3(0, 0, (Mathf.PerlinNoise(Time.time * 5, 0) * 2 - 1)
                                               * rotationAngle * remainingTime);
            keySprite.transform.localEulerAngles = newRotation;
            yield return null;
        }
    }
}
