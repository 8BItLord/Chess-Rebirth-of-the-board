using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;    // biar gampang diakses
    public float moveSpeed = 5f;              // kecepatan gerak

    private void Awake()
    {
        instance = this;
    }

    public void MoveTo(Vector3 targetPos)
    {
        StopAllCoroutines();
        StartCoroutine(MoveSmooth(targetPos));
    }

    private System.Collections.IEnumerator MoveSmooth(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }
}
