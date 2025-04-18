using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
namespace BlackOut
{

    public class SliderClose : Interaction
    {
        [SerializeField] bool _isAssessment;
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] bool lockToValue;
        [SerializeField] XRSlider _slider;
        [SerializeField] GameObject _Highlight;
        [SerializeField] Transform _startPos;
        [SerializeField] Transform _endPos;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            HightingAnim.instance.StopAnimating();
            _slider.enabled = false;
        }

        public override void StartInteraction()
        {
            _slider.enabled = true;
            _slider.value = 0;
            if (!_isAssessment)
            {
                _Highlight.SetActive(true);

                HightingAnim.instance.HighlightedAnim(_Highlight, _startPos.position, _endPos.position, 1.5f);

            }
            StepUIManager.Instance?.ToggleConfirmButton(false);

            _slider.onValueChange.AddListener(CheckIfValveClosed);

            if (lockToValue)
            {
                _slider.selectExited.AddListener(SliderReleased);
            }
        }

        private void SliderReleased(SelectExitEventArgs arg0)
        {
            if (!isInteractionCompleted)
            {
                _slider.value = 0;
            }
        }

        public void CheckIfValveClosed(float val)
        {

            if (val == 1)
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
            _Highlight.SetActive(false);
            isInteractionCompleted = true;
            _slider.onValueChange.RemoveListener(CheckIfValveClosed);
        }
    }
}