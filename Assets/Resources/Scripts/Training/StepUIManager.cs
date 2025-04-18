using System.Collections;
using System.Collections.Generic;
using InnovateLabs;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StepUIManager : MonoBehaviour
{
    public static StepUIManager Instance;
    public StepUIManagerSO StepUIScriptable;

    public AudioClip buttonClick;

    public List<Transform> canvasPositions;

    private GameObject Welcome, Training, Completed;

    private TextMeshProUGUI welcomeDescription;
    private TextMeshProUGUI finalDescription;

    private Button StartTrainingButton;
    [HideInInspector] public List<TextMeshProUGUI> title;
    private TextMeshProUGUI stepName;
    private TextMeshProUGUI stepNumber;
    private TextMeshProUGUI stepDescription;

    [HideInInspector] public Button NextButton, NarrateButton;
    [HideInInspector] public List<Button> HomeButton;

    private TrainingState trainingState;
    public int CurrentStep { get; private set; }
    public int CurrentSubStep { get; private set; }

    private LookAtCamera lookAt;

    private void AssignVariables()
    {
        Welcome = transform.GetChildWithName("Welcome").gameObject;
        Training = transform.GetChildWithName("Training").gameObject;
        Completed = transform.GetChildWithName("Finished").gameObject;

        StartTrainingButton = Welcome.transform.GetChildWithName("StartTraining").GetComponent<Button>();

        stepName = Training.transform.GetChildWithName("StepName").GetComponent<TextMeshProUGUI>();
        stepNumber = Training.transform.GetChildWithName("StepNumber").GetComponent<TextMeshProUGUI>();
        stepDescription = Training.transform.GetChildWithName("StepDescription").GetComponent<TextMeshProUGUI>();

        NextButton = Training.transform.GetChildWithName("Next").GetComponent<Button>();
        NarrateButton = Training.transform.GetChildWithName("Narrate").GetComponent<Button>();


        welcomeDescription = Welcome.transform.GetChildWithName("Description").GetComponent<TextMeshProUGUI>();
        finalDescription = Completed.transform.GetChildWithName("Description").GetComponent<TextMeshProUGUI>();

        List<Transform> homes = new();
        transform.GetChildrenWithName("Home", homes);
        homes.ForEach((x) =>
            HomeButton.Add(x.GetComponent<Button>())
        );

        List<Transform> texts = new();
        transform.GetChildrenWithName("Title", texts);
        texts.ForEach((x) =>
            title.Add(x.GetComponent<TextMeshProUGUI>())
        );
    }

    private void SetEvents()
    {
        StartTrainingButton?.onClick.AddListener(StartTraining);

        foreach (var b in HomeButton)
        {
            b?.onClick.AddListener(Home);
        }

        NarrateButton?.onClick.AddListener(NarrateStep);
        NextButton?.onClick.AddListener(NextStep);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        lookAt = GetComponentInChildren<LookAtCamera>();

        AssignVariables();
        SetEvents();
    }

    private void Start()
    {
        foreach (var t in title)
        {
            t.text = StepUIScriptable.LevelName;
        }

        trainingState = TrainingState.Start;
        Welcome.SetActive(true);
        Training.SetActive(false);
        Completed.SetActive(false);

        CurrentStep = -1;
        CurrentSubStep = -1;

        welcomeDescription.text = StepUIScriptable.TrainingStartText;

        MyAudioManager.instance?.PlayGuidance(StepUIScriptable.TrainingStartAudio);
    }

    private void StartTraining()
    {
        trainingState = TrainingState.Training;

        Welcome.SetActive(false);
        Training.SetActive(true);
        Completed.SetActive(false);

        CurrentStep = 0;
        CurrentSubStep = 0;

        BlackOut.StepManager.instance?.StartSteps();

        UpdateStep();
    }

    public void FinishTraining()
    {
        trainingState = TrainingState.Finished;

        Welcome.SetActive(false);
        Training.SetActive(false);
        Completed.SetActive(true);

        finalDescription.text = StepUIScriptable.TrainingFinishText;

        MyAudioManager.instance?.PlayGuidance(StepUIScriptable.TrainingFinishAudio);
    }

    public void GoToStep(int i, int j)
    {
        CurrentStep = i;
        CurrentSubStep = j;
        UpdateStep();
    }
    private void NarrateStep()
    {
        MyAudioManager.instance?.PlaySfxOneShot(buttonClick);
        if (StepUIScriptable.Steps[CurrentStep].stepDatas[CurrentSubStep].AudioClip != null)
        {
            MyAudioManager.instance?.PlayGuidance(StepUIScriptable.Steps[CurrentStep].stepDatas[CurrentSubStep].AudioClip);
        }
    }
    private void NextStep()
    {
        BlackOut.StepManager.instance?.MoveToNextInteraction();
    }
    public void ToggleConfirmButton(bool on)
    {
        NextButton.gameObject.SetActive(on);
    }

    public void UpdateStep()
    {
        if (StepUIScriptable == null)
        {
            Debug.LogError("Please create a scriptable object");
            return;
        }

        if (CurrentStep >= StepUIScriptable.Steps.Count)
        {
            return;
        }

        foreach (var t in title)
        {
            t.text = StepUIScriptable.LevelName;
        }

        SubStepData subStep = StepUIScriptable.Steps[CurrentStep].stepDatas[CurrentSubStep];

        stepName.text = subStep.Name;

        stepDescription.text = subStep.Description;

        stepNumber.text = StepUIScriptable.GetSubStepsCountTill(CurrentStep, CurrentSubStep).ToString() + "/" + StepUIScriptable.GetTotalSubStepsCount();

        lookAt.enabled = subStep.allowRotation;
        if (subStep.positionKey != null && subStep.positionKey != "")
        {
            Transform loc = canvasPositions.Find((x) => x.name == subStep.positionKey);
            if (loc != null)
            {
                target = loc;
            }
            if(subStep.positionKey == "Follow"){
                lookAt.enabled = true;
                lookAt.AllowX = true;
            }
            else{
                lookAt.AllowX = false;
            }
        }

        if (subStep.AudioClip != null)
        {
            MyAudioManager.instance?.PlayGuidance(subStep.AudioClip);
        }
        else
        {
            MyAudioManager.instance?.StopGuidance();
        }
    }

    [SerializeField] float lerpSpeed = 5;
    Transform target;
    void LateUpdate()
    {
        if (target == null) return;
        transform.position = Vector3.Lerp(transform.position, target.position, lerpSpeed * Time.deltaTime);

        if (!lookAt.enabled)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, lerpSpeed * Time.deltaTime);
        }
    }

    private void Home()
    {
        if (MySceneManager.Instance != null)
        {
            MySceneManager.Instance.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}

public enum TrainingState
{
    Start,
    Training,
    Finished
}