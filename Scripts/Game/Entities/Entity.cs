using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour, IEntity
{
    public static List<Entity> All = new List<Entity>();

    public int Id { get; set; }
    public bool Destroyed { get; private set; }
    public bool debug { get; set; }

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    public Vector3 Position => transform.position;
    public float Speed => Agent.velocity.magnitude;

    /*------------------------------------------------------------------*\
    |*							COMPONENTS
    \*------------------------------------------------------------------*/

    public MetaComponent Meta { get; set; }
    public OwnerComponent Owner { get; set; }
    public StateComponent State { get; set; }
    public HealthComponent Health { get; set; }
    public MovableComponent Movable { get; set; }
    public EngagingComponent Engaging { get; set; }
    public AwarenessComponent Awareness { get; set; }
    public AnimationComponent Animation { get; set; }
    public SelectableComponent Selectable { get; set; }

    public CharacterController Controller { get; set; }
    public NavMeshAgent Agent { get; set; }
    public Rigidbody Rb { get; set; }
    public Animator Animator { get; set; }

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Start() {
        All.Add(this);
    }

    private void Update() {
        State?.Update();
        if (Destroyed) return;
        Movable.DrawDestination();
        Engaging.DrawTarget();
    }

    /*------------------------------------------------------------------*\
    |*							IEntity implementation
    \*------------------------------------------------------------------*/

    public void SetDestination(Vector3 destination) {
        Movable.SetDestination(destination);
    }

    public void SetTarget(Entity target) {
        Engaging.SetTarget(target);
        Movable.SetDestination(target.Position);
    }

    public void ClearDestination() {
        Movable.ClearDestination();
    }

    public void ClearTarget() {
        Engaging.ClearTarget();
    }

    /*------------------------------------------------------------------*\
    |*							HELPERS
    \*------------------------------------------------------------------*/

    public void Kill() {
        ClearTarget();
        ClearDestination();
        Health.Kill();

        Selection.Instance.RemoveFromSelectables(this);
        Selection.Instance.RemoveFromSelection(this);
        gameObject.layer = LayerMask.NameToLayer("Dead");

        Controller.enabled = false;
        Agent.radius = .1f;
        // Agent.enabled = false;

        // Movable = null;
        Selectable = null;
        State = null;
        Awareness = null;

        Destroyed = true;
    }

    public void DestroyEntity() {
        Destroy(gameObject);
    }

    /*------------------------------------------------------------------*\
    |*							STATIC
    \*------------------------------------------------------------------*/

    public static void DestroyAll() {
        All.ForEach(i => i.DestroyEntity());
        All.Clear();
    }
}
