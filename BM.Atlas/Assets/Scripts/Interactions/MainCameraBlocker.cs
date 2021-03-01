//============================================================================================================
/**
 *  @file       MainCameraBlocker.cs
 *  @brief      Main camera blocker class.
 *  @details    This file contains the implementation of the SceneInteractions.MainCameraBlocker class.
 *  @author     Omar Mendoza Montoya (email: omendoz@live.com.mx).
 *  @copyright  All rights reserved to BrainModes project of the Charité Universitätsmedizin Berlin.
 */
//============================================================================================================

//============================================================================================================
//        REFERENCES
//============================================================================================================

using UnityEngine;
using UnityEngine.EventSystems;

//============================================================================================================
namespace Interactions
{
    /**
     *  @brief      Main camera blocker class.
     *  @details    This class is used to define a component that blocks the main camera when the pointer
     *              is over a GUI element of the interface.
     */
    public class MainCameraBlocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        //// Fields ////

        private GameObject cameraManager = null;         /**< Camera manager. */

        //// Methods //// 

        /**
         *  @brief      Start method.
         *  @details    This method is called whenever the scene is being initialized.
         */
        private void Start()
        {
            if (!Input.mousePresent)
                enabled = false;

            cameraManager = GameObject.Find("MainCamera/CameraManager");
            if (cameraManager == null)
                enabled = false;
        }

        /**
         *  @brief      On mouse enter event.
         *  @details    This method is called whenever the mouse pointer has enter into the 
         *              UI element.
         *  @param[in]  eventData  The data of attached to the event.
         */
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (cameraManager != null)
                cameraManager.SetActive(false);
        }

        /**
         *  @brief      On mouse exit event.
         *  @details    This method is called whenever the mouse pointer has abandoned the 
         *              UI element.
         *  @param[in]  eventData  The data of attached to the event.
         */
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (cameraManager != null)
                cameraManager.SetActive(true);
        }
    }
}

//============================================================================================================
//        END OF FILE
//============================================================================================================
