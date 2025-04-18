using UnityEngine;
using UnityEngine.XR.Content.Interaction;
namespace BlackOut
{

    public class ValveOpen : Interaction
    {
        [SerializeField] bool _isAssessment;
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] XRKnob _knob;
        //[SerializeField] GameObject _bucket;
        //[SerializeField] Material _red;
        //[SerializeField] Material _blue;
        [SerializeField] GameObject _valveHighlight;

        [SerializeField] GameObject _valveHandleHighlight;
        [SerializeField] Vector3 _startRot;
        [SerializeField] Vector3 _endRot;

        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            _knob.enabled = false;
            HightingAnim.instance.StopAnimating();
        }

        [ContextMenu("Start Interaction")]
        public override void StartInteraction()
        {
            _knob.enabled = true;
            _knob.value = 1;
            isInteractionCompleted = false;
            if (!_isAssessment)
            {
                _valveHighlight.SetActive(true);

                // PathDirector.instance.showArrows = false;
                // PathDirector.instance.ClearArrows();

                HightingAnim.instance.HighlightedAnim(_valveHandleHighlight, Quaternion.Euler(_startRot), Quaternion.Euler(_endRot), 1.5f);
            }
            StepUIManager.Instance?.ToggleConfirmButton(false);
            _knob.onValueChange.AddListener(CheckIfValveTurned);
        }

        public void CheckIfValveTurned(float valveValue)
        {
            // Debug.Log(valveValue);  
            if (valveValue == 0)
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
            _knob.onValueChange.RemoveListener(CheckIfValveTurned);
        }
    }
}