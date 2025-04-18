using UnityEngine;
using UnityEngine.XR.Content.Interaction;
namespace BlackOut
{

    public class ValveClose : Interaction
    {
        [SerializeField] bool _isAssessment;
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] XRKnob _knob;
        [SerializeField] GameObject _valveHighlight;
        [SerializeField] GameObject _valveHandleHighlight;
        [SerializeField] Vector3 _startRot;
        [SerializeField] Vector3 _endRot;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            HightingAnim.instance.StopAnimating();
            _knob.enabled = false;
        }

        public override void StartInteraction()
        {
            _knob.enabled = true;
            _knob.value = 0;
            if (!_isAssessment)
            {
                _valveHighlight.SetActive(true);

                HightingAnim.instance.HighlightedAnim(_valveHandleHighlight, Quaternion.Euler(_startRot), Quaternion.Euler(_endRot), 1.5f);

            }
            StepUIManager.Instance?.ToggleConfirmButton(false);

            _knob.onValueChange.AddListener(CheckIfValveClosed);
        }

        public void CheckIfValveClosed(float valveValue)
        {
            if (valveValue == 1)
            {
                NextStep();
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
            
                if (automaticNextInteraction)
                {
                    StepManager.instance?.MoveToNextInteraction();
                }
            }
        }

        public void NextStep()
        {
            HightingAnim.instance.StopAnimation();
            _valveHighlight.SetActive(false);
            isInteractionCompleted = true;
            _knob.onValueChange.RemoveListener(CheckIfValveClosed);
        }
    }
}