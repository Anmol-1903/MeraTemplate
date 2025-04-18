using UnityEngine;
using UnityEngine.XR.Content.Interaction;
namespace BlackOut
{

    public class SliderOpen : Interaction
    {
        [SerializeField] bool _isAssessment;
        [SerializeField] bool automaticNextInteraction;
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
            _slider.value = 1;
            if (!_isAssessment)
            {
                _Highlight.SetActive(true);

                HightingAnim.instance.HighlightedAnim(_Highlight, _startPos.position, _endPos.position, 1.5f);
            }
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);

            _slider.onValueChange.AddListener(CheckIfValveClosed);
        }

        public void CheckIfValveClosed(float valveValue)
        {
            if (valveValue == 0)
            {
                NextStep();
                StepManager.instance?.MoveToNextInteraction();
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