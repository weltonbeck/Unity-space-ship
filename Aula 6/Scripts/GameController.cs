using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// quantidade de vidas
	public int lifes = 2;
	// esta variavel vai guardar todos os corações que aparecerão na tela
	List<GameObject> hudLifes = new List<GameObject>();

	// variavel para o score
	public int score = 0;
	GameObject hudScore;
	Text hudScoreText;

	// variaveis para controllar o respawn
	public float minRespawn = 0.2f;
	public float maxRespawn = 3f;
	float respawn = 0f;
	float lastRespawn;

	// variavel para guardar o perigo
	public GameObject[] hazards;
	
	// Use this for initialization
	void Start () {
		// gravo o coração em uma variavel
		GameObject heart = transform.FindChild ("Panel/Hearts/Heart").gameObject;

		// é aqui que vou inserir o hud de coração
		for (int i = 0; i < lifes; i++) {

			// instancio um novo coração na tela
			GameObject instanciate = Instantiate (heart, heart.transform.position, heart.transform.rotation) as GameObject;
			// insiro este novo coração dentro do elemento Hearts no canvas
			instanciate.transform.parent = transform.FindChild ("Panel/Hearts");
			// ativo o elemento
			instanciate.SetActive (true);

			// mudo o posicionamento do coração, colocando cada um um pouco pro lado
			RectTransform instanciateBarRect = instanciate.GetComponent<RectTransform> ();
			instanciateBarRect.anchoredPosition = new Vector2 ( instanciateBarRect.anchoredPosition.x + ( i * 30 ), instanciateBarRect.anchoredPosition.y );
			// jogo o elemento dentro da variavel hudsLifes para controlar mais facil
			hudLifes.Add( instanciate );
		}

		// score
		// pego o elemento score
		hudScore = transform.FindChild ("Panel/Score/points").gameObject;
		// pego a classe Text deste elemento
		hudScoreText = hudScore.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		// de tempo em tempo eu instancio um novo elemento
		if (Time.time - lastRespawn > respawn) {

			Vector2 position = new Vector2( Random.Range (-8, 8), Random.Range ( 9, 13) );
			int index = Random.Range (0, hazards.Length);
			GameObject instance = Instantiate (hazards[index], position, transform.rotation) as GameObject;
			respawn = Random.Range (minRespawn, maxRespawn);
			lastRespawn = Time.time;
		}

		// caso a vida seja igual a 0 eu volto pra tela inicial
		if ( lifes <= 0 ) {
			Application.LoadLevel ( "menu" );
		}
	}

	void ApplyDamage () {
		lifes--;
		
		if ( hudLifes[lifes] ) {
			hudLifes[lifes].SetActive(false);
		}
	}

	// função para inserir o score
	void SetScore( int points ) {
		score += points;
		hudScoreText.text = score.ToString ("D8");
	}
}
