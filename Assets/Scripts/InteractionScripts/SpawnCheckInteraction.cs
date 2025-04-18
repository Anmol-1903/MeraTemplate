using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackOut{

public class SpawnCheckInteraction : Interaction
{

    [SerializeField] GameObject _Objprefab;
    [SerializeField] GameObject _spawnPosition;
    [SerializeField] Quaternion _spawnRotation;
    [SerializeField] GameObject _spawnParent;
     GameObject _object;
    [SerializeField] Vector3 objPos;
    [SerializeField] Quaternion objRot;

    public override bool isInteractionCompleted { get ; set; }
   // public override bool MoveToNextAutomatically { get ; set; }



    public override void StartInteraction()
    {
        _object = ObjectSpawner.instance.SpawnObject(_Objprefab,_spawnPosition.transform.position,_spawnRotation, _spawnParent.transform, objPos, objRot);
        _object.SetActive(true);
        isInteractionCompleted = true;       
    }
    

    public override void InteractionCompleted()
    {


        _object.SetActive(false);


        //bool ifCalled = false;
        //if (MoveToNextAutomatically)
        //{
        //    if (!ifCalled)
        //    {
        //        ifCalled = true;
        //        StepManager.instance.MoveToNextInteraction();

        //    }
        //}
    }

}
}