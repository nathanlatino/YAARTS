using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EntityFactory : MonoBehaviour
{
    public static EntityFactory Instance { get; private set; }

    private static Dictionary<String, EntityData> Prefabs;


    public int id = 1;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        Instance = this;
        InitializeEntitiesData();
    }

    /*------------------------------------------------------------------*\
    |*							INITIALIZATION
    \*------------------------------------------------------------------*/

    private void InitializeEntitiesData() {
        string icons = "Textures/Icons/Icons";
        string projectiles = "Prefabs/Projectiles";
        string entities = "Prefabs/Entities";
        string characters = $"{entities}/Characters";
        string collectables = $"{entities}/Collectables";

        Prefabs = new Dictionary<string, EntityData>() {
                {"Arrow", new EntityData($"{projectiles}/Arrow")},

                {"Gold", new EntityData($"{collectables}/Gold", $"{icons}/Gold_Parts")},

                {"Worker", new EntityData($"{characters}/Worker", $"{icons}/Axe_01")},
                {"Knight", new EntityData($"{characters}/Knight", $"{icons}/Sword_02")},
                {"Archer", new EntityData($"{characters}/Archer", $"{icons}/Arrow_01")},
        };
    }


    /*------------------------------------------------------------------*\
    |*							PROJECTILES
    \*------------------------------------------------------------------*/

    public GameObject CreateArrow() {
        return Instantiate(Prefabs["Arrow"].Prefab);
    }

    /*------------------------------------------------------------------*\
    |*							GOLD
    \*------------------------------------------------------------------*/

    public Entity CreateGold(Vector3 position, Quaternion rotation) {
        var go = Instantiate(Prefabs["Gold"].Prefab, position, rotation);
        var entity = go.GetComponent<Entity>();
        SetupGold(entity);
        go.name = $"Gold_Patch_{entity.Id}";
        return entity;
    }

    public void SetupGold(Entity entity) {
        entity.Id = id++;
        entity.Health = new HealthComponent(entity, healt: 30000f);
        entity.Selectable = new SelectableComponent(entity);
        entity.Meta = new MetaComponent(entity, "Gold", "Collectable", Prefabs["Gold"].Portrait);

    }

    /*------------------------------------------------------------------*\
    |*							WORKER
    \*------------------------------------------------------------------*/

    public Entity CreateWorker(Vector3 position, Quaternion rotation, bool cpu = false) {
        var go = Instantiate(Prefabs["Worker"].Prefab, position, rotation);
        var entity = go.GetComponent<Entity>();

        entity.Controller = entity.GetComponent<CharacterController>();
        entity.Animator = entity.GetComponent<Animator>();
        entity.Agent = entity.GetComponent<NavMeshAgent>();
        entity.Rb = entity.GetComponent<Rigidbody>();

        SetupWorker(entity, cpu);

        go.name = cpu ? $"Worker_CPU_{entity.Id}" : $"Worker_{entity.Id}";
        return entity;
    }

    public void SetupWorker(Entity entity, bool cpu) {
        var strategy = new MeleStrategy(entity);

        entity.Id = id++;
        entity.Owner = new OwnerComponent(entity, cpu);
        entity.State = new StateComponent(entity);
        entity.Health = new HealthComponent(entity, healt: 300f);
        entity.Movable = new MovableComponent(entity, acceleration: 10f, speed: 5, stoppingDistance: 2f);
        entity.Engaging = new EngagingComponent(entity, strategy, rng: 2f, dmg: 20f, cd: 1.5f, delay: .4f);
        entity.Awareness = new AwarenessComponent(entity, ligneOfSight: 15f);
        entity.Animation = new AnimationComponent(entity);
        entity.Selectable = new SelectableComponent(entity);
        entity.Meta = new MetaComponent(entity, "Worker", "Character", Prefabs["Worker"].Portrait);

    }

    /*------------------------------------------------------------------*\
    |*							KNIGHT
    \*------------------------------------------------------------------*/


    public Entity CreateKnight(Vector3 position, Quaternion rotation, bool cpu = false) {
        var go = Instantiate(Prefabs["Knight"].Prefab, position, rotation);
        var entity = go.GetComponent<Entity>();

        entity.Controller = entity.GetComponent<CharacterController>();
        entity.Animator = entity.GetComponent<Animator>();
        entity.Agent = entity.GetComponent<NavMeshAgent>();
        entity.Rb = entity.GetComponent<Rigidbody>();

        SetupKnight(entity, cpu);

        go.name = cpu ? $"Knight_CPU_{entity.Id}" : $"Knight_{entity.Id}";
        return entity;
    }

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
        entity.Meta = new MetaComponent(entity, "Knight", "Character", Prefabs["Knight"].Portrait);

    }

    /*------------------------------------------------------------------*\
    |*							ARCHER
    \*------------------------------------------------------------------*/

    public Entity CreateArcher(Vector3 position, Quaternion rotation, bool cpu = false) {
        var go = Instantiate(Prefabs["Archer"].Prefab, position, rotation);
        var entity = go.GetComponent<Entity>();

        entity.Controller = entity.GetComponent<CharacterController>();
        entity.Animator = entity.GetComponent<Animator>();
        entity.Agent = entity.GetComponent<NavMeshAgent>();
        entity.Rb = entity.GetComponent<Rigidbody>();

        SetupArcher(entity, cpu);

        go.name = cpu ? $"Archer_CPU_{entity.Id}" : $"Archer_{entity.Id}";
        return entity;
    }

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
        entity.Meta = new MetaComponent(entity, "Archer", "Character", Prefabs["Archer"].Portrait);

    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/
}


public struct EntityData
{
    public GameObject Prefab;
    public Texture2D Portrait;

    public EntityData(string prefabPath, string portraitPath = null) {
        Prefab = Resources.Load(prefabPath) as GameObject;
        Portrait = Resources.Load(portraitPath, typeof(Texture2D)) as Texture2D;
    }
}
