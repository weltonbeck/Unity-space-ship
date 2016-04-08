using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Player : MonoBehaviour {

	// variavel que determina a minha velocidade
	// sempre que uma variavel for publica vc pode mudar o valor dela direto no objeto(prefab), 
	// por isto deve tomar um certo cuidado, pois as vezes mudamos o valor no codigo mas no objeto fica outro.
	public float speed = 10f;

	// Nesta função é chamada apenas quando inicia
	void Start () {
	
	}
	
	// Esta função é chamada a cada ciclo
	void Update () {

		// Pego o movimento horizontal do controle
		// ele me retorna de -1 a +1 sendo 0 o estado parado
		float moveHorizontal = Input.GetAxis ("Horizontal");

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

	}

}
