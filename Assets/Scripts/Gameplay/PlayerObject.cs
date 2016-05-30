﻿using UnityEngine;
using System.Collections;

public class PlayerObject : MonoBehaviour {

    Transform _transform;
    Rigidbody2D _rigidBody;

	// Use this for initialization
	void Start () {
        _transform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody2D>();

        DontDestroyOnLoad(this);
    }

    public void Reset()
    {
        _rigidBody.velocity = Vector2.zero;
        _transform.position = Vector3.zero;
    }
}