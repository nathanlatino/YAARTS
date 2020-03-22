
public class IdleState : IState
{
    private readonly Entity Entity;

    public IdleState(Entity entity) {
        Entity = entity;
    }

    public void Update() {
        Entity.Awareness.Scan();
    }

    public void OnEntry() {
        Entity.Animation.SetIdleAnimation();
    }

    public void OnExit() {
        Entity.Awareness.Aggro.Clear();
        Entity.Awareness.Closest = null;
    }

    public override string ToString() {
        return "State: Idle";
    }
}
