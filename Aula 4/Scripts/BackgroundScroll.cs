using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

	/*
	 * A ideia é simular que a nave esta indo pra frente, animando o backgroud
	 * 
	 */

	// variavel que define o valor de quanto iremos mover o background
	public Vector2 Scroll = new Vector2 (0f , 0.03f);
	// variavel que guarda quanto o bckground já andou
	Vector2 Offset = new Vector2 (0f, 0f);
		
	void Update () {

		// a variavel Offset recebe o valor dela mais a soma de Scroll * Time.deltatime
		// Como a função Update é executada a cada ciclo do processamento sem um tempo exato existe a variavel Time.deltaTime
		// voce sempre deve multiplicar por ela para deixar o tempo exato independe da maquina
		Offset +=  Scroll * Time.deltaTime;

		// atualizamos o offset da imagem do background criando um efeito de parallax
		GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", Offset);
	}
}