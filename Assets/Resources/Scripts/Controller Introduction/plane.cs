using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour
{
    public GameObject uiObject;
    public head_rotation head_Rotation;
    private Animator anim;
    public Collider collider;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void AnimationEventLeftCollider()
    {
      
        if (head_Rotation.leftColliderDetected)
        {
           anim.speed = 1f;
            Debug.Log("entered in if for left");
        }
        else
        {
           anim.speed = 0f;
            Debug.Log("entered in else for left");
        }
        //collider.enabled = false;
    }

    public void AnimationEventRightCollider()
    {
        
        if (head_Rotation.rightColliderDetected)
        {
            anim.speed = 1f;
        }
        else
        {
            anim.speed = 0f;
        }
    }
    public GameObject joystick;
    public void AnimationEnd()
    {
        Destroy(gameObject);
        uiObject.SetActive(true);
        joystick.SetActive(true);
        AudioManager.Instance.PlayAudioClip(uiObject.name); 
    }
    
    
}
