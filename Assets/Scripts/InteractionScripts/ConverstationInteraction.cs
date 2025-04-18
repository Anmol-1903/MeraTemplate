using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace BlackOut{

public class ConverstationInteraction : Interaction
{
    public String[] ConvoLines;
    public Canvas canvas;
    public TextMeshProUGUI _bubbleText;
    public float duration;



    public override bool isInteractionCompleted { get ; set ; }

    public override void InteractionCompleted()
    {
       
    }

    public override void StartInteraction()
    {
        canvas.gameObject.SetActive(true);
        StartCoroutine(ConvoStart());
    }

    IEnumerator ConvoStart()
    {
        foreach  (String i in ConvoLines)
        {
            _bubbleText.text = i;
            yield return new WaitForSecondsRealtime(duration);

        }

        isInteractionCompleted = true;
        StepManager.instance?.MoveToNextInteraction();

    }
    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}