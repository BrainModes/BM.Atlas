//============================================================================================================
/**
 *  @file       Singleton.cs
 *  @brief      Singleton class template.
 *  @details    This file contains the implementation of the Utils.Singleton base class.
 *  @author     Omar Mendoza Montoya (email: omendoz@live.com.mx).
 *  @copyright  All rights reserved to BrainModes project of the Charité Universitätsmedizin Berlin.
 */
//============================================================================================================

//============================================================================================================
//        REFERENCES
//============================================================================================================
using UnityEngine;

//============================================================================================================
namespace Utils
{
    /**
     *  @brief      Singleton helper class.
     *  @details    This helper class is used to implement the "Singleton" design pattern.
     */
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        //// Fields ////

        private static T classInstance;                         /**< Current instance of the object. */

        private static object lockInstance = new object();      /**< Object that locks the instance. */

        private static bool applicationIsQuitting = false;      /**< Flag that indicates that the application is quitting. */

        //// Properties ////

        /**
         *  @brief      Current instance of the class.
         *  @details    This property retrieves the current instance of the class.
         */
        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                    return null;

                lock (lockInstance)
                {
                    if (classInstance == null)
                    {
                        classInstance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                            return classInstance;

                        if (classInstance == null)
                        {
                            GameObject singleton = new GameObject();
                            classInstance = singleton.AddComponent<T>();
                            singleton.name = typeof(T).ToString();

                            DontDestroyOnLoad(singleton);
                        }
                    }

                    return classInstance;
                }
            }
        }

        //// Methods ////

        /**
         *  @brief      On destroy event.
         *  @details    When Unity quits, it destroys objects in a random order.
         *              In principle, a Singleton is only destroyed when application quits.
         *              If any script calls Instance after it have been destroyed, 
         *              it will create a buggy ghost object that will stay on the Editor scene
         *              even after stopping playing the Application. Really bad!
         *              So, this was made to be sure we're not creating that buggy ghost object.
         */
        public virtual void OnDestroy()
        {
            applicationIsQuitting = true;
        }

    }
}

//============================================================================================================
//        END OF FILE
//==========================================================================================================