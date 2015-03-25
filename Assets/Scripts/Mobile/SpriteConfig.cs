using UnityEngine;
using System.Collections;

/// <summary>
/// SpriteConfig
/// Configure sprite's scale based on screen resolution
/// </summary>
public class SpriteConfig : MonoBehaviour {
    public Vector2 screenPosition;          // Position of the element on the screen in pixels.
    public bool configPosition, configScale;
    public bool constrainScaleX;

    private Sprite sprite;

    void Start() {
        if(configScale) {
            sprite = (renderer as SpriteRenderer).sprite;
            //float scaleX = ((sprite.rect.width / CameraConfig.DefaultWidth) * Camera.main.pixelWidth) / sprite.rect.width;
            float scaleY = ((sprite.rect.height / CameraConfig.DefaultHeight) * Camera.main.pixelHeight) / sprite.rect.height;
            float scaleX = constrainScaleX ? scaleY :
                ((sprite.rect.width / CameraConfig.DefaultWidth) * Camera.main.pixelWidth) / sprite.rect.width;
            transform.localScale = new Vector3(scaleX, scaleY, 1);
        }

        if(configPosition) {
            Vector3 newPosition = new Vector3();
            newPosition.x = ((screenPosition.x / CameraConfig.DefaultWidth) * Camera.main.pixelWidth);
            newPosition.y = ((screenPosition.y / CameraConfig.DefaultHeight) * Camera.main.pixelHeight);
            newPosition.z = Camera.main.nearClipPlane;
            newPosition = Camera.main.ScreenToWorldPoint(newPosition);
            newPosition.z = transform.localPosition.z;
            transform.localPosition = newPosition;
        }
    }
}
