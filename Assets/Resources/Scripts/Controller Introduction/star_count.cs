using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star_count : MonoBehaviour
{
    public GameObject starHighlight, okButton;
    public int requiredStar = 1;
    private int collectedStars = 0;
    public void increment()
    {
        if (collectedStars< requiredStar)
        {
            collectedStars++;
            okButton.SetActive(true);
            starHighlight.SetActive(false);
        }
        
    }
}
