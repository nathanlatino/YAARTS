using System.Collections.Generic;
using Ludiq.PeekCore;
using UnityEngine;


public class AwarenessComponent
{
    private readonly Entity Entity;
    public readonly float LigneOfSight;

    public HashSet<Collider> Aggro = new HashSet<Collider>();
    public Entity Closest;

    public AwarenessComponent(Entity entity, float ligneOfSight) {
        Entity = entity;
        LigneOfSight = ligneOfSight;
    }

    public void Scan() {

        Aggro = Physics.OverlapSphere(
                Entity.Position,
                LigneOfSight,
                Entity.Owner.TargetLayer
        ).ToHashSet();

        if (Aggro.Count > 0) {
            Closest = Getclosest();
            Entity.SetTarget(Closest);
        }
    }

    private Entity Getclosest() {
        Collider closest = null;

        float currentClosestDistance = float.MaxValue;

        foreach (var target in Aggro) {
            float distance = Vector3.Distance(Entity.Position, target.transform.position);

            if (distance < currentClosestDistance) {
                currentClosestDistance = distance;
                closest = target;
            }
        }
        return closest.GetComponent<Entity>();
    }
}
