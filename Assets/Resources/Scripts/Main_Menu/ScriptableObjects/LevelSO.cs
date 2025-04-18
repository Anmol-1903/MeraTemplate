using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "ScriptableObjects/LevelSO")]
public class LevelSO : ScriptableObject
{
    public GameObject cardTemplate;
    public Card[] cards;
}

[Serializable]
public class Card{
    public string key;
    public Sprite sprite;
    public string title;
    
    [TextArea(2, 4)]
    public string description;
    public string sceneToLoad;
}