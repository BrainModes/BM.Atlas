//============================================================================================================
/**
 *  @file       ColorPalette.cs
 *  @brief      ColorPalette class.
 *  @details    This file contains the implementation of the Graph.ColorPalette class.
 *  @author     Omar Mendoza Montoya (email: omendoz@live.com.mx).
 *  @copyright  All rights reserved to BrainModes project of the Charité Universitätsmedizin Berlin.
 */
//============================================================================================================

//============================================================================================================
//        REFERENCES
//============================================================================================================
using System;
using System.Collections.Generic;
using UnityEngine;

//============================================================================================================
namespace Graph
{
    /**
     *  @brief      Color Palette.
     *  @details    This class defines a component that is used to manipulate a color palette.
     */
    public class ColorPalette : MonoBehaviour
    {

        //// Fields ////    

        public Gradient colorGradient = new Gradient(); /**< Color gradient. */


        public float min = 0;           /**< Minimum value in the range. */

        public float max = 1;           /**< Maximum value in the range. */


        public float distance = 10;     /**< Distance from the camera to the center of the palette */        

        public float width = 0.05f;     /**< Width of the color palette in the normalized space. */

        public float height = 0.3f;     /**< Height of the color palette in the normalized space. */

        public float x = 0.425f;        /**< Horizontal position in the normalized space between -0.5 and 0.5. */

        public float y = -0.45f;        /**< Vertical position in the normalized space between -0.5 and 0.5. */

        public float textSize = 0.01f;  /**< Size of the text elements in the normalized space. */

        public string label = "";       /**< Name of what is depcited on the palette. */


        private LineRenderer palette = null;   /**< Line renderer of the color palette. */

        private TextMesh minLabel = null;      /**< Minimum value label. */

        private TextMesh maxLabel = null;      /**< Maximum value label. */

        private TextMesh midLabel = null;      /**< Mid value label. */

        private TextMesh nameLabel = null;     /**< Palette name label. */


        //// Properties ////   

        //// Methods ////   
        public void SetMin(float m) {
            min = m;
            UpdatePalette();
        }

        public void SetMax(float m)
        {
            max = m;
            UpdatePalette();

        }
        /**
         *  @brief      Start method.
         *  @details    This method is called whenever the scene is being initialized.
         */
        void Start()
        {
            if (palette == null)
            {
                if (transform.Find("ColorPalette") == null)
                {
                    var prefab = Resources.Load<GameObject>("Graph/ColorPalette");
                    var obj = Instantiate(prefab);
                    obj.transform.parent = transform;
                    obj.transform.localScale = new Vector3(1F, 1F, 1F);
                    obj.name = "ColorPalette";

                    palette = obj.GetComponentInChildren<LineRenderer>();
                }
                else
                {
                    var obj = transform.Find("ColorPalette").gameObject;
                    palette = obj.GetComponentInChildren<LineRenderer>();
                }                
            }

            if (minLabel == null)
            {
                if (transform.Find("MinLabel") == null)
                {
                    var prefab = Resources.Load<GameObject>("Graph/ColorPaletteLabel");
                    var obj = Instantiate(prefab);
                    obj.transform.parent = transform;
                    obj.transform.localScale = new Vector3(1F, 1F, 1F);
                    obj.name = "MinLabel";

                    minLabel = obj.GetComponentInChildren<TextMesh>();
                }
                else
                {
                    var fiberObject = transform.Find("MinLabel").gameObject;
                    minLabel = fiberObject.GetComponentInChildren<TextMesh>();
                }
            }

            if (maxLabel == null)
            {
                if (transform.Find("MaxLabel") == null)
                {
                    var prefab = Resources.Load<GameObject>("Graph/ColorPaletteLabel");
                    var obj = Instantiate(prefab);
                    obj.transform.parent = transform;
                    obj.transform.localScale = new Vector3(1F, 1F, 1F);
                    obj.name = "MaxLabel";

                    maxLabel = obj.GetComponentInChildren<TextMesh>();
                }
                else
                {
                    var obj = transform.Find("MaxLabel").gameObject;
                    maxLabel = obj.GetComponentInChildren<TextMesh>();
                }
            }

            if (midLabel == null)
            {
                if (transform.Find("MidLabel") == null)
                {
                    var prefab = Resources.Load<GameObject>("Graph/ColorPaletteLabel");
                    var obj = Instantiate(prefab);
                    obj.transform.parent = transform;
                    obj.transform.localScale = new Vector3(1F, 1F, 1F);
                    obj.name = "MidLabel";

                    midLabel = obj.GetComponentInChildren<TextMesh>();
                }
                else
                {
                    var obj = transform.Find("MidLabel").gameObject;
                    midLabel = obj.GetComponentInChildren<TextMesh>();
                }
            }

            if (nameLabel == null)
            {
                if (transform.Find("NameLabel") == null)
                {
                    var prefab = Resources.Load<GameObject>("Graph/ColorPaletteLabel");
                    var obj = Instantiate(prefab);
                    obj.transform.parent = transform;
                    obj.transform.localScale = new Vector3(1F, 1F, 1F);
                    obj.name = "NameLabel";
                   
                    nameLabel = obj.GetComponentInChildren<TextMesh>();
                    nameLabel.text = label;
                }
                else
                {
                    var obj = transform.Find("NameLabel").gameObject;
                    nameLabel = obj.GetComponentInChildren<TextMesh>();
                    nameLabel.text = label;

                }
            }

            UpdatePalette();
        }

