using UnityEngine;
using System.Collections;

// Stores the number of times a card was clicked
public class ClickCounter : MonoBehaviour {
    // Clicks = 1 adds one more click to the card
    // Use reset() to reset to 0.
    public int Clicks {
        set { _clicks += 1; }
        get { return _clicks; }
    }

    private int _clicks;

    void Reset() {
        _clicks = 0;
    }
}
