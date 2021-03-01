//============================================================================================================
/**
 *  @file       TouchInteractions.cs
 *  @brief      Manager for touch interactions.
 *  @details    This file contains the implementation of the SceneInteractions.TouchInteractions class.
 *  @author     Jessica Palmer.
 *  @copyright  All rights reserved to BrainModes project of the Charité Universitätsmedizin Berlin.
 */
//============================================================================================================

//============================================================================================================
//        References
//============================================================================================================

using UnityEngine;
using HedgehogTeam.EasyTouch;

//============================================================================================================
namespace Interactions
{
    /**
     *  @brief      Touch interactions manager.
     *  @details    This class defines an object that controls the camera according to touch events.
     */
    public class TouchInteractions : MonoBehaviour
    {
        //// Fields ////

        public float rotationSpeed = 30.0f;     /**< Rotation speed. */

        public float pinchZoomSpeed = 10.0f;    /**< Pinch zoom speed. */

        public float minFOV = 1.0f;             /**< Minimum field of view. */

        public float maxFOV = 120.0f;           /**< Maximum field of view. */

        public bool doubleTapResetEnabled = true; /**< Toggle double tap camera view reset. */

        public bool axisLockEnabled = false; /**< Toggle if rotations are locked to XY or free. */

        public GameObject cameraPivotX; /**< Parent Object responsible for X rotations. */

        public GameObject cameraPivotY; /**< Parent Object responsible for Y rotations. */

        private float newFOV = 60f;             /**< New field of view. */

        private float originalFOV = 60f;        /**< Original field of view. */

        private Vector3 originalRotation = new Vector3(0f, 180f, 0f);   /**< Original camera rotation. */

        private Vector3 originalPosition = new Vector3(0f, 0f, 0f);     /**< Original camera position */


        private Transform cameraObject = null;  /**< The object that represents the main camera system. */

        private Camera cameraComponent = null;  /**< The component that handles the camera. */

        private bool touchInteractionsEnabled = true; /**< Bool set by this script to enable or disable touch interactions at runtime. */

        //// Methods //// 

        /**
         *  @brief      Start method.
         *  @details    This method is called whenever the scene is being initialized.
         */
        void Start()
        {
            if (!Input.touchSupported)
            {
                enabled = false;
                EasyTouch.SetEnabled(false);
            }

            cameraObject = transform.parent;
            if (cameraObject == null)
                cameraObject = transform;

            if (cameraObject.Find("Camera") != null)
                cameraComponent = cameraObject.Find("Camera").GetComponent<Camera>();
            else
                cameraComponent = Camera.main;
            newFOV = cameraComponent.fieldOfView;
        }

        /**
         *  @brief      Update method.
         *  @details    This method is called whenever the scene is being updated.
         */
        void Update()
        {

            // check if touch interactions are enabled
            if(touchInteractionsEnabled){

                Gesture current = EasyTouch.current;

                // Double Tap Reset

                if (current != null)
                {

                    // Two Tab Reset

                    if (doubleTapResetEnabled){

                        if (current.type == EasyTouch.EvtType.On_DoubleTap && current.touchCount == 1)
                        {

                            cameraObject.rotation = Quaternion.Euler(originalRotation);
                            cameraObject.position = originalPosition;
                            cameraComponent.fieldOfView = originalFOV;
                            newFOV = originalFOV;
                        }
                    }

                    // Two Finger Pan

                    // One Finger Swipe Rotate Model

                    if (current.type == EasyTouch.EvtType.On_Swipe && current.touchCount == 1)
                    {

                        if(axisLockEnabled){

                            // xy rotations only

                            float rotX = current.deltaPosition.y / Screen.height * rotationSpeed*Mathf.Deg2Rad;
                            float rotY = current.deltaPosition.x / Screen.width * rotationSpeed*Mathf.Deg2Rad;

                            cameraPivotY.transform.Rotate(0,rotY,0,Space.Self);
                            cameraPivotX.transform.Rotate(0 - rotX,0,0,Space.Self);
                        }else{

                            // free rotations

                            cameraObject.Rotate(Vector3.up * current.deltaPosition.x / Screen.width * rotationSpeed*Mathf.Deg2Rad);
                            cameraObject.Rotate(Vector3.left * current.deltaPosition.y / Screen.height * rotationSpeed*Mathf.Deg2Rad);
                        }

                    }
                }

                    // Twist Rotate Camera

                    // Pinch Zoom
                
        

            // If there are two touches on the device...
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;



                    // Otherwise change the field of view based on the change in distance between the touches.
                cameraComponent.fieldOfView += deltaMagnitudeDiff * pinchZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, minFOV, maxFOV);

                }
                // smoother, but springy and conflicts with idle rotator
            //  cameraComponent.fieldOfView = Mathf.Lerp(cameraComponent.fieldOfView, newFOV, Time.deltaTime * pinchZoomSpeed);

            }

        }

        // use this function to enable and disable the touch during runtime
        public void TouchInteractionsEnabled(bool enabled){

        if(enabled == true){
            touchInteractionsEnabled = true;
        }else{
            touchInteractionsEnabled = false;
        }

    }
    }
}
//============================================================================================================
//        End of file
//============================================================================================================