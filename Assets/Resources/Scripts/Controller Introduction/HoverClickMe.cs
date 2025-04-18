//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class HoverClickMe : MonoBehaviour
//{
//    private AudioSource audioSource;
//    public AudioClip clickME;
//    private Animator animator;
//    // Start is called before the first frame update
//    void Start()
//    {
//        audioSource = FindObjectOfType<AudioSource>();
//        audioSource.PlayOneShot(clickME);
//        animator.enabled = false;
//        StartCoroutine(playAnimClickMe());
//    }

//    // Update is called once per frame
//    // void Update()
//    // {
        
//    // }
//    private IEnumerator playAnimClickMe(){
//        yield return new WaitForSeconds(4f);
//        animator.enabled= true;
//    }
//}
