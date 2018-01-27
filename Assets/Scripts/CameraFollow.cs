using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {
    
    public float minZoom;
    public float maxZoom = 110f;
    public float dampTime = 0.2f;
    public float zoomSpeed = 1;
    //[HideInInspector]
    public List<Transform> targets = new List<Transform>();
    private Camera mainCamera;

    public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
    private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
    private Vector3 m_DesiredPosition;              // The position the camera is moving towards.

    private Bounds bounds;
    public SpriteRenderer spriteBounds;


    private float rightBound;
    private float leftBound;
    private float topBound;
    private float bottomBound;



    void Start () {
        mainCamera = gameObject.GetComponent<Camera>();
        bounds.Encapsulate(spriteBounds.bounds);
    }
	
	void LateUpdate () {
        Move();
        Zoom();
        if(Input.GetKeyDown("joystick button 7"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
    

    private void Awake()
    {
        mainCamera = GetComponentInChildren<Camera>();
    }
    private void Move()
    {
        // Find the average position of the targets.
        FindAveragePosition();


        float camVertExtent = mainCamera.orthographicSize;
        float camHorzExtent = mainCamera.aspect * camVertExtent;


        leftBound = bounds.min.x + camHorzExtent;
        rightBound = bounds.max.x - camHorzExtent;
        bottomBound = bounds.min.y + camVertExtent;
        topBound = bounds.max.y - camVertExtent;

        float camX = Mathf.Clamp(m_DesiredPosition.x, leftBound, rightBound);
        float camY = Mathf.Clamp(m_DesiredPosition.y, bottomBound, topBound);


        // Smoothly transition to that position.
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(camX,camY,m_DesiredPosition.z), ref m_MoveVelocity, dampTime);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        // Go through all the targets and add their positions together.
        for (int i = 0; i < targets.Count; i++)
        {
            // If the target isn't active, go on to the next one.
            if (!targets[i].gameObject.activeSelf)
                continue;

            // Add to the average and increment the number of targets in the average.
            averagePos += targets[i].position;
            numTargets++;
        }

        // If there are targets divide the sum of the positions by the number of them to find the average.
        if (numTargets > 0)
            averagePos /= numTargets;

        // Keep the same z value.
        averagePos.z = transform.position.z;

        // The desired position is the average position;
        m_DesiredPosition = averagePos;
    }


    private void Zoom()
    {
        // Find the required size based on the desired position and smoothly transition to that size.
        float requiredSize = FindRequiredSize();
        mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
    }


    private float FindRequiredSize()
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        // Start the camera's size calculation at zero.
        float size = 0f;

        // Go through all the targets...
        for (int i = 0; i < targets.Count; i++)
        {
            // ... and if they aren't active continue on to the next target.
            if (!targets[i].gameObject.activeSelf)
                continue;

            // Otherwise, find the position of the target in the camera's local space.
            Vector3 targetLocalPos = transform.InverseTransformPoint(targets[i].position);

            // Find the position of the target from the desired position of the camera's local space.
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / mainCamera.aspect);
        }

        // Add the edge buffer to the size.
        size += m_ScreenEdgeBuffer;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, minZoom);

        size = Mathf.Min(size, maxZoom);
        

        return size;
    }


    public void SetStartPositionAndSize()
    {
        // Find the desired position.
        FindAveragePosition();

        // Set the camera's position to the desired position without damping.
        transform.position = m_DesiredPosition;

        // Find and set the required size of the camera.
        mainCamera.orthographicSize = FindRequiredSize();
    }



}
