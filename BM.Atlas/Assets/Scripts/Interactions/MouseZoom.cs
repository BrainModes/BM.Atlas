//============================================================================================================
/**
 *  @file       MouseZoom.cs
 *  @brief      Mouse zoom class.
 *  @details    This file contains the implementation of the SceneInteractions.MouseZoom class.
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
     *  @brief      Mouse zoom  class.
     *  @details    This class is used to define a component that captures the mouse scroll event and produces the
     *              zoom in/out effects of the camera.
     */
    public class MouseZoom : MonoBehaviour
    {

        //// Fields ////

        public int zoomMax = 120;          /**< Maximum zoom value (degrees of the field view). */

        public int zoomMin = 10;           /**< Minimum zoom value (degrees of field view). */

        public float wheelSensitivity = 10f;    /**< Wheel sensitivity. */


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
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                var currentFOV = cameraComponent.fieldOfView;
                var newFOV = currentFOV - Input.GetAxis("Mouse ScrollWheel") * wheelSensitivity;
                newFOV = Mathf.Clamp(newFOV, zoomMin, zoomMax);
                cameraComponent.fieldOfView = newFOV;
            }
        }
    }
}
//============================================================================================================
//        END OF FILE
//============================================================================================================
