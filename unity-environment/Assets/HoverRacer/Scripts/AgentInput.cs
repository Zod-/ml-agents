using UnityEngine;

public class AgentInput : MonoBehaviour, IShipInput
{
    public float Thruster { get; set; } = 0;
    public float Rudder { get; set; } = 0;
    public bool IsBraking { get; set; } = false;
}