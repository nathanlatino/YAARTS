using System;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class HealthComponent
{
    private readonly Entity Entity;

    public readonly HealthBar healthBar;

    public readonly float Total;
    public float Health;
    public float Percent;

    /*------------------------------------------------------------------*\
    |*							Constructor
    \*------------------------------------------------------------------*/

    public HealthComponent(Entity entity, float healt) {
        Entity = entity;
        Health = healt;
        Total = healt;
        Percent = 100;

        healthBar = Entity.GetComponentInChildren<HealthBar>();
        healthBar.InitHealth(healt);
    }

    /*------------------------------------------------------------------*\
    |*							Core
    \*------------------------------------------------------------------*/

    public void Hit(float value) {
        Health -= value;
        Percent = 100 * (Health / Total);
        healthBar.SetHealth(Health);
        Entity.Animation.SetDamagesAnimation();
        Bleed();
    }

    public void Kill() {
        Entity.transform.Find("Extensions").gameObject.SetActive(false);
        Health = 0;
        BloodBath();
    }

    /*------------------------------------------------------------------*\
    |*							Public methodes
    \*------------------------------------------------------------------*/

    public bool IsNull() {
        return Health <= 0;
    }

    public override string ToString() {
        return $"hp: {Health}/{Total}\t{Percent}%";
    }

    /*------------------------------------------------------------------*\
    |*							Private methodes
    \*------------------------------------------------------------------*/

    private void Bleed() {
        FunctionInterval.Create(BloodSpill(8, 8), .2f, Random.Range(2, 4));
    }

    private void BloodBath() {
        FunctionInterval.Create(BloodSpill(5, 12), .1f, Random.Range(20, 40));
    }

    private Action BloodSpill(float distance, int particles) {
        return () => BloodParticleSystem.Instance.SpawnBlood(
                Entity.Position,
                distanceMax: distance,
                particles: particles
        );
    }
}
