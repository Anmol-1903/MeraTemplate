using TMPro;
using UnityEngine;
public class CharacterLimit : MonoBehaviour
{
    TMP_InputField inputField;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(CheckLimit);
    }

    private void CheckLimit(string text)
    {
        if (text.Length >= 4)
        {
            inputField.text = text.Substring(0, 4);
            inputField.selectionFocusPosition = 4;
            inputField.selectionAnchorPosition = 4;
        }
    }
}