using UnityEngine;
using System.Collections;

public class MovesDisplay : MonoBehaviour {
    public CardComparator cardComparator;
    public string defaultColor, warningColor;
    public int warningMovesCount;

    private TextMesh textMesh;
    private string   label;

    // Use this for initialization
    void Start() {
        textMesh = gameObject.GetComponent("TextMesh") as TextMesh;
        label = "<color={1}><size=80>{0}</size>\nmove</color>";
    }

    // Update is called once per frame
    void Update() {
        if(cardComparator.Moves > 1) {
            if(cardComparator.Moves <= warningMovesCount)
                textMesh.text = string.Format(label + "<color={1}>s</color>", cardComparator.Moves, warningColor);
            else
                textMesh.text = string.Format(label + "<color={1}>s</color>", cardComparator.Moves, defaultColor);
        }
        else {
            textMesh.text = string.Format(label, cardComparator.Moves, warningColor);
        }
    }
}
