using UnityEngine;
using System.Collections;

public class DisableMeshRenderer : MonoBehaviour {
    public MeshRenderer meshRenderer;
    public bool         on90Plus, on90Minus;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if(transform.parent.transform.localEulerAngles.y <= 90f)
            meshRenderer.enabled = on90Minus;
        else if(transform.parent.localEulerAngles.y >= 90f)
            meshRenderer.enabled = on90Plus;
    }
}
