using UnityEngine;

public class SelectableComponent
{
    private readonly Entity Entity;
    public bool IsSelected;

    private readonly Renderer projection;


    public SelectableComponent(Entity entity) {
        Entity = entity;

        projection = Entity.transform
                           .Find("Extensions")
                           .Find("Projection")
                           .GetComponent<Renderer>();
        SetUnSelected();
    }

    public void SetSelected() {
        IsSelected = true;
        projection.enabled = true;
    }

    public void SetUnSelected() {
        IsSelected = false;
        projection.enabled = false;
    }
}
