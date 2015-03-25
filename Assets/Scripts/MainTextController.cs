using UnityEngine;
using System.Collections;

public class MainTextController : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public Sprite         hoverSprite;

    private Sprite defaultSprite;

    void Start() {
        defaultSprite = spriteRenderer.sprite;
    }

    void Update() {

    }

    void OnMouseEnter() {
        if(gameObject.collider2D.enabled)
            spriteRenderer.sprite = hoverSprite;
    }

    void OnMouseExit() {
        if(gameObject.collider2D.enabled)
            spriteRenderer.sprite = defaultSprite;
    }
}
