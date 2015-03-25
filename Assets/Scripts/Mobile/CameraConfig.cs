using UnityEngine;
using System.Collections;

/// <summary>
/// CameraConfig
/// Configure the camera size, based on the screen resolution.
/// </summary>
public class CameraConfig : MonoBehaviour {
    public int defaultWidth, defaultHeight;
    public static int DefaultWidth { set; get; }
    public static int DefaultHeight { set; get; }
    public static float Scale { get { return Camera.main.pixelWidth / DefaultWidth; } }

    void Awake() {
        Camera.main.orthographicSize = (Camera.main.pixelHeight / 100) / 2;
        DefaultWidth = defaultWidth;
        DefaultHeight = defaultHeight;
    }
    void Start() {
        //Camera.main.orthographicSize = (Camera.main.pixelHeight / 100) / 2;
    }
    void Update() {

    }
}