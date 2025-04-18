using UnityEngine;
using UnityEngine.XR.Content.Interaction;
namespace BlackOut
{

    public class LeverClose : Interaction
    {
        [SerializeField] bool _isAssessment;
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] XRLever _lever;
        [SerializeField] GameObject _valveHighlight;
        [SerializeField] GameObject _valveHandleHighlight;
        [SerializeField] Vector3 _startRot;
        [SerializeField] Vector3 _endRot;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            _lever.enabled = false;
        }

        public override void StartInteraction()
        {
            _lever.enabled = true;
            _lever.value = false;
            if (!_isAssessment)
            {
                _valveHighlight.SetActive(true);

                HightingAnim.instance.HighlightedAnim(_valveHandleHighlight, Quaternion.Euler(_startRot), Quaternion.Euler(_endRot), 1.5f);

            }
            StepUIManager.Instance?.ToggleConfirmButton(false);

            _lever.onLeverActivate.AddListener(CheckIfValveClosed);
        }

        public void CheckIfValveClosed()
        {
            NextStep();
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
            
            if (automaticNextInteraction)
            {
                StepManager.instance?.MoveToNextInteraction();
            }
        }

        public void NextStep()
        {
            HightingAnim.instance.StopAnimation();
            _valveHighlight.SetActive(false);
            isInteractionCompleted = true;
            _lever.enabled = false;
            _lever.onLeverActivate.RemoveListener(CheckIfValveClosed);
        }
    }
}