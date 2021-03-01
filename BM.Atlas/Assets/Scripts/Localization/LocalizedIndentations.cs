using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedIndentations : MonoBehaviour
{

    private RectTransform thisTransform;
    public int indentLevel = 0;

    private float indentUnit = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        // get if left or right justified
        // if left adjust min x + add 0.6
        // if right ajust max x - subtract 0.6

        if (LocalizationManager.instance.GetLocalizedValue("text_anchor") == "right"){
            thisTransform.anchorMax = new Vector2(1 - (indentUnit * indentLevel),1);
            thisTransform.anchorMin = new Vector2(0,0);
        }else{
            thisTransform.anchorMin = new Vector2(0 + (indentUnit * indentLevel),0);
            thisTransform.anchorMax = new Vector2(1,1);


        }
        
    }
}
