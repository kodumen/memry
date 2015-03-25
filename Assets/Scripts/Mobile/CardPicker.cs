using UnityEngine;
using System.Collections;

public class CardPicker : MonoBehaviour {
    public Sprite spriteIdle, spritePressed;        // SideA sprite of crds when idle or pressed
    public bool useTouch;

    public FlipAction Card1 { get { return card1; } }
    public FlipAction Card2 { get { return card2; } }

    private FlipAction card1, card2, tempCard;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(card1 != null && card2 != null)
            return;

        #region Detect mouse/touch events
        if(useTouch) {
            if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
                tempCard = GetCard();
                if(tempCard != null)
                    tempCard.sideASprite = spritePressed;
            }
            else if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {
                FlipAction card = GetCard();
                if(card != null) {
                    if(tempCard == card)
                        AddCard(card);
                }

                if(tempCard != null)
                    tempCard.sideASprite = spriteIdle;
            }
        }
        else {
            if(Input.GetMouseButtonDown(0)) {
                tempCard = GetCard();
                if(tempCard != null)
                    tempCard.sideASprite = spritePressed;
            }
            else if(Input.GetMouseButtonUp(0)) {
                FlipAction card = GetCard();
                if(card != null) {
                    if(tempCard == card)
                        AddCard(card);
                }

                if(tempCard != null)
                    tempCard.sideASprite = spriteIdle;
            }
        }
        #endregion
    }

    private FlipAction GetCard() {
        Vector2 inputPos = useTouch ? Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) :
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(inputPos);
        //print(hit);
        if(hit != null)
            return hit.gameObject.GetComponent("FlipAction") as FlipAction;
        else
            return null;
    }

    private void AddCard(FlipAction card) {
        if(card != null && card.tag == "card") {
            card.FlipToB();
            if(card1 == null) {
                card1 = card;
                card1.Clicks++;
            }
            else if(card1 != card && card2 == null) {
                card2 = card;
                card2.Clicks++;
            }
        }
        //print(card1);
        //print(card2);
    }

    public void Clear() {
        card1 = null;
        card2 = null;
    }
}
