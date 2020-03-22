using UnityEngine;
using Utils;

namespace System
{
    public class RangedStrategy : IAttackStrategy
    {
        private readonly Entity Entity;
        private readonly Transform Spawn;

        public RangedStrategy(Entity entity) {
            Entity = entity;
            Spawn = Entity.transform.Find("Extensions").Find("ProjectileSpawn");
        }

        public void Attack() {
            Entity.Engaging.HardLookAtTarget();
            Entity.Animation.SetAttackAnimation();

            FunctionDelay.Create(AttackCallback, .2f);
            FunctionDelay.Create(() => Entity.Animation.SetIdleAnimation(), .75f);
        }

        public void AttackCallback() {
            var arrow = EntityFactory.Instance.CreateArrow().GetComponent<Rigidbody>();
            arrow.GetComponent<Arrow>().Entity = Entity;
            arrow.position = Spawn.position;
            var origin = Spawn.position;
            var target = Entity.Engaging.Target.Position;
            var yOffset = Entity.Engaging.Target.Agent.height * 0.75f;
            arrow.velocity = Ballistic.ComputeInitialVelocity(origin, target, yOffset);
        }
    }
}
