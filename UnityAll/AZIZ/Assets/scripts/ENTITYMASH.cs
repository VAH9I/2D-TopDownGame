using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyAI))]
public class ENTITYMASH : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
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
        _currentHealthPoint = _enemySO.enemyHealt;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, _enemySO.enemyDamage);
        }
    }
    public void TakeDamage(int damage)
    {
        _currentHealthPoint -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }
    private void DetectDeath()
    {
        if (_currentHealthPoint <= 0)
        {
            _enemyAI.SetDeathState();
            _boxCollider2D.enabled = false;
            _polygoncollider2d.enabled = false;
            OnDeath?.Invoke(this, EventArgs.Empty);
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


