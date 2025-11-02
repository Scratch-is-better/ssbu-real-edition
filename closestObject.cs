using System.Collections.Generic;
using UnityEngine;

public class closestObject : MonoBehaviour
{

    public List<Transform> targets = new List<Transform>();


    public Transform closestTarget;

    public float minDistance = Mathf.Infinity;
    private Transform referenceTransform;
    public bool draggable = false;

    void Start()
    {
        referenceTransform = this.transform;

        if (targets == null || targets.Count == 0)
        {
            Debug.LogWarning("no other objects");
        }
    }

    void Update()
    {
        FindClosestTarget();


        Debug.Log($"Closest object is: {closestTarget.name} at a distance of {minDistance:F2} units.");


        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        if (draggable) referenceTransform.position = mousePosition;//new Vector3(mousePosition.x, mousePosition.y, mousePosition.z);
    }


    public void FindClosestTarget()
    {
        closestTarget = null;
        minDistance = Mathf.Infinity;

        Vector3 referencePosition = referenceTransform.position;

        //for every object
        foreach (Transform target in targets)
        {



            float currentDistance = Vector3.Distance(referencePosition, target.position);



            // check if distance is the smaller
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                closestTarget = target;
            }
        }
    }
}
