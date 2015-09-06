using UnityEngine;
using System.Collections;

/// <summary>
/// Main camera functions.
/// </summary>
public class MainCameraFunctions : MonoBehaviour {

	public static GameObject MainCamera;
	public static Vector3 OriginalCameraPosition;
	public static float OriginalCameraSize = 5f;

	void Start(){
		MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		OriginalCameraPosition = MainCamera.transform.position;
	}

	public static void ChangeCameraSize(float newSize){
		MainCamera.GetComponent<Camera>().orthographicSize = newSize;
	}

	public static void DisableCamera(){
		MainCamera.SetActive (false);
	}

	public static void EnableCamera(){
		MainCamera.SetActive (true);
	}

	public static void RestoreCameraPosition(){
		MainCamera.transform.position = OriginalCameraPosition;
	}

	public static void ChangeBackgroundColor(Color backgroundColor){
		MainCamera.GetComponent<Camera>().backgroundColor = backgroundColor;
	}
}