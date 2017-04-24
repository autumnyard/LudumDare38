using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFarAway : MonoBehaviour {

    public float speed = 1;

    void Start()
    {
    }

	void Update ()
    {
        if (Director.Instance.currentScene == Structs.GameScene.Ingame)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
	}
}
