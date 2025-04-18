using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SphereHologram {
    right,
    left
}
public class ControllerCollistion : MonoBehaviour
{
    // private int ControllerNumber = 1;
    public ControllerOrientation controllerOrientation;
    public SphereHologram hologram;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Colliding with " + other.gameObject.name);
        if (hologram == SphereHologram.right && other.name == "Right Controller") //&& ControllerNumber == 1)
        {
            Debug.Log("right triggerd");
            // other.GetComponent<BoxCollider>().enabled = false;
            // ControllerNumber = 2;
            // StartCoroutine(controllerOrientation.RightControllerDetected());
            controllerOrientation.Right();
        }
        else if (hologram == SphereHologram.left && other.name == "Left Controller") //&& ControllerNumber == 2)
        {
            // disable hologram left
            // left controller detected
            other.GetComponent<BoxCollider>().enabled = false;
            // StartCoroutine(controllerOrientation.LeftControllerDetected());
            controllerOrientation.Left();
            // gameObject.SetActive(false);
        }
    }
    
}
