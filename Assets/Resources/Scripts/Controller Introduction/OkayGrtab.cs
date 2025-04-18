using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkayGrtab : MonoBehaviour
{
    public Image imgToModify;
    public float duration;// Duration of the decrease in seconds
    private float currentFillAmount;
    private float requiredFillAmount;
    private float timePasses;
    public GameObject[] activeFalse;
    public GameObject[] activeTrue;
    public Collider collider1;
    public Behaviour anycomponent;
    void Start()
    {
        // Initialize fill amount and timer

        requiredFillAmount = 1f;
        currentFillAmount = imgToModify.fillAmount;
        timePasses = 0f;
    }
    void Update()
    {
        timePasses += Time.deltaTime;
        float newFillAmount = Mathf.Lerp(currentFillAmount, requiredFillAmount, timePasses / duration);
        imgToModify.fillAmount = newFillAmount;      
        if (imgToModify.fillAmount == requiredFillAmount)
            ChangeObjectActivity();


    }
    private void ChangeObjectActivity()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < activeTrue.Length; i++)
        {
            activeTrue[i].SetActive(true);
        }
        for (int i = 0; i < activeFalse.Length; i++)
        {
            activeFalse[i].SetActive(false);
        }
        if (collider1 != null)
        {
            anycomponent.enabled = true;
            collider1.enabled = true;
            collider1.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}

