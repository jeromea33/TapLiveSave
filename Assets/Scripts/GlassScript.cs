using UnityEngine;
using System.Collections;

public class GlassScript : MonoBehaviour {
	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag == "Mouth"){
			this.gameObject.SetActive (false);
		}
	}
}
