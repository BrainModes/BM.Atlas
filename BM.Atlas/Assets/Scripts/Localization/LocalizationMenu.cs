using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    private LocalizationManager localizationManager;
    private radioButtonGroup radioButtonGroup;
    private bool menuGenerated = false;

    void Start()
    {
        localizationManager = LocalizationManager.instance;
        radioButtonGroup = gameObject.GetComponent<radioButtonGroup>();
    }

    void Update() 
    {
        if (localizationManager.GetIsReady() && !menuGenerated)
        {
            GenerateLocalizationMenu();
            menuGenerated = true;
        }
    }

    void GenerateLocalizationMenu()
    {
        string[] languageNames = localizationManager.GetListOfLanguages();
        if (languageNames.Length <= 1) return;
        foreach (string langName in languageNames)
        {
            GenerateLocalizationButton(langName);
        }
    }

    void GenerateLocalizationButton(string langName)
    {
        GameObject newButton = Instantiate(buttonPrefab, gameObject.transform);
        Button buttonComp = newButton.GetComponent<Button>();
        buttonComp.onClick.AddListener(() => localizationManager.SetLanguage(langName));
        buttonComp.onClick.AddListener(() => radioButtonGroup.buttonGroupClick(buttonComp));
        Text buttonText = newButton.GetComponentInChildren<Text>();
        buttonText.text = localizationManager.GetLanguageDisplayName(langName);
    }
}
