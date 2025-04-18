using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Highlight : MonoBehaviour
{
    public GameObject HighlightedMesh;
    private int hoverCount = 0;

    void Start()
    {
        XRSimpleInteractable xR = GetComponent<XRSimpleInteractable>();
        xR.hoverEntered.AddListener(OnHoverEntered);
        xR.hoverExited.AddListener(OnHoverExited);

        HighlightedMesh.SetActive(false);
    }

    private void OnHoverExited(HoverExitEventArgs arg0)
    {
        hoverCount--;
        if (hoverCount == 0)
        {
            HighlightedMesh.SetActive(false);
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs arg0)
    {
        hoverCount++;
        if (hoverCount > 0)
        {
            HighlightedMesh.SetActive(true);
        }
    }
}