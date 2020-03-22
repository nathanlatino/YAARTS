using System.Collections.Generic;
using System.Linq;
using Ludiq.PeekCore;
using UnityEngine;
using UnityEngine.UI;

public class GUIActionPanel : MonoBehaviour
{
    public static GUIActionPanel Instance { get; private set; }
    private Transform EmptyContainer;
    private Transform ItemContainer;

    private Text Target;
    private Text InRange;
    private Text Destination;
    private Text TargetAlive;


    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        Instance = this;
    }

    void Start() {
        EmptyContainer = transform.Find("Empty");
        ItemContainer = transform.Find("Item");

        Target = ItemContainer.Find("Target").GetComponent<Text>();
        InRange = ItemContainer.Find("InRange").GetComponent<Text>();
        Destination = ItemContainer.Find("Destination").GetComponent<Text>();
        TargetAlive = ItemContainer.Find("TargetAlive").GetComponent<Text>();

        EmptyContainer.gameObject.SetActive(true);

        Selection.Instance.OnSelectionChanged += SelectionChangeHandler;
    }


    private void OnGUI() {
        if (Selection.Instance.Entities.Count == 1) {
            DynamicContent(Selection.Instance.Entities.First());
        }
    }

    /*------------------------------------------------------------------*\
    |*							HANDLERS
    \*------------------------------------------------------------------*/

    private void SelectionChangeHandler(List<Entity> selection) {
        EmptyContainer.gameObject.SetActive(true);
        ItemContainer.gameObject.SetActive(false);

        if (selection.Count == 1) {
            SingleSelectionHandler(selection.First());
        }
    }


    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/


    private void SingleSelectionHandler(Entity entity) {
        EmptyContainer.gameObject.SetActive(false);
        ItemContainer.gameObject.SetActive(true);
        DynamicContent(entity);
    }

    private void DynamicContent(Entity entity) {
        string target, inRange, targetAlive;

        var destination = entity.Movable.Destination != null
                ? entity.Movable.Destination.ToString()
                : "/";

        if (entity.Engaging.Target) {
            target = entity.Engaging.Target.name;
            inRange = entity.Engaging.IsTargetInRange().ToString();
            targetAlive = (!entity.Engaging.Target.IsDestroyed()).ToString();
        }
        else {
            target = "/";
            inRange = "/";
            targetAlive = "/";
        }


        Destination.text = $"Destination: {destination}";
        Target.text = $"Target: {target}";
        InRange.text = $"InRange: {inRange}";
        TargetAlive.text = $"TargetAlive: {targetAlive}";
    }
}
