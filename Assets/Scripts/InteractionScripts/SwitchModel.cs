using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackOut{

public class SwitchModel : Interaction
{
    [SerializeField] GameObject objToBeReplaced;
    [SerializeField] GameObject objThatWillBePlaced;
    

    public override bool isInteractionCompleted { get ; set ; }

    public override void InteractionCompleted()
    {
        
    }

    public override void StartInteraction()
    {
       objThatWillBePlaced.transform.position = objToBeReplaced.transform.position;
       objToBeReplaced.SetActive(false);
       objThatWillBePlaced.SetActive(true);
       isInteractionCompleted = true;
       StepManager.instance?.MoveToNextInteraction();
    }

    // Start is called before the first frame update
    void Start()
    {
        objThatWillBePlaced.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}