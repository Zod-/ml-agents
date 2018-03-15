
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private bool _positiveGoal;
    private void OnTriggerEnter(Collider other)
    {
        var agent = other.gameObject.GetComponent<CarAgent>();
        if (agent)
        {
            if (_positiveGoal)
            {
                agent.ReachPositiveGoal();
            }
            else
            {
                agent.ReachNegativeGoal();
            }
        }
    }
}
