using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Entity Entity;
    private Rigidbody rb;
    private float delayBeforeDelete = 5f;
    private bool isImpact;


    void Start() {
        rb = GetComponent<Rigidbody>();
        var source = LayerMask.NameToLayer("Projectile");
        var ignore = LayerMask.NameToLayer(Entity.Owner.IsCPU ? "CPU" : "Player");
        Physics.IgnoreLayerCollision(source, source);
        Physics.IgnoreLayerCollision(source, ignore);
    }

    private void FixedUpdate() {
        if (isImpact) return;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
        transform.rotation *= Quaternion.Euler(270, 0, 0);
    }

    protected void OnTriggerEnter(Collider other) {
        if (other == Entity.GetComponent<Collider>()) return;
        Hit(other);
        delayBeforeDelete = 5f;
    }

    private void Hit(Collider other) {
        rb.isKinematic = true;

        Entity otherEntity = other.GetComponent<Entity>();

        if (!isImpact && otherEntity != null) {
            if (otherEntity.Owner.IsCPU != Entity.Owner.IsCPU) {
                delayBeforeDelete = 1f;
                other.GetComponent<Entity>().Health.Hit(Entity.Engaging.Damages);
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
