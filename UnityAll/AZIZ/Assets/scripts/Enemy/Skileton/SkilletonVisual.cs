using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SkilletonVisual : MonoBehaviour
{
   
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemyEntity _enemyEntity;  
    private Animator _animator;
    private Animator _animator2;
    private const string _isRunning = "IsRunning";
    private const string TAKEHIT = "TakeHit";
    private const string IS_DIE = "IsDie";
    private const string CHASING_SPEED_MULTIPLIER= "ChasingSpeedMultiplier";
    private const string ATTACK = "Attack";
    SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();  
    }
    private void Start()
    {
     _enemyAI.OnEnemyAttack += EnemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
        _enemyEntity.OnDeath += EnemyEntity_OnDeath;
    }
    private void EnemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = -1;
    }
    private void EnemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TAKEHIT);
    }
    private void OnDestroy()
    {
        _enemyAI.OnEnemyAttack -= EnemyAI_OnEnemyAttack;
    }
    private void EnemyAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
       _animator.SetTrigger(ATTACK);
    }
    private void Update()
    {
        _animator.SetBool(_isRunning,_enemyAI._IsRunning);
        _animator.SetFloat(CHASING_SPEED_MULTIPLIER, _enemyAI.GetRoamingAnimationSpeed());
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
