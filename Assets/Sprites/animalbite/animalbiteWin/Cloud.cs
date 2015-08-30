using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

    public float speed = 2f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed, 0, transform.position.z) * Time.deltaTime);
    }
}