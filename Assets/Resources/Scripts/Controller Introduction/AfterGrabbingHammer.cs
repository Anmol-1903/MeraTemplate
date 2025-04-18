using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
public class AfterGrabbingHammer: MonoBehaviour
{
    //public Grabber leftHand, rightHand;
    public GameObject leftTriggerhighlight, rightTriggerHighlight, grabTable, StartHighlight,lefthandGrabH,rightHandH;
    public CapsuleCollider starCollider;
    public AudioManager audiomanager;
    public PathManager pathManager;
    public AudioClip placeInside;
    public AudioSource audioSource;
    public GameObject highlightedHammerSnapPoint, highlightedDrillSnapPoint;
    public bool isHammer = true;
    public Animator changeAnimation;
    
    // Start is called before the first frame update
    public void OnGrab(int hand)
    {
        if (isHammer)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            lefthandGrabH.SetActive(false);
            rightHandH.SetActive(false);
            if (hand == 0) // left
            {
                // play audio for left hand and highlight trigger button left
                //leftTriggerhighlight.SetActive(true);
                audiomanager.PlayAudioClip("hammerLeft");
                //changeAnimation.Play("RightSelectButton");
                //changeAnimation.gameObject.transform.localScale = new Vector3(-1f, 1f, -1f);
            }
            else if (hand == 1)// right
            {
                // play audio for right hand and heighlight trigger button right
                //rightTriggerHighlight.SetActive(true);
                audiomanager.PlayAudioClip("hammerRight");
                //changeAnimation.Play("RightSelectButton");
                //changeAnimation.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
    //private void Update()
    //{
    //    if (InputBridge.Instance.LeftTriggerDown)
    //    {
    //        if (leftHand.HeldGrabbable != null)
    //        {
    //            var grabble = leftHand.HeldGrabbable.gameObject.GetComponent<Grabbable>();
    //            grabble.DropItem(grabble.GetPrimaryGrabber(), true, true);
    //            gameObject.GetComponent<Rigidbody>().useGravity = true;
    //            if (isHammer)
    //            {
    //                leftTriggerhighlight.SetActive(false);
    //                Callmethods();
    //                isHammer = false;
    //            }
    //        }
    //    }
    //    if (InputBridge.Instance.RightTriggerDown)
    //    {
    //        if (rightHand.HeldGrabbable != null)
    //        {
    //            var grabble = rightHand.HeldGrabbable.gameObject.GetComponent<Grabbable>();
    //            grabble.DropItem(grabble.GetPrimaryGrabber(), true, true);
    //            gameObject.GetComponent<Rigidbody>().useGravity = true;
    //            if (isHammer)
    //            {
    //                rightTriggerHighlight.SetActive(false);
    //                Callmethods();
    //                isHammer = false;
    //            }
    //        }
    //    }
    //}
    private void Callmethods()
    {
        //gameObject.GetComponent<Rigidbody>().useGravity = true;
        audiomanager.PlayAudioClip("lookAtArrows");
        StartHighlight.SetActive(true);
        grabTable.SetActive(false);
        pathManager.ShowNextPath();
        starCollider.enabled = true;
    }
    public void PlayAudio(AudioClip clipName)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clipName);
    }
    public void AfterGrabAudio()
    {
        audiomanager.PlayAudioClip("dropOnfloor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
           // GetComponent<Rigidbody>().isKinematic = true;
           // GetComponent<Rigidbody>().useGravity = false;
            //GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = false;
            Callmethods();
        }
    }
}
