using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{

    public NewGrapplingHook grapplingHook;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private int precision = 40;
    [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 2;
    private float waveSize = 0f;
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;
    private float moveTime = 0;
    private bool strightLine;


    private void OnEnable()
    {
        moveTime = 0;
        lineRenderer.positionCount = precision;
        waveSize = StartWaveSize;
        strightLine = false;

        LinePointsToFirePoint();

        lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
        grapplingHook.isGrappling = false;
    }
    private void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            lineRenderer.SetPosition(i, grapplingHook.transform.position);
        }
    }

    void DrawRope()
    {
        if (!strightLine)
        {
            if (lineRenderer.GetPosition(precision - 1).x == grapplingHook.grapplePoint.x)
            {
                strightLine = true;
                grapplingHook.Grapple();
            }
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {
            if (!grapplingHook.isGrappling)
            {
                grapplingHook.Grapple();
            }
            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;

                if (lineRenderer.positionCount != 2) { lineRenderer.positionCount = 2; }

                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);
            //Vector2 offset = Vector2.Perpendicular(grapplingHook.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingHook.transform.position, grapplingHook.grapplePoint, delta);
            Vector2 currentPosition = Vector2.Lerp(grapplingHook.transform.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            lineRenderer.SetPosition(i, currentPosition);
        }
    }

    void DrawRopeNoWaves()
    {
        lineRenderer.SetPosition(0, grapplingHook.transform.position);
        lineRenderer.SetPosition(1, grapplingHook.grapplePoint);
    }
}
