using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BrainTreeCreator : MonoBehaviour
{

    public enum AtlasType{
        DK_DES,
        VON_ECONOMO
    }

    public AtlasType theAtlas;

    // A List containing the names/keys of brain functions the atlas knows, if the atlas needs a function tree.
    List<string> atlasFunctions = new List<string> {"VisualPerception", "AuditoryPerception", "SomatosensoryPerception", "TastePerception",
        "OlfactoryPerception", "Motion", "Language", "Emotions", "ConsolidationOfNewMemories", "WorkingMemory", "Orientation",
        "HungerAndSatiety", "CircadianClocks", "BreathAndHeartbeatRegulation", "ConciousState" };

    // Lists containing the names/keys of regions which contain more
    // subregions, per atlas type.

    // REGIONS FOR DK_DES ATLAS:
    List<string> regionNames = new List<string> { "FrontalLobe", "ParietalLobe", "OccipitalLobe", "TemporalLobe", "Insula", "SubcorticalStructures", "Cerebellum", "BrainStem", "LimbicCortex" };
    // REGIONS FOR VON_ECONOMO ATLAS:
    List<string> vonEconomoRegionNames = new List<string>{"corpuscallosum", "FA", "FB", "FC", "FCBm", "FD", "FDdelta", "FDT", "FE", "FF", "FG", "FH", "FJK", "FLMN", "HA", "HB", "HC", "IA", "IB", "LA1", "LA2", "LC1", "LC2", "LC3", "LD", "LE", "OA", "OB", "OC", "PA", "PB", "PC", "PD", "PE", "PF", "PG", "PH", "TA", "TB", "TC", "TD", "TE", "TF", "TG"};
   
    // These are the regions which belong to the functions in atlasFunctions.
    List<string> subFunctionNames1 = new List<string> { "OccipitalLobe", "MedianAndInferiorTemporalLobe", "Thalamus" };
    List<string> subFunctionNames2 = new List<string> { "AuditoryCortex", "BrainStem", "Thalamus" };
    List<string> subFunctionNames3 = new List<string> { "SomatosensoryCortex", "Thalamus" };
    List<string> subFunctionNames4 = new List<string> { "GustatoryCortex", "Amygdala", "Thalamus", "BrainStem" };
    List<string> subFunctionNames5 = new List<string> { "PiriformCortex", "Amygdala" };
    List<string> subFunctionNames6 = new List<string> { "MotorCortex", "Cerebellum", "BasalGanglia" };
    List<string> subFunctionNames7 = new List<string> { "BrocaArea", "WernickeArea" };
    List<string> subFunctionNames8 = new List<string> { "Amygdala", "LimbicCortex", "Insula" };
    List<string> subFunctionNames9 = new List<string> { "Hippocampus" };
    List<string> subFunctionNames10 = new List<string> { "PrefrontalCortex", "PosteriorParietalLobe" };
    List<string> subFunctionNames11 = new List<string> { "Hippocampus" };
    List<string> subFunctionNames12 = new List<string> { "Hypothalamus" };
    List<string> subFunctionNames13 = new List<string> { "Hypothalamus" };
    List<string> subFunctionNames14 = new List<string> { "BrainStem" };
    List<string> subFunctionNames15 = new List<string> { "BrainStem" };

    // These Lists will populate the subregions List. The empty 
    // Lists are important placeholders for potential future subregions.

    // SUBREGIONS FOR DK_DES:
    List<string> subRegionNames1 = new List<string> { "MotorCortex", "BrocaArea", "PrefrontalCortex" };
    List<string> subRegionNames2 = new List<string> { "SomatosensoryCortex", "PosteriorParietalLobe" };
    List<string> subRegionNames3 = new List<string>();
    List<string> subRegionNames4 = new List<string> { "AuditoryCortex", "WernickeArea", "MedianAndInferiorTemporalLobe", "PiriformCortex" };
    List<string> subRegionNames5 = new List<string> { "GustatoryCortex" };
    List<string> subRegionNames6 = new List<string> { "Hippocampus", "Amygdala", "Thalamus", "Hypothalamus", "BasalGanglia" };
    List<string> subRegionNames7 = new List<string>();
    List<string> subRegionNames8 = new List<string>();
    List<string> subRegionNames9 = new List<string>();

    // SUBREGIONS FOR VON_ECONOMO:
    // since these are empty, they are generated in the sub region builder

    public List<string> getRegionNames()
    {
        if (theAtlas == AtlasType.VON_ECONOMO){
            return vonEconomoRegionNames;
        }else{
            // use DK-DES
            return regionNames;
        }
        
    }

    public Dictionary<string, List<string>> getFunctionsDict()
    {
        Dictionary<string, List<string>> functionRegions = new Dictionary<string, List<string>>();
        functionRegions.Add(atlasFunctions[0], subFunctionNames1);
        functionRegions.Add(atlasFunctions[1], subFunctionNames2);
        functionRegions.Add(atlasFunctions[2], subFunctionNames3);
        functionRegions.Add(atlasFunctions[3], subFunctionNames4);
        functionRegions.Add(atlasFunctions[4], subFunctionNames5);
        functionRegions.Add(atlasFunctions[5], subFunctionNames6);
        functionRegions.Add(atlasFunctions[6], subFunctionNames7);
        functionRegions.Add(atlasFunctions[7], subFunctionNames8);
        functionRegions.Add(atlasFunctions[8], subFunctionNames9);
        functionRegions.Add(atlasFunctions[9], subFunctionNames10);
        functionRegions.Add(atlasFunctions[10], subFunctionNames11);
        functionRegions.Add(atlasFunctions[11], subFunctionNames12);
        functionRegions.Add(atlasFunctions[12], subFunctionNames13);
        functionRegions.Add(atlasFunctions[13], subFunctionNames14);
        functionRegions.Add(atlasFunctions[14], subFunctionNames15);
        return functionRegions;
    }

    // Build a TreeNode tree called "brainFunctions". It contains a Dictionary
    // of function keys associated with region keys.

    public TreeNode buildFunctionsTree()
    {
        TreeNode brainFunctions = new TreeNode();

        TreeNode temp;

        for (int i = 0; i < atlasFunctions.Count; i++)
        {
            temp = brainFunctions.Add(atlasFunctions[i]);
            // Debug.Log("adding function " + atlasFunctions[i] + "...");

            // add keys and colors
            temp._key = atlasFunctions[i];

            GameObject functionGameObject = GameObject.Find(atlasFunctions[i]);

            if (functionGameObject != null)
            {
                // Debug.Log("Found a GameObject for this function.");
            }

            if (functionGameObject.GetComponent<FunctionProperties>())
            {
                temp._isFunctionNode = true;
                temp._clickColor = functionGameObject.GetComponent<FunctionProperties>().clickColor;
                temp._titleJsonKey = functionGameObject.GetComponent<FunctionProperties>().titleKey;
                temp._bodyJsonKey = functionGameObject.GetComponent<FunctionProperties>().bodyKey;
                temp._bestViewPosition = functionGameObject.GetComponent<FunctionProperties>().bestViewPosition;
                temp._bestFOVScale = functionGameObject.GetComponent<FunctionProperties>().bestFOVScale;
            }
        }

        // test getRegionsOfFunctions()
        // print(getRegionsOfFunctions("Emotions"));
        
        return brainFunctions;
    }

    // Build a TreeNode tree called "brain". It contains the regions and subregions.
    public TreeNode buildRegionsTree()
    {
        List<List<string>> subregions = new List<List<string>>();

        // Each region needs to have a corresponding subregions list, even if it is empty!
        if (theAtlas == AtlasType.VON_ECONOMO){
            //add voneconomo atlas sub regions
            foreach (string vcregion in vonEconomoRegionNames)
            {
                // add an empty string cos Von Economo atlas doesn't have sub regions
                subregions.Add(new List<string>());
               // Debug.Log("added fake sub region");
            }

        }else{
            // use DK-DES
            subregions.Add(subRegionNames1);
            subregions.Add(subRegionNames2);
            subregions.Add(subRegionNames3);
            subregions.Add(subRegionNames4);
            subregions.Add(subRegionNames5);
            subregions.Add(subRegionNames6);
            subregions.Add(subRegionNames7);
            subregions.Add(subRegionNames8);
            subregions.Add(subRegionNames9);
        }


        TreeNode brain = new TreeNode();
        TreeNode temp = brain.Add("regions");
        TreeNode temp2 = new TreeNode();
        GameObject regionGameObject;

        for (int i = 0; i < getRegionNames().Count; i++)
        {
            temp2 = temp.Add(getRegionNames()[i]);
            temp2._isFunctionNode = false;
            // TODO: add fragments of regionNames[i]
            // Debug.Log("added " + regionNames[i]);
            regionGameObject = GameObject.Find(getRegionNames()[i]);
            temp2._gameObject = regionGameObject;

            // check if object has regionProperties attached
            if (regionGameObject.GetComponent<regionProperties>())
            {
                //Set colors
                if (regionGameObject.GetComponent<regionProperties>().defaultColor != null)
                {
                    temp2._defaultColor = regionGameObject.GetComponent<regionProperties>().defaultColor;
                    temp2._rolloverColor = regionGameObject.GetComponent<regionProperties>().rolloverColor;
                    temp2._clickColor = regionGameObject.GetComponent<regionProperties>().clickColor;
                    temp2._bestViewPosition = regionGameObject.GetComponent<regionProperties>().bestViewPosition;
                    temp2._bestFOVScale = regionGameObject.GetComponent<regionProperties>().bestFOVScale;

                    temp2._bodyJsonKey = regionGameObject.GetComponent<regionProperties>().bodyKey;
                    temp2._titleJsonKey = regionGameObject.GetComponent<regionProperties>().titleKey;

                    //TODO: maybe the color lookup tables can go here too
                }

                Renderer[] tempFragments = regionGameObject.GetComponentsInChildren<Renderer>();
                // Debug.Log("THERE ARE :" + tempFragments.Length.ToString() + " RENDERERS IN " + regionGameObject.name);

                if (temp2._fragments != null)
                {
                    // Debug.Log("fragemnts list exists");
                }
                else
                {
                    Debug.LogError("fragment list does not exist");
                };

                foreach (Renderer fragment in tempFragments)
                {
                    temp2._fragments.Add(fragment);
                    fragment.material.SetColor("_Color", temp2._defaultColor);
                }
            }
            else
            {
                Debug.LogError(regionGameObject.name + " does not have properties attached");
            }

            if (subregions[i].Count != 0)
            {
                for (int j = 0; j < subregions[i].Count; j++)
                {
                    temp = temp2.Add(subregions[i][j]);
                    temp._isFunctionNode = false;
                    temp._gameObject = GameObject.Find(subregions[i][j]);
                    // Debug.Log("added " + subregions[i][j]);

                    // check if object has regionProperties attached
                    if (temp._gameObject.GetComponent<regionProperties>())
                    {

                        //Set colors
                        if (temp._gameObject.GetComponent<regionProperties>().defaultColor != null)
                        {
                            temp._defaultColor = temp._gameObject.GetComponent<regionProperties>().defaultColor;
                            temp._rolloverColor = temp._gameObject.GetComponent<regionProperties>().rolloverColor;
                            temp._clickColor = temp._gameObject.GetComponent<regionProperties>().clickColor;
                            temp._bestViewPosition = temp._gameObject.GetComponent<regionProperties>().bestViewPosition;
                            temp._bestFOVScale = temp._gameObject.GetComponent<regionProperties>().bestFOVScale;

                            temp._bodyJsonKey = temp._gameObject.GetComponent<regionProperties>().bodyKey;
                            temp._titleJsonKey = temp._gameObject.GetComponent<regionProperties>().titleKey;
                        }

                        Renderer[] tempFragments = temp._gameObject.GetComponentsInChildren<Renderer>();
                        //  Debug.Log("THERE ARE :" + tempFragments.Length.ToString() + " RENDERERS IN " + temp._gameObject.name);

                        foreach (Renderer fragment in tempFragments)
                        {
                            temp._fragments.Add(fragment);
                            // Debug.Log(fragment.name + " fragment added");
                            // set fragment to default color
                            fragment.material.SetColor("_Color", temp._defaultColor);
                        }
                    }
                    else
                    {
                        Debug.LogError(temp._gameObject.name + " does not have properties attached");
                    }
                }
            }
            

        }

        
        return brain;
        //Debug.Log("the last subregion is " + brain.Get("regions").Get("FrontalLobe").Get("MotorCortex")._key);
        /*testProperties("FrontalLobe");
        testProperties("PrefrontalCortex");
        testProperties("PiriformCortex");
        testProperties("MotorCortex");
        testProperties("OccipitalLobe");*/

        // Searching for a key not contained in the tree still 
        // throws a nullreference exception.
        //Debug.Log("finding Bananas? " + TreeNode.SearchTree("Bananas", brain)._key);
    }
}
