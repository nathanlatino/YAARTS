using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Entity Owner;
    private Rigidbody rb;
    private float delayBeforeDelete = 5f;
    private bool isImpact;
    private int OwnerLayer;
    private int ProjectileLayer;


    private void Start() {
        rb = GetComponent<Rigidbody>();
        OwnerLayer = Owner.gameObject.layer;
        ProjectileLayer = LayerMask.NameToLayer("Projectile");
    }

    private void FixedUpdate() {
        if (isImpact) return;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
        transform.rotation *= Quaternion.Euler(270, 0, 0);
    }


    protected void OnTriggerEnter(Collider other) {
        var otherLayer = other.gameObject.layer;
        if (otherLayer == OwnerLayer || otherLayer == ProjectileLayer) return;
        Hit(other);
        delayBeforeDelete = 5f;
    }

    private void Hit(Collider other) {
        rb.isKinematic = true;

        Entity otherEntity = other.GetComponent<Entity>();

        if (!isImpact && otherEntity != null) {
            if (otherEntity.Owner.IsCPU != Owner.Owner.IsCPU) {
                delayBeforeDelete = 1f;
                other.GetComponent<Entity>().Health.Hit(Owner.Engaging.Damages);
            }
        }
        isImpact = true;
        InitDestruction();
    }

    private void InitDestruction() {
        StartCoroutine(DestructionDelay());
    }

    private IEnumerator DestructionDelay() {
        yield return new WaitForSeconds(delayBeforeDelete);
        Destroy(this.gameObject);
    }
}
