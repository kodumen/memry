using UnityEngine;
using System.Collections;

public class CardGenerator : MonoBehaviour {
    public int gridWidth, gridHeight;   // Width and height are only limited to even numbers
    public float padding;               // IN PIXELS!!!
    //public int NoOfCardsToGen;        // Number of cards you want to generate divded by 2
    public GameObject cardObj;          // ^ Note that it can't be more than the size of cardFaces.
    public Sprite[] cardFaces;          // ^ Even if you do put a higher number in the inspector, it would be ignored.

    /// <summary>
    /// Fill Grid. Instantiates a set of cards positioned based on the set dimensions
    /// in this instance of CardGenerator. Returns a list of the children.
    /// </summary>
    /// <param name="cardsToGenerate">The number of unique cards to be generated.
    /// Total number of cards will be twice this parameter.</param>
    /// <returns></returns>
    public ArrayList FillGrid(int cardsToGenerate) {
        ArrayList children  = new ArrayList();
        ArrayList freeCells = new ArrayList();
        float x = transform.position.x;
        float y = transform.position.y;

        // Fill freeCells with positions for the cards
        for(int w = 0; w < gridWidth; w++) {
            for(int h = 0; h < gridHeight; h++) {
                float cardWidth, cardHeight;
                cardWidth = cardFaces[0].bounds.size.x;
                cardHeight = cardFaces[0].bounds.size.y;
                freeCells.Add(new float[] { x + (w * (cardWidth + (padding))), y + (h * (cardHeight + (padding))) });
            }
        }

        // Generate two of each card faces
        for(int c = 0; c < cardsToGenerate; c++) {
            if(c >= cardFaces.Length)
                break;

            float[] card1Pos = PickCell(freeCells);
            float[] card2Pos = PickCell(freeCells);
            Vector3 card1Vec = new Vector3(card1Pos[0], card1Pos[1], transform.position.z);
            Vector3 card2Vec = new Vector3(card2Pos[0], card2Pos[1], transform.position.z);
            GameObject card1 = (GameObject)Instantiate(cardObj, card1Vec, cardObj.transform.rotation);
            GameObject card2 = (GameObject)Instantiate(cardObj, card2Vec, cardObj.transform.rotation);
            //FlipAction card1Flip = (FlipAction)card1.GetComponent("FlipAction");
            //FlipAction card2Flip = (FlipAction)card2.GetComponent("FlipAction");
            //card1Flip.sideBSprite = cardFaces[c];
            //card2Flip.sideBSprite = cardFaces[c];
            card1.transform.parent = this.gameObject.transform;
            card2.transform.parent = this.gameObject.transform;
            children.Add(card1);
            children.Add(card2);
        }

        return children;
    }

    private float[] PickCell(ArrayList freeCells) {
        int index = (int)(Random.value * freeCells.Count);
        float[] cell = (float[])freeCells[index];
        freeCells.RemoveAt(index);
        return cell;
    }
}
