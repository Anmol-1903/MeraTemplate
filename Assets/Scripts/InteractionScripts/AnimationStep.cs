using UnityEngine;

namespace BlackOut
{
    public abstract class AnimationStep : Interaction
    {
        public Animator animator;

        public bool automaticNextInteraction = false;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
        }

        public override void StartInteraction()
        {
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
        }
        public abstract void AnimationEvent();
    }
}
