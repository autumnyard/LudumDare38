using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour {

    private float speed = 5f;
    private float radius = 0.1f;
    private Vector2 center;

    private float _angle;

    private void Start()
    {
        // _centre = transform.position;
        center = Vector2.zero;
        radius = 5f;
        speed = 0.7f;
    }

    private void Update()
    {

        _angle += speed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * radius;
        transform.position = center + offset;
    }
}
