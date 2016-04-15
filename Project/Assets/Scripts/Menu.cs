using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// falo qual é a orientação da tela
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// esta função é usada para chamar a proxima scenne
	public void start() {
		Application.LoadLevel ( "game" );
	}

	// esta função eh chamada para fechar o jogo
	public void close() {
		Application.Quit ();
	}
}
