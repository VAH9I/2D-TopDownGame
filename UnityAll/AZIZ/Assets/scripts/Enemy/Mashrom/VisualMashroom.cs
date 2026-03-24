using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class VisualMashroom : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemyEntity _enemyEntity;
    SpriteRenderer _spriteRenderer;
    
    
    private Animator _animator;
    private Animator _animator2;

    private const string ISDIE = "_isDie";
    private const string ISRUN = "_isRun";
    private const string ATTACK = "Attack";
    private const string CHASING = "ChasingSpeedMultiplie";
    private const string TAKEHIT = "TakeHit";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _enemyAI.OnEnemyAttack += EnemyEntity_OnAttack;
        _enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
        _enemyEntity.OnDeath += EnemyEntity_OnDeath;
    }

    private void EnemyEntity_OnAttack(object sender, System.EventArgs e)
    {
        
        _animator.SetTrigger(ATTACK);

    }

    private void EnemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TAKEHIT);
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(ISDIE,true);
        _spriteRenderer.sortingOrder = -1;
    }

    private void Update()
    {
        _animator.SetBool(ISRUN,_enemyAI._IsRunning);
        _animator.SetFloat(CHASING, _enemyAI.GetRoamingAnimationSpeed());
    }
    public void TrigerAttackAnimTurnOff()
    {
        _enemyEntity.DisableCollider();
    }
    public void TrigerAttackAnimTurnOn()
    {
        _enemyEntity.EnableCollider();
    }
}