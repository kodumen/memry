using UnityEngine;
using System.Collections;

/// <summary>
/// Card Maker
/// Ugly name.
/// Generate a 5x5 card grid.
/// </summary>
public class CardMaker : MonoBehaviour {
    public FlipAction cardSample;
    public float      padding;
    public Sprite[]   cardFaces;

    public int TotalCardCount { set; get; }      // Total cards generated since the first time NewGame() is called

    private ArrayList cells;               // Spaces where you can put cards.

    // Use this for initialization
    void Start() {
        //print((cardSample.renderer as SpriteRenderer).sprite.bounds.size.x);
        //print((cardSample.renderer as SpriteRenderer).sprite.bounds.size.y);
        TotalCardCount = 0;
    }

    // Update is called once per frame
    void Update() {
        //if(Input.GetMouseButtonUp(0)) {
        //    cells = GenerateSpaces();
        //    FillGrid(12);
        //}
    }

    public void RefillGrid(int pairOfCards) {
        foreach(Transform card in gameObject.transform)
            Destroy(card.gameObject);
        cells = GenerateSpaces();
        FillGrid(pairOfCards);
    }

    private void FillGrid(int pairOfCards) {
        for(int i = 0; i < pairOfCards; i++) {
            if(transform.childCount / 2 > cardFaces.Length)
                break;
            for(int c = 0; c < 2; c++) {
                FlipAction card = Instantiate(cardSample) as FlipAction;
                //print(card);
                card.transform.parent = transform;
                card.transform.localPosition = GetFreeCell();
                card.sideBSprite = cardFaces[i];
                TotalCardCount++;
            }
        }
    }

    private ArrayList GenerateSpaces() {
        ArrayList spc = new ArrayList();
        float width = (cardSample.renderer as SpriteRenderer).sprite.bounds.size.x;
        float height = (cardSample.renderer as SpriteRenderer).sprite.bounds.size.y;
        float yPos = height / 2;
        for(int y = 1; y <= 5; y++) {
            float xPos = width / 2;
            for(int x = 1; x <= 5; x++) {
                spc.Add(new Vector2(xPos, yPos));
                //print(spc[spc.Count - 1]);
                xPos = x * (width + padding / 100) + (width / 2);
            }
            yPos = y * (height + padding / 100) + (height / 2);
        }
        return spc;
    }

    private Vector2 GetFreeCell() {
        int index = (int)(Random.value * cells.Count);
        Vector2 cell = (Vector2)cells[index];
        cells.RemoveAt(index);
        return cell;
    }
}
