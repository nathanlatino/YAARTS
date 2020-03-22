public class EngagingState : IState
{
    private readonly Entity Entity;

    public EngagingState(Entity entity) {
        Entity = entity;
    }

    public void Update() {
        if (Entity.Engaging.Target) {
            Entity.SetDestination(Entity.Engaging.Target.Position);
        }

        if (Entity.Engaging.IsTargetInRange()) {
            Entity.ClearDestination();
        }
    }

    public void OnEntry() {
        Entity.Animation.SetMoveAnimation();
    }

    public void OnExit() { }

    public override string ToString() {
        return "State: Engaging";
    }
}
