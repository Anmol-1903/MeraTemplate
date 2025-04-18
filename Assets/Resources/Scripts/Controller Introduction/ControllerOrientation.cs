using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class ControllerOrientation : MonoBehaviour
{

    public GameObject LeftController, ButtonInteractionUI, RightHologram, LeftHologram, triggerPlace, image, triggerAnimationPlace;
    public TextMeshProUGUI instructionText;
    public AudioClip  _ForLeftControllerAudio, tryIt, hurray, _HForLeftControllerAudio, HtryIt, Hhurray;

    [SerializeField]
    private Animator animator;
    private string rightcontrollerClip = "RigthControllerOrientation";
    private string leftcontrollerClip = "LeftControllerOrientation";
    private AudioSource audioSource;

    // public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // audioManager = FindObjectOfType<AudioManager>();
        triggerPlace.SetActive(false);
        LeftHologram.SetActive(false);
        RightHologram.SetActive(false);
        // animator = GetComponentInChildren<Animator>();
        audioSource = FindObjectOfType<AudioSource>();
        if (AudioSelection.selectedLanguage == Language.English)
        {
            //PlayAudio(_IntroductionAudio);
            StartCoroutine(ChangeState( rightcontrollerClip));
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            //PlayAudio(_HIntroductionAudio);
            StartCoroutine(ChangeState(rightcontrollerClip));
        }
    }
    private IEnumerator ChangeState( string clipName)
    {
        Debug.Log("pause for11 sec");
        //yield return new WaitForSeconds(time);
        image.SetActive(false);
        triggerPlace.SetActive(true);
        animator = GetComponentInChildren<Animator>();
        animator.enabled = true;
        Debug.Log("pause for 4 sec");
        if (AudioSelection.selectedLanguage == Language.English)
        {
            PlayAudio(tryIt);
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            PlayAudio(HtryIt);
        }
        yield return new WaitForSeconds(5f);
        RightHologram.SetActive(true);
        Debug.Log("pause for 6 sec");

        // yield return new WaitForSeconds(6f);

        // display hologram right
    }
    public void Right()
    {
        StartCoroutine(RightControllerDetected());
    }
    public void Left()
    {
        StartCoroutine(LeftControllerDetected());
    }
    private IEnumerator RightControllerDetected()
    {
        Debug.Log("enter");
        yield return new WaitForSeconds(1f);
        Debug.Log("done with the");
        // animator.enabled = false;
        // animator.Play();
        triggerPlace.SetActive(false);
        RightHologram.SetActive(false);
        image.SetActive(true);
        animator.enabled = false;
        if (AudioSelection.selectedLanguage == Language.English)
        {
            PlayAudio(_ForLeftControllerAudio);
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            PlayAudio(_HForLeftControllerAudio);
        }
        instructionText.text = "Similarly place the left hand controller";
        // disable right hologram
        // enable left hologram
        // yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(3f);
        image.SetActive(false);
        animator.enabled = true;
        print("after yield");
        triggerPlace.SetActive(true);
        animator.Play(leftcontrollerClip);
        LeftHologram.SetActive(true);
        // yield return new WaitForSeconds(1f);
    }
    private IEnumerator LeftControllerDetected()
    {
        yield return new WaitForSeconds(1f);
        

        LeftHologram.SetActive(false);
        triggerAnimationPlace.SetActive(false);
        image.SetActive(true);
        instructionText.text = "Fantastic! You are holding right and left controllers in your respective hands. Let's learn the interactions now.";
        
        if (AudioSelection.selectedLanguage == Language.English)
        {
            PlayAudio(hurray);
            yield return new WaitForSeconds(hurray.length);
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            PlayAudio(Hhurray);
            yield return new WaitForSeconds(Hhurray.length);
        }
        ButtonInteractionUI.SetActive(true);
        gameObject.SetActive(false);
    }
    private void PlayAudio(AudioClip clipName)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clipName);
    }
}
