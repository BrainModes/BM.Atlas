//============================================================================================================
/**
 *  @file       MouseInteractions.cs
 *  @brief      Mouse ointeractions class.
 *  @details    This file contains the implementation of the SceneInteractions.MouseInteractions class.
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
     *  @brief      Mouse interactions class.
     *  @details    This class is used to define a component that captures mouse events that manipulates
     *              the camera position and rotation.
     */
    public class MouseInteractions : MonoBehaviour
    {

        //// Fields ////

        // Reset variables

        public int resetTolerance = 250;        /**< Tolerance in milliseconds to produce the event. */

        private int clickCount = 0;             /**< Count of mouse clicks. */

        private float lastClickTime = 0;        /**< Last click time. */

        private float originalFOV = 60f;        /**< Original field of view. */

        private Vector3 originalRotation = new Vector3(0f, 180f, 0f);   /**< Original camera rotation. */

        private Vector3 originalPosition = new Vector3(0f, 0f, 0f);     /**< Original camera position */

        public bool doubleClickResetEnabled = true; /**< toggle double click camera view reset. */

        // Zoom variables

        public int zoomMax = 120;               /**< Maximum zoom value (degrees of the field view). */

        public int zoomMin = 10;                /**< Minimum zoom value (degrees of field view). */

        public float wheelSensitivity = 10f;    /**< Wheel sensitivity. */       

        // Rotation variables

        public float rotationSpeed = 200f;      /**< Rotation speed. */

        public bool axisLockEnabled = false; /**< Toggle if rotations are locked to XY or free. */

        private Vector3 lastMousePosition = new Vector3(0, 0, 0);   /**< Last mouse position. */

        // Camera objects

        private Transform cameraObject = null;  /**< The object that represents the main camera system. */

        private Camera cameraComponent = null;  /**< The component that manages the camera. */

        public GameObject cameraPivotX; /**< Parent Object responsible for X rotations. */
        public GameObject cameraPivotY; /**< Parent Object responsible for Y rotations. */

        private bool mouseInteractionsEnabled = true; /**< Bool set by this script to enable or disable mouse interactions at runtime. */

        private bool mouseIsOverUI = false; /**< Bool set by this script to enable or disable mouse interactions at runtime when mouse is over UI. */

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
  

            // check if mouse interactions are enabled
            if(mouseInteractionsEnabled && !mouseIsOverUI){
                // Mouse reset
                if (Input.GetMouseButtonDown(0))
                {
                    var currentTime = Time.time;
                    var delta = System.Convert.ToInt32(1000 * (currentTime - lastClickTime));
                    if (delta <= resetTolerance)
                        clickCount++;
                    else
                        clickCount = 1;

                    lastClickTime = currentTime;
                }

                if (doubleClickResetEnabled){
                    if (clickCount == 2)
                    {
                        cameraComponent.fieldOfView = originalFOV;
                        cameraObject.position = originalPosition;
                        cameraObject.eulerAngles = originalRotation;

                        clickCount = 0;
                    }
                }
                
                // Mouse rotation
                if (Input.GetMouseButtonDown(0))
                {
                    lastMousePosition = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
        
                    if (axisLockEnabled){

                    // only xy roations

                    float rotX = Input.GetAxis("Mouse Y")*rotationSpeed*Mathf.Deg2Rad;
                    float rotY = Input.GetAxis("Mouse X")*rotationSpeed*Mathf.Deg2Rad;

                    cameraPivotY.transform.Rotate(0,rotY,0,Space.Self);
                    cameraPivotX.transform.Rotate(0 - rotX,0,0,Space.Self);


                    }else{

                    // free roations

                    var currentPosition = Input.mousePosition;

                    var deltay = currentPosition[1] - lastMousePosition[1];
                    var deltax = currentPosition[0] - lastMousePosition[0];  

                    var rotation = Quaternion.Euler(deltay / Screen.height * rotationSpeed, -deltax / Screen.width * rotationSpeed, 0);
                    cameraObject.localRotation *= Quaternion.Inverse(rotation);

                    lastMousePosition = currentPosition;
                    }




                }


                // Mouse zoom
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    var currentFOV = cameraComponent.fieldOfView;
                    var newFOV = currentFOV - Input.GetAxis("Mouse ScrollWheel") * wheelSensitivity;
                    newFOV = Mathf.Clamp(newFOV, zoomMin, zoomMax);
                    cameraComponent.fieldOfView = newFOV;
                }

            }           
        }
        // use this function to enable and disable the mouse during runtime
        public void MouseInteractionsEnabled(bool enabled){

            if(enabled == true){
                mouseInteractionsEnabled = true;
            }else{
                mouseInteractionsEnabled = false;
            }

        }
        
        // use this function to enable and disable the mouse during runtime when over UI elements where model zoom/roation is not desired.
        public void MouseOverUI(bool isOverUI){
            
            if(isOverUI == true){
                mouseIsOverUI = true;
            }else{
                mouseIsOverUI = false;
            }

        }


    }
}
//============================================================================================================
//        END OF FILE
//============================================================================================================
