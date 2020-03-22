using System.Linq;
using Ludiq.OdinSerializer.Utilities;
using UnityEngine;

public class OwnerComponent
{
    private static string colorsTextures = "Materials/TeamColors";

    private readonly Entity Entity;
    public readonly bool IsCPU;

    public readonly LayerMask Layer;
    public readonly LayerMask TargetLayer;

    public OwnerComponent(Entity entity, bool cpu) {
        IsCPU = cpu;
        Entity = entity;
        Layer = LayerMask.NameToLayer(IsCPU ? "CPU" : "Player");
        TargetLayer = LayerMask.GetMask(IsCPU ? "Player" : "CPU");
        Entity.gameObject.layer = Layer;
        SetColor();
    }

    private void SetColor() {
        var renderers = Entity.GetComponentsInChildren<Renderer>()
                              .Where(i => i.name != "Projection");
        var color = IsCPU ? "Units_red" : "Units_blue";
        var path = $"{colorsTextures}/{color}";

        var mat = Resources.Load(path, typeof(Material)) as Material;
        renderers.ForEach(r => r.material = mat);
    }
}
