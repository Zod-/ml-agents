using UnityEngine;

public class DepthSensor : MonoBehaviour
{
    public float Distance;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private float _maxDistance = 1000f;


    private void FixedUpdate()
    {
        Distance = GetDistance();
    }

    private float GetDistance()
    {
        RaycastHit hitInfo;
        return Physics.Raycast(transform.position, transform.forward, out hitInfo, _maxDistance, _layerMask) ? hitInfo.distance : float.MaxValue;
    }


    private void OnDrawGizmosSelected()
    {
        var distance = GetDistance();
        if (distance > _maxDistance)
        {
            Gizmos.DrawRay(transform.position, transform.forward);
            return;
        }
        Gizmos.DrawRay(transform.position, transform.forward * distance);
        Gizmos.DrawSphere(transform.position + transform.forward * distance, 0.1f);
    }
}
