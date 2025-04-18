using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrabEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text1;
    public TMP_Text text2;
    public AudioSource audioSource;
    public AudioClip audioClip, HAudioClip;
    public string nText1 = "<color=yellow>Grab</color> From Wall";
    public string nText2 = "Reach up and grab! Practice picking up objects from the wall to become proficient in VR object interaction.";

    // Start is called before the first frame update
    public void OnSnappingHammer()
    {
        UpdateTexts(nText1, nText2);
        audioSource.Stop();
        if (AudioSelection.selectedLanguage == Language.English)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            audioSource.PlayOneShot(HAudioClip);
        }
    }

    public void UpdateTexts(string nText1, string nText2)
    {
        // Update the text of the TMP components
        text1.text = nText1;
        text2.text = nText2;
    }

}
