using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarAgent))]
public class WallCollisions : MonoBehaviour
{
    private readonly List<Collider> _touchingWallColliders = new List<Collider>();
    private CarAgent _carAgent;

    private void Start()
    {
        _carAgent = GetComponent<CarAgent>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag(Tags.Wall)) { return; }
        _touchingWallColliders.Add(other.collider);
        SetTouchingWall();
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.CompareTag(Tags.Wall)) { return; }
        _touchingWallColliders.Remove(other.collider);
        SetTouchingWall();
    }
    private void SetTouchingWall()
    {
        _carAgent.SetTouchingWall(_touchingWallColliders.Count > 0);
    }
}
