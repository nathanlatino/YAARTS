using System.Collections.Generic;
using System.Linq;
using Ludiq.OdinSerializer.Utilities;
using Ludiq.PeekCore;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scenarios : MonoBehaviour
{


    public ScenarioTypes scenario;
    public Vector3 PlayerPosition = new Vector3(6, 0, -9);
    public Vector3 CPUPosition = new Vector3(6, 0, 16);
    public int MassSize = 49;
    public bool Archer;

    public enum ScenarioTypes { None, Basic, Mass, MassTwoTeams };

    public ScenarioTypes OldScenario { get; set; }


    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    private static EntityFactory factory => EntityFactory.Instance;
    private static Quaternion Rotation() => Quaternion.Euler(0, Random.Range(0, 361), 0);


    /*------------------------------------------------------------------*\
    |*							SUBSCRIPTIONS
    \*------------------------------------------------------------------*/

    private void SubscribeToGUIEvents() {
        GUIScenarioPanel.Instance.OnResetClicked += ResetHandler;
    }

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Start() {
        SubscribeToGUIEvents();
    }

    private void Update() {
        if (OldScenario == scenario) return;
        Clear();

        switch (scenario) {
            case ScenarioTypes.None:
                SetNone();
                break;
            case ScenarioTypes.Basic:
                if (Archer) SetBasicArcher();
                else SetBasic();
                break;
            case ScenarioTypes.Mass:
                SetMassOneTeam();
                break;
            case ScenarioTypes.MassTwoTeams:
                if (Archer) SetMassTwoTeamsArcher();
                else SetMassTwoTeams();
                break;
        }
    }


    /*------------------------------------------------------------------*\
    |*							HANDLERS
    \*------------------------------------------------------------------*/

    private void ResetHandler() {
        OldScenario = ScenarioTypes.None;
    }

    /*------------------------------------------------------------------*\
    |*							SCENARIOS
    \*------------------------------------------------------------------*/

    private void SetNone() {
        OldScenario = ScenarioTypes.None;
    }

    private void SetBasic() {
        OldScenario = ScenarioTypes.Basic;

        Selection.Instance.Selectables = new HashSet<Entity>() {
                factory.CreateKnight(new Vector3(8.3f, 0f, -20f), Rotation()),
                factory.CreateKnight(new Vector3(-1.3f, 0f, -0.5f), Rotation()),
                factory.CreateKnight(new Vector3(1.2f, 0f, 16.5f), Rotation()),
                factory.CreateKnight(new Vector3(16f, 0f, 2.4f), Rotation(), true)
        };
    }

    private void SetBasicArcher() {
        OldScenario = ScenarioTypes.Basic;

        Selection.Instance.Selectables = new HashSet<Entity>() {
                factory.CreateKnight(new Vector3(8.3f, 0f, -20f), Rotation()),
                factory.CreateArcher(new Vector3(-1.3f, 0f, -0.5f), Rotation()),
                factory.CreateKnight(new Vector3(1.2f, 0f, 16.5f), Rotation()),
                factory.CreateKnight(new Vector3(16f, 0f, 2.4f), Rotation(), true)
        };
    }

    private void SetMassOneTeam() {
        OldScenario = ScenarioTypes.Mass;
        var formation = Formations.SquareFormation(MassSize, PlayerPosition);

        for (int i = 0; i < MassSize; i++) {
            var entity = i > MassSize * 0.25f
                    ? factory.CreateKnight(formation[i], Rotation())
                    : factory.CreateArcher(formation[i], Rotation());

            Selection.Instance.Selectables.Add(entity);
        }
    }

    private void SetMassTwoTeams() {
        OldScenario = ScenarioTypes.MassTwoTeams;

        // var formation = Formations.SquareFormation(MassSize, PlayerPosition);

        Selection.Instance.Selectables = Formations.SquareFormation(MassSize, PlayerPosition)
                                                   .Select(i => factory.CreateKnight(i, Rotation()))
                                                   .ToHashSet();

        Selection.Instance.Selectables.AddRange(
                Formations.SquareFormation(MassSize, PlayerPosition)
                          .Select(i => factory.CreateKnight(i, Rotation()))
                          .ToHashSet()
        );
    }

    private void SetMassTwoTeamsArcher() {
        OldScenario = ScenarioTypes.MassTwoTeams;

        var formation = Formations.SquareFormation(MassSize, PlayerPosition);

        for (int i = 0; i < MassSize; i++) {
            var entity = i > MassSize * 0.25f
                    ? factory.CreateKnight(formation[i], Rotation())
                    : factory.CreateArcher(formation[i], Rotation());

            Selection.Instance.Selectables.Add(entity);
        }

        formation = Formations.SquareFormation(MassSize, CPUPosition);

        for (int i = 0; i < MassSize; i++) {
            var entity = i > MassSize * 0.75f
                    ? factory.CreateArcher(formation[i], Rotation(), true)
                    : factory.CreateKnight(formation[i], Rotation(), true);

            Selection.Instance.Selectables.Add(entity);
        }
    }


    /*------------------------------------------------------------------*\
    |*							PRIVATE
    \*------------------------------------------------------------------*/

    private void Clear() {
        MeshParticleSystem.Instance.ClearParticles();
        Selection.Instance.SetEmptySelection();
        Selection.Instance.Selectables.ForEach(i => Destroy(i.gameObject));
        Selection.Instance.Selectables.Clear();
        Entity.DestroyAll();
    }
}
