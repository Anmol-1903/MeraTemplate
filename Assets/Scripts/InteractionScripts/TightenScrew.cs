using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace BlackOut
{
    public class TightenScrew : Interaction
    {
        public ScrewTool screw;
        public ScrewdriverTool screwdriverTool;
        bool _startCheckingvalue = false;
        public override bool isInteractionCompleted { get ; set ; }

        public override void InteractionCompleted()
        {
            screw.gameObject.GetComponent<XRSocketInteractor>().enabled = false;

            _startCheckingvalue = false;
            screwdriverTool.knob.value = 0;
        }

        public override void StartInteraction()
        {
            _startCheckingvalue = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(_startCheckingvalue){
                if(screw.value <= -5f){
                    isInteractionCompleted = true;
                    _startCheckingvalue = false;
                    StepManager.instance?.MoveToNextInteraction();
                }
            }
        }
    }
}
