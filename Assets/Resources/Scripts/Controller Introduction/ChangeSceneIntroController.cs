using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneIntroController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    CanvasGroup canvasGroup;
    bool loading;

    private void Awake() {
        canvasGroup = panel.GetComponentInParent<CanvasGroup>();
    }

    public void ChangeScene()
    {
        canvasGroup.alpha = 0;
        StartCoroutine(LoadingScene());
    }

    private void Update() {
        if (loading && canvasGroup.alpha < .97f) {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, 2 * Time.deltaTime);
        } else if (loading) {
            canvasGroup.alpha = 1;
            loading = false;
        }
    }

    IEnumerator LoadingScene() {
        // Start loading the scene before the canvas fades in
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");
        operation.allowSceneActivation = false;
        panel.GetComponentInParent<Animator>().enabled = false;
        panel.SetActive(true);
        loading = true;
        
        // Allow the scene to activate as soon as it is ready
        while (!operation.isDone) {
            if (operation.progress >= 0.9f && canvasGroup.alpha >= 1) {
                operation.allowSceneActivation = true;
            }
            yield return null; // Wait until the next frame
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            ChangeScene();
        }
    }
}
