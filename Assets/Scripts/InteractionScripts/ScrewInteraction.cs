
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
namespace BlackOut{

public class ScrewInteraction : Interaction
{

    public XRGrabInteractable ScrewGrab;
    public XRKnob ScrewKnob;
    public XRSocketInteractor ScrewSocket;
    public XRSocketInteractor ScrewPlaceSocket;





    public override bool isInteractionCompleted { get ; set ; }

    public override void InteractionCompleted()
    {
       
    }

    public override void StartInteraction()
    {
        
    }


    private void Start() {
        ScrewPlaceSocket.enabled = false;
        ScrewKnob.enabled = false;
        ScrewSocket.enabled = false;
    }
}
}