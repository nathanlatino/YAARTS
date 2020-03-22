using System;
using UnityEngine;

public class GUIDrawController : MonoBehaviour
{
    [Header("Rectangle Selection")]
    public float thickness = 1f;
    public Color color = Color.white;

    private bool selectionRectangleFlag;

    private SelectionRectangleDrawer selectionRectangle;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Start() {
        MouseListener.Instance.OnDrag += () => selectionRectangleFlag = true;

        selectionRectangle = new SelectionRectangleDrawer(thickness, color);
    }

    private void OnGUI() {
        if (selectionRectangleFlag) {
            MultiSelectionHandler();
        }
    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/

    private void MultiSelectionHandler() {
        if (!MouseListener.Instance.dragging) {
            selectionRectangleFlag = false;
        }
        selectionRectangle.DrawScreenRectBorder();
    }
}
