using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
namespace BlackOut
{

    public class GrabObject : Interaction
    {
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] bool showArrows;
        [SerializeField] bool _isAssessment;
        [SerializeField] float distance = 1;
        [SerializeField] GameObject _objectToGrab;
        [SerializeField] GameObject _objectHighlight;
        [SerializeField] GameObject _socketHighlight;
        [SerializeField] XRGrabInteractable _interactable;
        [SerializeField] XRSocketInteractor _socket;
        [SerializeField] bool isGrabbed = false;
        [SerializeField] bool turnOffSocketOnComplete = false;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {
            // gameObject.SetActive(false);
            //_objectToGrab.GetComponent<XRGrabInteractable>().enabled = false;
            if (turnOffSocketOnComplete)
            {
                _socket.enabled = false;
            }
            _objectHighlight.SetActive(false);
            _socketHighlight.SetActive(false);

            PathDirector.instance.showArrows = false;
            PathDirector.instance.ClearArrows();
        }

        public override void StartInteraction()
        {
            if (!_isAssessment)
            {
                _objectHighlight.SetActive(true);
            }

            StepUIManager.Instance?.ToggleConfirmButton(false);

            //_objectToGrab.GetComponent<Rigidbody>().isKinematic = true;
            _objectToGrab.GetComponent<XRGrabInteractable>().enabled = true;
            _socket.enabled = true;
            if (_interactable != null)
            {
                _interactable.selectEntered.AddListener(Grabbed);
                _interactable.selectExited.AddListener(NotGrabbed);
                _socket.selectEntered.AddListener(interactionComplete);
                Debug.Log("Listeners added successfully!");
            }
            else
            {
                Debug.Log("XRGrabInteractable component is missing!");
            }
        }

        public void interactionComplete(SelectEnterEventArgs args)
        {
            args.interactableObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _objectHighlight.SetActive(false);
            _socketHighlight.SetActive(false);
            PathDirector.instance.ClearArrows();
            isInteractionCompleted = true;
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
            if (automaticNextInteraction)
            {
                StepManager.instance?.MoveToNextInteraction();
            }
        }
        public void Grabbed(SelectEnterEventArgs args) // Accept the event argument
        {
            if (args.interactorObject.transform.name == "Near-Far Interactor")
            {
                _objectHighlight.SetActive(false);
                isGrabbed = true;
            }
        }
        public bool IsCloseToPoint(GameObject obj, Vector3 targetPoint)
        {
            Vector3 objPosition = obj.transform.position;
            objPosition.y = targetPoint.y; // Ignore Y distance

            float distance = Vector3.Distance(objPosition, targetPoint);

            if (distance <= this.distance)
            {
                PathDirector.instance?.ClearArrows();
                return true;
            }

            return false;
        }
        public void NotGrabbed(SelectExitEventArgs args) // Accept the event argument
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
                    _socketHighlight.SetActive(true);
                    if (showArrows)
                    {
                        PathDirector.instance.showArrows = true;
                        PathDirector.instance.SetEndValue(_socketHighlight.transform.position);
                    }
                }
                else
                {
                    _socketHighlight.SetActive(false);
                    _objectHighlight.SetActive(true);
                    if (showArrows)
                    {
                        PathDirector.instance.showArrows = true;
                        PathDirector.instance.SetEndValue(_interactable.transform.position);
                    }
                }
            }

            else
            {
                PathDirector.instance.showArrows = false;
                _objectHighlight.SetActive(false);
                _socketHighlight.SetActive(false);
            }
        }
    }
}