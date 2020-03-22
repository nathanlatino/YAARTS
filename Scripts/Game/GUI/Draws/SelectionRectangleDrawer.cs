using UnityEngine;

public class SelectionRectangleDrawer
{
    private bool selectionStarted;
    private Vector3 mouseStartPoint;

    private readonly Texture2D rectangleTexture;
    private readonly Color color;
    private readonly float thickness;
    private bool draw;

    public SelectionRectangleDrawer(float thickness, Color color) {
        this.thickness = thickness;
        this.color = color;
        rectangleTexture = PrimitivesFactory.getInstance().getRectangleTexture();
    }

    public void DrawScreenRectBorder() {
        Rect rect = GetScreenRect();
        Rect Top = new Rect(rect.xMin, rect.yMin, rect.width, thickness);
        Rect Left = new Rect(rect.xMin, rect.yMin, thickness, rect.height);
        Rect Right = new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height);
        Rect Bottom = new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness);

        GUI.color = color;
        GUI.DrawTexture(Top, rectangleTexture);
        GUI.DrawTexture(Left, rectangleTexture);
        GUI.DrawTexture(Right, rectangleTexture);
        GUI.DrawTexture(Bottom, rectangleTexture);
    }

    private Rect GetScreenRect() {
        Vector3 p1 = MouseListener.Instance.start;
        Vector3 p2 = Input.mousePosition;

        p1.y = Screen.height - p1.y;
        p2.y = Screen.height - p2.y;

        Vector3 topLeft = Vector3.Min(p1, p2);
        Vector3 bottomRight = Vector3.Max(p1, p2);
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }
}
