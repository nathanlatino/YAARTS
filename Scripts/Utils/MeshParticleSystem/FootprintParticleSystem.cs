
using UnityEngine;
using Utils;

public class FootprintParticleSystem : MonoBehaviour {

    public static FootprintParticleSystem Instance { get; private set; }

    private MeshParticleSystem meshParticleSystem;

    private void Awake() {
        Instance = this;
        meshParticleSystem = GetComponent<MeshParticleSystem>();
    }

    public void SpawnFootprint(Vector3 position, Vector3 direction) {
        Vector3 quadSize = new Vector3(3f, 3f);
        float rotation = UtilsClass.GetAngleFromVectorFloat(direction) + 90f;
        meshParticleSystem.AddQuad(position, rotation, quadSize, false, 0);
    }

}
