using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    Transform _transform;
    Transform playerTransform;
    bool isFollowing;

	// Use this for initialization
	void Start () {
        _transform = GetComponent<Transform>();
        isFollowing = false;

        DontDestroyOnLoad(this);
    }

    public void FollowPlayer(){
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if (playerTransform != null)
        {
            StartCoroutine(FollowPhase());
        }
    }

    public void Stop()
    {
        isFollowing = false;
    }
    public void Reset()
    {
        Stop();
        _transform.position = Vector3.back;
    }


    IEnumerator FollowPhase()
    {
        if (!isFollowing)
        {
            isFollowing = true;
            while (isFollowing)
            {
                _transform.position = Vector3.Lerp(_transform.position, playerTransform.position, 0.5f) + Vector3.back;
                yield return false;
            }
        }
    }
}
