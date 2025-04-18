using System.Collections.Generic;
using UnityEngine;
namespace BlackOut
{

    public class StepManager : MonoBehaviour
    {

        public List<GameObject> StepsContainers;
        public List<Step> steps;
        [SerializeField] public int currentStepIndex { get; private set; }
        [HideInInspector] public Step currentStep;

        public static StepManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
            currentStepIndex = -1;
        }

        [ContextMenu("Refresh Steps")]
        public void AssignSteps()
        {
            steps = new List<Step>();
            foreach (var stepsContainer in StepsContainers)
            {
                Step newStep = new Step();
                Interaction[] interactions = stepsContainer.GetComponentsInChildren<Interaction>();
                newStep.interactions = interactions;
                steps.Add(newStep);
            }
        }

        public void StartSteps()
        {
            currentStepIndex = 0;
            StartCurrentStep();
        }

        public void StartCurrentStep()
        {
            //currentStep.gameObject.SetActive(true);
            if (currentStepIndex < steps.Count)
            {
                currentStep = steps[currentStepIndex];
                foreach (Interaction interactionObj in currentStep.interactions)
                {
                    interactionObj.gameObject.SetActive(true);
                }
                currentStep?.StartStep();

                foreach (Step step in steps)
                {
                    if (step != currentStep)
                    {
                        foreach (Interaction interactionObj in step.interactions)
                        {
                            interactionObj.gameObject.SetActive(false);
                        }

                    }
                }
            }
        }


        public void MoveToNextInteraction()
        {
            currentStep.MoveToNextInteraction();
            currentStep.UpdateUI();
        }

        public void MoveToNextStep()
        {
            if (currentStep.isStepCompleted)
            {


                currentStep.StepCompleted();
                //currentStep.gameObject.SetActive(false);


                {
                    // MoveToNextStep();
                }





                Debug.Log("Next Called");
                currentStepIndex++;
                if (currentStepIndex < steps.Count)
                {
                    StartCurrentStep();
                }
                else
                {
                    StepUIManager.Instance?.FinishTraining();
                }
            }
        }
    }


}




