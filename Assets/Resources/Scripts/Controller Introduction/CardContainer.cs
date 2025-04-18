
using UnityEngine;
public class CardContainer : MonoBehaviour
{
    [SerializeField] private LevelSO levelSO;
    
    private void Awake() {
        InstantiateCards();
    }
  
    private void InstantiateCards(){
        if(levelSO.cards != null && levelSO.cards.Length > 0){
            foreach (Card card in levelSO.cards){
                var temp = Instantiate(levelSO.cardTemplate, transform);
                temp.GetComponent<CardTemplate>().SetCardValues(card.sprite, card.title, card.description, card.sceneToLoad, -1);
            }
        }
    }
}