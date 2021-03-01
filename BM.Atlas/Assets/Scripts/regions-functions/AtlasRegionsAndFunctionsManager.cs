using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BrainTreeCreator))]
[RequireComponent(typeof(RolloverSelectionEffectsManager))]
[RequireComponent(typeof(BrainRotator))]
public class AtlasRegionsAndFunctionsManager : MonoBehaviour
{
    // This script depends on the objects being named exactly this way. The 
    // names are exactly the same ones as in the Unity hierarchy. As in, 
    // if two objects have the same name listed here, Unity will pick one and 
    // it might be the wrong one.

    // Singleton
    public static AtlasRegionsAndFunctionsManager instance;

    // A List containing the names/keys of regions which contain more
    // subregions.
    List<string> regionNames;

    // This is a Dictionary containing the regions belonging to each
    // brain function. It gets populated on Start.
    Dictionary<string, List<string>> functionRegions;

    private BrainTreeCreator brainTreeCreator;
    private RolloverSelectionEffectsManager effectsManager;
    
    // The main camera of the Atlas.
    // (MainCamera -> MainCameryYPivot -> MainCameraXPivot -> Camera)
    public Camera mainCamera;

    // The Brain Regions tree.
    private TreeNode brain;

    // The Brain Functions tree.
    private TreeNode brainFunctions;

    // The current and previous nodes which were clicked on or rolled over. 
    // Used to coordinate the rollover and click effects.
    private TreeNode currentSelectedNode;
    private TreeNode previousSelectedNode;
    private TreeNode currentRolloverNode;
    private TreeNode previousRolloverNode;

    // The fragment whose collider is hit by the RayCast currently.
    private Renderer currentFragment;

    // Some public elements for the popUp window to work.
    // UI_InfoPopup -> Canvas -> Exhibitioninfobox
    public Animator popUpAnimator;
    // UI_InfoPopup -> Canvas -> Exhibitioninfobox -> TitleText
    public LocalizedText popUpTitleText;
    // UI_InfoPopup -> Canvas -> Exhibitioninfobox -> BodyText
    public LocalizedText popUpBodyText;

    // Only used by findRegionandSubRegions() to debug.
    List<GameObject> atlasRegions = new List<GameObject>();

    // Event for informing buttons to be selected
    // Hiding in inspector as for now we are listening to 
    // this event in code
    [HideInInspector]
    public SelectRegionEvent selectRegionEvent;

    // the mouse position during a mouse up/down event on the scalp
    Vector3 mouseScalpDownPos = new Vector3(0,0,0);
    Vector3 mouseScalpUpPos = new Vector3(0, 0, 0);

    RaycastHit hit;
    LayerMask mask;
    RaycastHit[] hits;
    Ray ray;


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
        if (selectRegionEvent == null)
        {
            selectRegionEvent = new SelectRegionEvent();
        }
        brainTreeCreator = GetComponent<BrainTreeCreator>();
        effectsManager = GetComponent<RolloverSelectionEffectsManager>();

        regionNames = brainTreeCreator.getRegionNames();
        // skip funtion tree if Von Economo Atlas
        if(brainTreeCreator.theAtlas != BrainTreeCreator.AtlasType.VON_ECONOMO){
            functionRegions = brainTreeCreator.getFunctionsDict();
            brainFunctions = brainTreeCreator.buildFunctionsTree();
        }
        
        brain = brainTreeCreator.buildRegionsTree();

        // Set current nodes to something to avoid startup confusion.
        currentSelectedNode = brain;
        previousSelectedNode = null;
        currentRolloverNode = brain;
        previousRolloverNode = null;

