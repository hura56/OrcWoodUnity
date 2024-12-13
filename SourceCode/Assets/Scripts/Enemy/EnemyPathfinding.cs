using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockback.knockedBack) { return; }
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        moveDir = direction;
    }

    public void ChangeMovementSpeed(float movementSpeed)
    {
        moveSpeed = movementSpeed;
    }
}
