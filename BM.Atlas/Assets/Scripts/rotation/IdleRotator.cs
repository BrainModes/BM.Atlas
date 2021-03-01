using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRotator : MonoBehaviour
{
    // timer variables
    private float thisTimer = 0.0f;
    private int thisSeconds = 0;

    // original rotation values
    private float originalRotationX = 0.0f;
    private float originalRotationY = 0.0f;
    private float originalRotationZ = 0.0f;

    private float originalFOV = 0.0f;

    private bool isOriginFOVScaling = false;
    public Camera thisMainCamera;

    private bool isOriginRotating;
    public float originRotationSpeed = 5.0f;

    public int idleSeconds = 30;

    public GameObject rotatorObjectX;

    public GameObject rotatorObjectY;

    private AtlasRegionsAndFunctionsManager thisAFManager;

    public float rotatorSpeed = 5.0f;

    public float FOVspeed = 5.0f;

    void Start()
    {
      //  originalRotationX = rotatorObject.transform.rotation.eulerAngles.x;
      //  originalRotationY = rotatorObject.transform.rotation.eulerAngles.y;
      //  originalRotationZ = rotatorObject.transform.rotation.eulerAngles.z;

      originalFOV = thisMainCamera.fieldOfView;

      thisAFManager = AtlasRegionsAndFunctionsManager.instance;

    }

    // Update is called once per frame
    void Update()
    {
        // count up
        thisTimer += Time.deltaTime;
        thisSeconds = Mathf.RoundToInt(thisTimer % 60);
       // Debug.Log("seconds - " + thisSeconds.ToString());

        if(Input.anyKey || Input.touchCount > 0 || Input.GetAxis("Mouse ScrollWheel") != 0 || Input.GetMouseButton(0)){
            thisTimer = 0.0f;
            thisSeconds = 0;
           // Debug.Log("idle exit");
        }
        
        if (thisSeconds >= idleSeconds){
            // rotate
          //  Debug.Log("is now idle");


          if(thisAFManager != null){
            thisAFManager.closePopUp();
          }

          //rotate to origin position

          if(rotatorObjectX.transform.rotation.x != originalRotationX
          || rotatorObjectX.transform.rotation.z != originalRotationZ
          || rotatorObjectY.transform.rotation.z != originalRotationZ){
              isOriginRotating = true;
          }else{
              isOriginRotating = false;
          }

          if (isOriginRotating){

              Vector3 toX = new Vector3(originalRotationX, rotatorObjectX.transform.rotation.eulerAngles.y, originalRotationZ);

            if (Vector3.Angle(rotatorObjectX.transform.eulerAngles, toX) > 0.01f){

                Quaternion currentRotation = rotatorObjectX.transform.rotation;
                Quaternion wantedRotation = Quaternion.Euler(toX);
                rotatorObjectX.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * originRotationSpeed);


            }else{
              rotatorObjectX.transform.eulerAngles = toX;
              isOriginRotating = false;
            }
         }

         if (thisMainCamera.fieldOfView != originalFOV){
           isOriginFOVScaling = true;
         }else{
           isOriginFOVScaling = false;
         }

         if (isOriginFOVScaling){

           thisMainCamera.fieldOfView = Mathf.Lerp(thisMainCamera.fieldOfView, originalFOV, Time.deltaTime + FOVspeed);

         }

        //   Debug.Log("Is Origin Rotating = " + isOriginRotating.ToString());

          //rotate continously on Y axis

          rotatorObjectY.transform.Rotate(new Vector3(0,Time.deltaTime * rotatorSpeed,0));
        }
        
    }


}
