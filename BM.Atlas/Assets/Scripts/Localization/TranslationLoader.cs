using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using Utils;

// All other components on this gameObject will 
// be sent to the translation dictionary once it is loaded
// via msg LoadTranslations
[RequireComponent(typeof(LocalizationManager))]
public class TranslationLoader : MonoBehaviour
{

    // get display name for language (need to respect rtl here) and capitalise the first letter
    // be aware that order is important when dealing with rtl + capitalisation

    public string getURL = "https://amazooglesoft.com/WEBGL/Trans/";

    public enum TranslationSet{
        DK_DES,
        VON_ECONOMO,
        BM_VIS_DX
    }
    private enum WebLanguage { 
        english,
        arabic, 
        hebrew, 
        german, 
        polish, 
        spanish
    }

    public TranslationSet theTranslationSet;


    private string translationsFolderName = "translations";

    public GameObject errorHandler;
    private ErrorHandler errorMsgHandler;
    
    private Dictionary<string, Dictionary<string, string>> translations = new Dictionary<string, Dictionary<string, string>>();
    private List<WebLanguage> DK_DES_WebLanguages = new List<WebLanguage> { WebLanguage.english, WebLanguage.arabic, WebLanguage.hebrew, WebLanguage.german, WebLanguage.polish, WebLanguage.spanish };
    private List<WebLanguage> VON_ECONOMO_WebLanguages = new List<WebLanguage> { WebLanguage.english };
    private List<WebLanguage> BM_VIS_DX_WebLanguages = new List<WebLanguage> { WebLanguage.english, WebLanguage.german };

    private int coroutineCounter;

    void Start () 
    {
        if (errorHandler == null)
        {
            errorHandler = GameObject.FindGameObjectsWithTag("Errors")[0];
        }
        errorMsgHandler = errorHandler.GetComponent<ErrorHandler>();
        LoadTranslationSet();
    }

    private string GetFileNamePrefix() {
        string prefix = "";
        switch ((int)theTranslationSet)
        {
            case 0:
                prefix = "AtlasText_";
                break;
            case 1:
                prefix = "VonEconomoAtlasText_";
                break;
            case 2:
                prefix = "BrainModesVisualizerDX_";
                break;
            default:
                prefix = "AtlasText_";
                break;
        }
        return prefix;
    }

    private Regex GetRegex() {

        // filename must begin with "AtlasText_" or "VonEconomoAtlasText_" or "BrainModesVisualizerDX_"
        // can be followed by any number of characters between a and z
        // must be followed by .json
        // and then nothing else!
        return new Regex("^" + GetFileNamePrefix() + "[a-z]*\\.json$");
    }

    private string GetLanguageIdentifier(string languageFileName)
    {
        return languageFileName.Replace(GetFileNamePrefix(), "").Replace(".json", "");
    }

    private void LoadTranslationSet()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        GetWebHostedTranslations();
#else
        GetLocalTranslations();
#endif
    }

    private void GetWebHostedTranslations() {
        List<WebLanguage> languages;
        switch (theTranslationSet)
        {
            case TranslationSet.VON_ECONOMO:
                languages = VON_ECONOMO_WebLanguages;
                break;
            case TranslationSet.BM_VIS_DX:
                languages = BM_VIS_DX_WebLanguages;
                break;
            case TranslationSet.DK_DES:
                languages = DK_DES_WebLanguages;
                break;
            default:
                languages = DK_DES_WebLanguages;
                break;
        }
        coroutineCounter = languages.Count;
        foreach (WebLanguage language in languages)
        {
            StartCoroutine(LoadHostedFile(language.ToString(), getURL + GetFileNamePrefix() + language.ToString()));
        }
    }

    private void GetLocalTranslations() {
        string folderPath = LocalStreamingAssetLoader.GetStreamingAssetsPath();
        List<string> translationFilePaths = GetTranslationPaths(folderPath);
        foreach (string language in translationFilePaths)
        {
            Dictionary<string, string> translation = GetTranslation(language);
            translations.Add(language, translation);
        }
        Debug.Log("Sending LoadTranslations Message from TranslationLoader. ");
        gameObject.SendMessage("LoadTranslations", translations);
    }

    private List<string> GetTranslationPaths(string folderPath)
    {
        // Debug.Log("In GetTranslationPaths, folderPath is: " + folderPath);
        string[] allFilePaths = Directory.GetFiles(folderPath + "/" + translationsFolderName);
        List<string> translationFilePaths = new List<string>();
        foreach (string path in allFilePaths)
        {
            // Using forward and backward slashes to split so it works for both
            // Windows and iOS paths.
            string[] fileParts = path.Split('/');
            fileParts = fileParts[fileParts.Length - 1].Split('\\');
            string fileName = fileParts[fileParts.Length - 1];

            if (GetRegex().IsMatch(fileName))
                {
                    string langName = GetLanguageIdentifier(fileName);
                    translationFilePaths.Add(langName);
                }
        }
        return translationFilePaths;
    }

    private Dictionary<string, string> GetTranslation(string language)
    {
        Dictionary<string, string> translation = new Dictionary<string, string>();
        string path = translationsFolderName + "/" + GetFileNamePrefix() + language + ".json";
        Debug.Log(path);
        string jsonText = LocalStreamingAssetLoader.GetStreamingAsset(path);
        translation = ReadJson(jsonText);
        return translation;
    }

    private IEnumerator LoadHostedFile(string langName, string filePath)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(filePath + ".json"))
        {
            yield return webRequest.SendWebRequest();

            translations.Add(langName, ReadJson(webRequest.downloadHandler.text));
            coroutineCounter -= 1;
            if (coroutineCounter == 0)
            {
                Debug.Log("send msg");
                gameObject.SendMessage("LoadTranslations", translations);
            }
        }
    }

    private Dictionary<string, string> ReadJson(string json)
    {
        Dictionary<string, string> text = new Dictionary<string, string>();

        if (json != null)
        {
            LocalizationData loadedData = new LocalizationData();
            try
            {
                loadedData = JsonUtility.FromJson<LocalizationData>(json);

            }
            catch (ArgumentException)
            {
                string errorMsg = "The following JSON file was unable to be parsed, please check the contents in a json validator: " + json;
                errorMsgHandler.DisplayErrorMessage(errorMsg);
            }
            if (loadedData.items != null) 
            {
                for (int i = 0; i < loadedData.items.Length; i++)
                {

                    text.Add(loadedData.items[i].key, loadedData.items[i].value);
                    //  Debug.Log("Data loaded, dictionary contains" + localizedText.Count + " entries");
                }
            } 
        }
        else
        {
            string errorMsg = "One of the json files in the StreamingAssets folder contains contains no data";
            errorMsgHandler.DisplayErrorMessage(errorMsg);

        }

        return text;
    }

}
