using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// variavel que define a vida
	public float life = 5f;
	
	// variavel da velocidade de deslocamento
	public float minSpeed = 2f;
	public float maxSpeed = 5f;
	float speed;

	// variavel com a bala para atirar
	public GameObject bullet;
	float lastShoot = 0;
		
	// variavel para guardar a particula da explosão
	public GameObject explosion;

	bool directionRight;

	// Use this for initialization
	void Start () {
		// gero a velocidade
		speed = Random.Range (minSpeed, maxSpeed);

		directionRight = (Random.value < 0.5);
		Debug.Log (directionRight);
	}
	
	// Update is called once per frame
	void Update () {
		// faço o deslocamento pra frente
		Vector3 velocity = Vector3.down * speed;

		// deslocamento lateral
		if (directionRight) {
			velocity += Vector3.right * speed;
		} else {
			velocity += Vector3.left * speed;
		}
		GetComponent<Rigidbody2D> ().velocity = velocity;


		// caso chegar a Y7 eu mudo o lado do deslocamento
		if ( transform.position.x >= 8 ) {
			directionRight = false;
		} else if( transform.position.x <= -8 ) {
			directionRight = true;
		}

		// caso ele passar do 4 eu começo a atirar
		if ( transform.position.y <= 5 ) {
			Shoot();
		}

		// caso ele passar da tela eu destruo ele
		if ( transform.position.y <= -5 ) {
			Destroy ( gameObject );
		}
				
		if ( life <= 0 ) {
			Dead ();
		}

	}

	// função para atirar
	void Shoot () {
		
		// caso ele apertar a tecla Space eu atiro
		// verifico se o ultimo tiro não foi a pouco tempo, para impedir tiros consecutivos
		if (lastShoot + .8f <= Time.time ) {
			
			// instancio um novo objeto(prefab) na tela
			GameObject instance = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
			lastShoot = Time.time;
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
