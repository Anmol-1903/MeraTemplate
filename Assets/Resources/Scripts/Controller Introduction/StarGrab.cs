using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BNG;
using UnityEngine.Events;
public class StarGrab : MonoBehaviour// : GrabbableEvents
{
    private Rigidbody rigidbody;
    void start()
    {
       rigidbody = GetComponent<Rigidbody>();
    }
    //public override void OnRelease()
    //{
    //    rigidbody.isKinematic = false;
    //    base.OnRelease();
    //}
}
