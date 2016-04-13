using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	// variavel que define a vida
	public float life = 5f;

	// variavel da velocidade de deslocamento
	public float minSpeed = 2f;
	public float maxSpeed = 8f;
	float speed;

	// variavel para rotacionar o asteroid
	Quaternion rotate;

	// variavel para guardar a particula da explosão
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		// gero a velocidade
		speed = Random.Range (minSpeed, maxSpeed);

		// gero o grau de rotação
		rotate = Quaternion.Euler (Random.Range (-10, 10), Random.Range (-10, 10), Random.Range (-10, 10));
	}
	
	// Update is called once per frame
	void Update () {
		// faço o deslocamento
		GetComponent<Rigidbody2D> ().velocity = Vector3.down * speed;

		// caso ele passar da tela eu destruo ele
		if ( transform.position.y <= -4 ) {
			Destroy ( gameObject );
		}

		// faço a rotação
		transform.rotation = transform.rotation * rotate;
	
		if ( life <= 0 ) {
			Dead ();
		}
	}

	// caso ele morrer
	void Dead () {
		Destroy (gameObject);

		// chamo a explosão
		GameObject instance = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
		Destroy (instance, 3f);
	}

	// caso ele colidir com outro elemento
	void OnCollisionEnter2D(Collision2D coll) {		
		if (coll.gameObject.tag == "Player" ) {
			// mato o player
		}
	}

	// caso ele colidir com um elemento trigger
	void OnTriggerEnter2D(Collider2D coll) {		
		if (coll.gameObject.tag == "Bullet") {
			// destruo a bala
			Destroy (coll.gameObject);

			// tiro a vida
			life -= 5;
		}
	}


}
