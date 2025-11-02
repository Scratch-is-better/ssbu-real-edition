/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{

    public Camera UiCam;
    public Camera fightCam;
    public Canvas menu;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        UiCam.enabled = true;
        fightCam.enabled = false;
        menu.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("swtiched1");

            fightCam.enabled = !fightCam.enabled;

            UiCam.enabled = !UiCam.enabled;


        }



        fightCam.fieldOfView = player.transform.position;

        
        


         /*   fightCam.enabled = true;
            UiCam.enabled = false;
            menu.enabled = false;

        }
        else if(Input.GetKeyDown(KeyCode.O))
        {

            Debug.Log("swtiched2");
            fightCam.enabled = false;
            UiCam.enabled = true;
            menu.enabled = true;

        }





    }


    public void playerAvgPos()
    {
        
        int max = 111;
        int default = 60


        player1.pos =
        player2pos=
        player3pos=
        player4pos=


        


    }



}
*/








using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class cameraControl : MonoBehaviour
{
    [Header("Tracking")]
    [Tooltip("The list of player-controlled characters to track.")]
    public List<Transform> targets;

    [Header("Movement & Zoom")]
    [Tooltip("How quickly the camera pans to the target position. Smaller values are faster.")]
    public float smoothTime = 0.3f;

    [Tooltip("The minimum (closest) orthographic size the camera can zoom to.")]
    public float minCamSize = 60;

    [Tooltip("The maximum (farthest) orthographic size the camera can zoom to.")]
    public float maxCamSize = 111;

    [Tooltip("The amount of padding (in world units) to add around the targets.")]
    public float borderPadding = 2f;

    // Private variables for smoothing
    private Camera cam;
    private Vector3 moveVelocity;
    private float zoomVelocity;
    private Vector3 startPos;
    void Start()
    {
        cam = GetComponent<Camera>();
        startPos = cam.transform.position;

    }


    void LateUpdate()
    {


        // Get the bounding box that contains all targets
        Bounds targetBounds = GetBoundsForAllTargets();

        // calculate the target position for the camera (the center of the bounds)
        Vector3 targetPosition = GetTargetPosition(targetBounds);

        // get  target cam size (the "zoom")
        float targetSize = GetTargetOrthographicSize(targetBounds);

        //  Smoothly move the camera's position

        Vector3 newPosition = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref moveVelocity,
            smoothTime
        );
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        // 5. Smoothly adjust the camera's orthographic size (zoom)
        cam.focalLength = Mathf.SmoothDamp(
            cam.focalLength,
            targetSize,
            ref zoomVelocity,
            smoothTime
        );
    }


    Vector3 GetTargetPosition(Bounds bounds)
    {
        // The target position is the center of the bounds.

        return new Vector3(bounds.center.x, bounds.center.y, transform.position.z);
    }


    float GetTargetOrthographicSize(Bounds bounds)
    {
        // get the size of the bounds plus padding
        float boundsHeight = bounds.size.y + borderPadding;
        float boundsWidth = bounds.size.x + borderPadding;


        // the cam size is half the vertical view.
        float requiredSizeForHeight = boundsHeight * 0.5f;

        // calculate size based on Width 

        float requiredSizeForWidth = (boundsWidth * 0.5f) / cam.aspect;



        // The final target size is the larger of the two required sizes.

        float targetSize = Mathf.Max(requiredSizeForHeight, requiredSizeForWidth);

        // use clamp do set the size between our defined min and max zoom levels
        return Mathf.Clamp(targetSize, minCamSize, maxCamSize);
    }



    Bounds GetBoundsForAllTargets()
    {
        // for only one target
        if (targets.Count == 1)
        {
            return new Bounds(targets[0].position, Vector3.zero);
        }

        // If there's more than one start with the first target's position.
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        // check for each player
        for (int i = 1; i < targets.Count; i++)
        {

            bounds.Encapsulate(targets[i].position);

        }

        return bounds;
    }


    public void AddPlayer(Transform target)
    {
        if (targets == null)
        {
            targets = new List<Transform>();
        }
        if (target != null && !targets.Contains(target))
        {
            targets.Add(target);
        }
    }


    public void Removeplayer(Transform target)
    {
        if (targets != null && target != null)
        {
            targets.Remove(target);
        }
    }
}
