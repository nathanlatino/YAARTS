using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }


    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    private MouseListener MouseListener => MouseListener.Instance;
    private Inputs Inputs => Inputs.Instance;


    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        MouseListener.Update();
    }
}
