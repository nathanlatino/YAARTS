using UnityEngine;

public class MetaComponent
{
    private readonly Entity Entity;


    public readonly Texture2D portrait;
    public readonly Color color;
    public readonly string className;
    public readonly string playerType;

    public MetaComponent(Entity entity, string className, string portraitPath) {
        Entity = entity;
        this.className = className;
        this.portrait = Resources.Load(portraitPath, typeof(Texture2D)) as Texture2D;

        if (Entity.Owner.IsCPU) {
            this.playerType = "CPU";
            this.color = Color.red;
        }
        else {
            this.playerType = "Player";
            this.color = Color.blue;
        }
    }
}
