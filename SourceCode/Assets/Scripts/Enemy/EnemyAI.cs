using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Chasing
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    [SerializeField] private float detectRange = 7f;
    private PlayerController playerController;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int enemyDamage = 1;

    public TextMeshProUGUI lvlText;
    public int lvl = 1;

    private int currentEnemyDamage;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        currentEnemyDamage = enemyDamage;
        playerController = PlayerController.Instance;
        lvlText.text = $"Level {lvl}";
        StartCoroutine(StateMachine());
    }

    private void OnDestroy()
    {
        playerController.AddScore(100);
    }

    public int GetBaseDamage()
    {
        return enemyDamage;
    }

    public void SetDamage(int damage)
    {
        enemyDamage = damage;
        Debug.Log("Enemy damage set to: " + enemyDamage);
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            switch (state)
            {
                case State.Roaming:
                    yield return StartCoroutine(RoamingRoutine());
                    break;

                case State.Chasing:
                    yield return StartCoroutine(ChasingRoutine());
                    break;
            }
            yield return null;
        }
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            enemyPathfinding.ChangeMovementSpeed(2f); 
            if (IsPlayerInRange())
            {
                state = State.Chasing;
                yield break;
            }
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator ChasingRoutine()
    {
        while (state == State.Chasing)
        {
            enemyPathfinding.ChangeMovementSpeed(4f);
            if (!IsPlayerInRange())
            {
                state = State.Roaming;
                yield break;
            }
            if (IsInAttackRange())
            {
                playerController.TakeDamage(currentEnemyDamage);
                yield return new WaitForSeconds(1f);
            }

            enemyPathfinding.MoveTo(playerController.transform.position);
            yield return null;
        }
    }

    private bool IsPlayerInRange()
    {
        if (playerController == null) return false;

        float distance = Vector2.Distance(transform.position, playerController.transform.position);
        return distance <= detectRange;
    }

    private bool IsInAttackRange()
    {
        if(playerController == null) return false;

        float distance = Vector2.Distance(transform.position, playerController.transform.position);
        return distance <= attackRange;
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-25f, 25f), Random.Range(-25f, 25f));
    }

    public void SetLevel(int level)
    {
        lvl = level;
    }

}
