using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {
    public Transform thisScreen, nextScreen;
    public float lerpDuration,gameOverLerpDelay;

    private CardComparator cardComparator;
    private Vector3 startPos, nextPos;
    private bool startLerp;
    private float lerpTimer, gameOverLerpTimer;

    void Start() {
        cardComparator = gameObject.GetComponent("CardComparator") as CardComparator;
        startPos = thisScreen.position;
        startPos.z = Camera.main.transform.position.z;
        nextPos = nextScreen.position;
        nextPos.z = Camera.main.transform.position.z;
        lerpTimer = 0;
        gameOverLerpTimer = 0;
        startLerp = false;
    }

    // Update is called once per frame
    void Update() {
        if(Camera.main.transform.position.x == startPos.x &&
            Camera.main.transform.position.y == startPos.y) {
            gameOverLerpTimer += cardComparator.Moves == 0 ? Time.deltaTime : 0;
            startLerp = gameOverLerpTimer >= gameOverLerpDelay;
            if(startLerp)
                EnableButtons(thisScreen, false);
            //print(startLerp);
        }

        if(startLerp) {
            lerpTimer += Time.deltaTime;
            Camera.main.transform.position = SineLerp(startPos, nextPos, lerpTimer / lerpDuration);
            startLerp = !(Camera.main.transform.position == nextPos);
            gameOverLerpTimer = 0;
            if(!startLerp)
                EnableButtons(nextScreen, true);
        }
        else {
            lerpTimer = 0;
        }
    }

    private Vector3 SineLerp(Vector3 startPos, Vector3 targetPos, float fraction) {
        float angle = Mathf.Lerp(0f, 90f, fraction) * Mathf.Deg2Rad;    // Mathf.Sin() takes radians
        float sineFraction = Mathf.Sin(angle);
        //print(string.Format("Angle: {0}, Sine: {1}", angle, sineFraction));
        return Vector3.Lerp(startPos, targetPos, sineFraction);
    }

    private void EnableButtons(Transform buttonGroup, bool enable) {
        foreach(Transform button in buttonGroup)
            EnableButtons(button, enable);
        if(buttonGroup.collider2D != null)
            buttonGroup.collider2D.enabled = enable;
    }
}
