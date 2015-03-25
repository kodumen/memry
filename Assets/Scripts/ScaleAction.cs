using UnityEngine;
using System.Collections;

// Scale Action
// Scale the object to a specified value on hover or press.
// Scale the object to its default scale when clicked or idle.
// Requires a collider to run
// Requires Clickable script
// -R
public class ScaleAction : MonoBehaviour {
    public Vector3 hoverScale;
    public Vector3 pressScale;
    public float   duration;

    private Vector3     defaultScale;
    private Vector3     nextScale;      // The scale the object needs to arrive at
    private Vector3     origScale;      // The scale the object has on a mouse event
    private float       timer;          // The time since the mouse event is invoked
                                        // ^ Resets to 0 every new mouse event
    public bool IsPressScale {
        get {
            return nextScale == pressScale && transform.localScale == pressScale;
        }
    }
    public bool IsHoverScale {
        get {
            return nextScale == hoverScale && transform.localScale == hoverScale;
        }
    }
    public bool IsDefaultScale {
        get {
            return nextScale == defaultScale && transform.localScale == defaultScale;
        }
    }

    void Start() {
        defaultScale = transform.localScale;
        nextScale = defaultScale;
        origScale = defaultScale;
        timer = 0;
    }

    void Update() {
        timer += Time.deltaTime;
        transform.localScale = Vector3.Lerp(origScale, nextScale, timer / duration);
    }

    public void ScaleToHover() {
        nextScale = hoverScale;
        origScale = transform.localScale;
        timer = 0;
    }

    public void ScaleToDefault() {
        nextScale = defaultScale;
        origScale = transform.localScale;
        timer = 0;
    }

    public void ScaleToPress() {
        nextScale = pressScale;
        origScale = transform.localScale;
        timer = 0;
    }

    void OnMouseEnter() {
        ScaleToHover();
    }

    void OnMouseExit() {
        ScaleToDefault();
    }

    void OnMouseDown() {
        ScaleToPress();
    }

    void OnMouseUpAsButton() {
        ScaleToDefault();
    }
}
