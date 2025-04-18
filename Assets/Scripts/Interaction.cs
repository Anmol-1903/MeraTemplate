using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace BlackOut{

public abstract class Interaction : MonoBehaviour
{
    public static GameObject _controller;
    //public GameObject[] GameObjects;



    private void Awake()
    {
        _controller = GameObject.Find("Left Controller");

      //  Debug.Log(_controller.transform.position);
    }



    public abstract bool isInteractionCompleted { get; set; }
    //public abstract bool MoveToNextAutomatically { get; set; }



    public abstract void StartInteraction();
    public abstract void InteractionCompleted();




}
}