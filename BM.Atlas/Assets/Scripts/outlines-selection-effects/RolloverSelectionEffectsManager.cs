using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RolloverSelectionEffectsManager : MonoBehaviour
{

    // Sounds that get played on rollover/click.
    public AudioClip rolloverSound;
    public AudioClip clickSound;
    public bool playSoundEffects = true;
    private AudioSource source;

    // Outline shaders for click effects (attached to camera).
    public Camera outlineCam;
    private EasyOutlineSystem selectionOutline;

    private AtlasRegionsAndFunctionsManager thisAFManager;

    // Start is called before the first frame update
    void Start()
    {
        thisAFManager = AtlasRegionsAndFunctionsManager.instance;
        source = GetComponent<AudioSource>();

        // Set outline shaders to default state.
        if (outlineCam == null)
        {
            Debug.LogError("Reference to camera with outliner system is missing");
        }
        else
        {
            selectionOutline = outlineCam.GetComponent<EasyOutlineSystem>();
        }
        selectionOutline.outlineMode = Mode.Solid;
        selectionOutline.rendererList.Clear();
    }

    // Enable the Rollover Effects for a node.
    public void EnableRollOverEffects(TreeNode thisNode)
    {
        if (thisNode == null)
        {
            return;
        }
        // Debug.Log("enableRolloverEffects function: Enabling rollover effects of node " + thisNode._key);
        PlayRolloverSound();
        foreach (Renderer fragment in thisNode._fragments)
        {
            fragment.material.SetColor("_Color", thisNode._rolloverColor);
        }
    }

    // Disable Rollover Effects for a node.
    public void DisableRollOverEffects(TreeNode thisNode)
    {
        if (thisNode == null)
        {
            return;
        }
        // Debug.Log("DisableRollOverEffects function: Disabling rollover effects of node " + thisNode._key);
        foreach (Renderer fragment in thisNode._fragments)
        {
            fragment.material.SetColor("_Color", thisNode._defaultColor);
        }
    }

    public void EnableClickEffects(TreeNode thisNode)
    {
        if (thisNode == null)
        {
            return;
        }
        
        // Debug.Log("E function: Enabling click effects of node " + thisNode._key);
        PlayClickSound();
        selectionOutline.rendererList.Clear();
        selectionOutline.outlineColor = thisNode._clickColor;

        // Check if the node is a region node in "brain" or a function node in "brainFunctions".
        if (thisNode._isFunctionNode == false)
        {
            foreach (Renderer fragment in thisNode._fragments)
            {
                fragment.material.SetColor("_Color", thisNode._clickColor);
                selectionOutline.rendererList.Add(fragment.GetComponent<Renderer>());
            }
        }
        else if (thisNode._isFunctionNode == true)
        {
            List<string> nodeList = GetRegionsOfFunction(thisNode._key);
            foreach (string nodeName in nodeList)
            {
                foreach (Renderer fragment in TreeNode.SearchTree(nodeName, thisAFManager.GetBrainTree())._fragments)
                {
                    fragment.material.SetColor("_Color", thisNode._clickColor);
                    selectionOutline.rendererList.Add(fragment.GetComponent<Renderer>());
                }
            }
        }
        else
        {
            Debug.LogError("Enabling click effects failed because _isFunctionNode of clicked node is not assigned.");
        }
    }
    public void DisableClickEffects(TreeNode thisNode)
    {
        if (thisNode == null)
        {
            return;
        }
        // Debug.Log("D function: Disabling click effects of node " + thisNode._key);
        selectionOutline.rendererList.Clear();

        if (thisNode._isFunctionNode == false)
        {
            foreach (Renderer fragment in thisNode._fragments)
            {
                fragment.material.SetColor("_Color", thisNode._defaultColor);
            }
        }
        else if (thisNode._isFunctionNode == true)
        {
            List<string> nodeList = GetRegionsOfFunction(thisNode._key);
            foreach (string nodeName in nodeList)
            {
                TreeNode functionRegionNode = TreeNode.SearchTree(nodeName, thisAFManager.GetBrainTree());
                foreach (Renderer fragment in functionRegionNode._fragments)
                {
                    fragment.material.SetColor("_Color", functionRegionNode._defaultColor);
                }
            }
        }
        else
        {
            Debug.LogError("Disabling click effects failed because _isFunctionNode of clicked node is not assigned.");
        }
    }


    public void PlayRolloverSound()
    {
        if (playSoundEffects)
        {
            source.PlayOneShot(rolloverSound);
        }
    }

    public void PlayClickSound()
    {
        if (playSoundEffects)
        {
            source.PlayOneShot(clickSound);
        }
    }


    // Takes the name of a function, returns the associated regions.
    private List<string> GetRegionsOfFunction(string functionName)
    {
        List<string> result;
        if (thisAFManager.GetFunctionsRegionsDict().TryGetValue(functionName, out result))
        {
            return result;
        }
        else
        {
            Debug.LogError("Tried to find regions for an unknown function.");
            return null;
        }
    }

}
