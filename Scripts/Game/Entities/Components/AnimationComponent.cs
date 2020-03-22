using System;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class AnimationComponent
{
    public static readonly int IDLE = Animator.StringToHash("idle");
    public static readonly int MOVE = Animator.StringToHash("move");
    public static readonly int ATTACK = Animator.StringToHash("attack");
    public static readonly int DAMAGES = Animator.StringToHash("damages");
    public static readonly int DEATH_A = Animator.StringToHash("death_a");
    public static readonly int DEATH_B = Animator.StringToHash("death_b");

    private readonly Entity Entity;

    private Animator Animator => Entity.Animator;


    public AnimationComponent(Entity entity) {
        Entity = entity;
    }

    public void UpdateMoveAnimationSpeedValue(float speed) {
        Animator.SetFloat(MOVE, speed);
    }

    public void SetIdleAnimation() {
        Animator.SetBool(ATTACK, false);
        Animator.SetBool(IDLE, true);
    }

    public void SetMoveAnimation() {
        Animator.SetBool(ATTACK, false);
        Animator.SetBool(IDLE, false);
    }

    public void SetAttackAnimation() {
        Animator.SetBool(ATTACK, true);
        Entity.Agent.speed = 0;
        Entity.Agent.acceleration = 1000f;
        FunctionDelay.Create(AttackCallback, 1.5f);
    }

    public void SetDamagesAnimation() {
        Animator.SetBool(DAMAGES, true);
        Entity.Agent.speed = 0;
        Entity.Agent.acceleration = 1000f;
        FunctionDelay.Create(DamagesCallback, 0.1f);
    }

    public void SetDeathAnimation() {
        Animator.SetBool(IDLE, false);
        Animator.SetBool(ATTACK, false);
        Animator.SetBool(Random.Range(0, 2) == 0 ? DEATH_A : DEATH_B, true);
    }


    /*------------------------------------------------------------------*\
    |*							CALLBACKS
    \*------------------------------------------------------------------*/

    public void DamagesCallback() {
        Animator.SetBool(DAMAGES, false);
        Entity.Agent.acceleration = Entity.Movable.Acceleration;
        Entity.Agent.speed = Entity.Movable.Speed;
    }

    public void AttackCallback() {
        // Animator.SetBool(DAMAGES, false);
        Entity.Agent.acceleration = Entity.Movable.Acceleration;
        Entity.Agent.speed = Entity.Movable.Speed;
    }
}
