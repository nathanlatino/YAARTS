using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BloodParticleSystem : MonoBehaviour
{
    public static BloodParticleSystem Instance { get; private set; }

    private MeshParticleSystem meshParticleSystem;
    private List<Single> singleList;

    private void Awake() {
        Instance = this;
        meshParticleSystem = GetComponent<MeshParticleSystem>();
        singleList = new List<Single>();
    }

    private void Update() {
        for (int i = 0; i < singleList.Count; i++) {
            Single single = singleList[i];
            single.Update();

            if (single.IsParticleComplete()) {
                singleList.RemoveAt(i);
                i--;
            }
        }
    }

    public void SpawnBlood(Vector3 position, Vector3? direction = null, float distanceMax = 7f, float particles = 3) {

        var dir = direction ?? new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)).normalized;

        for (int i = 0; i < Random.Range(3, particles + 1); i++) {
            singleList.Add(
                    new Single(
                            position,
                            UtilsClass.ApplyRotationToVector(dir, Random.Range(-15f, 15f)),
                            Random.Range(2f, distanceMax),
                            meshParticleSystem
                    )
            );
        }
    }


    /*
     * Represents a single Dirt Particle
     * */
    private class Single
    {
        private MeshParticleSystem meshParticleSystem;
        private Vector3 position;
        private Vector3 direction;
        private int quadIndex;
        private Vector3 quadSize;
        private float moveSpeed;
        private float rotation;
        private int uvIndex;

        public Single(Vector3 position, Vector3 direction, float distanceMax, MeshParticleSystem mesh) {

            this.position = position;
            this.direction = direction;
            this.meshParticleSystem = mesh;

            quadSize = new Vector3(.3f, .3f);
            rotation = Random.Range(0, 360f);
            moveSpeed = Random.Range(1f, distanceMax);
            uvIndex = Random.Range(0, 8);

            quadIndex = meshParticleSystem.AddQuad(position, rotation, quadSize, false, uvIndex);
        }

        public void Update() {
            position += direction * (moveSpeed * Time.deltaTime);
            rotation += 360f * (moveSpeed / 10f) * Time.deltaTime;

            meshParticleSystem.UpdateQuad(quadIndex, position, rotation, quadSize, false, uvIndex);

            float slowDownFactor = 3.5f;
            moveSpeed -= moveSpeed * slowDownFactor * Time.deltaTime;
        }

        public bool IsParticleComplete() {
            return moveSpeed < .1f;
        }
    }
}
