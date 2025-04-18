using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StepUIManagerSL", menuName = "ScriptableObjects/StepUIManagerSO")]
public class StepUIManagerSO : ScriptableObject
{

#if UNITY_EDITOR
    [ContextMenu("Count Steps")]
    public void CountSteps()
    {
        Debug.Log(GetTotalSubStepsCount());
    }
#endif

    public string LevelName;

    public AudioClip TrainingStartAudio, TrainingFinishAudio;

    [TextArea(3,4)]
    public string TrainingStartText, TrainingFinishText;

    // [Header("Welcome Panel")]
    // public string WelcomeTitle;
    // [TextArea(3, 10)]
    // public string WelcomeDescription;

    // [Header("Finish Panel")]
    // public string FinishTitle;
    // [TextArea(3, 10)]
    // public string FinishDescription;

    public List<StepData> Steps;
    public bool isAssessment;

    public int GetSubStepsCountTill(int stepIndex, int subStepIndex)
    {
        int totalSubSteps = 0;
        for (int i = 0; i <= stepIndex; i++)
        {
            foreach (var subStep in Steps[i].stepDatas)
            {
                if (i == stepIndex && subStepIndex < Steps[i].stepDatas.IndexOf(subStep))
                {
                    break;
                }
                if (subStep.countAsStep)
                {
                    totalSubSteps++;
                }
            }
        }
        return totalSubSteps;
    }
    public int GetTotalSubStepsCount()
    {
        int totalSubSteps = 0;
        foreach (var step in Steps)
        {
            totalSubSteps += step.GetSubStepsCount();
        }
        return totalSubSteps;
    }
}

[System.Serializable]
public struct StepData
{
    public List<SubStepData> stepDatas;
    public int GetSubStepsCount()
    {
        int totalSubSteps = 0;
        foreach (var step in stepDatas)
        {
            if (step.countAsStep)
            {
                totalSubSteps++;
            }
        }
        return totalSubSteps;
    }
}

[System.Serializable]
public struct SubStepData
{

    [Tooltip("Name of the step to be displayed")]
    public string Name;

    [Tooltip("Description of the step to be displayed")]
    [TextArea(2, 10)]
    public string Description;

    [Tooltip("Does this step count as a step or not")]
    public bool countAsStep;

    [Tooltip("At what position must the Step UI be on")]
    public string positionKey;

    [Tooltip("Shall the Step UI rotate or not")]
    public bool allowRotation;

    [Tooltip("AudioClip to play")]
    public AudioClip AudioClip;
}