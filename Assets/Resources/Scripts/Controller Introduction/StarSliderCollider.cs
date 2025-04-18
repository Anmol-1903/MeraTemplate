using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
public class StarSliderCollider : MonoBehaviour
{
    [SerializeField] private float startPoint;
    [SerializeField] private float endPoint;
    [SerializeField] private int numberOfStars = 5;
    [SerializeField] private int requiredStars = 3;
    [SerializeField] private int collectedStars = 0;
    private float[] points;
    [SerializeField] private GameObject[] Stars = new GameObject[5];
    public GameObject RatingUI,selfAssementFalse;
    public PathManager pathManager;
    public CapsuleCollider exitCollider;
    // Start is called before the first frame update
    void Awake()
    {
        //print(endPoint +"      " +startPoint);
        points = new float[numberOfStars];
        var interval = (endPoint - startPoint) / (numberOfStars-1);
        for(int i= 0; i < points.Length; i++)
        {
            points[i] = (startPoint + i * interval);
            print(points[i]);
        }
        
    }
    public XRSlider slider;
    public void EnableStars()
    {
        if (slider.value == 0) return;
        
        float value = slider.value;
        int enabledStars = 0;
        for(int i = 0; i < points.Length; i++)
        {
            if (value <= points[i]+0.05f && value >= points[i]-0.05f)
            {
                for (int j = 0; j <= i; j++)
                {
                    Stars[j].SetActive(true);
                    
                }

                // Deactivate stars after the current point
                for (int j = i + 1; j < Stars.Length; j++)
                {
                    Stars[j].SetActive(false);
                }
            }
        }
        foreach(GameObject child in Stars)
        {
            if (child.activeSelf) enabledStars++;
            else break;
        }
        collectedStars = enabledStars;
    }
    public void CollectedStars()
    {
        if (collectedStars >= requiredStars)
        {
            RatingUI.SetActive(true);
            selfAssementFalse.SetActive(false);
            pathManager.ShowNextPath();
            exitCollider.enabled = true;
            AudioManager.Instance.PlayAudioClip(RatingUI.name);
        }
    }
}
