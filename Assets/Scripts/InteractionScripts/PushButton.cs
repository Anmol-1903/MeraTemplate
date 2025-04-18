using UnityEngine;
using UnityEngine.XR.Content.Interaction;
namespace BlackOut
{

    public class PushButton : Interaction
    {
        [SerializeField] bool _isAssessment;
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] XRPushButton _button;
        [SerializeField] GameObject _buttonHighlight;
        [SerializeField] Vector3 _startPos;
        [SerializeField] Vector3 _endPos;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            HightingAnim.instance.StopAnimating();
            _button.enabled = false;
        }

        public override void StartInteraction()
        {
            _button.enabled = true;

            if (!_isAssessment)
            {
                _buttonHighlight.SetActive(true);

                HightingAnim.instance.HighlightedAnim(_buttonHighlight, _startPos, _endPos, 1.5f);

            }
            StepUIManager.Instance?.ToggleConfirmButton(false);

            _button.onPress.AddListener(CheckIfButtonPressed);
        }

        public void CheckIfButtonPressed()
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
            _buttonHighlight.SetActive(false);
            isInteractionCompleted = true;
            _button.onPress.RemoveListener(CheckIfButtonPressed);
        }
    }
}