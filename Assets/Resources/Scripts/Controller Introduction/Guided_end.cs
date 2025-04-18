using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guided_end : MonoBehaviour
{
    public GameObject uiElement; // Assign your UI element in the inspector

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Left Controller") || other.CompareTag("Right Controller"))
        {
            print("inside");
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }
            // Disable the target object
            gameObject.SetActive(false);

            // Activate the UI element
            
        }
    }
}

