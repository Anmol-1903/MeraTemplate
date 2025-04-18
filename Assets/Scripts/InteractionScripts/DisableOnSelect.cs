using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
namespace BlackOut
{

    public class DisableOnSelect : Interaction
    {
        public bool automaticNextInteraction;
        [Tooltip("Layermask to add to interactable items on enable")]
        public InteractionLayerMask layerMask;
        public ObjectsToEquip[] equipments;
        private bool[] isGrabbed;
        public override bool isInteractionCompleted { get; set; }

        public void AllowGrab()
        {
            isGrabbed = new bool[equipments.Length];
            foreach (ObjectsToEquip xr in equipments)
            {
                xr.equipable.selectEntered.AddListener(Grabbed);
                xr.equipable.hoverEntered.AddListener(HoverEnter);
                xr.equipable.hoverExited.AddListener(HoverExit);
                xr.equipable.interactionLayers |= layerMask;

                xr.highlight.SetActive(true);
                xr.mainObject.SetActive(false);
            }
        }
        private void HoverExit(HoverExitEventArgs arg)
        {
            XRSimpleInteractable interactable = arg.interactableObject as XRSimpleInteractable;
            if (interactable != null)
            {
                for (int i = 0; i < equipments.Length; i++)
                {
                    if (equipments[i].equipable == interactable && isGrabbed[i] == false)
                    {
                        equipments[i].mainObject.SetActive(false);
                        equipments[i].highlight.SetActive(true);
                        break;
                    }
                }
            }
        }

        private void HoverEnter(HoverEnterEventArgs arg)
        {
            XRSimpleInteractable interactable = arg.interactableObject as XRSimpleInteractable;
            if (interactable != null)
            {
                for (int i = 0; i < equipments.Length; i++)
                {
                    if (equipments[i].equipable == interactable)
                    {
                        equipments[i].mainObject.SetActive(true);
                        equipments[i].highlight.SetActive(false);
                        break;
                    }
                }
            }
        }

        private void Grabbed(SelectEnterEventArgs arg)
        {
            XRSimpleInteractable interactable = arg.interactableObject as XRSimpleInteractable;
            if (interactable != null)
            {
                for (int i = 0; i < equipments.Length; i++)
                {
                    if (equipments[i].equipable == interactable)
                    {
                        isGrabbed[i] = true;
                        equipments[i].equipable.interactionLayers &= ~layerMask;
                        equipments[i].equipable.gameObject.SetActive(false);

                        // equipments[i].equipable.transform.position = equipments[i].highlight.transform.position;
                        // equipments[i].equipable.transform.rotation = equipments[i].highlight.transform.rotation;

                        equipments[i].highlight.SetActive(false);
                        equipments[i].mainObject.SetActive(true);
                        break;
                    }
                }
            }
        }

        public override void InteractionCompleted()
        {
            // gameObject.SetActive(false);
        }

        public override void StartInteraction()
        {
            AllowGrab();
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
        }

        // Update is called once per frame
        void Update()
        {
            if (AllGrabbed())
            {
                isInteractionCompleted = true;
                StepManager.instance?.MoveToNextInteraction();

            }
        }
        public bool AllGrabbed()
        {
            return isGrabbed.All(x => x);
        }
    }
}
[System.Serializable]
public struct ObjectsToEquip
{
    public XRSimpleInteractable equipable;
    public GameObject highlight;
    public GameObject mainObject;
}