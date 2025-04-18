using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using BNG;
using UnityEngine.XR;
using TMPro;
using System.Linq;

public class ButtonDetection : MonoBehaviour
{  
    public SelectButtonUI selectButtonUI;
    public InputDevice rightController;
    public InputDevice leftController;
    [SerializeField]
     private XRNode xRNode = XRNode.RightHand;
    //private XRNode xRNode;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice device;
    //to avoid repeat readings
    // private bool triggerIsPressed;
    // private bool primaryButtonIsPressed;
    // private bool primary2DAxisIsChosen;
    // private Vector2 primary2DAxisValue = Vector2.zero;
    // private Vector2 prevPrimary2DAxisValue;
    public bool rightGripIsPressed =false;
    public bool leftGripIsPressed = true;
    public bool rightTriggerIsPressed = false;
    public bool leftTriggerIsPressed = true;
    public bool fortrigger = false;
    public bool forgrip = false;
    // public TextMeshProUGUI tmp;
    // Update is called once per frame
    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xRNode, devices);
        device = devices.FirstOrDefault();
    }
    void OnEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
    }
    void Update()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
        // capturing trigger button press and release    
        bool triggerButtonValue = false;

        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue && fortrigger)
        {
            // rightTriggerIsPressed = true;
            Debug.Log("entered");
            //tmp.text = $"TriggerButton activated {triggerButtonValue} on {xRNode}";
            if(xRNode == XRNode.RightHand && !rightTriggerIsPressed)
            {
                rightTriggerIsPressed = true;
                selectButtonUI.RightPressedCoroutine();
                xRNode = XRNode.LeftHand;
                GetDevice();
                Debug.Log(xRNode);
                fortrigger = false;
                
                
            }
            else if(!leftTriggerIsPressed && xRNode == XRNode.LeftHand){
                leftTriggerIsPressed = true;
                selectButtonUI.LeftPressedCoroutine();
                xRNode = XRNode.RightHand;
                GetDevice();
                fortrigger = false;
            }
            
        }
        float gripValue;
        InputFeatureUsage<float> gripUsage = CommonUsages.grip;
        
            if (device.TryGetFeatureValue(gripUsage, out gripValue) && gripValue > 0 && forgrip)
            {
                // rightGripIsPressed = true;
                // tmp.text = $"Grip value {gripValue} activated on {xRNode}";
                if(xRNode == XRNode.RightHand && !rightGripIsPressed)
                   {
                    rightGripIsPressed = true;
                    forgrip = false;
                    selectButtonUI.RightGrabPressedCoroutine();
                    xRNode = XRNode.LeftHand;
                    GetDevice();
                   }
                else if(xRNode == XRNode.LeftHand && !leftGripIsPressed){
                    leftGripIsPressed = true;
                    selectButtonUI.LeftGrabPressedCoroutine();
                // xRNode = XRNode.LeftHand;
                // GetDevice();
                }
            }
    }
}