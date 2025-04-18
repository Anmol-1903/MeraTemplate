using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectButtonUI : MonoBehaviour
{
    public AudioClip  withRightAnimation, audioForLeftTrigger, forGrab, tryLeftGrab, HwithRightAnimation, HaudioForLeftTrigger, HforGrab, HtryLeftGrab;
    public GameObject clickMe, rightTriggerButton, leftTriggerButton, rightGrabButton, leftGrabButton, rightHighlightedTrigger, rigthHandPointer;

    [SerializeField] private Animator animator;

    private AudioSource source;
    public TMP_Text text1;
    public TMP_Text text2;
    public string newText1 = "Controller Button Basics(<color=yellow>Grab</color>)";
    public string newText2 = "Locate and press the grab button on your right hand controller using your middle finger";
    private ButtonDetection buttonDetection;
    void Start()
    {

        buttonDetection = GetComponent<ButtonDetection>();
        source = FindObjectOfType<AudioSource>();
        //if (LanguageChange.GetSelectedLanguage() == 0)
        //{
        //    PlayTheAudio(intro_);
        //}
        //else
        //{
        //    PlayTheAudio(Hintro_);
        //}

        StartCoroutine(Starting(0f));
    }

    private IEnumerator Starting(float time)
    {
        yield return new WaitForSeconds(time);
        // image.enabled = false;
        animator = GetComponentInChildren<Animator>();
        animator.enabled = true;
        rightTriggerButton.SetActive(true);
        if (AudioSelection.selectedLanguage == Language.English)
        {
            PlayTheAudio(withRightAnimation);
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            PlayTheAudio(HwithRightAnimation);
        }

        buttonDetection.fortrigger = true;
    }

    private IEnumerator RightPressed()
    {
        rightTriggerButton.SetActive(false);
        text2.text = "Similarlyt for the left hand controller";
        yield return new WaitForSeconds(1f);
        ChangeScaleX(-1f);
        if (AudioSelection.selectedLanguage == Language.English)
        {
            PlayTheAudio(audioForLeftTrigger);
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            PlayTheAudio(HaudioForLeftTrigger);
        }
        //buttonDetection.rightTriggerIsPressed = false;
        leftTriggerButton.SetActive(true);
        buttonDetection.fortrigger = true;
        buttonDetection.leftTriggerIsPressed = false;
    }

    private IEnumerator LeftPressed()
    {
        leftTriggerButton.SetActive(false);
        UpdateTexts(newText1, newText2);
        if (AudioSelection.selectedLanguage == Language.English)
        {
            PlayTheAudio(forGrab);
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            PlayTheAudio(HforGrab);
        } 
        yield return new WaitForSeconds(1f);
        rightGrabButton.SetActive(true);
        ChangeScaleX(1f);
        animator.Play("RightGrabButton"); // corrected the animation name
        yield return new WaitForSeconds(1f);
        buttonDetection.forgrip = true;

        // buttonDetection.rightGripIsPressed = false;
    }

    private IEnumerator RightGrabPressed()
    {
        rightGrabButton.SetActive(false);
        Debug.Log("entered in rightGrabPressed");
        text2.text = "Similarly for the left hand controller"; 
        if (AudioSelection.selectedLanguage == Language.English)
        {
            PlayTheAudio(tryLeftGrab);
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            PlayTheAudio(HtryLeftGrab);
        }
        leftGrabButton.SetActive(true);
        yield return new WaitForSeconds(2f);
        ChangeScaleX(-1f);
        Debug.Log("audio for telling user to press grab with left hand");
        buttonDetection.forgrip = true;
        buttonDetection.leftGripIsPressed = false;
        // buttonDetection.xRNode = XRNode.LeftHand;
    }

    private IEnumerator LeftGrabPressed()
    {
        leftGrabButton.SetActive(false);
        // yield return new WaitForSeconds(2f);
        rigthHandPointer.SetActive(true);
        clickMe.SetActive(true);
        AudioManager.Instance.PlayAudioClip(clickMe.name);
        rightHighlightedTrigger.SetActive(true);
        gameObject.SetActive(false);
        yield return null;
    }

    public void RightPressedCoroutine()
    {
        StartCoroutine(RightPressed());
    }

    public void LeftPressedCoroutine()
    {
        StartCoroutine(LeftPressed());
    }

    public void RightGrabPressedCoroutine()
    {
        StartCoroutine(RightGrabPressed());
    }

    public void LeftGrabPressedCoroutine()
    {
        StartCoroutine(LeftGrabPressed());
    }

    private void PlayTheAudio(AudioClip clipName)
    {
        source.Stop();
        source.PlayOneShot(clipName);
    }

    public void UpdateTexts(string newText1, string newText2)
    {
        // Update the text of the TMP components
        text1.text = newText1;
        text2.text = newText2;
    }

    public void ChangeScaleX(float newXScale)
    {
        Vector3 currentScale = animator.transform.localScale;
        animator.transform.localScale = new Vector3(newXScale, currentScale.y, currentScale.z);
    }

}
