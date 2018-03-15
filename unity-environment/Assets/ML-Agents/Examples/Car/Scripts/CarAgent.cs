using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(IShipInput))]
[RequireComponent(typeof(Rigidbody))]

public class CarAgent : Agent
{
    private IShipInput _input;
    private Rigidbody _rb;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private bool _positiveGoalReached;
    private bool _negativeGoalReached;
    [SerializeField]
    private bool _isTouchingWall;

    [SerializeField] private Course _course;

    public void ReachPositiveGoal()
    {
        _positiveGoalReached = true;
    }

    public void ReachNegativeGoal()
    {
        _negativeGoalReached = true;
    }


    private void Start()
    {
        _input = GetComponent<IShipInput>();
        _rb = GetComponent<Rigidbody>();
        _startPosition = transform.localPosition;
        _startRotation = transform.localRotation;
        _course.Randomize();
    }

    public override List<float> CollectState()
    {
        var states = new List<float>();
        //acts
        states.Add(_input.Thruster);
        states.Add(_input.Rudder);
        states.Add(_input.IsBraking ? 1.0f : 0f);

        //position
        states.Add(gameObject.transform.position.z);
        states.Add(gameObject.transform.position.y);
        states.Add(gameObject.transform.position.x);

        //velocity
        states.Add(_rb.velocity.x);
        states.Add(_rb.velocity.y);
        states.Add(_rb.velocity.z);

        //sensors
        states.AddRange(GetComponentsInChildren<DepthSensor>().Select(distanceSensor => distanceSensor.Distance));

        return states;
    }

    public override void AgentStep(float[] action)
    {
        _input.Thruster = Mathf.Clamp(action[0], -1, 1);
        _input.Rudder = Mathf.Clamp(action[1], -1, 1);
        _input.IsBraking = action[2] > 0f;
        if (done) return;

        reward -= 0.01f;
        reward += 0.005f * _rb.velocity.magnitude;
        if (_isTouchingWall)
        {
            reward -= 0.5f;
        }
        if (_positiveGoalReached)
        {
            reward += 10f;
            done = true;
        }
        if (_negativeGoalReached)
        {
            reward += -10f;
            done = true;
        }
    }

    public override void AgentReset()
    {
        _negativeGoalReached = false;
        _positiveGoalReached = false;
        _isTouchingWall = false;
        _rb.velocity = Vector3.zero;
        _input.Thruster = 0;
        _input.Rudder = 0;
        _input.IsBraking = false;
        transform.localRotation = _startRotation;
        transform.localPosition = _startPosition + new Vector3(1, 0, 0) * Random.Range(-7.5f, 7.5f);
        _course.Randomize();
    }

    public void SetTouchingWall(bool isTouchingWall)
    {
        _isTouchingWall = isTouchingWall;
    }
}
