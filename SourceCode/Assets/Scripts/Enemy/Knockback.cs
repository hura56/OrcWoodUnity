using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool knockedBack {  get; private set; }

    [SerializeField] private float knockBackTime = .2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damage, float kbThrust)
    {
        knockedBack = true;
        Vector2 difference = (transform.position - damage.position).normalized * kbThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        knockedBack = false;
    }
}
