using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Player : MonoBehaviour {

	// variavel que determina a minha velocidade
	// sempre que uma variavel for publica vc pode mudar o valor dela direto no objeto(prefab), 
	// por isto deve tomar um certo cuidado, pois as vezes mudamos o valor no codigo mas no objeto fica outro.
	public float speed = 15f;

	[Header ("Atirar")]

	// variavel com a bala para atirar
	[Tooltip("Prefab da bala")]
	public GameObject bullet;
	float lastShoot = 0;

	GameObject gameController;

	// Nesta função é chamada apenas quando inicia
	void Start () {
		gameController = GameObject.FindWithTag("GameController");
	}
	
	// Esta função é chamada a cada ciclo
	void Update () {

		float moveHorizontal = Input.acceleration.x;

		if ( moveHorizontal == 0 ) {
			// Pego o movimento horizontal do controle
			// ele me retorna de -1 a +1 sendo 0 o estado parado
			moveHorizontal = Input.GetAxis ("Horizontal");
		}

		// pego o movimento vertical
		float moveVertical = Input.GetAxis ("Vertical");

		// Variavel do tipo Vector2 sendo os valores referente aos eixos X e Y
		// multiplico o valor do meu movimentoHorizontal pelo speed para determinar a velocidade do deslocamento.
		Vector2 movement = new Vector2(moveHorizontal * speed, moveVertical * speed);

		// Adiciono uma velocidade a meu player
		// GetComponent faz com que eu pegue algum component ou script inserido no meu objeto(prefab)
		// O velocity aplica o deslocamento apenas enquanto a força é aplicada, assim que parar de receber esta força o deslocamento tambem para.
		GetComponent<Rigidbody2D> ().velocity = movement;

		/*
		 * Outra forma de deslocar um objeto é inserindo uma força a ele, mas este funciona mais como um empurrão 
		 * que tem um grande impacto no começo e depois vai perdendo sua força, usado geralmente para pulos em jogos de plataforma.
		 *  
		 *	GetComponent<Rigidbody2D> ().AddForce ( new Vector2( 10f, 0f ) );
		 */

		/*
		 * Outra forma de fazer embora errado neste caso é usar os metodos o Transform
		 * não é indicado o uso deste recurso pois oque ele faz é um "micro teleporte" 
		 * caso deveria acontecer uma colisão ela só é detectada apos o deslocamento, diferente do velocity
		 * 
		 * transform.Translate(Vector3.right * moveHorizontal * speed * Time.deltaTime);
		 * 
		 * transform.position = new Vector3(transform.position.x + (moveHorizontal * speed * Time.deltaTime), transform.position.y);
		 */
				
		// faz o efeito de rotacionar ao virar
		// o objeto Quaternion é usado para controlar rotação 
		transform.rotation = Quaternion.Euler (0.0f, GetComponent<Rigidbody2D> ().velocity.x * -5, 0.0f);

		// limito a navegação na tela
		// o metodo Mathf.Clamp me retorna um valor, limitando o maximo e o minimo, com isto eu forço estes valores na posição
		transform.position = new Vector2( Mathf.Clamp (transform.position.x, -8, 8), Mathf.Clamp (transform.position.y,-3.5f, 3f) );

		Shoot ();

	}

	// função para atirar
	void Shoot () {

		// caso ele apertar a tecla Space eu atiro
		// verifico se o ultimo tiro não foi a pouco tempo, para impedir tiros consecutivos
		if ( ( Input.GetKeyDown (KeyCode.Space) 
		      || Input.GetTouch(0).phase == TouchPhase.Began ) && lastShoot + .2f <= Time.time ) {

			// instancio um novo objeto(prefab) na tela
			GameObject instance = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
			lastShoot = Time.time;
		}

	}

	// caso ele colidir com outro elemento
	void OnCollisionEnter2D(Collision2D coll) {		
		if ( coll.gameObject.tag == "Enemy" ) {
			// tiro uma vida
			// envio uma mensagem para este objeto, se ele estiver apto a ouvir ele faz alguma coisa
			gameController.SendMessage("ApplyDamage");
		}
	}

	// caso ele colidir com um elemento trigger
	void OnTriggerEnter2D(Collider2D coll) {		
		if (coll.gameObject.tag == "BulletEnemy") {
			// tiro uma vida
			// envio uma mensagem para este objeto, se ele estiver apto a ouvir ele faz alguma coisa
			gameController.SendMessage("ApplyDamage");
		}
	}

}
