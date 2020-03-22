using UnityEngine;

public class DeadState : IState
{
    private readonly Entity Entity;

    public DeadState(Entity entity) {
        Entity = entity;
    }

    public void Update() { }

    public void OnEntry() {
        Entity.Animation.SetDeathAnimation();
        Entity.Kill();
    }

    public void OnExit() { }

    public override string ToString() {
        return "State: Dead";
    }
}
