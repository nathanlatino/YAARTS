using System;
using System.Collections.Generic;

public class StateComponent
{
    private readonly Dictionary<Type, IState> states;
    private readonly Entity Entity;
    public IState State;

    public StateComponent(Entity entity) {
        Entity = entity;

        states = new Dictionary<Type, IState>() {
                { typeof(IdleState), new IdleState(Entity) },
                { typeof(MovingState), new MovingState(Entity) },
                { typeof(EngagingState), new EngagingState(Entity) },
                { typeof(EngagedState), new EngagedState(Entity) },
                { typeof(DeadState), new DeadState(Entity) }
        };
    }

    public void Update() {
        Entity.Animation.UpdateMoveAnimationSpeedValue(Entity.Speed);
        CheckConditions();
        State?.Update();
    }

    private void SetState(IState state) {
        State?.OnExit();
        State = state;
        State.OnEntry();
    }

    private void CheckConditions() {
        var haveTarget = Entity.Engaging.Target != null;
        var haveDestination = Entity.Movable.Destination != null;

        if (State == null) {
            SetState(states[typeof(IdleState)]);
        }

        if (State.GetType() != typeof(DeadState) && Entity.Health.IsNull()) {
            SetState(states[typeof(DeadState)]);
        }

        else if (!haveTarget && !haveDestination) {
            if (State.GetType() != typeof(IdleState)) {
                SetState(states[typeof(IdleState)]);
            }
        }

        else if (!haveTarget && haveDestination) {
            if (State.GetType() != typeof(MovingState)) {
                SetState(states[typeof(MovingState)]);
            }
        }

        else if (haveTarget && haveDestination) {
            if (State.GetType() != typeof(EngagingState)) {
                SetState(states[typeof(EngagingState)]);
            }
        }

        else if (haveTarget && !haveDestination) {
            if (State.GetType() != typeof(EngagedState)) {
                SetState(states[typeof(EngagedState)]);
            }
        }
    }
}
