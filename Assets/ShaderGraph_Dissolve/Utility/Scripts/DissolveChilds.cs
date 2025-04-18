﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DissolveExample
{

    
    public class DissolveChilds : MonoBehaviour
    {   
        private float value;
        private float _val;
        [SerializeField]
        private float _speed;

        private bool isDissolved = false;
        // Start is called before the first frame update
        List<Material> materials = new List<Material>();
        bool PingPong = false;
        void Start()
        {
            var renders = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renders.Length; i++)
            {
                materials.AddRange(renders[i].materials);
            }
        }

        private void OnEnable()
        {
            value = 0;
            //_val = 0;
        }

        private void Reset()
        {
            Start();
            SetValue(0);
        }

        // Update is called once per frame
        void Update()
        {
            if(value < 1.0f)
            {
                value = Mathf.MoveTowards(value, 1, Time.deltaTime * _speed);
                //Debug.Log(1 - value);
                SetValue(1 - value);
            }
            //value = Mathf.PingPong(Time.time * 0.5f, 1f);
            // 1 is completely dissolved
            
            //
            //Debug.Log("Exec");
        }

        

       
        //IEnumerator enumerator()
        //{

        //    //float value =         while (true)
        //    //{
        //    //    Mathf.PingPong(value, 1f);
        //    //    value += Time.deltaTime;
        //    //    SetValue(value);
        //    //    yield return new WaitForEndOfFrame();
        //    //}
        //}

        public void SetValue(float value)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetFloat("_Dissolve", value);
            }
        }
    }
}