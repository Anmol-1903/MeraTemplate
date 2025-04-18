using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject uiRateUrself;
    public GameObject StarSlider,StarController;
    public PathManager pathManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StarController.SetActive(true);
            AudioManager.Instance.PlayAudioClip(uiRateUrself.name);
            uiRateUrself.SetActive(true);
            GetComponent<CapsuleCollider>().enabled = false;
            StarSlider.SetActive(true);
            gameObject.SetActive(false);
            pathManager.HideShowPath();
        }
    }
}
