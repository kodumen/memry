using UnityEngine;
using System.Collections;

// Card Comparator
// Compare if cards clicked are of equal symbols.
// Keep score and remaining moves.
public class CardComparator : MonoBehaviour {
    public float flipToADelay;
    public int   reward1, reward2, reward3;             // Additional moves with reward1 as the highest reward
    public int   clickSum_lucky, clickSum_normal;

    public int Score { set; get; }
    public int Moves { set; get; }

    private FlipAction   card1, card2;
    //private ArrayList    flipBackCards;
    //private ArrayList    flipBackTimer;                     // A timer for each card before they flip back to side A
    private CardPicker   cardPicker;
    private float        flipToATimer;
    private bool         cardsDoNotMatch;

    void Start() {
        card1 = null;
        card2 = null;
        //flipBackCards = new ArrayList();
        //flipBackTimer = new ArrayList();
        Score = 0;
        cardPicker = gameObject.GetComponent("CardPicker") as CardPicker;
        flipToATimer = 0;
        cardsDoNotMatch = false;
    }

    void Update() {
        card1 = cardPicker.Card1;
        card2 = cardPicker.Card2;
        CompareCards();

        if(cardsDoNotMatch) {
            flipToATimer += Time.deltaTime;
            if(flipToATimer >= flipToADelay) {
                card1.FlipToA();
                card2.FlipToA();
            }
            if(card1.IsOnSideA && card2.IsOnSideA) {
                cardPicker.Clear();
                card1 = null;
                card2 = null;
                flipToATimer = 0;
                cardsDoNotMatch = false;
            }
        }

        // Delay cards' flip action
        //for(int i = 0; i < flipBackTimer.Count; i++) {
        //    float timer = (float)flipBackTimer[i];
        //    timer += Time.deltaTime;

        //    if(timer >= flipBackDelay) {
        //        FlipAction card = flipBackCards[i] as FlipAction;
        //        card.FlipToA();
        //        //(card.gameObject.GetComponent("Collider2D") as Collider2D).enabled = true;

        //        flipBackCards.RemoveAt(i);
        //        flipBackTimer.RemoveAt(i);
        //        //continue;
        //    }
        //    else
        //        flipBackTimer[i] = timer;
        //}

        //#region Flip to A delay timer
        //ArrayList forRemoval = new ArrayList();     // Cards to be removed in flipBackCards
        //foreach(FlipAction card in flipBackCards) {
        //    int index = flipBackCards.IndexOf(card);
        //    float timer = (float)flipBackTimer[index];
        //    timer += Time.deltaTime;
        //    flipBackTimer[index] = timer;

        //    if(timer >= flipBackDelay) {
        //        card.FlipToA();
        //        forRemoval.Add(index);
        //    }
        //}
        //// Remove
        //for(int i = forRemoval.Count - 1; i >= 0; i--) {
        //    flipBackCards.RemoveAt(i);
        //    flipBackTimer.RemoveAt(i);
        //}
        //#endregion

    }

    /// <summary>
    /// Compare the cards in card1 and card2.
    /// Add score if equal and add moves based on total clicks in the cards.
    /// Prepare variables, card1 and card2, for the new cards clicked.
    /// If not equal, flip back to sideA.
    /// </summary>
    private void CompareCards() {
        if(card1 != null && card2 != null && !cardsDoNotMatch) {
            // Wait until the cards are flipped to sideB
            // If we don't, the moment you click the second card, both will be compared
            // and we might not get a chance to see what's behind the second card
            if(card1.IsOnSideB && card2.IsOnSideB) {
                if(card1.sideBSprite == card2.sideBSprite) {
                    Score++;
                    Moves += GetReward(card1.Clicks, card2.Clicks);
                    //print("Score: " + Score);
                    cardPicker.Clear();
                    card1 = null;
                    card2 = null;
                    flipToATimer = 0;
                    cardsDoNotMatch = false;
                }
                else {
                    Moves--;
                    //flipBackCards.Add(card1);
                    //flipBackTimer.Add(0f);
                    //flipBackCards.Add(card2);
                    //flipBackTimer.Add(0f);
                    cardsDoNotMatch = true;
                }

                //cardPicker.Clear();
                //card1 = null;
                //card2 = null;
                //card1Clicks = null;
                //card2Clicks = null;
            }
        }
    }

    private int GetReward(int c1Clicks, int c2Clicks) {
        int clickSum = c1Clicks + c2Clicks;
        int reward;

        if(clickSum == clickSum_lucky)
            reward = reward1;
        else if(clickSum > clickSum_lucky && clickSum < clickSum_normal)
            reward = reward2;
        else
            reward = reward3;

        //print(string.Format("card1: {0}, card2: {1}, Sum: {2}\nReward: {3}", c1Clicks, c2Clicks, clickSum, reward));
        return reward;
    }
}
