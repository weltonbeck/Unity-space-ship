using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class Bullet : MonoBehaviour {

	public float speed = 8f;

	public bool isEnemy = false;

	// variavel para guardar a hora em que foi inicializado
	float initTime;

	// Use this for initialization
	void Start () {
		initTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (isEnemy) {
			transform.Translate (Vector3.down * speed * Time.deltaTime);
		} else {
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		}

		// caso tenha se passado muito tempo ele é destruido
		if (initTime + 1.5f <= Time.time) {
			Destroy (gameObject);
		}
	}
}
