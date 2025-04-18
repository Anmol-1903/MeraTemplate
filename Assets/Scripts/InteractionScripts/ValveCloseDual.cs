using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
namespace BlackOut
{

    public class ValveCloseDual : Interaction
    {
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] bool _isAssessment;
        [SerializeField] XRKnobDual _knob;
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
            StopAllCoroutines();
        }

        [ContextMenu("Start Interaction")]
        public override void StartInteraction()
        {
            _knob.enabled = true;
            _knob.value = 0;
            isInteractionCompleted = false;
            if (!_isAssessment)
            {
                _valveHighlight.SetActive(true);

                // PathDirector.instance.showArrows = false;
                // PathDirector.instance.ClearArrows();

                HightingAnim.instance.HighlightedAnim(_valveHandleHighlight, Quaternion.Euler(_startRot), Quaternion.Euler(_endRot), 1.5f);
            }
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
            _knob.onValueChange.AddListener(CheckIfValveTurned);
        }

        public void CheckIfValveTurned(float valveValue)
        {
            // Debug.Log(valveValue);  
            if (valveValue == 1)
            {
                NextStep();
                StepManager.instance?.MoveToNextInteraction();
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