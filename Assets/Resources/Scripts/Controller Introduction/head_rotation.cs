using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class head_rotation : MonoBehaviour
{
    public LayerMask colliderLayer; // Assign the layer where your colliders are placed
    public Transform leftCollider;
    public Transform rightCollider;
    public Animator anim; 
    // public GameObject uiNext;
    public bool leftColliderDetected = false;
    public bool rightColliderDetected = false;
    public plane airplane;
    private void Update()
    {
        RaycastHit hit;

        // Cast a ray from the user's gaze direction
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, colliderLayer))
        {
            Debug.Log("raycasted");
            Debug.Log("entered if loop");
            // Debug.DrawRay(transform.position, transform.forward, Color.red, 1f, true);

            if (hit.collider.transform == leftCollider && !leftColliderDetected)
            {
                Debug.Log("detected left collider");
                leftColliderDetected = true;
                airplane.AnimationEventLeftCollider();
                // rightColliderDetected = false;
            }
            else if (hit.collider.transform == rightCollider && !rightColliderDetected )
            {
                Debug.Log("detected right collider");
                rightColliderDetected = true;
                airplane.AnimationEventRightCollider();
            }

            // Trigger UI changes or other actions based on collider detection
            HandleColliderDetection();
        }
        // else
        // {
        //     leftColliderDetected = false;
        //     rightColliderDetected = false;
        // }
    }

    private void HandleColliderDetection()
    {
        if (leftColliderDetected && rightColliderDetected)
        {   
            Debug.Log("Both colliders detected!");
            // uiNext.SetActive(true);
        }
        // else if (leftColliderDetected)
        // {
        //     Debug.Log("Left collider detected!");
        //     leftColliderDetected = true;
        // }
        // else if (rightColliderDetected)
        // {
        //     Debug.Log("Right collider detected!");
        // }
    }

    // Animation event functions
    
}




  


