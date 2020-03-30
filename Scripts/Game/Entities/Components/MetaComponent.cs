using UnityEngine;

public class MetaComponent
{
    private readonly Entity Entity;

    public readonly Color Color;
    public readonly Texture2D Portrait;

    public readonly string Type; // Todo: Use enum !
    public readonly string ClassName; // Todo: Use enum !
    public readonly string PlayerType; // Todo: Use enum !

    public MetaComponent(Entity entity, string className, string type, Texture2D portrait) {
        Entity = entity;
        Type = type;
        ClassName = className;
        Portrait = portrait;

        if (Entity.Owner != null) {
            if (Entity.Owner.IsCPU) {
                this.PlayerType = "CPU";
                this.Color = Color.red;
            }
            else {
                this.PlayerType = "Player";
                this.Color = Color.blue;
            }
        }
    }
}
