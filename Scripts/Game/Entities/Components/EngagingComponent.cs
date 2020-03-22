using System;
using UnityEngine;
using Utils;

public class EngagingComponent
{
    private readonly Entity Entity;

    public Entity Target;
    public readonly float Range;
    public readonly float Damages;
    public readonly float AttackCoolDown;
    public readonly float AttackDelay;
    public readonly float DPS;

    private IAttackStrategy Strategy;


    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    public bool IsTargetInRange() => Vector3.Distance(Entity.Position, Target.Position) <= Range;
    public bool IsTargetAlive() => Target && !Target.Destroyed;

    /*------------------------------------------------------------------*\
    |*							CONSTRUCTOR
    \*------------------------------------------------------------------*/

    public EngagingComponent(Entity entity, IAttackStrategy strategy, float rng, float dmg, float cd, float delay) {
        Entity = entity;
        Range = rng;
        Damages = dmg;
        Strategy = strategy;
        AttackCoolDown = cd;
        AttackDelay = delay;
        DPS = (float) Math.Round(Damages / AttackCoolDown, 1);

    }

    /*------------------------------------------------------------------*\
    |*							PUBLIC METHODES
    \*------------------------------------------------------------------*/

    public void SetTarget(Entity target) {
        if (target != Entity) Target = target;
    }

    public void ClearTarget() {
        Target = null;
    }

    public void Attack() {
        Strategy.Attack();
    }

    public void HardLookAtTarget() {
        if (Entity.Engaging.Target == null) return;
        var deltaVec = Entity.Engaging.Target.Position - Entity.Position;
        var rotation = Quaternion.LookRotation(deltaVec);
        Entity.transform.rotation = rotation;
    }

    public void DrawTarget() {
        if (Target && Selection.Instance.TargetOverlay && Entity.Selectable.IsSelected) {
            DebugTools.DrawTarget(Entity);
        }
    }

    public override string ToString() {
        return $"dmg: {Damages} spd: {AttackCoolDown} dps: {DPS}";
    }
}
