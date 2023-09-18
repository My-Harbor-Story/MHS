using UnityEngine;
using System.Collections;

public class change : MonoBehaviour {

	// on définit une variable privée qui va contenir le composant Animator de l'objet sur lequel est appliqué le script
	public KeyCode keyboard;
	public GameObject target1;
	public GameObject target2;

	// Fonction Start se lance 1 fois au lancement du jeu
	void Start () {
		target1.SetActive (true);
		target2.SetActive (false);
	}

	// Fonction Update s'execute à chaque frame (si tu tourne à 60FPS elles s'éxecute 60 fois en 1 seconde.
	void Update () {

		if (Input.GetKeyDown (keyboard)) {
			if (target1.activeSelf) {
				target1.SetActive (false);
				target2.SetActive (true);

			} else {
				target1.SetActive (true);
				target2.SetActive (false);
			}
		}
	}
}

//
