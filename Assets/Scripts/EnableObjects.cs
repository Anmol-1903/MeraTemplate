using UnityEngine;
public class EnableObjects : MonoBehaviour
{
    [SerializeField] GameObject[] objects;

    void OnEnable()
    {
        foreach (GameObject obj in objects){
            obj.SetActive(true);
        }
    }

    void OnDisable()
    {
        foreach(GameObject obj in objects){
            obj.SetActive(false);
        }
    }
}