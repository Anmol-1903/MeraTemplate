using UnityEngine;

namespace BlackOut
{
    public class ToggleObjectState : Interaction
    {
        public GameObject[] ObjectsToEnable;
        public GameObject[] ObjectsToDisable;
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
            foreach (var obj in ObjectsToEnable)
            {
                obj.SetActive(true);
            }
            foreach (var obj in ObjectsToDisable)
            {
                obj.SetActive(false);
            }
            Interacted();
        }
    }
}
