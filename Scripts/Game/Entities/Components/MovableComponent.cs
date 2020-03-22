using UnityEngine;
using UnityEngine.AI;

public class MovableComponent
{
    private readonly Entity Entity;
    public Vector3? Destination { get; set; }

    public readonly float Acceleration;
    public readonly float Speed;
    public readonly float StoppingDistance;

    private float TotalDistance;

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    public NavMeshAgent Agent => Entity.Agent;
    public float RemainingDistance => Agent.remainingDistance;
    public bool ReachedDestination => RemainingDistance == 0;

    /*------------------------------------------------------------------*\
    |*							CONSTRUCTOR
    \*------------------------------------------------------------------*/

    public MovableComponent(Entity entity, float acceleration, float speed, float stoppingDistance) {
        Entity = entity;
        Speed = speed;
        Acceleration = acceleration;
        StoppingDistance = stoppingDistance;
        Agent.speed = speed;
        Agent.stoppingDistance = stoppingDistance;
    }

    /*------------------------------------------------------------------*\
    |*							PUBLIC METHODES
    \*------------------------------------------------------------------*/

    public void SetDestination(Vector3 destination) {
        TotalDistance = Vector3.Distance(destination, Entity.Position);
        Agent.SetDestination(destination);
        Destination = destination;

        Agent.stoppingDistance = TotalDistance > 2 * StoppingDistance
                ? StoppingDistance
                : StoppingDistance / 2;
    }

    public void ClearDestination() {
        ResetStoppingDistance();
        Agent.SetDestination(Entity.Position);
        Destination = null;
        TotalDistance = 0;
    }

    public void ResetStoppingDistance() {
        Agent.stoppingDistance = StoppingDistance;
    }

    public void DrawDestination() {
        if (Destination != null && Selection.Instance.DestinationOverlay && Entity.Selectable.IsSelected) {
            DebugTools.DrawDestination(Entity);
        }
    }
}
