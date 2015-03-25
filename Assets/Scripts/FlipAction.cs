using UnityEngine;
using System.Collections;

// FlipAction
// Flip the object to side A or B
// Change sprite if specfied
public class FlipAction : MonoBehaviour {
    public static Vector3 sideA = new Vector3();
    public static Vector3 sideB = new Vector3(0, 180f, 0);

    public Sprite sideASprite;
    public Sprite sideBSprite;
    public float  toSideADuration, toSideBDuration;

    public int Clicks { set; get; }

    private float          timer;          // Time since mouse event happened
    private Vector3        origRotation;
    private Vector3        nextRotation;
    private bool           toSideA;        // True if rotating to side A, false if to B.
    private SpriteRenderer spriteRenderer;
    private float          duration;

    public bool IsOnSideA {
        get {
            return Mathf.Floor(transform.eulerAngles.y) == sideA.y;
        }
    }
    public bool IsOnSideB {
        get {
            return Mathf.Floor(transform.eulerAngles.y) == sideB.y;
        }
    }

    void Start() {
        origRotation = transform.eulerAngles;
        nextRotation = origRotation;
        toSideA = true;             // Set to true so first click wil rotate to side B
        spriteRenderer = renderer as SpriteRenderer;
    }

    void Update() {
        timer += Time.deltaTime;
        //transform.eulerAngles = Vector3.Lerp(origRotation, nextRotation, timer / duration);
        transform.eulerAngles = SineLerp(origRotation, nextRotation, timer / duration);

        if(toSideA && transform.eulerAngles.y <= 90) {
            if(sideASprite != null)
                spriteRenderer.sprite = sideASprite;
        }
        else if(!toSideA && transform.eulerAngles.y >= 90) {
            if(sideBSprite != null)
                spriteRenderer.sprite = sideBSprite;
        }

    }

    public void FlipToA() {
        toSideA = true;
        nextRotation = sideA;
        origRotation = transform.eulerAngles;
        duration = toSideADuration;
        timer = 0;
    }

    public void FlipToB() {
        toSideA = false;
        nextRotation = sideB;
        origRotation = transform.eulerAngles;
        duration = toSideBDuration;
        timer = 0;
    }

    public void Flip() {
        toSideA = !toSideA;

        if(toSideA)
            FlipToA();
        else
            FlipToB();
    }

    //void OnMouseUpAsButton() {
    //    Flip();
    //}

    private Vector3 SineLerp(Vector3 startPos, Vector3 targetPos, float fraction) {
        float angle = Mathf.Lerp(0f, 90f, fraction) * Mathf.Deg2Rad;    // Mathf.Sin() takes radians
        float sineFraction = Mathf.Sin(angle);
        //print(string.Format("Angle: {0}, Sine: {1}", angle, sineFraction));
        return Vector3.Lerp(startPos, targetPos, sineFraction);
    }
}
