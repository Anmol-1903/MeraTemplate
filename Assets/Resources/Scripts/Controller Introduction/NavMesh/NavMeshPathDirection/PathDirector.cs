using UnityEngine;
using UnityEngine.AI;

public class PathDirector : MonoBehaviour
{
    public static PathDirector instance;
    public Transform start;
    private Vector3 end;
    public GameObject DirectionArrow;
    public float prefabSpacing; // Spacing between each prefab

    private NavMeshPath path;
    private GameObject arrowsContainer;
    public bool showArrows = false;
    public Vector3 startPosVector;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        
        //startPosVector = new Vector3(start.position.x, 0, start.position.z);
        path = new NavMeshPath();
        arrowsContainer = new GameObject("ArrowsContainer");
        //arrowsContainer.layer = 18;
    }
    public void SetEndValue(Vector3 EndPosition)
    {
        end = EndPosition;
        startPosVector = new Vector3(start.position.x, 0, start.position.z);
        CalculatePath();
        MovePrefabAlongPath();
    }

    private void Update()
    {
        if (showArrows)
        {
            CalculatePath();
            MovePrefabAlongPath();
        }
    }

    //private void SetStartPoint()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        start.position = hit.point;
    //    }
    //} // when starting point is based on the raycasting use this

    private void CalculatePath()
    {
        NavMesh.CalculatePath(start.position, end, NavMesh.AllAreas, path);
    }

    private void MovePrefabAlongPath()
    {
        ClearArrows();

        if (path.corners.Length < 2)
        {
            return;
        }

        float distance = 0f;

        for (int i = 1; i < path.corners.Length; i++)
        {
            Vector3 direction = path.corners[i] - path.corners[i - 1];
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            float segmentDistance = Vector3.Distance(path.corners[i - 1], path.corners[i]);
            while (distance < segmentDistance)
            {
                Vector3 position = path.corners[i - 1] + (direction.normalized * distance);
                var arrowPrefab =   Instantiate(DirectionArrow, position, rotation, arrowsContainer.transform);
                //arrowPrefab.layer = 18;
                distance += prefabSpacing;
            }
            distance -= segmentDistance;
        }
    }

    public void ClearArrows()
    {
        foreach (Transform child in arrowsContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void DestoryInstance()
    {
        instance = null;
    }
}
