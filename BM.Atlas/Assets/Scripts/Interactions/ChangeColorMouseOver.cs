using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorMouseOver : MonoBehaviour
{

public Color baseColor;
public Color changeColor;

void OnMouseEnter(){

    GetComponent<Renderer>().material.SetColor("_Color", changeColor);
}

void OnMouseExit(){

    GetComponent<Renderer>().material.SetColor("_Color", baseColor);
}

}
