using UnityEngine;
using System.Collections;

/// <summary>
/// For insect bite
/// </summary>
public class BiteScript : MonoBehaviour {

	public bool isCreamed = false;

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag == "Cream"){
			isCreamed = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Cream"){
			isCreamed = false;
		}
	}
}
