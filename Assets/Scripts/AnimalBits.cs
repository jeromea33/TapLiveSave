using UnityEngine;
using System.Collections;

public class AnimalBits : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		gameObject.SetActive (false);
	}
}
