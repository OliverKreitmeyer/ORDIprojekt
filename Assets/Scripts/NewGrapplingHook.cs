using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrapplingHook : MonoBehaviour
{
    public RopeScript grappleRope;

    [SerializeField] private LayerMask grapplingLayer;
    [SerializeField] private Camera playerCam;
    private DistanceJoint2D joint;
    [SerializeField] private float throwDistance = 5f;
    [HideInInspector] public Vector3 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    [SerializeField] private float pullSpeed = 4f;
    public bool isGrappling;
    [SerializeField] private float maxAngle = 8f;
    [SerializeField] private int rayCount = 8;

    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        grappleRope.enabled = false;
    }

    void Update()
    {
        if (!isGrappling && Input.GetMouseButtonDown(0))
            fireGrapplingHook();

        if (isGrappling)
        {
            if (Input.GetMouseButton(0))
                joint.distance = Mathf.Max(joint.distance - pullSpeed * Time.deltaTime, 1f);
            if (Input.GetMouseButtonDown(1))
            {
                joint.enabled = false;
                grappleRope.enabled = false;
            }
                
        }
    }


    private void fireGrapplingHook()
    {
        Vector3 mouseWorldPos = playerCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        RaycastHit2D hit = FindGrapplePointInCone(direction, maxAngle, rayCount);

        if (hit && hit.collider != null)
        {
            grapplePoint = hit.point;
            grapplePoint.z = 0f;
            grappleDistanceVector = (Vector2)(grapplePoint - transform.position);
            grappleRope.enabled = true;

        }
    }

    public void Grapple()
    {
        joint.connectedAnchor = grapplePoint;
        joint.enabled = true;
        joint.distance = Vector2.Distance(transform.position, grapplePoint);
        isGrappling = true;
    }

    private RaycastHit2D FindGrapplePointInCone(Vector3 baseDirection, float maxAngle, int rayCount)
    {
        for (int i = 0; i < rayCount; i++)
        {
            float angle = -maxAngle + (2 * maxAngle * i / (rayCount - 1));
            Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * baseDirection;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, throwDistance, grapplingLayer);

            if (hit.collider != null)
                return hit;
        }

        return Physics2D.Raycast(transform.position, baseDirection, throwDistance, grapplingLayer);
    }
}
