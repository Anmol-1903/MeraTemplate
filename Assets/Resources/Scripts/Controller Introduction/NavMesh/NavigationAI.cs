using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavigationAI : MonoBehaviour
{
    //Drop the the navMeshObject
    public GameObject Player;
    NavMeshAgent myNavMeshAgent;
    LineRenderer myLineRenderer;
    public GameObject[] pathPoints;
    Vector3 currentWP;
    // Start is called before the first frame update
    void Start()
    {
        //myNavMeshAgent = Player.GetComponentInChildren<NavMeshAgent>();
        //myLineRenderer = Player.GetComponentInChildren<LineRenderer>();

        myNavMeshAgent = Player.GetComponent<NavMeshAgent>();
        myLineRenderer = Player.GetComponent<LineRenderer>();

        myLineRenderer.startWidth = 0.5f;
        myLineRenderer.endWidth = 0.5f;
        myLineRenderer.positionCount = 0;

        currentWP = pathPoints[0].transform.position;
        //SetDestination();
        myNavMeshAgent.SetDestination(currentWP);
    }


    void SetDestination()
    {

    }
    // Update is called once per frame
    void Update()
    {


        if (myNavMeshAgent.hasPath)
        {

            DrawPath();
        }

    }

    private void SetDestination(Vector3 target)
    {

    }

    void DrawPath()
    {
        //Draw Path 
        Debug.Log(myNavMeshAgent.remainingDistance);


        myLineRenderer.positionCount = myNavMeshAgent.path.corners.Length;
        myLineRenderer.SetPosition(0, Player.transform.position);

        if (myNavMeshAgent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < myNavMeshAgent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(myNavMeshAgent.path.corners[i].x, myNavMeshAgent.path.corners[i].y + 0.8f, myNavMeshAgent.path.corners[i].z);
            myLineRenderer.SetPosition(i, pointPosition);
        }
    }

    
}
