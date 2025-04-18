using UnityEngine;

public class PathManager : MonoBehaviour
{
    public GameObject[] endPoints;
    private int currentEndPointIndex = 0;
    public PathDirector pathDirector;
    public Transform startPosition;
    Vector3 endPointPosition;
    public GameObject starHighlight;
    public bool showPath = true;
    public GameObject navPathDirector;


    private void Update()
    {
        if(showPath){

            if (currentEndPointIndex < endPoints.Length)
            {
                endPointPosition = endPoints[currentEndPointIndex].transform.position;
                pathDirector.SetEndValue(endPointPosition);
                if(currentEndPointIndex == 2)
                {
                    starHighlight.SetActive(true);
                }
                
                //if (HasReachedEndPoint(startPosition.position, endPoints[currentEndPointIndex].transform.position, 1.5f))
                //if (HasReachedEndPoint())
                //{
                //    navPathDirector.SetActive(false);
                //    showPath = false;
                //    Debug.Log("Reached endpoint " + currentEndPointIndex);
                //    currentEndPointIndex++;
                    
                //    if (currentEndPointIndex < endPoints.Length)
                //    {
                //        endPointPosition = endPoints[currentEndPointIndex].transform.position;
                //        // if (currentEndPointIndex == 1) 
                //        // {
                //        //     starHighlight.SetActive(false);
                //        // }                    
                //    }
                //    PathDirector.instance.ClearArrows();
                //}
            
            }
            else
            {
                Debug.Log("All end points reached.");
                
            }
        }
    }


        // endPointPosition = endPoints[currentEndPointIndex].transform.position;
        // Debug.Log(startPosition);
        // if (currentEndPointIndex < endPoints.Length)
        // {
        //    Debug.Log("entered 1st if loop");
        //     if(HasReachedEndPoint(startPosition, endPointPosition, 5f) && currentEndPointIndex == 0 )
        //     {
        //         Debug.Log("incrementiong the index");
        //         currentEndPointIndex++;
        //         if(currentEndPointIndex <= endPoints.Length ) // Check if there are more endpoints
        //         {
        //             endPointPosition = endPoints[currentEndPointIndex].transform.position;
        //         }
                
        //     }
        //      else if(HasReachedEndPoint(startPosition, endPointPosition, 5f) && currentEndPointIndex ==1 ) //&& )
        //     {
        //         Debug.Log("incrementiong the index twice");
        //         currentEndPointIndex++;
        //         if(currentEndPointIndex <= endPoints.Length ) // Check if there are more endpoints
        //         {
        //             endPointPosition = endPoints[currentEndPointIndex].transform.position;
        //         }
        //     }
        //     pathDirector.SetEndValue(endPointPosition);          
        // }
        
        // else
        // {
        //     Debug.Log("All end points reached.");
        // }
        
        
    // }
    public bool HasReachedEndPoint()
    {
        if (showPath)
            return false;
        else
            return true;
        //float distance = Vector3.Distance(startPosition, endPointPosition);
        
        // Debug.Log(distance <= tolerance);
       // return distance <= tolerance;
    }
    public void ShowNextPath()
    {
        navPathDirector.SetActive(true);
        showPath = true;
    }
    public void HideShowPath()
    {
        showPath = false;
       // if (HasReachedEndPoint())
        //{
            navPathDirector.SetActive(false);
            showPath = false;
            Debug.Log("Reached endpoint " + currentEndPointIndex);
            currentEndPointIndex++;

            if (currentEndPointIndex < endPoints.Length)
            {
                endPointPosition = endPoints[currentEndPointIndex].transform.position;
                // if (currentEndPointIndex == 1) 
                // {
                //     starHighlight.SetActive(false);
                // }                    
            }
            PathDirector.instance.ClearArrows();
        //}
    }
     // public void SetNextEndPoint()
    // {
    //     // if (currentEndPointIndex < endPoints.Length)
    //     // {
    //     //     endPointPosition = endPoints[currentEndPointIndex].transform.position;
    //     //     currentEndPointIndex++;
            
    //     // }
    //     // else
    //     // {
    //     //     Debug.Log("All end points reached.");
    //     // }
    // }
}
