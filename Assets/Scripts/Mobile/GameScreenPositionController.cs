using UnityEngine;
using System.Collections;

/// <summary>
/// Switch the place of new game and continue buttons.Yay!
/// </summary>
public class GameScreenPositionController : MonoBehaviour {
    public Transform    newGameButton, continueButton, confirmScreen, gameOverScreen;
    public ButtonAction buttonNo, buttonNewGameOver;    // New Game button on Game Over screen

    private bool         isRan;     // Is only ran once
    private ButtonAction buttonAction;
    private ButtonAction newGameButtonAction;
    private Vector3      tempPositionOnConfirm, defaultPosition, tempPositionOnGameOver, camPositionOnNewGame;

    // Use this for initialization
    void Start() {
        isRan = false;
        buttonAction = gameObject.GetComponent("ButtonAction") as ButtonAction;
        newGameButtonAction = newGameButton.gameObject.GetComponent("ButtonAction") as ButtonAction;
        tempPositionOnConfirm = new Vector3(buttonAction.thisScreen.position.x, confirmScreen.position.y, confirmScreen.position.z);
        defaultPosition = buttonAction.thisScreen.position;
        tempPositionOnGameOver = buttonNewGameOver.nextScreen.position;
        camPositionOnNewGame = new Vector3(defaultPosition.x, defaultPosition.z, Camera.main.transform.position.z);
    }

    // Update is called once per frame
    void Update() {
        // Reposition the new game and continue buttons in the menu
        if(buttonAction.IsClicked && !isRan) {
            isRan = true;
            Vector3 tempPos = new Vector3(newGameButton.localPosition.x, continueButton.localPosition.y, 0);
            continueButton.localPosition = newGameButton.position;
            newGameButton.localPosition = tempPos;
            newGameButtonAction.startNewGame = false;
            newGameButtonAction.enableLerp = false;
            newGameButtonAction.nextScreen = confirmScreen;
            //newGameButtonAction.disableColliders = false;
            continueButton.collider2D.enabled = true;
        }

        if(Camera.main.transform.position.x == confirmScreen.position.x &&
            Camera.main.transform.position.y == confirmScreen.position.y) {
            buttonAction.thisScreen.position = tempPositionOnConfirm;
        }
        else if((Camera.main.transform.position.x == tempPositionOnConfirm.x &&
            Camera.main.transform.position.y == tempPositionOnConfirm.y)) {
            buttonAction.thisScreen.position = defaultPosition;
            Camera.main.transform.position = camPositionOnNewGame;
        }
        else if(Camera.main.transform.position.x == buttonAction.nextScreen.position.x &&
            Camera.main.transform.position.y == buttonAction.nextScreen.position.y) {
            buttonAction.thisScreen.position = defaultPosition;
        }
        else if(Camera.main.transform.position.x == gameOverScreen.position.x &&
            Camera.main.transform.position.y == gameOverScreen.position.y) {
            // Move game screen next to game over screen
            buttonAction.thisScreen.position = tempPositionOnGameOver;
        }
        else if(Camera.main.transform.position.x == tempPositionOnGameOver.x &&
            Camera.main.transform.position.y == tempPositionOnGameOver.y) {
            buttonAction.thisScreen.position = defaultPosition;
            Camera.main.transform.position = camPositionOnNewGame;
        }
    }
}
