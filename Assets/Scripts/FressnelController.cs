using UnityEngine;

public class FressnelController : MonoBehaviour
{
    public Material material;
    public float minValue = 0;
    public float maxValue = 1;
    public float speed = 1;

    private float currentValue;
    private bool isIncreasing = true;
    void Start()
    {
        currentValue = minValue;
    }

    void Update()
    {
        if (isIncreasing)
        {
            currentValue += speed * Time.deltaTime;
            if (currentValue >= maxValue)
            {
                currentValue = maxValue;
                isIncreasing = false;
            }
        }
        else
        {
            currentValue -= speed * Time.deltaTime;
            if (currentValue <= minValue)
            {
                currentValue = minValue;
                isIncreasing = true;
            }
        }

        material.SetFloat("Vector1_E0355572", currentValue);
    }
}