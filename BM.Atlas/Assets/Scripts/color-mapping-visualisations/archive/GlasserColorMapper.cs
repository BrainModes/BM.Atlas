using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using Graph;
using System;

public class GlasserColorMapper : MonoBehaviour, IEventSystemHandler
{
    public GameObject pathwayMenu;
    public GameObject braakMenu;
    public GameObject receptorMenu;
    public GameObject regionMenu;
    public GameObject brainParcelsParent;
    public Color missingDataColor;
    public ColorPalette palette;

    private Dictionary<Map, GameObject> menus;
    private GameObject currentMenu;
    private Renderer[] brainParcels;
    private string leftPrefix = "L-parcel";
    private string rightPrefix = "R-parcel";
    private Gradient gradient;

    private void Start()
    {
        currentMenu = pathwayMenu;
        brainParcels = brainParcelsParent.GetComponentsInChildren<Renderer>();
        InitialiseGradient();
        palette.colorGradient = gradient;
        SelectMap("PATHWAYS");

    }

    //Handle message "RemapColors" sent by GradientManager component
    // which must be on the same object
    
    public void SelectMap (string mapS) {
        // convert string to enum name
        Map map = (Map)Enum.Parse(typeof(Map), mapS);
        switch (map) {
            case Map.PATHWAYS: 
                currentMenu.SetActive(false);
                pathwayMenu.SetActive(true);
                currentMenu = pathwayMenu;
                SelectVisualisation("scai_map_5-HT1A");
                palette.SetMin(0.05982156284f);
                palette.SetMax(0.740034975f);
                palette.label = "Relative Entropy";
                palette.UpdatePalette();
                break;
            case Map.BRAAK:
                currentMenu.SetActive(false);
                braakMenu.SetActive(true);
                currentMenu = braakMenu;
                SelectVisualisation("Abeta_C");
                palette.SetMin(1f);
                palette.SetMax(3f);
                palette.label = "Stages";
                palette.UpdatePalette();
                break;
            case Map.RECEPTOR:
                currentMenu.SetActive(false);
                receptorMenu.SetActive(true);
                currentMenu = receptorMenu;
                SelectVisualisation("rec_map_5-H1A");
                palette.SetMin(197f);
                palette.SetMax(616.8333333f);
                palette.label = "Tracer binding in 1 fmol/mg";
                palette.UpdatePalette();
                break;
            case Map.REGIONS:
                currentMenu.SetActive(false);
                regionMenu.SetActive(true);
                currentMenu = regionMenu;
                break;
        }
    }

    public void SelectVisualisation(string fileName) {
        TextAsset file = Resources.Load("Color-maps/" + "glasser-" + fileName) as TextAsset;
        string[] mapping = file.text.Split('\n');

        foreach (Renderer parcel in brainParcels) {
            string name = parcel.transform.parent.name;
            name = name.Replace(leftPrefix, "");
            name = name.Replace(rightPrefix, "");
            int parcelIdx;
            int.TryParse(name, out parcelIdx);
            SetParcelColor(parcelIdx, parcel, mapping);
        }


    }

    private void SetParcelColor(int parcelIdx, Renderer parcel, string[] mapping) {
        // this is subcortex surface, should not be coloured
        if (parcelIdx == 0)
        {
            parcel.material.color = missingDataColor;
        }
        else
        {
            string colorValueS = mapping[parcelIdx - 1];
            // there is no data for availbale for parcels marked "NA" so they should not be coloured
            if (colorValueS.Contains("NA"))
            {
                parcel.material.color = missingDataColor;

            }
            else
            {
                float colorValue;
                float.TryParse(colorValueS, out colorValue);
                parcel.material.color = gradient.Evaluate(colorValue);
            }

        }
    }
    private void InitialiseGradient() {
        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].color = new Color32(255, 255, 0, 1);
        colorKey[0].time = 0f;
        colorKey[1].color = new Color32(205, 0, 0, 1);
        colorKey[1].time = 1f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;
        gradient = new Gradient();
        gradient.SetKeys(colorKey, alphaKey);    
    }
}
