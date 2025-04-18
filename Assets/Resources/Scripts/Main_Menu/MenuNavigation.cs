using System.Collections;
using InnovateLabs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Added for scene management

public class MenuNavigation : MonoBehaviour
{
    public static MenuNavigation instance;

    private Transform MainCanvas;

    private GameObject LoginListPanel;
    private GameObject LoadingUserPanel, EmployeeDataNotFoundPanel, EmployeeDataFoundPanel;
    private Transform UserListContainer;

    [Tooltip("A prefab that will be instantiated for each user in the users list")]
    [SerializeField] private UserData UserListPrefab;

    public AudioClip WelcomeAudio, LoginAudio, LevelAudio;

    private Button RefreshButton;

    private GameObject PasswordPanel;
    private GameObject LoggingInPanel, LogInSuccessfulPanel, LogInFailedPanelNoInternet, LogInFailed400, LogInFailed401, LogInFailedDefult;
    private TextMeshProUGUI selectedUserName, selectedUserId;
    private Button clearButton, backButton, loginButton;
    private TMP_InputField passwordInputField;
    private GameObject numpad;
    private Button[] keypadButtons;

    private GameObject LevelSelectorPanel;
    private GameObject trainingPanel, assessmentPanel, settingsPanel;
    private Toggle trainingToggle, assessmentToggle, settingsToggle;

    private TextMeshProUGUI selectedUserNameForSettings;
    private TextMeshProUGUI selectedUserIdForSettings;
    private Button logoutButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void SubscribeToEvents()
    {
        APIManager.instance.OnFetchUserSuccess += PopulateUserList;
        APIManager.instance.OnFetchUserFailed += DataFetchFailed;
        APIManager.instance.OnLoginSuccess += CompleteLogin;
        APIManager.instance.OnLoginFailedNoInternet += RetryLoginNoInternet;
        APIManager.instance.OnLoginFailedIncorrectPin += RetryLoginIncorrectPin;
        APIManager.instance.OnLoginFailedDigits += RetryLoginDigits;
        APIManager.instance.OnLoginFailedDefault += RetryLoginDefault;
    }

    private void SetupUI()
    {
        RefreshButton?.onClick.AddListener(TryLogin);
        logoutButton?.onClick.AddListener(Logout);
        loginButton?.onClick.AddListener(LoginButtonClicked);
        SetupKeypad();
        SetupButtomBar();
    }

    private void AssignParameters()
    {
        MainCanvas = transform.GetComponentInChildren<Canvas>().transform;

        // Login list
        LoginListPanel = MainCanvas.GetChildWithName("Login").gameObject;
        LoadingUserPanel = MainCanvas.GetChildWithName("LoadingUsers").gameObject;
        EmployeeDataNotFoundPanel = MainCanvas.GetChildWithName("DataNotFound").gameObject;
        EmployeeDataFoundPanel = MainCanvas.GetChildWithName("DataFound").gameObject;
        UserListContainer = LoginListPanel.transform.GetChildWithName("UserContainer");
        RefreshButton = LoginListPanel.transform.GetChildWithName("Refresh").GetComponent<Button>();

        // Enter Password
        PasswordPanel = MainCanvas.GetChildWithName("Password").gameObject;
        LoggingInPanel = MainCanvas.GetChildWithName("LoggingIn").gameObject;
        LogInSuccessfulPanel = MainCanvas.GetChildWithName("LoggedIn").gameObject;
        LogInFailedPanelNoInternet = MainCanvas.GetChildWithName("LoginFailed (no internet)").gameObject;
        LogInFailed400 = MainCanvas.GetChildWithName("LoginFailed (400)").gameObject;
        LogInFailed401 = MainCanvas.GetChildWithName("LoginFailed (401)").gameObject;
        LogInFailedDefult = MainCanvas.GetChildWithName("LoginFailed Default").gameObject;
        
        selectedUserName = PasswordPanel.transform.GetChildWithName("Name").GetComponent<TextMeshProUGUI>();
        selectedUserId = PasswordPanel.transform.GetChildWithName("Id").GetComponent<TextMeshProUGUI>();
        
        clearButton = PasswordPanel.transform.GetChildWithName("Clear").GetComponent<Button>();
        backButton = PasswordPanel.transform.GetChildWithName("Back").GetComponent<Button>();
        loginButton = PasswordPanel.transform.GetChildWithName("LoginButton").GetComponent<Button>();

        passwordInputField = PasswordPanel.transform.GetChildWithName("InputField").GetComponent<TMP_InputField>();

        numpad = PasswordPanel.transform.GetChildWithName("Numpad").gameObject;
        keypadButtons = numpad.GetComponentsInChildren<Button>();

        // Level Selector
        LevelSelectorPanel = MainCanvas.GetChildWithName("LevelSelector").gameObject;
        trainingPanel = LevelSelectorPanel.transform.GetChildWithName("TrainingSection").gameObject;
        assessmentPanel = LevelSelectorPanel.transform.GetChildWithName("AssessmentSection").gameObject;
        settingsPanel = LevelSelectorPanel.transform.GetChildWithName("SettingsSection").gameObject;
        trainingToggle = LevelSelectorPanel.transform.GetChildWithName("TrainingToggle").GetComponent<Toggle>();
        assessmentToggle = LevelSelectorPanel.transform.GetChildWithName("AssessmentToggle").GetComponent<Toggle>();
        settingsToggle = LevelSelectorPanel.transform.GetChildWithName("SettingsToggle").GetComponent<Toggle>();

        // Settings
        selectedUserNameForSettings = settingsPanel.transform.GetChildWithName("Name").GetComponent<TextMeshProUGUI>();
        selectedUserIdForSettings = settingsPanel.transform.GetChildWithName("Id").GetComponent<TextMeshProUGUI>();
        logoutButton = settingsPanel.transform.GetChildWithName("Logout").GetComponent<Button>();
    }

