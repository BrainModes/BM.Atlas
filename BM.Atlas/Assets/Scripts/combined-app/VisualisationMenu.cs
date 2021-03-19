using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Graph;

public class VisualisationMenu : MonoBehaviour
{
    public bool combinedAtlas = false;

    [Tooltip("These arrays should be in same order. I.e. RECEPTOR map should be first element in maps and Receptor menu game object should be first element in menus.")]
    public Map[] maps;
    public GameObject[] menus;
    public string[] firstVisualisationSelectedPerMap;

    public bool colorMapVisualisation;
    public ColorMapper colorMapper;
    public ColorPalette palette;

    public bool regionVisualisation;


    private GameObject currentMenu;
    private GameObject[] regionObjects;
    private GameObject[] colorMapperObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (colorMapVisualisation && regionVisualisation)
        { 
            regionObjects = GameObject.FindGameObjectsWithTag("RegionObjects");
            
            colorMapperObjects = GameObject.FindGameObjectsWithTag("ColorMapObjects");
            foreach (GameObject ob in colorMapperObjects)
            {
                ob.SetActive(false);
            }
        }
        if (!combinedAtlas)
        {
            currentMenu = menus[0];
            
        }
        SelectMap(maps[0].ToString());
    }

    void SwitchBetweenColorMapRegions(bool colorMap) {
        if (colorMapVisualisation && regionVisualisation)
        {
            if (colorMap)
            {
                foreach (GameObject obj in regionObjects) {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in colorMapperObjects)
                {
                    obj.SetActive(true);
                }

            }
            else {

                foreach (GameObject obj in colorMapperObjects)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in regionObjects)
                {
                    obj.SetActive(true);
                }
            }
        }

    }

    public void SelectMap(string mapS) {
        // convert string to enum name
        Map currentMap = (Map)Enum.Parse(typeof(Map), mapS);
        int idx = 0;
        if (!combinedAtlas)
        {
            idx = Array.IndexOf(maps, currentMap);
            currentMenu.SetActive(false);
            menus[idx].SetActive(true);
            currentMenu = menus[idx];
        }

        switch (currentMap)
        {
            case Map.PATHWAYS:
                SwitchBetweenColorMapRegions(true);
                colorMapper.SelectVisualisation(firstVisualisationSelectedPerMap[idx]);
                break;
            case Map.BRAAK:
                SwitchBetweenColorMapRegions(true);
                colorMapper.SelectVisualisation(firstVisualisationSelectedPerMap[idx]);
                break;
            case Map.RECEPTOR:
                SwitchBetweenColorMapRegions(true);
                colorMapper.SelectVisualisation(firstVisualisationSelectedPerMap[idx]);
                break;
            case Map.REGIONS:
                SwitchBetweenColorMapRegions(false);
                break;
            case Map.CYTOLOGIC:
                SwitchBetweenColorMapRegions(true);
                colorMapper.SelectVisualisation(firstVisualisationSelectedPerMap[idx]);
                break;
        }
    }

}

public enum Map
{
    PATHWAYS,
    BRAAK,
    RECEPTOR,
    REGIONS,
    CYTOLOGIC
}