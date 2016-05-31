using UnityEngine;
using System.Collections;

public class RippleObject : MonoBehaviour {

    CircleCollider2D _collider;
    SpriteRenderer skinSprite;
    [SerializeField] private Transform skinTransform;

    float delay = 0.5f;
    float timer;
    bool isActive;

    Vector2 newPosition;

	// Use this for initialization
	void Start () {
        _collider = GetComponent<CircleCollider2D>();
        LevelBall.rippleDistance = _collider.radius;
        skinSprite = skinTransform.GetComponent<SpriteRenderer>();

        timer = 0f;
        isActive = false;

        StartCoroutine(WaitForInput());

        DontDestroyOnLoad(this);
    }
	
	IEnumerator WaitForInput()
    {
        while (!isActive)
        {
            //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log(Input.GetTouch(0).deltaPosition);
                newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = newPosition;
                StartCoroutine(ActivePhase());
            }
            yield return false;
        }
    }

    IEnumerator ActivePhase()
    {
        isActive = true;
        timer = delay;

        _collider.enabled = true;
        transform.Translate(Vector2.zero);
        StartCoroutine(RippleEffect());
        yield return false;

        _collider.enabled = false;
        while (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                break;
            yield return false;
        }

        isActive = false;
        StartCoroutine(WaitForInput());
    }

    IEnumerator RippleEffect()
    {
        skinTransform.gameObject.SetActive(true);
        float limit = _collider.radius * 2 + 1;
        skinTransform.localScale = Vector3.zero;

        while (skinTransform.localScale.x < limit)
        {
            skinTransform.localScale = Vector3.one * (skinTransform.lossyScale.x + Time.deltaTime*limit*2);
            skinSprite.color = Color.Lerp(Color.white, Color.clear, Mathf.Pow(skinTransform.localScale.x / limit, 2f));
            yield return false;
        }
        skinTransform.gameObject.SetActive(false);
    }
}
