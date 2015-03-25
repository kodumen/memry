using UnityEngine;
using System.Collections;

// Disable other colliders when clicked
public class DisableCollider : MonoBehaviour {
    public bool disableOnClick;
    public Collider2D[] otherColliders;
    void Start() {
    }

    void OnMouseUpAsButton() {
        if(disableOnClick)
            gameObject.collider2D.enabled = false;

        foreach(Collider2D otherCollider2D in otherColliders)
            otherCollider2D.enabled = false;
    }
}
