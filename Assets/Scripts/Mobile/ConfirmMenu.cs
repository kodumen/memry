using UnityEngine;
using System.Collections;

public class ConfirmMenu : MonoBehaviour {
    public Transform    confirmUI, mainUI;

    private bool         isRan;     // Is only ran once
    private ButtonAction buttonAction;
    private ButtonAction newGameButtonAction;

    void Start() {
        isRan = false;
        buttonAction = gameObject.GetComponent("ButtonAction") as ButtonAction;
    }

    void Update() {
        if(buttonAction.IsClicked && !isRan && !buttonAction.startNewGame) {
            isRan = true;
            Vector3 tempPos = mainUI.localPosition;
            mainUI.localPosition = confirmUI.position;
            confirmUI.localPosition = tempPos;
            //buttonAction.moveCamera = false;
        }
    }
}
