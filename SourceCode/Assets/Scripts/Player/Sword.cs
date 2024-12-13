using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private Transform weaponCollider;

    private PlayerInput playerInput;
    private Animator animator;
    private PlayerController playerController;
    private Equipped equipped;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        equipped = GetComponentInParent<Equipped>();
        animator = GetComponent<Animator>();
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    void Start()
    {
        playerInput.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        //Debug.Log("atakuje");
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    // Odkomentowaæ to i void Update() jak juz zmienie sprite'a playera na bok i zeby podazal za myszka
    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if (mousePosition.x < playerScreenPoint.x)
        {
            equipped.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            equipped.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
