using System;
using UnityEngine;


public class MouseListener
{
    private static readonly MouseListener instance = new MouseListener();

    public float dragSensibility = 10f;
    public bool dragging;
    public Vector3 start;


    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    public static MouseListener Instance => instance;

    /*------------------------------------------------------------------*\
    |*							CONSTRUCTORS
    \*------------------------------------------------------------------*/

    static MouseListener() { }
    private MouseListener() { }

    /*------------------------------------------------------------------*\
    |*							EVENTS
    \*------------------------------------------------------------------*/

    public event Action OnClickLeft;
    public void ClickLeftEvent() => OnClickLeft?.Invoke();

    public event Action OnClickRight;
    public void ClickRightEvent() => OnClickRight?.Invoke();

    public event Action OnDrag;
    public void DragEvent() => OnDrag?.Invoke();

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            InitDragging();
            ClickLeftEvent();
        }

        if (Input.GetMouseButtonDown(1)) {
            ClickRightEvent();
        }

        if (Input.GetMouseButtonUp(0)) {
            dragging = false;
        }

        if (dragging && Vector3.Distance(start, Input.mousePosition) > dragSensibility) {
            DragEvent();
        }
    }

    private void InitDragging() {
        if (!dragging) {
            dragging = true;
            start = Input.mousePosition;
        }
    }
}
