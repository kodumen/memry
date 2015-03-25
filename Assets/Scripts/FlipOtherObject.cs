using UnityEngine;
using System.Collections;

// FlipOtherObject
// Flips another object with a FlipAction component and
// scales another object with a ScaleAction compoenent
// when clicked.
public class FlipOtherObject : MonoBehaviour {
    public GameObject otherObject;

    private FlipAction  objFlipAction;
    private ScaleAction objScaleAction;

    void Start() {
        objFlipAction = otherObject.GetComponent("FlipAction") as FlipAction;
        objScaleAction = otherObject.GetComponent("ScaleAction") as ScaleAction;
    }

    void Update() {
        if(objScaleAction != null && objScaleAction.IsPressScale)
            objScaleAction.ScaleToDefault();
    }

    void OnMouseUpAsButton() {
        objFlipAction.Flip();
        objScaleAction.ScaleToPress();
    }
}
