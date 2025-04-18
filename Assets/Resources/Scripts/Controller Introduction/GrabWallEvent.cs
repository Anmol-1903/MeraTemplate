// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GrabWallEvent : MonoBehaviour
// {
//     public ActivateTable activateTable;
//     public AudioClip audioClip;
//     public GameObject direction;
//     private AudioSource audioSource;
//     public bool drillTriggered;
//     public GameObject selfAssessmentUI;
//     // public PathDirector pathDirector1;
//     // Start is called before the first frame update

//     private void OnTriggerEnter(Collider other)
//     {
//         if(other.CompareTag("Drill") && !drillTriggered)
//         {
//             // activateTable.UnhighlightObject(activateTable.toolbox, activateTable.toolboxOriginalMaterial);
//             // activateTable.UnhighlightObject(activateTable.drill, activateTable.drillOriginalMaterial);
//             audioSource = FindObjectOfType<AudioSource>();
//             audioSource.PlayOneShot(audioClip);
//             selfAssessmentUI.SetActive(true);
//             drillTriggered = true;
//             // pathDirector1.SetEndValue(direction.transform.position);
//         }
//     }
// }
