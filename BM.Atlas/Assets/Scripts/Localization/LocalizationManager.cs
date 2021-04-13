using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TranslationLoader))]
public class LocalizationManager : MonoBehaviour, IEventSystemHandler
{
    public static LocalizationManager instance;
    private Dictionary <string, string> localizedText;

    private Dictionary <string, Dictionary <string, string>> translations;
    private bool isReady = false;

    #if UNITY_WEBGL
    private string missingTextString = " ";
    #else
    private string missingTextString = "No Translation Found";
    #endif

    private bool languageToggle = false;
    
    void Awake()
    {
        
        // there can only be one, and it lives forever
        if (instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }
    }

    void Update() {
      

    }

    //Handles message "LoadTranslations" and incoming translationsDictionary
    //Is sent by a component which loads translations on the same GameObject
    public void LoadTranslations(Dictionary<string, Dictionary<string, string>> translationsDict) {
        translations = translationsDict;
        if (GetListOfLanguages().Contains("english")) {
            SetLanguage("english");
        } else {
            SetLanguage(GetListOfLanguages()[0]);
        }
        isReady = true;
     
    }

    public string[] GetListOfLanguages() 
    {
        return translations.Keys.ToArray();
    }

    public bool GetLanguageToggle() 
    {
        return languageToggle;
    }

    public string GetLanguageDisplayName(string langName)
    {
        if (translations[langName]["text_anchor"] == "right")
        {
            return ReverseString(translations[langName]["display_language"]);
        }
        else
        {
            return translations[langName]["display_language"];
        }
    }

    public void SetLanguage(string langName)
    {
        localizedText = translations[langName];
        languageToggle = !languageToggle;
    }

    public bool IsCurrentLanguageLeftToRight() 
    {
        return localizedText["text_anchor"] == "left";
    }


    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key)){
            result = localizedText[key];

        }
        return result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }

    private string ReverseString(string s) 
    {
        char[] array = new char[s.Length];
        int forward = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            array[forward++] = s[i];
        }
        return new string(array);
    }
    
}
