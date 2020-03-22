using System;
using Ludiq.PeekCore;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class EntityFactory : MonoBehaviour
{
    public static EntityFactory Instance { get; private set; }
    public int id = 1;

    private static Object ArrowPrefab;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        Instance = this;
        ArrowPrefab = Resources.Load("Prefabs/Projectiles/Arrow");
    }

    private void Start() {
    }

    /*------------------------------------------------------------------*\
    |*							KNIGHT
    \*------------------------------------------------------------------*/

    public void SetupKnight(Entity entity, bool cpu) {
        var strategy = new MeleStrategy(entity);

        entity.Id = id++;
        entity.Owner = new OwnerComponent(entity, cpu);
        entity.State = new StateComponent(entity);
        entity.Health = new HealthComponent(entity, healt: 300f);
        entity.Movable = new MovableComponent(entity, acceleration: 10f, speed: 5, stoppingDistance: 2f);
        entity.Engaging = new EngagingComponent(entity, strategy, rng: 1.5f, dmg: 20f, cd: 1.5f, delay: .4f);
        entity.Awareness = new AwarenessComponent(entity, ligneOfSight: 15f);
        entity.Animation = new AnimationComponent(entity);
        entity.Selectable = new SelectableComponent(entity);
        entity.Meta = new MetaComponent(entity, "Knight", "Textures/Icons/Icons/Sword_02");

    }

    public Entity CreateKnight(Vector3 position, Quaternion rotation, bool cpu = false) {
        var prefab = Resources.Load("Prefabs/Characters/Prefabs/Infantry/Knight");
        var go = Instantiate(prefab, position, rotation) as GameObject;
        var entity = go.GetComponent<Entity>();

        entity.Controller = entity.GetComponent<CharacterController>();
        entity.Animator = entity.GetComponent<Animator>();
        entity.Agent = entity.GetComponent<NavMeshAgent>();
        entity.Rb = entity.GetComponent<Rigidbody>();

        SetupKnight(entity, cpu);

        go.name = cpu ? $"Knight_CPU_{entity.Id}" : $"Knight_{entity.Id}";
        return entity;
    }

    /*------------------------------------------------------------------*\
    |*							ARCHER
    \*------------------------------------------------------------------*/

    public void SetupArcher(Entity entity, bool cpu) {

        var strategy = new RangedStrategy(entity);

        entity.Id = id++;
        entity.Owner = new OwnerComponent(entity, cpu);
        entity.State = new StateComponent(entity);
        entity.Health = new HealthComponent(entity, healt: 150f);
        entity.Movable = new MovableComponent(entity, acceleration: 10f, speed: 6, stoppingDistance: 2f);
        entity.Engaging = new EngagingComponent(entity, strategy, rng: 12f, dmg: 30f, cd: 3f, delay: .8f);
        entity.Awareness = new AwarenessComponent(entity, ligneOfSight: 16f);
        entity.Animation = new AnimationComponent(entity);
        entity.Selectable = new SelectableComponent(entity);
        entity.Meta = new MetaComponent(entity, "Archer", "Textures/Icons/Icons/Arrow_01");

    }

    public Entity CreateArcher(Vector3 position, Quaternion rotation, bool cpu = false) {
        var prefab = Resources.Load("Prefabs/Characters/Prefabs/Infantry/Archer");
        var go = Instantiate(prefab, position, rotation) as GameObject;
        var entity = go.GetComponent<Entity>();

        entity.Controller = entity.GetComponent<CharacterController>();
        entity.Animator = entity.GetComponent<Animator>();
        entity.Agent = entity.GetComponent<NavMeshAgent>();
        entity.Rb = entity.GetComponent<Rigidbody>();

        SetupArcher(entity, cpu);

        go.name = cpu ? $"Archer_CPU_{entity.Id}" : $"Archer_{entity.Id}";
        return entity;
    }

    public GameObject CreateArrow() {
        return Instantiate(ArrowPrefab) as GameObject;
    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/
}
