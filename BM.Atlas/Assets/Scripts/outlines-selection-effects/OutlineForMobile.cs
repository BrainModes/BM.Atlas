using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EasyOutlineSystem))]
public class OutlineForMobile : MonoBehaviour
{
    // Start is called before the first frame update

    public float mobileOutlineThickness = 6f;
    void Start()
    {
#if UNITY_IOS
        EasyOutlineSystem easyOutline = gameObject.GetComponent<EasyOutlineSystem>();
        easyOutline.outlineThickness = mobileOutlineThickness;
#endif
    }

}
