using Utils;

public class EngagedState : IState
{
    private static int count;

    private readonly Entity Entity;
    private int id;

    public EngagedState(Entity entity) {
        Entity = entity;
    }

    public void Update() {
        if (!Entity.Engaging.IsTargetAlive() || !Entity.Engaging.IsTargetInRange()) {
            FunctionDelay.Create(
                    () => {
                        Entity.ClearTarget();
                        Entity.ClearDestination();
                    }, .4f
            );
        }
    }

    public void OnEntry() {
        id = count++;

        // Entity.Engaging.HardLookAtTarget();
        FunctionInterval.Create(
                Entity.Engaging.Attack,
                Entity.Engaging.AttackCoolDown,
                tag: $"AttackLoop#{id}"
        );
    }

    public void OnExit() {
        FunctionInterval.Stop($"AttackLoop#{id}");
    }

    public override string ToString() {
        return "State: Engaged";
    }
}
