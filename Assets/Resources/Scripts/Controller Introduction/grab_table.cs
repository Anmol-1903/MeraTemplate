using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grab_table : MonoBehaviour
{
    // public Transform fromParent;
    // public Transform toParent;
    // public GameObject uiElement;

    // private int childrenTransferred = 0;

    // void Start()
    // {
    //     // Disable the UI element initially
    //     if (uiElement != null)
    //     {
    //         uiElement.SetActive(false);
    //     }
    // }

    // // Call this method whenever an object is grabbed
    // public void OnObjectGrabbed(GameObject grabbedObject)
    // {
    //     // Transfer the grabbed object to the new parent
    //     grabbedObject.transform.SetParent(toParent);
    //     childrenTransferred++;

    //     // Check if all children have been transferred
    //     if (childrenTransferred == fromParent.childCount)
    //     {
    //         ActivateUI();
    //     }
    // }

    // void ActivateUI()
    // {
    //     // Activate the UI element when all children are transferred
    //     if (uiElement != null)
    //     {
    //         uiElement.SetActive(true);
    //     }
    // }
    
    public GameObject uiPopup; // Reference to your UI pop-up

    private void Start()
    {
        UpdateUI();
    }
    public void OnChildDetached()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (transform.childCount == 0)
        {
            // Activate the UI pop-up
            uiPopup.SetActive(true);
        }
        else
        {
            // Deactivate the UI pop-up
            uiPopup.SetActive(false);
        }
    }
}


