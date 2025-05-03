using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class RaceController : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;

    public Vector3 GetClosestPoint(Vector3 point, out Vector3 direction)
    {
        SplineUtility.GetNearestPoint(splineContainer.Spline, point, out float3 nearestPositon, out float t);
        Vector3 nearestPositionWorld = splineContainer.transform.TransformPoint(nearestPositon);
        SplineUtility.Evaluate(splineContainer.Spline, t + 0.001f, out float3 nearestPositionPlusDelta, out float3 tangent, out _);
        Vector3 nearestPositionPlusDeltaWorld = splineContainer.transform.TransformPoint(nearestPositionPlusDelta);
        direction = nearestPositionPlusDeltaWorld - nearestPositionWorld;
        direction.y = 0;
        direction = direction.normalized;

        return nearestPositionWorld;
    }
}

