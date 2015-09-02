using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class PeopleScript : MonoBehaviour {

	public float speed = 1f;
	public bool trollololol = false;
	public bool isPanned = false;

	private Vector3 originalPosition;
	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
	}

	void OnEnable(){
		GetComponent<TouchScript.Gestures.Simple.SimplePanGesture>().PanStarted += OnPan;
		GetComponent<TouchScript.Gestures.Simple.SimplePanGesture>().PanCompleted += OnPanEnd;
	}

	void OnDisable(){
		GetComponent<TouchScript.Gestures.Simple.SimplePanGesture>().PanStarted -= OnPan;
		GetComponent<TouchScript.Gestures.Simple.SimplePanGesture>().PanCompleted -= OnPanEnd;
	}

	void OnPan(object sender, System.EventArgs e){
		Debug.Log (name + " is Panned");
		isPanned = true;
	}

	void OnPanEnd(object sender, System.EventArgs e){
		Debug.Log (name + " pan ended");
		isPanned = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPanned){
			return;
		}
		if(trollololol){
			if (transform.position.x < 0)
				MoveRight();
			else if(transform.position.x > 0)
				MoveLeft();
			if (transform.position.y > originalPosition.y)
				MoveDown();
			else if(transform.position.y < originalPosition.y)
				MoveUp();
		}
		else{
			if (transform.position.x < originalPosition.x)
				MoveRight();
			else if(transform.position.x > originalPosition.x)
				MoveLeft();
			if (transform.position.y > originalPosition.y)
				MoveDown();
			else if(transform.position.y < originalPosition.y)
				MoveUp();
		}
	}

	void MoveLeft(){
		transform.Translate (Vector3.left * Time.deltaTime * speed);
	}

	void MoveRight(){
		transform.Translate (Vector3.right * Time.deltaTime * speed);
	}

	void MoveUp(){
		transform.Translate (Vector3.up * Time.deltaTime * speed);
	}

	void MoveDown(){
		transform.Translate (Vector3.down * Time.deltaTime * speed);
	}
}
