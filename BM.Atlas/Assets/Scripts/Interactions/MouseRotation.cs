//============================================================================================================
/**
 *  @file       MouseRotation.cs
 *  @brief      Mouse rotation class.
 *  @details    This file contains the implementation of the SceneInteractions.MouseRotation class.
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
     *  @brief      Mouse rotation class.
     *  @details    This class is used to define a component that captures mouse events and produces the
     *              rotation effect of a camera.
     */
    public class MouseRotation : MonoBehaviour
    {

        //// Fields ////

        public float rotationSpeed = 200f;      /**< Rotation speed. */

        private Vector3 lastMousePosition = new Vector3(0, 0, 0);         /**< Last mouse position. */


        private Transform cameraObject = null;  /**< The object that represents the main camera system. */

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
        }

        /**
         *  @brief      Update method.
         *  @details    This method is called whenever the scene is being updated.
         */
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                var currentPosition = Input.mousePosition;

                var deltay = currentPosition[1] - lastMousePosition[1];
                var deltax = currentPosition[0] - lastMousePosition[0];
                var rotation = Quaternion.Euler(deltay / Screen.height * rotationSpeed,
                   -deltax / Screen.width * rotationSpeed, 0);
                cameraObject.localRotation *= Quaternion.Inverse(rotation);

                lastMousePosition = currentPosition;
            }

        }
    }
}

//============================================================================================================
//        END OF FILE
//============================================================================================================
