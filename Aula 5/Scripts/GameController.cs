using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// variaveis para controllar o respawn
	public float minRespawn = 0.2f;
	public float maxRespawn = 3f;
	float respawn = 0f;
	float lastRespawn;

	// variavel para guardar o perigo
	public GameObject[] hazards;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// de tempo em tempo eu instancio um novo elemento
		if (Time.time - lastRespawn > respawn) {

			Vector2 position = new Vector2( Random.Range (-8, 8), Random.Range ( transform.position.y - 2,   transform.position.y + 4) );
			int index = Random.Range (0, hazards.Length);
			GameObject instance = Instantiate (hazards[index], position, transform.rotation) as GameObject;
			respawn = Random.Range (minRespawn, maxRespawn);
			lastRespawn = Time.time;
		}

	}
}
