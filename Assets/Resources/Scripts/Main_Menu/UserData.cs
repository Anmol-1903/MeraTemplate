using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UserData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button Button { get; private set; }
    public  TextMeshProUGUI nameText;
    public TextMeshProUGUI idText;

    private void Awake() {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(UserButtonClicked);
    }

    public void SetTextColor(Color color){
        nameText.color = color;
        idText.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetTextColor(Color.black);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTextColor(Color.white);
    }

    public string GetName(){
        return nameText.text;
    }
    public string GetId(){
        return idText.text;
    }

    public void SetData(string name, string empId){
        nameText.text = name;
        idText.text = empId;
    }
    private void UserButtonClicked(){
        MenuNavigation.instance?.EnterPassword(idText.text);
    }

}