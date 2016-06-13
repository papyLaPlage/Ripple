using UnityEngine;
using System.Collections;

public class PlayerObject : MonoBehaviour {

    public static PlayerObject Instance { get; private set; }

    public static float maxDepthAllowed;
    public SpriteRenderer _renderer;
    Transform _transform;
    Rigidbody2D _rigidBody;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        _transform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody2D>();

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (_transform.position.y < maxDepthAllowed)
            Reset();
    }

    public void Reset()
    {
		if(_rigidBody != null)
			_rigidBody.velocity = Vector2.zero;
		if (_transform != null)
			_transform.position = Vector3.zero;
    }
}
