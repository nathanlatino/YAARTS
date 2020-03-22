using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIScenarioPanel : MonoBehaviour
{
    public static GUIScenarioPanel Instance { get; private set; }

    private Transform ItemContainer;
    private Button ResetButton;

    /*------------------------------------------------------------------*\
    |*							EVENTS
    \*------------------------------------------------------------------*/

    public event Action OnResetClicked;
    public void ResetClickedEvent() => OnResetClicked?.Invoke();

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/
    private void Awake() {
        Instance = this;
    }

    void Start() {
        ItemContainer = transform.Find("Item");
        ResetButton = ItemContainer.Find("ResetButton").GetComponent<Button>();

        InitButtons();
    }

    /*------------------------------------------------------------------*\
    |*							INITIALIZATION
    \*------------------------------------------------------------------*/

    private void InitButtons() {
        ResetButton.onClick.AddListener(ResetClickedEvent);
    }
}
