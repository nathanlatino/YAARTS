using System;
using System.Collections.Generic;
using System.Linq;
using Ludiq.OdinSerializer.Utilities;
using UnityEngine;
using UnityEngine.Serialization;


public class Selection : MonoBehaviour, IEntity
{
    public static Selection Instance { get; set; }

    [Header("Formation")]
    public float Offset = 1.5f;

    [Header("Overlays")]
    public bool DestinationOverlay = true;
    public bool TargetOverlay;

    public Color DestinationColor;
    public Color TargetColor;

    public HashSet<Entity> Selectables = new HashSet<Entity>();
    public List<Entity> Entities = new List<Entity>();

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    private Camera cam => Camera.main;

    /*------------------------------------------------------------------*\
    |*							EVENTS
    \*------------------------------------------------------------------*/

    public event Action<List<Entity>> OnSelectionChanged;

    public void SelectionChanged(List<Entity> selection) => OnSelectionChanged?.Invoke(selection);

    /*------------------------------------------------------------------*\
    |*							SUBSCRIPTIONS
    \*------------------------------------------------------------------*/

    private void SubscribeToInputEvents() {
        Inputs.Instance.OnEntityLeftClicked += EntityLeftClickedHandler;
        Inputs.Instance.OnEntityRightClicked += EntityRightClickedHandler;
        Inputs.Instance.OnGroundLeftClicked += GroundLeftClickedHandler;
        Inputs.Instance.OnGroundRightClicked += GroundRightClickedHandler;
        Inputs.Instance.OnRectangleSelection += RectangleSelectionHandler;
    }

    private void SubscribeToGUIEvents() {
        GUISelectionPanel.Instance.OnPortraitClicked += EntityLeftClickedHandler;
    }

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        SubscribeToInputEvents();
        SubscribeToGUIEvents();
    }

    /*------------------------------------------------------------------*\
    |*							IEntity implementation
    \*------------------------------------------------------------------*/


    public void SetDestination(Vector3 position) {
        if (Entities.Count == 1) {
            Entities.First().SetDestination(position);
        }
        else {
            var offsets = Formations.SquareFormation(Entities.Count, position, Offset);
            Entities.Zip(offsets, Tuple.Create).ForEach(i => i.Item1.SetDestination(i.Item2));
        }

        if (Entities.Count > 3) {
            Entities.ForEach(e => e.Agent.stoppingDistance = 1f);
        }
    }

    public void SetTarget(Entity target) {
        Entities.ForEach(e => e.SetTarget(target));
        // entities.ForEach(e => { if (!e.Owner.IsCPU)  e.SetTarget(target); });
    }

    public void ClearDestination() {
        Entities.ForEach(e => e.ClearDestination());
    }

    public void ClearTarget() {
        Entities.ForEach(e => e.ClearTarget());
    }

    /*------------------------------------------------------------------*\
    |*							HANDLERS
    \*------------------------------------------------------------------*/

    public void EntityLeftClickedHandler(Entity entity) {
        SetSelection(entity);
    }

    public void GroundLeftClickedHandler(Vector3 position) {
        ClearSelection();
        SelectionChanged(Entities);
    }

    public void EntityRightClickedHandler(Entity entity) {
        SetTarget(entity);
    }

    public void GroundRightClickedHandler(Vector3 position) {
        SetDestination(position);
        ClearTarget();
    }

    private void RectangleSelectionHandler(Bounds bounds) {
        var point = new Func<Vector3, Vector3>(p => cam.WorldToViewportPoint(p));

        var bounded = Selectables
                     .Where(s => bounds.Contains(point(s.transform.position)))
                     .ToList();

        SetSelection(bounded);
    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/

    private void SetSelection(Entity entity) {
        ClearSelection();
        Entities.Add(entity);
        entity.Selectable.SetSelected();
        SelectionChanged(Entities);
    }

    private void SetSelection(List<Entity> selection) {
        if (selection.Count == Entities.Count) return;

        Entities = selection;
        Entities.ForEach(i => i.Selectable.SetSelected());

        Selectables
               .Where(s => !selection.Contains(s))
               .ForEach(i => i.Selectable.SetUnSelected());

        SelectionChanged(Entities);
    }

    private void ClearSelection() {
        Entities.ForEach(e => e.Selectable.SetUnSelected());
        Entities.Clear();
    }

    public void RemoveFromSelection(Entity entity) {
        if (!entity.Selectable.IsSelected) return;
        entity.Selectable.SetUnSelected();
        Entities.Remove(entity);
        SelectionChanged(Entities);
    }

    /*------------------------------------------------------------------*\
    |*							PUBLIC METHODES
    \*------------------------------------------------------------------*/

    public void RemoveFromSelectables(Entity entity) {
        Selectables.Remove(entity);
    }

    public void SetEmptySelection() {
        SetSelection(new List<Entity>());
    }
}
