using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace BlackOut
{
    public class InteractableToggleState : Interaction
    {
        public XRBaseInteractable[] targetInteractable;
        public InteractionLayerMask addLayerMask;
        public InteractionLayerMask removeLayerMask;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
        }
        void Interacted()
        {
            isInteractionCompleted = true;
            StepUIManager.Instance?.UpdateStep();
            StepManager.instance?.MoveToNextInteraction();
        }
        public override void StartInteraction()
        {
            foreach(var interactable in targetInteractable){
                interactable.interactionLayers |= addLayerMask;
                interactable.interactionLayers &= ~removeLayerMask;
            }
            Interacted();
        }
    }
}
