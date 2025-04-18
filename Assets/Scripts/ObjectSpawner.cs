using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
        }
    }

    public GameObject SpawnObject(GameObject Prefab, Vector3 position,Quaternion rotation, Transform parent, Vector3 offset, Quaternion rotationOffset)
    {
        // Instantiate the object with the correct parent
        GameObject SpawnedObject = Instantiate(Prefab, parent);

        // Apply position offset
        SpawnedObject.transform.position = new Vector3(
            position.x + offset.x,
            position.y + offset.y,
            position.z + offset.z
        );

        // Apply rotation offset
        SpawnedObject.transform.rotation = rotation * rotationOffset;
        return SpawnedObject;
    }
}
