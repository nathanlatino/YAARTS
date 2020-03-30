using System.Linq;
using Ludiq.OdinSerializer.Utilities;
using UnityEngine;

public class Debuging : MonoBehaviour
{
    public static Debuging Instance { get; private set; }

    [Header("General")]
    public bool ShowDebugLayout;

    public bool ShowSingleSelectionLayout;

    [Header("Layouts")]
    public bool LineOfSight;

    public bool Range;
    public bool AggroList;
    public bool Target;

    [Header("Other")]
    public bool DebugClicks;

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    public Selection Selection => Selection.Instance;

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

    private void Update() {
        DrawEntitiesLayouts();
    }

    /*------------------------------------------------------------------*\
    |*							SUBSCRIPTIONS
    \*------------------------------------------------------------------*/

    private void SubscribeToInputEvents() {
        Inputs.Instance.OnEntityLeftClicked += EntityLeftClickedHandler;
        Inputs.Instance.OnEntityRightClicked += EntityRightClickedHandler;
        Inputs.Instance.OnGroundLeftClicked += GroundLeftClickedHandler;
        Inputs.Instance.OnGroundRightClicked += GroundRightClickedHandler;
    }

    private void SubscribeToGUIEvents() {
        GUISelectionPanel.Instance.OnDebugCheckBoxClicked += DebugCheckBoxClickedHandler;
    }


    /*------------------------------------------------------------------*\
    |*							ENTITY DEBUG HANDLERS
    \*------------------------------------------------------------------*/

    private void DebugCheckBoxClickedHandler(bool value) => Selection.Entities[0].debug = value;

    /*------------------------------------------------------------------*\
    |*							CLICK HANDLERS
    \*------------------------------------------------------------------*/
    private void EntityLeftClickedHandler(Entity entity) => Log($"EntityLeft: \t{entity.name}");
    private void EntityRightClickedHandler(Entity entity) => Log($"EntityRight: \t{entity.name}");
    private void GroundLeftClickedHandler(Vector3 position) => Log($"GroundLeft: \t{position}");
    private void GroundRightClickedHandler(Vector3 position) => Log($"GroundRight: \t{position}");

    private void Log(string message) {
        if (!DebugClicks) return;
        Debug.Log(message);
    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/

    private void DrawEntitiesLayouts() {
        if (ShowDebugLayout) {
            Selection.Selectables.Where(i => i.debug).ForEach(DebugFunctions);

            var count = Selection.Entities.Count;

            if (ShowSingleSelectionLayout && count == 1 && !Selection.Entities.First().debug) {
                DebugFunctions(Selection.Entities.First());
            }
        }
    }

    private void DebugFunctions(Entity entity) {
        if (entity.Meta.Type != "Character") return;

        if (LineOfSight) DebugTools.DrawLineOfSight(entity);
        if (Range) DebugTools.DrawRange(entity);
        if (AggroList) DebugTools.DrawAggroList(entity);
    }
}
