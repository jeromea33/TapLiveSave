using UnityEngine;
using System.Collections;

public class AnimalBiteTowel : MonoBehaviour {
	
	void OnCollisionStay2D(Collision2D col){
		col.gameObject.SetActive (false);
	}
	
}
