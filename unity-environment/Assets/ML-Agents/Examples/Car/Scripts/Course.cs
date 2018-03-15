using System.Collections.Generic;
using UnityEngine;

public class Course : MonoBehaviour
{
    [SerializeField]
    private float _obstacleOffset;
    [SerializeField]
    private List<GameObject> _obstacles;
    [SerializeField]
    private List<GameObject> _curves;

    private readonly Dictionary<GameObject, Vector3> _originalPositions = new Dictionary<GameObject, Vector3>();


    private void Awake()
    {
        foreach (var obstacle in _obstacles)
        {
            _originalPositions[obstacle] = obstacle.transform.localPosition;
        }
    }

    public void Randomize()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.transform.localPosition = _originalPositions[obstacle] + new Vector3(1f, 0, 0) * Random.Range(-_obstacleOffset, _obstacleOffset);
        }

        if (_curves.Count > 0)
        {
            foreach (var curve in _curves)
            {
                curve.SetActive(false);
            }
            _curves[Random.Range(0, _curves.Count)].SetActive(true);
        }
    }
}
