//============================================================================================================
/**
 *  @file       MouseDoubleClickReset.cs
 *  @brief      Mouse double click reset class.
 *  @details    This file contains the implementation of the SceneInteractions.DoubleClickReset class.
 *  @author     Omar Mendoza Montoya (email: omendoz@live.com.mx).
 *  @copyright  All rights reserved to BrainModes project of the Charité Universitätsmedizin Berlin.
 */
//============================================================================================================

//============================================================================================================
//        REFERENCES
//============================================================================================================

using UnityEngine;

//============================================================================================================
namespace Interactions
{
    /**
     *  @brief      Mouse Double click reset class.
     *  @details    This class is used to define a component that resets the camera when a double click event 
     *              is detected.
     */
    public class MouseDoubleClickReset : MonoBehaviour
    {

        //// Fields ////

        public int tolerance = 250;    /**< Tolerance in milliseconds to produce the event. */

        private int clickCount = 0;    /**< Count of mouse clicks. */

        private float lastClickTime = 0;        /**< Last click time. */
            
        private float originalFOV = 60f;        /**< Original field of view. */

        private Vector3 originalRotation = new Vector3(0f, 180f, 0f);   /**< Original camera rotation. */

        private Vector3 originalPosition = new Vector3(0f, 0f, 0f);     /**< Original camera position */


        private Transform cameraObject = null;  /**< The object that represents the main camera system. */

        private Camera cameraComponent = null;  /**< The component that manages the camera. */

        //// Methods //// 

        /**
         *  @brief      Start method.
         *  @details    This method is called whenever the scene is being initialized.
         */
        private void Start()
        {
            if (!Input.mousePresent)
                enabled = false;

            cameraObject = transform.parent;
            if (cameraObject == null)
                cameraObject = transform;

            if (cameraObject.Find("Camera") != null)
                cameraComponent = cameraObject.Find("Camera").GetComponent<Camera>();
            else
                cameraComponent = Camera.main;
        }

        /**
         *  @brief      Update method.
         *  @details    This method is called whenever the scene is being updated.
         */
        private void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                var currentTime = Time.time;
                var delta = System.Convert.ToInt32(1000 * (currentTime - lastClickTime));
                if (delta <= tolerance)
                    clickCount++;
                else
                    clickCount = 1;

                lastClickTime = currentTime;
            }

            if (clickCount == 2)
            {
                cameraComponent.fieldOfView = originalFOV;
                cameraObject.position = originalPosition;
                cameraObject.eulerAngles = originalRotation;
                clickCount = 0;
            }
        }
    }
}

//============================================================================================================
//        END OF FILE
//============================================================================================================
