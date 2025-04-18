using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class ProjectTitle : MonoBehaviour
{

    [TextArea(1, 3)]
    public string title;
    public Sprite sprite;

    [SerializeField] private List<TextMeshProUGUI> headerTexts;
    [SerializeField] private List<Image> images;

    public void OnValidate()
    {
        if (headerTexts != null & headerTexts.Count > 0)
        {
            if (title != "")
            {
                foreach (var text in headerTexts)
                {
                    text.text = title;
                    text.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (var text in headerTexts)
                {
                    text.text = "";
                    text.gameObject.SetActive(false);
                }
            }
        }

        if (images != null & images.Count > 0)
        {
            if (sprite != null)
            {
                foreach (var image in images)
                {
                    image.sprite = sprite;
                    image.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (var image in images)
                {
                    image.sprite = null;
                    image.gameObject.SetActive(false);
                }
            }
        }
    }
}