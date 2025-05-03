using UnityEngine;

[ExecuteAlways]
public class CameraPosition : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 localOffset;

    public void Update()
    {
        if (target == null)
            return;

        Vector3 positon = target.position;
        Vector3 offset = target.TransformVector(localOffset);
        offset.y = 0;
        positon += offset;
        positon += Vector3.up * localOffset.y;

        this.transform.position = positon;
    }
}
