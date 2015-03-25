using UnityEngine;
using System.Collections;

public class LevelControl : MonoBehaviour {
    //public bool IsGameStart { get { return isGameStart; } }

    private CardMaker      cardMaker;
    private CardComparator cardComparator;
    //private bool isGameStart;

    void Start() {
        cardMaker = gameObject.GetComponent("CardMaker") as CardMaker;
        cardComparator = gameObject.GetComponent("CardComparator") as CardComparator;
        //isGameStart = false;
    }

    void Update() {
        //isGameStart = 

        if(cardMaker.TotalCardCount / 2 == cardComparator.Score && cardMaker.TotalCardCount != 0) {
            //print(cardMaker.totalCardCount / 2);
            cardMaker.RefillGrid(cardComparator.Score + 1);
        }
    }
}
