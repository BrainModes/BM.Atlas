using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OnBrainRegionSelected : MonoBehaviour
{
    private AtlasRegionsAndFunctionsManager thisAFManager;
    private UIAccordion accordion;
    private Button btn;
    private string regionName;

    // Start is called before the first frame update
    void Start()
    {
        thisAFManager = AtlasRegionsAndFunctionsManager.instance;
        thisAFManager.selectRegionEvent.AddListener(OnRegionSelect);
        regionName = gameObject.name.Replace("Button- ", "").Replace("Btn", "");
        btn = gameObject.GetComponent<Button>();
        accordion = gameObject.transform.parent.transform.parent.GetComponent<UIAccordion>();
    }

    void OnRegionSelect(string nodeKey)
    {
        if (regionName == nodeKey)
        {
            btn.Select();
            //opening regions accordion menu and closing functions one
            if (accordion != null)
            {
                accordion.SelectItem(0);
            }
        }
    }
}