        /**
         *  @brief      Update palette.
         *  @details    This method upates the palette.
         */
        public void UpdatePalette()
        {
            if (palette != null)
            {
                palette.positionCount = 100;
                palette.colorGradient = colorGradient;
                for (int i = 0; i< palette.positionCount; i++)
                    palette.SetPosition(i, new Vector3(0.0f, (i)/(99f), 0.0f));
            }

            if (minLabel != null)
                minLabel.text = "-  " + min.ToString("G3");

            if (maxLabel != null)
                maxLabel.text = "-  " + max.ToString("G3");

            if (midLabel != null)
                midLabel.text = "-  " + (0.5f*(max + min)).ToString("G3");

            var obj = transform.Find("NameLabel").gameObject;
            if (obj != null) {
                nameLabel = obj.GetComponentInChildren<TextMesh>();
                nameLabel.text = label;
            }

        }

        /**
         *  @brief      Update method.
         *  @details    This method is called whenever the scene is being updated.
         */
        void LateUpdate()
        {            
            // Set position and orientation.
            var p = -distance*Camera.main.transform.position.normalized;
            transform.position = p;

            var v = Camera.main.transform.position - transform.position;
            v.x = v.z = 0.0f;
            transform.LookAt(Camera.main.transform.position - v);
            transform.rotation = (Camera.main.transform.rotation);

            // Set local scale of object.
            var realDistance = (Camera.main.transform.position - p).magnitude;
            float h = Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad * 0.5f) * 2f * realDistance;
            float w = h * Camera.main.aspect;            
            transform.localScale = new Vector3(w, h, 1f);

            // Set width and positions of palette.          
            if (palette != null)
            {                
                palette.widthMultiplier = w*width;
                palette.colorGradient = colorGradient;

                var totH = height;
                palette.positionCount = 100;                
                for (int i = 0; i < palette.positionCount; i++)
                    palette.SetPosition(i, new Vector3(x - width, y + totH*i/(99f), 0.0f));

            }

            // Set size of text objects.        
            var rate = w / h;
  
            var tw = textSize;
            var th = textSize * rate;
            var off = -0.15f * th;
            if (minLabel != null)
            {
                minLabel.transform.localScale = new Vector3(tw, th, 0);
                minLabel.transform.localPosition = new Vector3(x - width / 2f,
                    y + th + off, 0);
            }

            if (maxLabel != null)
            {
                maxLabel.transform.localScale = new Vector3(tw, th, 0);
                maxLabel.transform.localPosition = new Vector3(x - width / 2f,
                    y + height + th + off, 0);
            }

            if (midLabel != null)
            {
                midLabel.transform.localScale = new Vector3(tw, th, 0);
                midLabel.transform.localPosition = new Vector3(x - width / 2f,
                    y + height / 2f + th + off, 0);
            }

            if (nameLabel != null)
            {
                nameLabel.transform.localScale = new Vector3(tw, th, 0);
                nameLabel.transform.localPosition = new Vector3(x - width * 3 / 2f,
                    y + th + off - th * 2, 0);
            }
        }
    }
}

//============================================================================================================
//        END OF FILE
//============================================================================================================