    private void SetActiveBasedOnScene()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") // Replace "MainMenu" with the actual name of your main menu scene
        {
            if(CurrentAppState.currentUser.employeeId != null)
            {
                MyAudioManager.instance?.PlayGuidance(WelcomeAudio);
            }
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetActiveBasedOnScene(); // Check active scene when a new scene is loaded
    }

    void GoToLogin(){
        StartCoroutine(TryGetUsers());

        if(CurrentAppState.currentUser.employeeId != null){
            CompleteLogin();
        }
    }
    public void Start()
    {
        AssignParameters();
        SubscribeToEvents();
        SetupUI();
        SetActiveBasedOnScene(); // Initial check
        
        TryLogin();
        MyAudioManager.instance?.PlayGuidance(LoginAudio);
    }
    public void TryLogin(){
        ClosePopup();
        ClearList();
        LoadingUserPanel.SetActive(true);
        counter2 = timeout;
        loaded = false;
        loadingReady = true;
    }
    private void ClearList()
    {
        UserData[] users = UserListContainer.GetComponentsInChildren<UserData>();
        foreach (UserData user in users)
        {
            Destroy(user.gameObject);
        }
    }

    private IEnumerator TryGetUsers()
    {   
        yield return new WaitForSeconds(1f);
        ExperienceManager.instance.GetUsers();
    }

    private void DataFetchPassed()
    {
        ClosePopup();
        if(LoginListPanel.activeInHierarchy){
            EmployeeDataFoundPanel.SetActive(true);
        }
    }

    private void DataFetchFailed()
    {
        ClosePopup();
        if(LoginListPanel.activeInHierarchy){
            EmployeeDataNotFoundPanel.SetActive(true);
        }
    }

    public void ClosePopup()
    {
        LoadingUserPanel.SetActive(false);
        EmployeeDataNotFoundPanel.SetActive(false);
        EmployeeDataFoundPanel.SetActive(false);
        LoggingInPanel.SetActive(false);
        LogInFailedPanelNoInternet.SetActive(false);
        LogInFailed400.SetActive(false);
        LogInFailed401.SetActive(false);
        LogInFailedDefult.SetActive(false);
        LogInSuccessfulPanel.SetActive(false);
    }

    private void PopulateUserList()
    {
        AssignParameters();
        DataFetchPassed();
        ClearList();
        foreach (var user in CurrentAppState.currentUsers)
        {
            UserData newUser = Instantiate(UserListPrefab, UserListContainer);
            newUser.SetData(user.name, user.employeeId);
        }
    }

    private void SetupKeypad()
    {
        for (int i = 0; i < keypadButtons.Length; i++)
        {
            int buttonIndex = (i + 1) % 10;
            keypadButtons[i].onClick.AddListener(() =>
            {
                passwordInputField.text += buttonIndex;
            });
        }
        clearButton.onClick.AddListener(() => { passwordInputField.text = ""; });
        backButton.onClick.AddListener(() =>
        {
            passwordInputField.text = "";
            CurrentAppState.currentUser = new();
            LoginListPanel.SetActive(true);
            PasswordPanel.SetActive(false);
        });
    }
    public void EnterPassword(string id)
    {
        LoginListPanel.SetActive(false);
        PasswordPanel.SetActive(true);

        passwordInputField.text = "";
        CurrentAppState.currentUser = CurrentAppState.currentUsers.Find(x => x.employeeId == id);

        selectedUserName.text = CurrentAppState.currentUser.name;
        selectedUserId.text = CurrentAppState.currentUser.employeeId;
    }

