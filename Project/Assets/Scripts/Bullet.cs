using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class Bullet : MonoBehaviour {

	public float speed = 8f;

	// variavel para guardar a hora em que foi inicializado
	float initTime;

	// Use this for initialization
	void Start () {
		initTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * speed * Time.deltaTime);

		// caso tenha se passado muito tempo ele é destruido
		if (initTime + 1f <= Time.time) {
			Destroy (gameObject);
		}
	}
}
