using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScale : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> panels; // List of panel GameObjects

    // Function to activate all panels
    public void ActivateAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            if(panel.activeSelf)
            {
            panel.transform.localScale = Vector3.one; // Set scale to (1, 1, 1)
            }
        }
    }
}
