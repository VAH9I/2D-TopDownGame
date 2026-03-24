using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI; // Обязательно добавляем для работы со Slider!

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnFlashBlink;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] public int _maxHealth = 10;
    [SerializeField] private float _recoveryTime = 0.5f;
    [SerializeField] private int _dashSpeed = 4;
    [SerializeField] private float _dashRecoveryTime = 0.2f;
    [SerializeField] private TrailRenderer _dashTrailRenderer;
    [SerializeField] public float _DashColdownTime = 3f;
    
    // --- НАШ НОВЫЙ ХП БАР ---
    [SerializeField] private Slider _hpBar; 
    // ------------------------

    private float _initialmovingSpeed;
    private Rigidbody2D rb;
    private KnockBack _knockBack;

    private float minMovingSpeed = 0.1f;
    private bool _isRunning;
    private float _curreHealth;
    private bool _canTakeDamage;
    private bool _isAlive;
    private bool _isDashing;
    Vector2 targetVelocity;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
        _initialmovingSpeed = speed;
    }

    void Start()
    {  
        _isAlive = true;
        _canTakeDamage = true;
        _curreHealth = _maxHealth;

        // --- НАСТРАИВАЕМ UI ПРИ СТАРТЕ ---
        if (_hpBar != null)
        {
            _hpBar.maxValue = _maxHealth;
            _hpBar.value = _curreHealth;
        }
        // ---------------------------------

        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        GameInput.Instance.OnPlayerDash += GameInput_OnPlayerDash;
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void GameInput_OnPlayerDash(object sender, System.EventArgs e)
    {
        Dash();
    }

    private void Dash()
    {
        if(!_isDashing)
            StartCoroutine(DashRecoveryRoutine());
    }

    private IEnumerator DashRecoveryRoutine()
    {   
        _isDashing = true;
        speed *= _dashSpeed;
        _dashTrailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashRecoveryTime);
        _dashTrailRenderer.emitting = false;
        speed = _initialmovingSpeed;
        yield return new WaitForSeconds(_DashColdownTime);
        _isDashing = false;
    }

    private void Update()
    {
        targetVelocity = GameInput.Instance.move();
    }

    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockBack)
        {
            return;
        }

        HandleMovement();
    }

    public bool IsAlive()
    {
        return _isAlive;
    }

    public void TakeDamage(Transform damageTransform, int damage)
    {   
        if (_canTakeDamage && _isAlive)
        {
            _canTakeDamage = false;
            
            // Слегка поправил логику отнимания ХП для надежности
            _curreHealth -= damage;
            _curreHealth = Mathf.Max(0, _curreHealth); 
            
            Debug.Log($"Текущее ХП: {_curreHealth}");

            // --- ОБНОВЛЯЕМ ПОЛОСКУ UI ---
            if (_hpBar != null)
            {
                _hpBar.value = _curreHealth;
            }
            // ----------------------------

            _knockBack.GetKnokedBack(damageTransform);
            OnFlashBlink?.Invoke(this, EventArgs.Empty);

            StartCoroutine(DamageRecoveryRoutine());
        }
        DetectDeath();
    }

    private void DetectDeath()
    {
        if(_curreHealth <= 0 && _isAlive)
        {   
            _isAlive = false;
            _knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();
            OnPlayerDeath?.Invoke(this,EventArgs.Empty);
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_recoveryTime);
        _canTakeDamage = true;
    }

    private void HandleMovement()
    {
        Vector2 moveVelocity = targetVelocity * speed;
        rb.linearVelocity = moveVelocity;
        
        if (Mathf.Abs(targetVelocity.x) > minMovingSpeed || Mathf.Abs(targetVelocity.y) > minMovingSpeed)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }

    public bool IsRunning()
    {
        return _isRunning;
    }

    public Vector3 GetPlayerScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
        GameInput.Instance.OnPlayerDash -= GameInput_OnPlayerDash; // Добавил отписку от дэша, чтобы не было утечек памяти!
    }
}