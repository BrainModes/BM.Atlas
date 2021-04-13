using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactions;
using HedgehogTeam.EasyTouch;
public class StartApp : MonoBehaviour
{
    private static StartApp instance;
    public bool showIntroMsgOnStart = true;
    public bool showIntroMsgWhenIdle = true;
    private List<Canvas> accordionMenu = new List<Canvas>();

    public Canvas introCanvas;
    private CanvasGroup canvasGroup;
    private GameObject brain;
    private GameObject scalp;
    private MouseInteractions mouseInteractions;
    private TouchInteractions touchInteractions;
    private EasyTouch easyTouch;
    private float t = 0f;
    private bool introMsgOn = false;
    private bool touchToDismissMsgHasOccurred = false;

    public float touchClickDelaySeconds = 2.0f;
    private bool canTouchClick = false;

    void Awake()
    {
        // there can only be one, and it lives forever
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        FindCanvasGroup();
        if (showIntroMsgOnStart || showIntroMsgWhenIdle) {
            FindBrainInteractionObjects();
            if (showIntroMsgOnStart) {
                introMsgOn = true;
                TurnOffBrainInteractions();
            }
            else {
                canvasGroup.alpha = 0;
            }
        } else {
            canvasGroup.alpha = 0;
        }

        StartCoroutine(touchClickDelayTimer());
        
    }
    void Update()
    {
        if ((showIntroMsgOnStart || showIntroMsgWhenIdle)) {
            if(canTouchClick){
                if (Input.touchCount > 0 || Input.GetMouseButton(0) || touchToDismissMsgHasOccurred) {
                    touchToDismissMsgHasOccurred = true;
                    if (introMsgOn) {
                        t += Time.deltaTime;
                        StartCoroutine(FadeOutAndTurnOffIntroMsg());
                        TurnOnBrainInteractions();
                        introMsgOn = false;
                    }
                }
            }
        }

        

    }

    public bool GetIntroMessageOn()
    {
        return introMsgOn;
    }

    private IEnumerator FadeOutAndTurnOffIntroMsg()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f) {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0f, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        introCanvas.gameObject.SetActive(false);
        yield return null;
       
        
    }

    void FindCanvasGroup() {
        canvasGroup = introCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("StartApp: Canvas Group Missing from Intro UI");
        }
    }

    void FindBrainInteractionObjects() {
        GameObject[] menus = GameObject.FindGameObjectsWithTag("AccordionMenu");
        if (menus.Length < 1) {
            Debug.LogError("StartApp: Accordion Menu GameObject does not have tag 'AcccordionMenu' or is missing from the scene");
        }
        GameObject menu = menus[0];
        if (menu.activeInHierarchy == true) {
            foreach (Transform child in menu.transform)
            {
                accordionMenu.Add(child.gameObject.GetComponent<Canvas>());
            }
        }

        if (accordionMenu.Count == 0) {
            Debug.LogError("StartApp: No active Accordion Menu found.");
        }

        brain = GameObject.FindGameObjectWithTag("Brain");
        if (brain == null) {
            Debug.LogError("StartApp: Brain/Model GameObject does not have tag 'Brain' or is missing from the scene");
        }
        scalp = GameObject.FindGameObjectWithTag("Scalp");
        if (scalp == null)
        {
            Debug.LogError("StartApp: Scalp GameObject does not have tag 'Scalp' or is missing from the scene");
        }
        GameObject cameraManager = GameObject.FindGameObjectWithTag("CameraManager");
        if (cameraManager == null) {
            Debug.LogError("StartApp: CameraManager GameObject does not have tag 'CameraManager' or is missing from the scene");
        }
        mouseInteractions = cameraManager.GetComponent<MouseInteractions>();
        if (mouseInteractions == null) {
            Debug.LogError("StartApp: CameraManager GameObject does not have component MouseInteractions");
        }
        touchInteractions = cameraManager.GetComponent<TouchInteractions>();
        if (touchInteractions == null) {
            Debug.LogError("StartApp: CameraManager GameObject does not have component TouchInteractions");
        }
        easyTouch = cameraManager.GetComponent<EasyTouch>();
        if (easyTouch == null) {
            Debug.LogError("StartApp: CameraManager GameObject does not have component EasyTouch");
        }
    }

    void TurnOffBrainInteractions() {
        foreach(Canvas c in accordionMenu) {
            c.enabled = false;
        }
        SetLayerRecursively(brain, 2); //IgnoreRaycast layer
        SwitchMouseTouchInteraction(false);
        easyTouch.enable = false;
    }

    void TurnOnBrainInteractions() {
        foreach (Canvas c in accordionMenu){
            c.enabled = true;
        }
        SetLayerRecursively(brain, 14); //Brain layer
        SetLayerRecursively(scalp, 0); // Scalp on Default layer
        SwitchMouseTouchInteraction(true);
        easyTouch.enable = true;
    }

    public void ShowIntroMsgOnIdle() {
        if (showIntroMsgWhenIdle) {
            introMsgOn = true;
            TurnOffBrainInteractions();
            canvasGroup.alpha = 1;
            touchToDismissMsgHasOccurred = false;
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer) {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    void SwitchMouseTouchInteraction(bool enabled) {
        if (enabled == true)
        {
            mouseInteractions.MouseInteractionsEnabled(true);
            touchInteractions.TouchInteractionsEnabled(true);
        }
        else
        {
            mouseInteractions.MouseInteractionsEnabled(false);
            touchInteractions.TouchInteractionsEnabled(false);
        }
    }

    IEnumerator touchClickDelayTimer(){

        yield return new WaitForSeconds(touchClickDelaySeconds);
        canTouchClick = true;
        
    }
    


}