    void InitializeTimeoutLoop(){
        counter1 = timeout;
        loginReady = true;
        sentApiCall = false;
    }
    void LoginButtonClicked(){
        InitializeTimeoutLoop();
        CheckPassword();
    }
    void CheckPassword()
    {
        CurrentAppState.loginEmployee.employeeId = CurrentAppState.currentUser.employeeId;
        CurrentAppState.loginEmployee.pin = passwordInputField.text;
        LoggingInPanel.SetActive(true);
    }

    private void RetryLoginIncorrectPin()       // If the pin entered is incorrect
    {   
        ClosePopup();
        LogInFailed400.SetActive(true);
    }

    private void RetryLoginDigits()             // If the pin entered is not 4 digits
    {
        ClosePopup();
        LogInFailed401.SetActive(true);
    }

    private void RetryLoginNoInternet()         // If the internet is not available at the time of login
    {
        ClosePopup();
        LogInFailedPanelNoInternet.SetActive(true);
    }

    private void RetryLoginDefault()
    {
        ClosePopup();
        LogInFailedDefult.SetActive(true);
    }

    [SerializeField] float timeout = 10;
    public float counter1, counter2;
    public bool sentApiCall = false, loginReady = false, loaded = false, loadingReady = false;
    void LoginTimeout(){
        if(counter1 > 0){
            counter1 -= Time.deltaTime;
            if(InternetMonitoringService.instance.isConnected && !sentApiCall){
                APIManager.instance.Login();
                sentApiCall = true;
                loginReady = false;
                counter1 = 0;
            }
        }
        else{
            if(!sentApiCall && loginReady){
                loginReady = false;
                RetryLoginNoInternet();
            }
        }
    }
    void LoadingTimeout(){
        if(counter2 > 0){
            counter2 -= Time.deltaTime;
            if(InternetMonitoringService.instance.isConnected && !loaded){
                GoToLogin();
                loaded = true;
                loadingReady = false;
                counter2 = 0;
            }
        }
        else{
            if(!loaded && loadingReady){
                loadingReady = false;
                DataFetchFailed();
            }
        }
    }
    private void Update() {
        LoadingTimeout();
        LoginTimeout();
    }

    private void CompleteLogin()
    {
        MyAudioManager.instance?.PlayGuidance(LevelAudio);
        ClosePopup();
        LoginListPanel.SetActive(false);
        PasswordPanel.SetActive(false);
        LevelSelectorPanel.SetActive(true);
    }

    private void Logout()
    {
        CurrentAppState.currentUser = new();
        ResetToggles();
        ClosePopup();
        PasswordPanel.SetActive(false);
        LevelSelectorPanel.SetActive(false);
        LoginListPanel.SetActive(true);
        MyAudioManager.instance.PlayGuidance(LoginAudio);
    }

    private void SetupButtomBar()
    {
        trainingToggle.onValueChanged.AddListener(delegate { OpenTrainingPanel(trainingToggle); });
        assessmentToggle.onValueChanged.AddListener(delegate { OpenAssessmentPanel(assessmentToggle); });
        settingsToggle.onValueChanged.AddListener(delegate { OpenSettingsPanel(settingsToggle); });
    }

    private void ResetToggles()
    {
        trainingToggle.isOn = true;
        assessmentToggle.isOn = false;
        settingsToggle.isOn = false;
    }

    public void OpenTrainingPanel(Toggle toggle)
    {
        if (toggle)
        {
            trainingPanel.SetActive(true);
            settingsPanel.SetActive(false);
            assessmentPanel.SetActive(false);
        }
    }

    public void OpenAssessmentPanel(Toggle toggle)
    {
        if (toggle)
        {
            assessmentPanel.SetActive(true);
            trainingPanel.SetActive(false);
            settingsPanel.SetActive(false);
        }
    }

    public void OpenSettingsPanel(Toggle toggle)
    {
        if (toggle)
        {
            settingsPanel.SetActive(true);
            trainingPanel.SetActive(false);
            assessmentPanel.SetActive(false);

            selectedUserNameForSettings.text = CurrentAppState.currentUser.name;
            selectedUserIdForSettings.text = CurrentAppState.currentUser.employeeId;
        }
    }

    public void ExitApp()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }

    private void OnApplicationQuit() {
        CurrentAppState.currentUser = new();
    }
}