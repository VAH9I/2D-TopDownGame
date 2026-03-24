using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyAI))]

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
    private float _maxHealth;
    private float _damage;
    public static event EventHandler OnAnyDeath;
   // [SerializeField] private int _maxhealtPoint;
    private int _currentHealthPoint;
    private PolygonCollider2D _polygoncollider2d;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;
    private void Awake()
    {
        _polygoncollider2d = GetComponent<PolygonCollider2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }
   
   
    private void Start()
    {
        // Записываем значения из наших ползунков в переменные врага
        _maxHealth = POLYAEnemy.GameSettings.EnemyMaxHP;
        _damage = POLYAEnemy.GameSettings.EnemyDamage;

        // Устанавливаем текущее здоровье (округляем до целого числа, так как у тебя int)
        _currentHealthPoint = Mathf.RoundToInt(_maxHealth); 
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            // Наносим урон из наших настроек (тоже округляем до int, если TakeDamage просит int)
            player.TakeDamage(transform, Mathf.RoundToInt(_damage));
        }
    }
    public void TakeDamage(int damage)
    {
        _currentHealthPoint -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }
    public void DetectDeath()
    {
        if (_currentHealthPoint <= 0)
        {
            _enemyAI.SetDeathState();
            _boxCollider2D.enabled = false;
            _polygoncollider2d.enabled = false;
            OnDeath?.Invoke(this, EventArgs.Empty);
            OnAnyDeath?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject,5f);
        }
    }

    public void DisableCollider()
    {
        _polygoncollider2d.enabled = false;
    }
    public void EnableCollider() 
    {
        _polygoncollider2d.enabled = true;
    }
    

}
