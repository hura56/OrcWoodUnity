using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] public int health = 40;
    [SerializeField] public int maxThirst = 100;
    [SerializeField] public int maxHunger = 100;
    [SerializeField] private DamageFlash damageFlash;

    AudioManager audioManager;

    private PlayerInput playerInput;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    public int currentHealth;
    public HealthBar healthBar;
    public ThirstBar thirstBar;
    public HungerBar hungerBar;
    public int currentThirst;
    public int currentHunger;
    public int playerScore;
    private float timeSurvived = 0f;

    private void Awake()
    {
        Instance = this;
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    public void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        thirstBar.SetMaxThirst(maxThirst);
        hungerBar.SetMaxHunger(maxHunger);
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        StartCoroutine(CheckHungerRoutine());
        StartCoroutine(CheckThirstRoutine());
        playerScore = 0;
    }

    private void Update()
    {
        timeSurvived += Time.deltaTime;
        PlayerInput();
        Vector2 position = transform.position;

        position.x = Mathf.Clamp(position.x, -25f, 25f);
        position.y = Mathf.Clamp(position.y, -25f, 25f);

        transform.position = position;
    }

    private void FixedUpdate()
    {
        AdjustPlayerDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerInput.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    private void AdjustPlayerDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        audioManager.PlaySFX(audioManager.hit);
        if (damageFlash != null)
        {
            damageFlash.TriggerFlash();
        }
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            SaveHighScore(playerScore, timeSurvived);
            Debug.Log("Nie zyjesz");
            SceneManager.LoadScene("EndGame");
        }
    }

    private IEnumerator CheckHungerRoutine()
    {
        while (true)
        {
            currentHunger -= 1;
            if (currentHunger > 50 && currentHealth < health)
            {
                currentHealth += 1;
                currentHunger -= 1;
                healthBar.SetHealth(currentHealth);
            }
            if (currentHunger < 1 && currentHealth >= 2)
            {
                currentHealth -= 1;
                currentHunger = 0;
                healthBar.SetHealth(currentHealth);
            }
            hungerBar.SetHunger(currentHunger);
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator CheckThirstRoutine()
    {
        while (true)
        {
            currentThirst -= 1;
            thirstBar.SetThirst(currentThirst);
            if (currentThirst < 1)
            {
                currentHealth -= 2;
                currentThirst = 0;
                healthBar.SetHealth(currentHealth);
            }
            DetectDeath();
            yield return new WaitForSeconds(3f);
        }
    }

    private void SaveHighScore(int score, float timeSurvived)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float highTime = PlayerPrefs.GetFloat("HighTime", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        if (timeSurvived > highTime)
        {
            PlayerPrefs.SetFloat("HighTime", timeSurvived);
        }
        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.SetFloat("LastTime", timeSurvived);
        PlayerPrefs.Save();
    }

}
