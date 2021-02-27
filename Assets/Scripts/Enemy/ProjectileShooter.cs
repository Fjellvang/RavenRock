using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject Projectile;
    public float spawnSeconds;
	public float projectTileSpeed;

	// Start is called before the first frame update
	void Start()
    {
        InvokeRepeating(nameof(SpawnProjectile), 0, spawnSeconds);
    }

    public void SpawnProjectile()
	{
        var spawned = Instantiate(Projectile, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
        var spawnedRig = spawned.GetComponent<Rigidbody2D>();
        spawnedRig.AddForce(gameObject.transform.right * gameObject.transform.localScale.x * projectTileSpeed);
	}
}
