using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTable : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject grabTable, leftThumbstickHighlight;
    private bool triggered = false;
    public PathManager pathManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            AudioManager.Instance.PlayAudioClip(grabTable.name);
            grabTable.SetActive(true);
            leftThumbstickHighlight.SetActive(false);
            triggered = true;
            Debug.Log("coroutine stopped");
            pathManager.HideShowPath();
        }
    }
}
