using Unity.XR.CoreUtils;
using UnityEngine;
public class DoorController : MonoBehaviour
{
    Transform player;
    [SerializeField] Transform inside;
    Animator animator;

    [SerializeField] Transform doorPos;
    [SerializeField] float minDistance = 5;
    bool doorOpenOut = false, doorOpenIn = false;
    void Awake()
    {
        player = FindObjectOfType<XROrigin>().transform;
        animator = GetComponent<Animator>();
    }

    void Update(){
        CheckDoorState();
    }
    void CheckDoorState()
    {
        if (IsPlayerCloseToDoor())
        {
            if (Vector3.Dot(player.position - doorPos.position, inside.position - doorPos.position) > 0)
            {
                // Player is close from the inside, open door outwards if it's not already open inwards
                if (!doorOpenIn)
                {
                    doorOpenOut = true;
                    doorOpenIn = false;
                }
            }
            else
            {
                // Player is close from the outside, open door inwards if it's not already open outwards
                if (!doorOpenOut)
                {
                    doorOpenOut = false;
                    doorOpenIn = true;
                }
            }
        }
        else
        {
            // Player is not close, reset door state
            doorOpenOut = false;
            doorOpenIn = false;
        }
        animator.SetBool("OpenOut", doorOpenOut);
        animator.SetBool("OpenIn", doorOpenIn);
    }
    bool IsPlayerCloseToDoor(){
        return Vector3.Distance(player.position, doorPos.position) < minDistance;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(doorPos.position, minDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(doorPos.position, inside.position);
    }
#endif
}