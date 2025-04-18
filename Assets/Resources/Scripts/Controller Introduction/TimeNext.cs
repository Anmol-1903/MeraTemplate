using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeNext : MonoBehaviour
{
    public GameObject rightHandPointer;
    public Image imgToModify;
    public float duration;// Duration of the decrease in seconds
    public GameObject NextUI;
    private float currentFillAmount;
    private float requiredFillAmount;
    private float timePasses;
    public CharacterController chr;
    //public RecenterOrigin recenterOrigin;
    //public AudioSource audioSource;
    void Start()
    {
        // Initialize fill amount and timer

        AudioManager.Instance.PlayAudioClip(gameObject.name);
        requiredFillAmount = 1f;
        currentFillAmount = imgToModify.fillAmount;
        timePasses = 0f;
        //StartCoroutine(Wait());
    }
    
    void Update()
    {
        timePasses += Time.deltaTime;
        float newFillAmount = Mathf.Lerp(currentFillAmount, requiredFillAmount, timePasses / duration);
        imgToModify.fillAmount = newFillAmount;
        // If the timer exceeds the duration, reset the timer and update the current fill amount
        //if (timePasses >= duration)
        //{
        //    timePasses = 0f;
        //    currentFillAmount = newFillAmount;
        //}

        if (imgToModify.fillAmount == requiredFillAmount)
            ChangeUI();
            

    }
    private void ChangeUI()
    {
        AudioManager.Instance.StopAudioSource();
        gameObject.SetActive(false);
        NextUI.SetActive(true);
        rightHandPointer.SetActive(false);
        //chr.enabled = false;
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        
        // chr.enabled = false;
        //recenterOrigin.Recenter();
    }
    public void SkipButton()
    {
        // change Scene
        print("Scene change");
    }
}