        //Set the LayerMask for the Raycast and initialize the Raycast hits array
        mask = LayerMask.GetMask("Brain");
    }

    // Update is called once per frame.
    void Update() 
    {

        // check if rotating for a best view, not idle roation, first, before raycast interaction

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray, 500, mask); //TODO: check if Infinity can be reduced for performance reasons

        // If the raycast hits a collider.
        if (hits.Length > 0) {
            hit = hits[0];

            //Debug.Log("Object hit: " + hit.collider.transform.parent.transform.parent.transform.name);
            TreeNode hitNode = TreeNode.SearchTree(hit.collider.transform.parent.transform.parent.transform.name, brain);
            currentFragment = hit.collider.gameObject.GetComponent<Renderer>();
            
            if (hitNode != null) {
                
                if (Input.GetMouseButtonUp(0) && (Input.mousePosition == mouseScalpDownPos))
                {
                    OnBrainClick(hitNode);
                } 
                else if (!Input.GetMouseButton(0))
                {
                    OnBrainHover(hitNode);  
                }
            }
            else 
            {
                OnHoverOutsideBrain();
            }

        }
        else
        {
            OnHoverOutsideBrain();
        }
        
    }

    public void MouseDownOnScalp()
    {
        //Debug.Log("Setting mouseDownPos to " + Input.mousePosition);
        mouseScalpDownPos = Input.mousePosition;
    }

    public Dictionary<string, List<string>> GetFunctionsRegionsDict() 
    {
        return functionRegions;
    }

    public TreeNode GetBrainTree()
    {
        return brain;
    }

    public void openPopUp(TreeNode thisNode){
        if (thisNode == null)
        {
            return;
        }
        popUpBodyText.key = thisNode._bodyJsonKey;
        popUpTitleText.key = thisNode._titleJsonKey;

        // Debug.Log("open popup. title key: " + thisNode._titleJsonKey);
        // Debug.Log("open popup. body key: " + thisNode._bodyJsonKey);

        if(popUpAnimator.GetBool("isOpen")){
            popUpAnimator.SetTrigger("reTrigger");
           // Debug.Log("UI Retriggered");
        }else{
            popUpAnimator.SetBool("isOpen", true);
        }
    }

    public void closePopUp(){
        popUpAnimator.SetBool("isOpen", false);
        // deselect the menu item currently selected
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        effectsManager.DisableClickEffects(currentSelectedNode);
        effectsManager.DisableRollOverEffects(currentRolloverNode);
        effectsManager.DisableClickEffects(previousSelectedNode);
        effectsManager.DisableRollOverEffects(previousRolloverNode);
        currentSelectedNode = brain;
        previousSelectedNode = null;
        currentRolloverNode = brain;
        previousRolloverNode = null;
    }

    public void buttonClickRegion(string thisNodeKey){
        TreeNode thisNode = TreeNode.SearchTree(thisNodeKey, brain);
        // Debug.Log("Region button clicked with this node: " + thisNode._key);
        OnSelection(thisNode);
    }

    public void buttonClickFunction(string thisNodeKey){
        TreeNode thisNode = TreeNode.SearchTree(thisNodeKey, brainFunctions);
        // Debug.Log("Function button clicked with this node: " + thisNode._key);
        OnSelection(thisNode); 
    }

    private void OnSelection(TreeNode thisNode)
    {
        previousSelectedNode = currentSelectedNode;
        currentSelectedNode = thisNode;
        if (currentSelectedNode != previousSelectedNode)
        {
            effectsManager.DisableClickEffects(previousSelectedNode);
            effectsManager.DisableRollOverEffects(previousRolloverNode);
            effectsManager.DisableRollOverEffects(currentRolloverNode);
            effectsManager.EnableClickEffects(thisNode);
            openPopUp(currentSelectedNode);
            SendMessage("RotateBrainToBestView", currentSelectedNode._bestViewPosition);
            SendMessage("ZoomBrainToBestView", currentSelectedNode._bestFOVScale);
            previousRolloverNode = null;
        }
    }

    private void OnHoverOutsideBrain()
    {
        if (currentSelectedNode != currentRolloverNode) 
        {
            effectsManager.DisableRollOverEffects(currentRolloverNode);
        }
        previousRolloverNode = null;
        currentRolloverNode = brain;
    }

    private void OnBrainHover(TreeNode hitNode)
    {
        previousRolloverNode = currentRolloverNode;
        currentRolloverNode = hitNode;

        //DebugRolloverEffects(previousRolloverNode, currentRolloverNode, currentSelectedNode, currentFragment);

        if (currentSelectedNode == currentRolloverNode && previousRolloverNode != currentRolloverNode)
        {
            effectsManager.DisableRollOverEffects(previousRolloverNode);
        }
        else if (currentSelectedNode == previousRolloverNode || currentRolloverNode == previousRolloverNode || previousRolloverNode._parent == currentSelectedNode || currentSelectedNode._fragments.Contains(currentFragment) || previousRolloverNode._fragments.Contains(currentFragment))
        {
            // do nothing (leave rollover effects enabled)
        }
        else
        {
            effectsManager.DisableRollOverEffects(previousRolloverNode);
            effectsManager.EnableRollOverEffects(currentRolloverNode);
        }
    }

    private void OnBrainClick(TreeNode hitNode)
    {
        selectRegionEvent.Invoke(hitNode._key);
        OnSelection(hitNode);
    }

    // Debugging method. Look if the brain tree contains a key.                    
    void testProperties(string key)
    {

        TreeNode thisNode = TreeNode.SearchTree(key, brain);
        // Debug.Log(key + " has " + thisNode._fragments.Count + "fragments" + "\n" +
        // "This regions colors are: " + thisNode._clickColor.ToString() + ", " + thisNode._defaultColor.ToString() + "," + thisNode._rolloverColor.ToString() + "\n" +
        // "This regions keys are: " + thisNode._bodyJsonKey + ", " + thisNode._titleJsonKey

        // );
    }

    // A debug method that goes through regionNames and checks if there is
    // a gameObject with this name, i.e. if this region exists.
    void findRegionandSubRegions()
    {

        foreach (string region in regionNames)
        {
            GameObject thisRegion = GameObject.Find(region);
            if (thisRegion == null)
            {
                Debug.LogError("Region " + region + " not found!");
            }
            else
            {

                atlasRegions.Add(thisRegion);
                // Debug.Log("Region Found:" + thisRegion.name);

            }

        }
    }


    // useful debugging method which can be placed at different spots inside the update method to debug the current state
    // best to call it conditionally depending what you want to debug otherwise it will print too many times
    // and it will be hard to read
    // for example" if previousRolloverNode._key == "MotorCortex"
    void DebugRolloverEffects(TreeNode previousRolloverNode, TreeNode currentRolloverNode, TreeNode currentSelectedNode, Renderer currentFragment)
    {
        Debug.Log("previousRolloverNode: " + previousRolloverNode._key);
        string previousRolloverNodeFragmentNames = " ";
        foreach (Renderer frag in previousRolloverNode._fragments)
        {
            previousRolloverNodeFragmentNames += " " + frag.gameObject.transform.parent.gameObject.name;
        }
        Debug.Log("previousRolloverNode fragments: " + previousRolloverNodeFragmentNames);

        Debug.Log("currentRolloverNode: " + currentRolloverNode._key);
        string currentRolloverNodeFragmentNames = " ";

        foreach (Renderer frag in currentRolloverNode._fragments)
        {
            currentRolloverNodeFragmentNames += " " + frag.gameObject.transform.parent.gameObject.name;

        }
        Debug.Log("currentRolloverNode fragments: " + currentRolloverNodeFragmentNames);
        string currentSelectedNodeFragmentNames = " ";
        Debug.Log("currentSelectedNode: " + currentSelectedNode._key);

        foreach (Renderer frag in currentSelectedNode._fragments)
        {
            currentSelectedNodeFragmentNames += " " + frag.gameObject.transform.parent.gameObject.name;
        }
        Debug.Log("currentSelectedNode fragments: " + currentSelectedNodeFragmentNames);
        Debug.Log("currentFragment: " + currentFragment.gameObject.transform.parent.name);
        Debug.Log(" ");
    }
}

[System.Serializable]
public class SelectRegionEvent : UnityEvent<string>
{
}