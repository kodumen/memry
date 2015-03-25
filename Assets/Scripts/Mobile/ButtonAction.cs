using UnityEngine;
using System.Collections;

/// <summary>
/// ButtonAction
/// Change button sprite on click, do stuff,
/// disable other buttons.
/// Buttons with this script pretty much just changes screens.
/// </summary>
public class ButtonAction : MonoBehaviour {
    public bool      useTouch;
    public Sprite    spriteIdle, spritePressed;
    public Transform thisScreen, nextScreen;
    public bool      startNewGame;
    public bool      enableLerp;
    public float     lerpDuration;
    public int       newGameCardPair, newGameMoves;

    public bool IsClicked {
        get { return isClicked; }
    }

    private float          timer;
    private bool           lerp;
    private Vector3        targetPos, startPos;
    private bool           isPressed;
    private CardMaker      cardMaker;
    private CardComparator cardComparator;
    private bool           isClicked;

    void Start() {
        timer = 0;
        lerp = false;
        isPressed = false;
        startPos = thisScreen.position;
        startPos.z = Camera.main.transform.position.z;
        targetPos = nextScreen ? nextScreen.position : startPos;
        targetPos.z = Camera.main.transform.position.z;
        cardMaker = startNewGame ?
            GameObject.FindGameObjectWithTag("card grid").GetComponent("CardMaker") as CardMaker : null;
        cardComparator = startNewGame ?
            GameObject.FindGameObjectWithTag("card grid").GetComponent("CardComparator") as CardComparator : null;
    }

    void Update() {
        #region Detect mouse/touch events
        if(useTouch) { // Touch events!!!
            if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                OnButtonDown();
            else if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
                OnButtonClick();
        }
        else {  // Mouse events!!!
            if(Input.GetMouseButtonDown(0)) {
                OnButtonDown();
                //print("DOWN!");
            }
            else if(Input.GetMouseButtonUp(0)) {
                OnButtonClick();
                //print("UP!");
            }
        }
        #endregion

        #region Move to targetPosition
        if(lerp && enableLerp) {
            timer += Time.deltaTime;
            Camera.main.transform.position = SineLerp(startPos, targetPos, timer / lerpDuration);
            if(Camera.main.transform.position == targetPos) {
                lerp = false;
                isPressed = false;
                isClicked = false;
                if(nextScreen != null) {
                    EnableButtons(nextScreen, true);
                }
            }
        }
        else if(lerp && !enableLerp) {
            EnableButtons(thisScreen, false);
            Camera.main.transform.position = new 
                Vector3(nextScreen.position.x, nextScreen.position.y, Camera.main.transform.position.z);
            lerp = false;
            isPressed = false;
            isClicked = false;
            if(nextScreen != null) {
                EnableButtons(nextScreen, true);
            }
        }
        #endregion
    }

    /// <summary>
    /// Ignore the ugly name. What it does is return true when it is touched or pressed/clicked.
    /// </summary>
    /// <returns></returns>
    private bool IsOnInput() {
        Vector2 inputPos = useTouch ? Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) :
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(inputPos);
        //print(hit);
        if(hit != null)
            return hit.gameObject == gameObject;
        else
            return false;
    }

    private void OnButtonDown() {
        if(IsOnInput()) {
            isPressed = true;
            (renderer as SpriteRenderer).sprite = spritePressed;
        }
    }

    private void OnButtonClick() {
        if(IsOnInput() && isPressed) {
            lerp = true;
            timer = 0;
            isClicked = true;
            EnableButtons(thisScreen, false);
            if(startNewGame) { 
                cardMaker.TotalCardCount = 0;
                cardMaker.RefillGrid(newGameCardPair);
                cardComparator.Score = 0;
                cardComparator.Moves = newGameMoves;
            }
        }
        else {
            isPressed = false;
        }
        (renderer as SpriteRenderer).sprite = spriteIdle;
    }

    /// <summary>
    /// Enable or disable the colliders of every object in
    /// a group of buttons. This means that we can disable buttons
    /// in a group all at once! AMAZING!
    /// </summary>
    /// <param name="buttonGroup">Button or group of buttons</param>
    /// <param name="enable">Enable or disable the collider</param>
    private void EnableButtons(Transform buttonGroup, bool enable) {
        foreach(Transform button in buttonGroup)
            EnableButtons(button, enable);
        if(buttonGroup.collider2D != null)
            buttonGroup.collider2D.enabled = enable;
    }

    /// <summary>
    /// Change the vector towards a specified target vector
    /// by sine or something. Put simply, this should create a smoothdamp effect
    /// when moving objects. Yeah!
    /// </summary>
    /// <param name="startPos">The starting position</param>
    /// <param name="targetPos">The position to reach</param>
    /// <param name="fraction">The fraction between 0 and 90 degrees</param>
    /// <returns></returns>
    private Vector3 SineLerp(Vector3 startPos, Vector3 targetPos, float fraction) {
        float angle = Mathf.Lerp(0f, 90f, fraction) * Mathf.Deg2Rad;    // Mathf.Sin() takes radians
        float sineFraction = Mathf.Sin(angle);
        //print(string.Format("Angle: {0}, Sine: {1}", angle, sineFraction));
        return Vector3.Lerp(startPos, targetPos, sineFraction);
    }
}
