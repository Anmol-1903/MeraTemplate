using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InnovateLabs;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class SplashScreenManager : MonoBehaviour
{
    private bool isSplashScreenPlaying = true;
    [SerializeField] private SplashScreenType splashScreenType = SplashScreenType.Image;
    public Sprite SplashScreenImage;
    private Image _image;
    public GameObject AnimationPrefab;
    private GameObject _animObject;
    public AnimationClip Clip;

    [Header("Splash Image")]
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private float _displayDuration = 2f;
    [SerializeField] float _distance;
    [SerializeField] float _smoothSpeed;
    [SerializeField] private float _verticalOffset;

    Camera _camera;

    private void Awake() {
        isSplashScreenPlaying = true;
        _camera = Camera.main;

        if(splashScreenType == SplashScreenType.Image){
            _image = transform.GetChildWithName("SplashScreenImage").GetComponent<Image>();
        }
        else if(splashScreenType == SplashScreenType.Animation){
            _animObject = transform.GetChildWithName("AnimationObject").gameObject;
        }
        else{
            isSplashScreenPlaying = false;
            return;
        }
    }

    private void Start()
    {
        if (SplashScreenImage != null && splashScreenType == SplashScreenType.Image)
        {
            InitiateSplashImage();
        }

        if (AnimationPrefab != null && splashScreenType == SplashScreenType.Animation)
        {
            InitiateAnim();
        }
        StartCoroutine(LoadMainMenu());
    }

    private void LateUpdate()
    {
        LazyFollowFunc();
    }

    void InitiateSplashImage()
    {
        _animObject?.SetActive(false);
        _image.gameObject.SetActive(true);
        _image.sprite = SplashScreenImage;
        _image.SetNativeSize();

        // Start fade sequence
        StartCoroutine(FadeSplashScreen());
    }

    void InitiateAnim()
    {
        _image?.gameObject.SetActive(false);
        _animObject.SetActive(true);

        GameObject animInstance = Instantiate(AnimationPrefab, _animObject.transform);

        // Ensure the object has an Animator component
        Animator animator = animInstance.GetComponent<Animator>();
        if (animator == null)
        {
            animator = animInstance.AddComponent<Animator>();
        }

        if (Clip == null)
        {
            Debug.LogError("Animation clip is missing!");
            return;
        }

        #if UNITY_EDITOR
        // Create a new Animator Controller
        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath("Assets/RuntimeAnimator.controller");

        // Add a new state with the animation clip
        AnimatorState state = animatorController.layers[0].stateMachine.AddState("ClipState");
        state.motion = Clip; // Assign the animation clip

        // Assign the controller to the Animator
        animator.runtimeAnimatorController = animatorController;
        #endif

        // Play the animation using the state name
        animator.Play("ClipState");
    }

    private IEnumerator FadeSplashScreen()
    {
        // Fade In
        yield return StartCoroutine(FadeImage(0f, 1f));

        // Hold for display duration
        yield return new WaitForSeconds(_displayDuration);

        // Fade Out
        yield return StartCoroutine(FadeImage(1f, 0f));

        // Optionally, disable the gameobject or load next scene
        _image.transform.gameObject.SetActive(false);
    }

    private IEnumerator FadeImage(float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;
        Color imageColor = _image.color;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / _fadeDuration);

            imageColor.a = newAlpha;
            _image.color = imageColor;

            yield return null;
        }

        // Ensure final alpha is exactly the target
        imageColor.a = targetAlpha;
        _image.color = imageColor;

        if(targetAlpha == 0){
            yield return new WaitForSeconds(1);
            isSplashScreenPlaying = false;
        }        
    }

    void LazyFollowFunc()
    {
        // Calculate desired position in front of the camera
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 desiredPosition = _camera.transform.position
            + cameraForward * _distance
            + Vector3.up * _verticalOffset;

        // Smoothly interpolate to the desired position
        _image.gameObject.transform.position = Vector3.Lerp(_image.gameObject.transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

        // Optional: Soft look-at for rotation
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        _image.gameObject.transform.rotation = Quaternion.Slerp(_image.gameObject.transform.rotation, targetRotation, _smoothSpeed * Time.deltaTime);
    }

    private IEnumerator LoadMainMenu(){
        AsyncOperation operation = SceneManager.LoadSceneAsync("ControllerIntroduction");
        operation.allowSceneActivation = false;
        while (!operation.isDone && isSplashScreenPlaying){
            yield return null;
        }
        operation.allowSceneActivation = true;
    }
}

public enum SplashScreenType{
    None,
    Image,
    Animation
}
