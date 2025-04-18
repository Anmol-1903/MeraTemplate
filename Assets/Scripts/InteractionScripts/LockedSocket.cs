using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
namespace BlackOut
{

    public class LockedSocket : Interaction
    {
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] bool _isAssessment;
        [SerializeField] float distance = 1;
        [SerializeField] XRSocketInteractor _socket;
        [SerializeField] GameObject _socketHighlight;
        [Range(0, 1)]
        [SerializeField] float sliderPercent;
        [SerializeField] GameObject _objectToGrab;
        [SerializeField] XRGrabInteractable _interactable;
        [SerializeField] GameObject _objectHighlight;
        [SerializeField] XRSlider _slider;
        [SerializeField] GameObject _sliderHighlight;
        [SerializeField] Transform highlightStartPos, highlightEndPos;
        [SerializeField] bool isGrabbed = false;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            _sliderHighlight.SetActive(false);
            _socketHighlight.SetActive(false);
            _objectHighlight.SetActive(false);
        }

        public override void StartInteraction()
        {
            if (!_isAssessment)
            {
                _objectHighlight.SetActive(true);
                _sliderHighlight.SetActive(true);
                _socketHighlight.SetActive(false);
                HightingAnim.instance.HighlightedAnim(_sliderHighlight, highlightStartPos.position, highlightEndPos.position, 1f);
            }
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
            //_objectToGrab.GetComponent<Rigidbody>().isKinematic = true;

            _objectToGrab.GetComponent<XRGrabInteractable>().enabled = true;
            _socket.enabled = false;

            //PathDirector.instance.showArrows = true;
            if (_interactable != null)
            {
                _interactable.selectEntered.AddListener(ObjectGrabbed);
                _interactable.selectExited.AddListener(ObjectNotGrabbed);
                _socket.selectEntered.AddListener(SocketEntered);
            }

            if (_slider != null)
            {
                _slider.enabled = true;
                _slider.value = 0;
                _slider.onValueChange.AddListener(OnSliderChange);
                _slider.selectExited.AddListener(SliderExit);
                _slider.selectEntered.AddListener(SliderEnter);
            }
        }

        private void SliderEnter(SelectEnterEventArgs arg)
        {
            if (!_isAssessment)
            {
                _sliderHighlight.SetActive(true);
                _socketHighlight.SetActive(false);
                _objectHighlight.SetActive(false);
            }
        }

        private void SliderExit(SelectExitEventArgs arg)
        {
            if (_slider.value >= sliderPercent)
            {
                if (_socket.IsSelecting(_interactable))
                {
                    _sliderHighlight.SetActive(false);
                    _socketHighlight.SetActive(false);
                    _objectHighlight.SetActive(false);

                    isInteractionCompleted = true;
                    if (automaticNextInteraction)
                    {
                        StepManager.instance?.MoveToNextInteraction();
                    }
                }
                else
                {
                    _slider.value = 0;

                    if (_isAssessment) return;

                    _sliderHighlight.SetActive(true);
                    _socketHighlight.SetActive(false);
                    _objectHighlight.SetActive(false);
                }
            }
            else
            {
                _slider.value = 0;

                if (_isAssessment) return;
                _sliderHighlight.SetActive(true);
                _socketHighlight.SetActive(false);
                _objectHighlight.SetActive(false);
            }
        }

        private void OnSliderChange(float val)
        {
            if (isInteractionCompleted) return;

            if (val >= sliderPercent)
            {
                _socket.enabled = true;

                if (_isAssessment) return;

                HightingAnim.instance.StopAnimating();
                _sliderHighlight.SetActive(false);
                _socketHighlight.SetActive(true);
                _objectHighlight.SetActive(true);
            }
            else
            {
                _socket.enabled = false;

                if (_isAssessment) return;
                HightingAnim.instance.HighlightedAnim(_sliderHighlight, highlightStartPos.position, highlightEndPos.position, 1f);
                _sliderHighlight.SetActive(true);
                _socketHighlight.SetActive(false);
                _objectHighlight.SetActive(false);
            }
        }

        public void SocketEntered(SelectEnterEventArgs args)
        {
            args.interactableObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _objectHighlight.SetActive(false);
            _socketHighlight.SetActive(false);
            PathDirector.instance.ClearArrows();
        }
        public void ObjectGrabbed(SelectEnterEventArgs args) // Accept the event argument
        {
            if (args.interactorObject.transform.name == "Near-Far Interactor")
            {
                _objectHighlight.SetActive(false);
                isGrabbed = true;
            }
        }
        public void ObjectNotGrabbed(SelectExitEventArgs args) // Accept the event argument
        {
            if (!isInteractionCompleted & !_isAssessment)
            {
                _objectHighlight.SetActive(true);
            }

            isGrabbed = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isInteractionCompleted & !_isAssessment)
            {
                if (isGrabbed)
                {
                    _objectHighlight.SetActive(false);
                }
                else
                {
                    _objectHighlight.SetActive(true);
                }

                if (_slider.value < sliderPercent)
                {
                    _sliderHighlight.SetActive(true);
                }
                else
                {
                    _sliderHighlight.SetActive(false);
                }

                if (_slider.value >= sliderPercent && isGrabbed)
                {
                    _socketHighlight.SetActive(true);
                }
                else
                {
                    _socketHighlight.SetActive(false);
                }
            }


            if (_isAssessment)
            {
                _sliderHighlight.SetActive(false);
                _objectHighlight.SetActive(false);
                _socketHighlight.SetActive(false);
            }
        }
    }
}