using UnityEngine;
using static UnityEngine.Mathf;

// Copied from https://youtu.be/0bgux-JWnRQ
[ExecuteAlways]
public class PixelCameraHelper : MonoBehaviour {

    [Header("Should Always make sure that the screen has an even resolution!")]
    [SerializeField]
    [Range(1, 200)]
    private int pixelsPerUnit = 75;

    [Tooltip("How many pixels should be on the screen on any given dimension for zooming to happen")]
    [SerializeField] 
    [Range(1, 3000)]
    private float zoomThreshold = 1000;

    [SerializeField] 
    private bool useCustomPixelScale = false;

    [SerializeField] 
    [Range(1f, 20)]
    private float pixelScale = 1;

    [SerializeField]
    private Vector2 realCameraPosition = Vector2.zero;

    public void Move(Vector2 dir) {
        ApplyZoom();
        realCameraPosition += dir;
        AdjustCamera();
    }

    public void MoveTo(Vector2 pos) {
        ApplyZoom();
        realCameraPosition = pos;
        AdjustCamera();
    }

    public void AdjustCamera() {
        Camera.main.transform.position = new Vector3(
            RoundToNearestPixel(realCameraPosition.x),
            RoundToNearestPixel(realCameraPosition.y),
            -10
        );
    }

    public float RoundToNearestPixel(float pos) {
        float screenPixelsPerUnit = Screen.height / (Camera.main.orthographicSize * 2);
        float pixelValue = Round(pos * screenPixelsPerUnit);

        return pixelValue / screenPixelsPerUnit;
    }

    public void ApplyZoom() {
        if (!useCustomPixelScale) {
            float smallestDimension = Screen.height < Screen.width ? Screen.height : Screen.width;
            pixelScale = Clamp(Floor(smallestDimension / (zoomThreshold / 2)), 1, 20);
        }

        Camera.main.orthographicSize = (Screen.height / (pixelsPerUnit * pixelScale)) / 2f;
    }

    // TODO: Move this else where (Only in Start() and resolution adjustment for production)
    private void Update() {
        ApplyZoom();
        AdjustCamera();
    }

    #if UNITY_EDITOR
    private void OnGUI() {
        if (Screen.width % 2 == 1 || Screen.height % 2 == 1) {
            GUI.contentColor = Color.red;
            GUI.Label(new Rect(10, 10, 600, 20), $"PixelCameraHelper doesn't work perfectly with an odd resolution ({Screen.width},{Screen.height})");
        }
    }
    #endif
}
