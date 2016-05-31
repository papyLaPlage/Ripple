using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    Transform _transform;
    Transform playerTransform;
    bool isFollowing;

    [SerializeField] private Vector3 offset;
    [SerializeField] private ScreenShader transitionScreenShader;

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
        transitionScreenShader.TransitionMaterial.SetFloat("_Cutoff", 1f);
    }

    IEnumerator FollowPhase()
    {
        if (!isFollowing)
        {
            isFollowing = true;
            while (isFollowing)
            {
                _transform.position = Vector3.Lerp(_transform.position, playerTransform.position, 0.5f) + offset;
                yield return false;
            }
        }
    }

    public void StartTransition()
    {
        StartCoroutine(TransitionPhase());
    }

    IEnumerator TransitionPhase()
    {
        transitionScreenShader.enabled = true;
        yield return false;

        while (Time.timeScale < 1f)
        {
            transitionScreenShader.TransitionMaterial.SetFloat("_Cutoff", 1f - Time.timeScale);
            yield return false;
        }
        transitionScreenShader.enabled = false;
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
}
