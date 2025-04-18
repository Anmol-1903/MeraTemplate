using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
namespace BlackOut
{
    public class GrabInteractor : Interaction
    {
        [SerializeField] XRSimpleInteractable Target;
        [SerializeField] GameObject highlight;
        [SerializeField] bool Hold;
        [SerializeField] Transform startPos;
        [SerializeField] Transform endPos;
        [SerializeField] AnimationCurve animationCurve;
        [SerializeField] float animationTime = 1f; // total time for the animation
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] bool _isAssessment;
        bool grabbed;
        private float elapsedTime = 0;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            Target.transform.SetPositionAndRotation(endPos.position, endPos.rotation);

            highlight.SetActive(false);
            PathDirector.instance.ClearArrows();
        }

        private void NotGrabbed(SelectExitEventArgs arg0)
        {
            grabbed = false;

            if (!_isAssessment && !isInteractionCompleted)
            {
                highlight.SetActive(true);
            }
        }

        private void Grabbed(SelectEnterEventArgs arg0)
        {
            grabbed = true;
            highlight.SetActive(false);
        }

        void Update()
        {
            if (grabbed && !isInteractionCompleted)
            {
                elapsedTime += Time.deltaTime;
                float curveValue = animationCurve.Evaluate(elapsedTime / animationTime);

                Target.transform.SetPositionAndRotation(
                    Vector3.Lerp(Target.transform.position, endPos.position, curveValue),
                    Quaternion.Lerp(Target.transform.rotation, endPos.rotation, curveValue)
                );

                if (elapsedTime >= animationTime)
                {
                    isInteractionCompleted = true;
                    StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
                    if (automaticNextInteraction)
                    {
                        StepManager.instance?.MoveToNextInteraction();
                    }
                }
            }
        }

        public override void StartInteraction()
        {
            if (!_isAssessment && !isInteractionCompleted)
            {
                highlight.SetActive(true);
            }

            StepUIManager.Instance?.ToggleConfirmButton(false);
            Target.transform.SetPositionAndRotation(startPos.position, startPos.rotation);
            Target.selectEntered.AddListener(Grabbed);
            if (Hold)
            {
                Target.selectExited.AddListener(NotGrabbed);
            }
        }
    }
}