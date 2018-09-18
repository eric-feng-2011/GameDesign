using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public ParticleSystem explosion;
    public LayerMask enemy;
    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float maxLifeTime = 2f;
    public float explosionRadius = 5f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, maxLifeTime);
	}

    public void OnTriggerEnter(Collider other)
    {
        // Find all the enemys in an area around the grenade
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, enemy);

        for (int i = 0; i < colliders.Length; i++) {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody) {
                continue;
            }

            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            EnemyHealth enemyHealth = targetRigidbody.GetComponent<EnemyHealth>();

            if (!enemyHealth) {
                continue;
            }

            Vector3 explosionTarget = targetRigidbody.position - transform.position;

            float damage = CalculateDamage(explosionTarget);

            enemyHealth.TakeDamage((int) damage, targetRigidbody.position);
        }

        explosion.transform.parent = null;
        explosion.Play();

        Destroy(explosion.gameObject, explosion.duration);

        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition) {

        float explosionDistance = targetPosition.magnitude;

        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

        float damage = Mathf.Max(0f, relativeDistance * maxDamage);

        return damage;
    }
}
