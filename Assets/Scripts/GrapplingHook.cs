using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private float grappleDistance;
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grapplingLayer;
    [SerializeField] private LineRenderer rope;


    [SerializeField] private RopeScript ropeScript;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float pullSpeed = 4f;
    


    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private bool isGrappling = false;

    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrappling && Input.GetMouseButtonDown(0))
            fireGrapplingHook();
      
        if (isGrappling)
        {
            if (Input.GetMouseButton(0))
                joint.distance = Mathf.Max(joint.distance - pullSpeed*Time.deltaTime, 1f);
            if (Input.GetMouseButtonDown(1))
                ReleaseGrapplingHook();
        }

        if (rope.enabled)
            rope.SetPosition(1, transform.position);
    }

    private void fireGrapplingHook()
    {
        Vector3 mouseWorldPos = playerCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, grapplingLayer);

        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            grapplePoint.z = 0f;
            joint.connectedAnchor = grapplePoint;
            joint.enabled = true;
            joint.distance = Vector2.Distance(transform.position, grapplePoint);
            rope.SetPosition(0, grapplePoint);
            rope.SetPosition(1, transform.position);
            rope.enabled = true;

            isGrappling = true;
        }
    }

    private void ReleaseGrapplingHook()
    {
        joint.enabled = false;
        rope.enabled = false;
        isGrappling = false;
    }
}
