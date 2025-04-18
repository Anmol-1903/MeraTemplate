using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class Card_flip : MonoBehaviour
{
    public Sprite frontSprite; // Front sprite of the card
    public Sprite backSprite;
    public Sprite whiteSprite;// Back sprite of the card
    public int maxFlippedCards = 3; // Maximum number of cards that can be flipped
    public GameObject rightHighlightedTrigger;
    private int flippedCards = 0;
    public GameObject cardcanva; // Reference to the card canvas
    public GameObject congrats; // Reference to the congrats panel
    private GameObject Card1;
    private Quaternion endRotation = Quaternion.Euler(0, 360f, 0);
    private Quaternion initailRotation = Quaternion.Euler(0f, 0f, 0f);
    public void Flip(Image cardImage)
    {
        
        if (Card1 == null)
        {
            //StartCoroutine(FlipAnimation(cardImage.gameObject, endRotation, 1, 0));
            Card1 = cardImage.gameObject;
            if (!Card1.GetComponent<InnovateCard>())
            {
                StartCoroutine(FlipAnimation(cardImage.gameObject, endRotation, 1, 1));
                //Card1.GetComponent<Button>().interactable = false;
                StartCoroutine(FlipAnimation(Card1, initailRotation, 0, 2));
                Card1.GetComponent<Button>().interactable = true;
                Card1 = null;

            }
            else
            {
                //Card1.GetComponent<Button>().enabled = false;
                StartCoroutine(FlipAnimation(Card1, endRotation, 1, 0));
            }
        }
        else// card1 != null
        {
            if (cardImage.gameObject.GetComponent<InnovateCard>())
            {
                cardImage.GetComponent<Button>().enabled = false;
                StartCoroutine(FlipAnimation(cardImage.gameObject, endRotation, 1,0));
                StartCoroutine(DisableCardCanvasAfterDelay(1.5f));
            }
            else
            {
                StartCoroutine(FlipAnimation(cardImage.gameObject, endRotation, 1, 1));
                StartCoroutine(FlipAnimation(Card1, initailRotation, 0,2));
                Card1.GetComponent<Button>().interactable = true;
                Card1 = null;
                StartCoroutine(FlipAnimation(cardImage.gameObject, initailRotation, 0,2));
                cardImage.gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    private IEnumerator FlipAnimation(GameObject card,Quaternion typeRotation,int rotationType,int imageType)
    {
        card.GetComponent<Button>().interactable = false;
        if (rotationType == 0)
            yield return new WaitForSeconds(1f);

        Quaternion startRotation = card.transform.rotation;
        
        card.transform.DOLocalRotateQuaternion(typeRotation, 0.5f);
        
        card.transform.rotation = typeRotation;
        
        // Change the sprite to the front sprite
        Image cardImage = card.GetComponent<Image>();
        if (cardImage != null && rotationType == 1 && imageType == 0)
        {
            cardImage.sprite = frontSprite;
        }
        else if (imageType == 1)
            cardImage.sprite = whiteSprite;
        else
            cardImage.sprite = backSprite;
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator DisableCardCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        cardcanva.SetActive(false);
        congrats.SetActive(true);
        rightHighlightedTrigger.SetActive(false);
        AudioManager.Instance.PlayAudioClip(congrats.name);
    }
}



