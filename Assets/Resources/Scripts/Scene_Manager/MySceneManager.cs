using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using InnovateLabs;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;

    string sceneToLoad;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;

        StartCoroutine(LoadScene_Fade());
    }

    public void LoadScene(int sceneIndex)
    {
        sceneToLoad = SceneManager.GetSceneAt(sceneIndex).name;

        StartCoroutine(LoadScene_Fade());
    }

    Animator anim;
    private IEnumerator LoadScene_Fade()
    {
        FindFirstObjectByType<XROrigin>()?
            .transform.GetChildWithName("FadeCanvas")?
            .GetComponent<Animator>()?
                .SetTrigger("Load");

        float c = 1;
        while (c > 0)
        {
            c -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}