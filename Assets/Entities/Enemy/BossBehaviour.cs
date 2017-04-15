using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float projectileSpeed = 10f;
	public float health = 2500f;
	public float shotsPerSecond = 1.5f;
	public int scoreValue = 7500;

	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update(){
		float prob = shotsPerSecond * Time.deltaTime;
		if(Random.value < prob){
			Fire ();
		}
	}

	void Fire(){
		GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Die();
			}
		}
	}

	void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		scoreKeeper.Score(scoreValue);
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
	}
}
