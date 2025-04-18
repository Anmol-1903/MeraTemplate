using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackOut
{

    public class CreateLine : Interaction
    {


        public GameObject[] GameObjects;

        LineManager _line;
        public Material material;
        //bool _interactionStarted = false;
        public override bool isInteractionCompleted { get; set; }

        public override void InteractionCompleted()
        {

        }

        public override void StartInteraction()
        {


            _line.line.startWidth = 0.1f;
            _line.line.endWidth = 0.1f;
            _line.line.material = material;
            _line.StartLine = true;
            isInteractionCompleted = true;
            StepManager.instance?.MoveToNextInteraction();

        }

        // Start is called before the first frame update
        private void Awake()
        {


            _line = GameObjects[0].AddComponent<LineManager>();


        }

        private void OnEnable()
        {
            _line.GameObjects = GameObjects;
        }

        // Update is called once per frame

    }
}
