using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackOut{

public class MoveTruck : Interaction
{

    public GameObject Object;
    public Vector3 startPoint; // Position A
    public Vector3 endPoint;   // Position B
    public float lerpSpeed = 2f;
    private float t = 0;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            t += Time.deltaTime * lerpSpeed;
            Object.transform.position = Vector3.Lerp(startPoint, endPoint, t);
            
            if (t >= 1)
            {
                isMoving = false;
                t = 1; // Ensure the object reaches the exact position
                isInteractionCompleted = true;
                //StepManager.instance.MoveToNextInteraction();
                
            }
        }
    }
    private void Start() 
    {
     Object.transform.position = startPoint;   
    }

    public void StartLerp()
    {
        t = 0;
        isMoving = true;
    }
    public override bool isInteractionCompleted { get ; set ; }

    public override void InteractionCompleted()
    {
        gameObject.SetActive(false);
    }

    public override void StartInteraction()
    {
        StartLerp();      
    }
}
}