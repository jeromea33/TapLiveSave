using UnityEngine;
using System.Collections;

public class FaucetHandleLimit : MonoBehaviour {
		
	// Update is called once per frame
	private float x;
	void Update () {
		x = transform.rotation.eulerAngles.z;
		Debug.Log (x);
		if (x < 361 && x > 270)
			transform.rotation = Quaternion.Euler(0f,0f,0f);
		else if (x < 180 && x > 90)
			transform.rotation = Quaternion.Euler(0f,0f,90f);
	}
}