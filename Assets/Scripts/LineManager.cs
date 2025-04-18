using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public LineRenderer line;
    public bool StartLine = false;
    public GameObject[] GameObjects;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>(); // Ensures LineRenderer is always present
        line.positionCount = GameObjects.Length;
    }

    // Update is called once per frame
    void Update()
    {
        // line.SetPosition(0,_ropeEnd1.transform.position);
        // line.SetPosition(1,gameObject.transform.position);
        // line.SetPosition(2,_ropeEnd2.transform.position);


        if (StartLine)
        {
            for (int i = 0; i < GameObjects.Length; i++)
            {
                line.SetPosition(i, GameObjects[i].transform.position);

            }
        }
    }
}
