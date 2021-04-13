using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactions;

[RequireComponent(typeof(StartApp))]
public class BrainRotator : MonoBehaviour
{
    public Camera thisMainCamera;
    public float originRotationSpeed = 5.0f;
    public float rotateAroundYAxisSpeed = 5.0f;

    public float FOVspeed = 5.0f;
    public float rotateToBestPointSpeed = 20f;

    public int idleSeconds = 30;

    public GameObject rotatorObjectX;

    public GameObject rotatorObjectY;

    private AtlasRegionsAndFunctionsManager thisAFManager;

    private TouchInteractions touchInteractions;
    private MouseInteractions mouseInteractions;
    private StartApp starter;
    public bool rotateOnStart = true;
    public bool rotateToBestPointEnabled = true;

    private bool rotatingTowardsBestView = false;
    private bool zoomingToBestView = false;
    private Vector3 bestView = new Vector3(0f, 0f, 0f);

    private float bestFOVView = 60f;

    // timer variables
    private float idleTimer = 0.0f;
    private float realTimer = 0.0f;

    // original rotation values
    private Vector3 originalRotation = new Vector3(0f, 0f, 0f);

    private float originalFOV = 0.0f;
    private float previousRotatorXAngle = 360f;


    void Start()
    {
        originalFOV = thisMainCamera.fieldOfView;

        thisAFManager = AtlasRegionsAndFunctionsManager.instance;
        if (rotateOnStart) { idleTimer = idleSeconds; }

        // find the camera manager, and make references to touch and mouse controls
        // so they can be enabled and disabled when needed
        GameObject tempCameraManager = GameObject.Find("CameraManager");
        if(tempCameraManager == null){
            Debug.LogError("This scene is missing the GameObject CameraManager, which contains touch and mouse interaction scripts");
        }
        touchInteractions = tempCameraManager.GetComponent<TouchInteractions>();
        mouseInteractions = tempCameraManager.GetComponent<MouseInteractions>();
        starter = gameObject.GetComponent<StartApp>();

    }

    // Update is called once per frame
    void Update()
    {

        //disable or enabled mouse and touch controls
        if(zoomingToBestView == true || rotatingTowardsBestView == true){
            setInteractions(false);
        }else{
            setInteractions(true);
        }
        
        // Debug.Log("rotator obj x: " + rotatorObjectX.transform.rotation.eulerAngles.x + " " + rotatorObjectX.transform.rotation.eulerAngles.y + " " + rotatorObjectX.transform.rotation.eulerAngles.z);
        // Debug.Log("rotator obj y: " + rotatorObjectY.transform.rotation.eulerAngles.x + " " + rotatorObjectY.transform.rotation.eulerAngles.y + " " + rotatorObjectY.transform.rotation.eulerAngles.z);
        // Debug.Log("FOV: " + thisMainCamera.fieldOfView);

        idleTimer += Time.deltaTime;
        realTimer += Time.deltaTime;

        if (!IsIdle())
        {
            // Debug.Log("reset idle timer");
            idleTimer = 0.0f;
        }

        if (rotateToBestPointEnabled)
        {
            if (rotatingTowardsBestView)
            {
                Quaternion currentRotation = rotatorObjectX.transform.rotation;
                Quaternion wantedRotation = Quaternion.Euler(bestView);

                if (rotatorObjectX.transform.rotation != wantedRotation)
                {
                    rotatorObjectX.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * rotateToBestPointSpeed);
                }
                else
                {
                    rotatingTowardsBestView = false;
                }
            }
            if (zoomingToBestView)
            {
                if (!IsCurrentFOV(bestFOVView))
                {
                    FOVScaleBrain(bestFOVView, FOVspeed * 2);
                }
                else
                {
                    zoomingToBestView = false;
                }
            }
        }
  
        if (!rotatingTowardsBestView && !zoomingToBestView)
        {
            if (idleTimer >= idleSeconds)
            {
                IdleRotate();
            }
        }
    }

    public void RotateBrainToBestView(Vector3 toX)
    {
        rotatingTowardsBestView = true;
        bestView = toX;
    }

    public void ZoomBrainToBestView(float FOV)
    {
        zoomingToBestView = true;
        bestFOVView = FOV;
    }

    private bool IsIdle()
    {
        if (Input.touchCount > 0 || Input.GetAxis("Mouse ScrollWheel") != 0 || Input.GetMouseButton(0)) 
        {
            return false;
        }
        Ray ray = thisMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // make sure the hit is on the layer brain (14), to filter out other, non-brain raycasts
            if (hit.collider.gameObject.layer == 14)
            {
                return false;
            }
        }
        return true;
    }

    // must be called in an update as it uses a lerp
    private void FOVScaleBrain(float FOV, float speed)
    {
        thisMainCamera.fieldOfView = Mathf.Lerp(thisMainCamera.fieldOfView, FOV, Time.deltaTime + speed);
    }

    private bool IsCurrentFOV(float FOV) {
        return Mathf.Abs(thisMainCamera.fieldOfView - FOV) < 0.1f ;
    }

    private void IdleRotate()
    {
        //  Debug.Log("is now idle");
        if (thisAFManager != null)
        {
            thisAFManager.closePopUp();
        }

        if (WasBrainRotatedByUser())
        {
            RotateBackToXZOrigin();
        }

        if (!IsCurrentFOV(originalFOV))
        {
           FOVScaleBrain(originalFOV, FOVspeed); 
        }

        if (!starter.GetIntroMessageOn() && realTimer > idleSeconds) {
            starter.ShowIntroMsgOnIdle();
        }

        RotateContinouslyOnYAxis(); 
    }

    private void RotateBackToXZOrigin()
    {
        Vector3 toX = new Vector3(originalRotation.x, rotatorObjectX.transform.rotation.eulerAngles.y, originalRotation.z);

        if (Vector3.Angle(rotatorObjectX.transform.eulerAngles, toX) > 0.01f)
        {
            Quaternion currentRotation = rotatorObjectX.transform.rotation;
            Quaternion wantedRotation = Quaternion.Euler(toX);
            rotatorObjectX.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * originRotationSpeed);
        }
        else
        {
            rotatorObjectX.transform.eulerAngles = toX;
        }
    }

    private void RotateContinouslyOnYAxis()
    {
        // Debug.Log("rotate on y axis");
        rotatorObjectY.transform.RotateAround(rotatorObjectY.transform.position, Vector3.up, 2 * Time.deltaTime * rotateAroundYAxisSpeed);
    }

    private bool WasBrainRotatedByUser()
    {
        if (rotatorObjectX.transform.rotation.x != originalRotation.x
        || rotatorObjectX.transform.rotation.z != originalRotation.y
        || rotatorObjectY.transform.rotation.z != originalRotation.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void setInteractions(bool enabled){
        if(enabled == true){
            if(mouseInteractions != null){
                mouseInteractions.MouseInteractionsEnabled(true);
            }
            if(touchInteractions != null){
                touchInteractions.TouchInteractionsEnabled(true);
            }
        }else{
            if(mouseInteractions != null){
                mouseInteractions.MouseInteractionsEnabled(false);
            }
            if(touchInteractions != null){
                touchInteractions.TouchInteractionsEnabled(false);
            }
        }

    }


}
