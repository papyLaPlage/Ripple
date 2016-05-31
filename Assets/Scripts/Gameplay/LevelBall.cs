using UnityEngine;
using System.Collections;

public class LevelBall : MonoBehaviour {

    [SerializeField] private float expensionDistance;
    Transform _transform;
    Rigidbody2D _rigidBody;
    Vector2 originPosition;
    Vector2 targetPosition;

    public static float rippleDistance;
    static float motionFactor = 4.5f; // 1 = normal speed, 2 = twice as fast, 0.5 half as fast
    float timer;

    bool isExpending;

    // Use this for initialization
    void Start () {
        _transform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody2D>();
        originPosition = _transform.position;
        timer = -1f;
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        targetPosition = originPosition + ((originPosition - (Vector2)co.transform.position).normalized * (1f - (Mathf.Clamp(Vector2.Distance(originPosition, co.transform.position) / rippleDistance, 0f, 1f))) * expensionDistance);
        if (timer <= 1f)
            timer = 0f;
        if (!isExpending)
            StartCoroutine(ExpendingPhase());
    }

    IEnumerator ExpendingPhase()
    {
        if (!isExpending)
        {
            isExpending = true;
            while (isExpending)
            {
                timer += Time.deltaTime * motionFactor;
                _rigidBody.MovePosition(Vector2.Lerp(originPosition, targetPosition, timer));
                if (timer > 1f)
                    break;
                yield return false;
            }

            if (timer > 1f)
            {
                timer = 1f;
                _rigidBody.velocity = Vector2.zero;
                _transform.position = targetPosition;
                if (isExpending)
                {
                    StartCoroutine(DeflatingPhase());
                }
            }
        }
    }
    IEnumerator DeflatingPhase()
    {
        if (isExpending)
        {
            isExpending = false;
            while (!isExpending)
            {
                timer -= Time.deltaTime * motionFactor;
                _rigidBody.MovePosition(Vector2.Lerp(originPosition, targetPosition, timer));
                if (timer <= 0f)
                    break;
                yield return false;
            }

            if (timer <= 0f)
            {
                timer = -1f;
                _rigidBody.velocity = Vector2.zero;
                _transform.position = originPosition;
            }
        }
    }
}
