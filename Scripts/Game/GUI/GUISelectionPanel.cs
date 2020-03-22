using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GUISelectionPanel : MonoBehaviour
{
    public static GUISelectionPanel Instance { get; private set; }

    private GameObject SelectionSlot;
    private Transform EmptyContainer;
    private Transform ItemContainer;
    private Transform ItemsContainer;
    private List<GameObject> Slots;

    private Text ClassType;
    private Text Id;
    private Text PlayerType;
    private Toggle Debug;
    private RawImage Slot;
    private Text Health;
    private Text Damages;
    private Text State;

    /*------------------------------------------------------------------*\
    |*							EVENTS
    \*------------------------------------------------------------------*/

    public event Action<Entity> OnPortraitClicked;
    public void PortraitClickedEvent(Entity entity) => OnPortraitClicked?.Invoke(entity);

    public event Action<bool> OnDebugCheckBoxClicked;
    public void DebugCheckBoxClickedEvent(bool value) => OnDebugCheckBoxClicked?.Invoke(value);

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/
    private void Awake() {
        Instance = this;
    }

    void Start() {
        SelectionSlot = Resources.Load("Prefabs/GUI/SelectionSlot") as GameObject;

        EmptyContainer = transform.Find("Empty");
        ItemsContainer = transform.Find("Items");
        ItemContainer = transform.Find("Item");

        PlayerType = ItemContainer.Find("PlayerType").GetComponent<Text>();
        ClassType = ItemContainer.Find("Class").GetComponent<Text>();
        Damages = ItemContainer.Find("Damages").GetComponent<Text>();
        Health = ItemContainer.Find("Health").GetComponent<Text>();
        State = ItemContainer.Find("State").GetComponent<Text>();
        Debug = ItemContainer.Find("Debug").GetComponent<Toggle>();
        Debug = ItemContainer.Find("Debug").GetComponent<Toggle>();
        Slot = ItemContainer.Find("SelectionSlot").GetComponentInChildren<RawImage>();
        Id = ItemContainer.Find("ID").GetComponent<Text>();


        EmptyContainer.gameObject.SetActive(true);

        Slots = Enumerable.Range(0, 16)
                          .Select(i => Instantiate(SelectionSlot, ItemsContainer))
                          .ToList();

        Slots.ForEach(i => i.SetActive(false));

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
        ItemsContainer.gameObject.SetActive(false);

        Slots.ForEach(
                s => {
                    var rawImage = s.GetComponentInChildren<RawImage>();
                    rawImage.texture = null;
                    rawImage.enabled = false;
                    s.SetActive(false);
                }
        );

        if (selection.Count == 1) {
            SingleSelectionHandler(selection.First());
        }
        else if (selection.Count > 1) {
            MultiSelectionHandler(selection);
        }
    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/

    private void MultiSelectionHandler(List<Entity> selection) {
        EmptyContainer.gameObject.SetActive(false);
        ItemContainer.gameObject.SetActive(false);
        ItemsContainer.gameObject.SetActive(true);

        for (var i = 0; i < selection.Count; i++) {
            if (i >= Slots.Count) {
                return;
            }
            var slot = Slots[i];
            var entity = selection[i];
            var button = slot.GetComponent<Button>();
            var rawImage = slot.GetComponentInChildren<RawImage>();

            slot.SetActive(true);
            rawImage.enabled = true;
            rawImage.texture = entity.Meta.portrait;
            button.onClick.AddListener(() => PortraitClickedEvent(entity));
        }
    }

    private void SingleSelectionHandler(Entity entity) {
        EmptyContainer.gameObject.SetActive(false);
        ItemContainer.gameObject.SetActive(true);
        ItemsContainer.gameObject.SetActive(false);

        ClassType.text = entity.Meta.className;
        Id.text = entity.Owner.IsCPU ? $"CPU_{entity.Id}" : $"_{entity.Id}";

        PlayerType = ItemContainer.Find("PlayerType").GetComponent<Text>();
        PlayerType.text = entity.Meta.playerType;
        PlayerType.color = entity.Meta.color;

        DynamicContent(entity);

        Debug.onValueChanged.AddListener(DebugCheckBoxClickedEvent);

        Slot.enabled = true;
        Slot.texture = entity.Meta.portrait;
    }

    private void DynamicContent(Entity entity) {
        Health.text = entity.Health.ToString();
        Damages.text = entity.Engaging.ToString();
        State.text = entity.State.State.ToString();
        Debug.isOn = entity.debug;
    }
}
