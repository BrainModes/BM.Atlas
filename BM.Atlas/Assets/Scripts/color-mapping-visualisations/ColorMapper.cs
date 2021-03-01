using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using Graph;
using System;

public class ColorMapper : MonoBehaviour, IEventSystemHandler
{

    public GameObject brainParcelsParent;
    public Color missingDataColor;
    public ColorPalette palette;
    public GradientManager gradientMngr;
    public string leftPrefix = "L-parcel";
    public string rightPrefix = "R-parcel";

    private Renderer[] brainParcels;
    private string currentVisualisation;

    void Awake()
    {
        brainParcels = brainParcelsParent.GetComponentsInChildren<Renderer>();
    }

    void Start() { 
        palette.colorGradient = gradientMngr.GetCurrentGradient();
    }

    //Handle message "RemapColors" sent by GradientManager component
    // which must be on the same object
    public void RemapColors() {
        SelectVisualisation(currentVisualisation);
    }

    public void SelectVisualisation(string fileName) {
        currentVisualisation = fileName;
        TextAsset file = Resources.Load("Color-maps/" + fileName) as TextAsset;
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
                parcel.material.color = palette.colorGradient.Evaluate(colorValue);
            }

        }
    }

}
