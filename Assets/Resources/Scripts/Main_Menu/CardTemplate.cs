using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CardTemplate : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Button button;
    [SerializeField] private GameObject completeImage, incompleteImage;

    public void SetCardValues(Sprite sprite, string title, string description, string scene, int progress){
        if(sprite != null){
            image.sprite = sprite;
        }

        if (progress != -1 && progress != 0 && progress != 1)
        {
            progress = -1;
        }

        switch (progress){
            case -1:
                completeImage.SetActive(false);
                incompleteImage.SetActive(false);
                break;
            case 0:
                completeImage.SetActive(false);
                incompleteImage.SetActive(true);
                break;
            case 1:
                completeImage.SetActive(true);
                incompleteImage.SetActive(false);
                break;
        }

        titleText.text = title;
        descText.text = description;
        
        button.onClick.AddListener(() => {
            if(MySceneManager.Instance != null){
                MySceneManager.Instance.LoadScene(scene);
            }
            else{
                StartCoroutine(LoadingScene(scene));
            }
        }); 
    }
    IEnumerator LoadingScene(string sceneName) {
        // Start loading the scene before the canvas fades in
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        while (!operation.isDone) {
            yield return null;
        }
    }
}