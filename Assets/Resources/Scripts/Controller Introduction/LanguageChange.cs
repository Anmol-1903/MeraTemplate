using System;
using UnityEngine;
public class LanguageChange : MonoBehaviour
{
    public Language Language;

    private void OnEnable() {
        Language = (Language) Enum.Parse(typeof(Language), PlayerPrefs.GetString("Lang", Language.English.ToString()));
    }

    public void Selectlanguage(int lang)
    {
        Language = (Language)lang;
        AudioSelection.selectedLanguage = (Language)lang;
        PlayerPrefs.SetString("Lang", Language.ToString());
    }
}
public enum Language{
    English = 0,
    Hindi = 1
}