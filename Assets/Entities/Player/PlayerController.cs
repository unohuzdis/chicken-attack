using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 15.0f;
	public GameObject projectile;
	public AudioClip fireSound;
	public float padding = 1f;
	public float projectileSpeed = 10;
	public float firingRate = 0.2f;
	public float health = 250f;
	
	float xmin;
	float xmax;
  float ymin;
  float ymax;
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			Debug.Log ("Player Collided with missile");
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Die();
			}
		}
	}
	
	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
	}
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;

    Vector3 downmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
    Vector3 upmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
    ymin = downmost.y + padding;
    ymax = upmost.y - padding;
  }
	
	void Fire(){
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.0001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * speed * Time.deltaTime;
		}else if (Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime; 
		} else if (Input.GetKey(KeyCode.UpArrow)) {
      transform.position += Vector3.up * speed * Time.deltaTime;
    } else if (Input.GetKey(KeyCode.DownArrow)) {
      transform.position += Vector3.down * speed * Time.deltaTime;
    }
		
		// restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);

    float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
    transform.position = new Vector3(newX, newY, transform.position.z);
  }
	
	
	
}
