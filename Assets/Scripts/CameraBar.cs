using UnityEngine;
using System.Collections;

public class CameraBar : MonoBehaviour {

	public float BarDisplay = 0f;
	public Vector2 BarPosition = new Vector2(10,90);
	public Vector2 BarSize = new Vector2 (80, 9);
	public float BarSpeed = 0.5f;
	public Texture2D ProgressBarFull;
	public Texture2D ProgressBarEmpty;
	private bool activateBar = true;

	// Update is called once per frame
	void Update () {
		if (activateBar) {
			BarDisplay -= Time.deltaTime * BarSpeed;
		}
	}

	void OnGUI(){
		if (activateBar) {
			// draw the background:
			GUI.BeginGroup (GetRekt (BarPosition, BarSize));
				GUI.Box (PercentBarSize(BarPosition, BarSize),ProgressBarEmpty);
				
				// draw the filled-in part:
				GUI.BeginGroup (PercentBarSize(BarPosition, BarSize));
					GUI.Box (GetFilledBar(BarPosition, BarSize),ProgressBarFull);
				GUI.EndGroup ();
			
			GUI.EndGroup ();
		}
	}

	/// <summary>
	/// Creates a rectangle based of screen size and percentage of the position chuchuchuchu
	/// </summary>
	/// <returns>The rekt.</returns>
	/// <param name="">.</param>
	/// <param name="yPercent">Y percent.</param>
	/// <param name="width">Width.</param>
	Rect GetRekt(float xPercent, float yPercent, float width, float height){
		float xTruePercent = xPercent * 0.01f;
		float yTruePercent = yPercent * 0.01f;
		float xTrueSize = width * 0.01f;
		float yTrueSize = height * 0.01f;
		float positionX = Screen.width * xTruePercent;
		float positionY = Screen.height * yTruePercent;
		float wed = Screen.width * xTrueSize;
		float hayt = Screen.height * yTrueSize;
		return new Rect(positionX,positionY,wed,hayt);
	}
	
	Rect GetRekt(Vector2 PercentPosition, Vector2 PercentSize){
		return GetRekt (PercentPosition.x, PercentPosition.y, PercentSize.x, PercentSize.y);
	}

	Rect PercentBarSize(Vector2 PercentPosition, Vector2 PercentSize){
		float xTrueSize = PercentSize.x * 0.01f;
		float yTrueSize = PercentSize.y * 0.01f;
		float wed = Screen.width * xTrueSize;
		float hayt = Screen.height * yTrueSize;
		return new Rect (0, 0, wed, hayt);
	}

	Rect GetFilledBar(Vector2 PercentPosition, Vector2 PercentSize){
		float xTrueSize = PercentSize.x * 0.01f;
		float yTrueSize = PercentSize.y * 0.01f;
		float wed = Screen.width - xTrueSize;
		float hayt = Screen.height * yTrueSize;
		return new Rect (0, 0, wed * BarDisplay, hayt);
	}

	public void ActivateBar(){
		activateBar = true;
	}

	public void DeactivateBar(){
		activateBar = false;
	}
}
