using UnityEngine;
namespace BlackOut
{
    public class ForceLookAt : Interaction
    {
        bool automaticNextInteraction = false;

        [Header("Highlighting Sphere Properties")]
        public GameObject highlightPosition;
        public Vector3 startSize;
        public Vector3 endSize;
        public float speed;
        public HighlightingType highlightingType;
        public HighlightingShape highlightingShape;

        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            HighlightingMesh.Instance?.StopHighlighting();
            gameObject.SetActive(false);
        }

        public override void StartInteraction()
        {
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);

            HighlightingMesh.Instance?.HighlightObject(highlightPosition.transform,startSize, endSize, speed, highlightingType, highlightingShape);

            Invoke(nameof(SetBool), 1f);
        }

        void SetBool(){
            isInteractionCompleted = true;    
        }
    }
}