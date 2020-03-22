using Utils;

namespace System
{
    public class MeleStrategy : IAttackStrategy
    {
        private readonly Entity Entity;
        private EngagingComponent Engaging => Entity.Engaging;


        public MeleStrategy(Entity entity) {
            Entity = entity;
        }

        public void Attack() {
            Engaging.HardLookAtTarget();
            Entity.Animation.SetAttackAnimation();

            FunctionDelay.Create(AttackCallback, Engaging.AttackDelay);
        }

        public void AttackCallback() {
            if (Engaging.Target) {
                Engaging.Target.Health.Hit(Engaging.Damages);
            }
            Entity.Animation.SetIdleAnimation();
        }
    }
}
