using UnityEngine;
using System.Collections;

/// <summary>
/// MainMenuAction
/// Changes the sprite of a button when pressed,
/// does the appropriate action based on tag
/// </summary>
public class ButtonSystem : MonoBehaviour {
    public bool       useTouch;
    public Sprite     button_idle, button_pressed;
    public GameObject mainScreen, mainScreenButtons;
    public GameObject gameScreen, gameScreenButtons;
    public float      panDuration;           // Time it takes for the camera to pan the screen

    private GameObject objOnDown;                   // Object under the mouse pointer/finger when input is down
    private Vector3    camDestination;
    private float      timer;

    void Start() {
        camDestination = Camera.main.transform.position;
    }

    void Update() {
        if(!useTouch) {
            if(Input.GetMouseButtonDown(0))
                objOnDown = GetButton(button_pressed);
            if(Input.GetMouseButtonUp(0)) {
                DoOnMouseUp(objOnDown, GetButton(), "new game", "help", "continue");
                if(objOnDown != null)
                    (objOnDown.renderer as SpriteRenderer).sprite = button_idle;
            }
        }

        // Pan the camera to the new screen
        timer += Time.deltaTime;
        Camera cam = Camera.main;
        cam.transform.position = Vector3.Lerp(cam.transform.position, camDestination, timer / panDuration);
    }

    void DisableButtons(Transform transformObj) {
        foreach(Transform child in transformObj) {
            DisableButtons(child);
        }
        if(transformObj.gameObject.collider2D != null)
            transformObj.gameObject.collider2D.enabled = false;
    }

    void EnableButtons(Transform transformObj) {
        foreach(Transform child in transformObj) {
            EnableButtons(child);
        }
        if(transformObj.gameObject.collider2D != null)
            transformObj.gameObject.collider2D.enabled = false;
    }

    /// <summary>
    /// Return the GameObject under the mouse pointer.
    /// Change its sprite
    /// </summary>
    /// <param name="buttonSprite"></param>
    /// <returns></returns>
    private GameObject GetButton(Sprite buttonSprite) {
        Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(hit != null) {
            (hit.renderer as SpriteRenderer).sprite = buttonSprite;
            return hit.gameObject;
        }
        else
            return null;
    }

    private GameObject GetButton() {
        Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(hit != null)
            return hit.gameObject;
        else
            return null;
    }

    private void DoOnMouseUp(GameObject buttonOnDown, GameObject buttonOnUp, string newGameTag, string helpTag, string resumeTag) {
        if(buttonOnUp == null)
            return;
        if(buttonOnDown == buttonOnUp) {
            if(buttonOnUp.tag == newGameTag) {
                DisableButtons(mainScreenButtons.transform);
                // Fill grid
                camDestination.x = gameScreen.transform.position.x + (Camera.main.pixelWidth / 100) / 2;    // 100 because that's the pixel to world units
                camDestination.y = gameScreen.transform.position.y + (Camera.main.pixelHeight / 100) / 2;
                timer = 0;
                // Enable buttons in game screen
                if(gameScreenButtons != null)
                    EnableButtons(gameScreenButtons.transform);
            }
            else if(buttonOnUp.tag == helpTag) {

            }
            else if(buttonOnUp.tag == resumeTag) {

            }
        }
    }
}
;