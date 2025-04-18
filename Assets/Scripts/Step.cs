using System;
using System.Linq;
using UnityEngine;

namespace BlackOut{


[Serializable]
public class Step
{



    [Header("Interactions")]

    
    public Interaction[] interactions;
    public int currentInteractionIndex {get; private set;}
    private Interaction currentInteraction;

    //private void Awake()
    //{
       

    //}
    public  bool isStepCompleted { get; set; }
    public void StartStep()
    {
        currentInteraction = interactions[currentInteractionIndex];
        currentInteraction.StartInteraction();
    }
    //private void Update()
    //{
    //    if (isStepCompleted)
    //    {
    //        StepCompleted();
    //    }

    //}
    public  void StepCompleted()
    {

    }

    public void UpdateUI()
    {
        StepUIManager.Instance?.GoToStep(StepManager.instance.currentStepIndex, currentInteractionIndex);
    }

    public void MoveToNextInteraction()
    {
        if (currentInteraction.isInteractionCompleted)
        {


            currentInteraction.InteractionCompleted();
            



            Debug.Log("Next Interaction Called");
            currentInteractionIndex++;
            if (currentInteractionIndex < interactions.Count())
            {
                currentInteraction = interactions[currentInteractionIndex];
                currentInteraction.StartInteraction();
            }
            else
            {
                Debug.Log("StepCompleted");
                isStepCompleted = true;

                StepManager.instance?.MoveToNextStep();


            }
        }
    }




    









}
}