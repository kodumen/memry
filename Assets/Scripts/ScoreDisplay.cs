using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
    public CardComparator cardComparator;
    public string   label;
    public bool appendS;

    private TextMesh textMesh;

    // Use this for initialization
    void Start() {
        textMesh = gameObject.GetComponent("TextMesh") as TextMesh;
        //label = "<size=80>{0}</size>\npoint";
    }

    // Update is called once per frame
    void Update() {
        if(cardComparator.Score > 1 && appendS) {
            textMesh.text = string.Format(label + "s", cardComparator.Score);
        }
        else {
            textMesh.text = string.Format(label, cardComparator.Score);
        }
    }
}