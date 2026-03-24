using UnityEngine;
using System;
using UnityEngine.AI;
using Adventure.Utils;
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State _startingState;
    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamingDistanceMin = 3f;
    [SerializeField] private float _roamingTimeMax = 2f;
    [SerializeField]private bool _isChasingEnemy = false;
    [SerializeField]private bool _isAttackingEnemy = false;
    [SerializeField] private float _attackingDistance = 2f;
    [SerializeField] private float _chasingSpeedMultiplier = 2f;
   [SerializeField] private float _chasingDistance = 4f;
    private NavMeshAgent _navMashAgent;
    private State _currentState;
    private float _roamingTime;
    private Vector3 _roamPosition;
    private Vector3 _startingposition;
    private float _roamingSpeed;
    private float _chasingSpeed;    
    [SerializeField]private float _AttackRate = 2f;
    private float _nextAttackTime = 0f;
    private float _checkDirectionDuration = 0.1f;
    private float _nextDirrectionTime = 0f;
    private Vector3 _lastPosition;
    public event EventHandler OnEnemyAttack;



    public bool _IsRunning
    {
        get {
            if (_navMashAgent.velocity == Vector3.zero)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    private enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking,
        Death



    }

    private void Awake()
    {
        _navMashAgent = GetComponent<NavMeshAgent>();
        _navMashAgent.updateRotation = false;
        _navMashAgent.updateUpAxis = false;
        _currentState = _startingState;
        _roamingSpeed = _navMashAgent.speed;
        _chasingSpeed = _navMashAgent.speed * _chasingSpeedMultiplier;
    }
    private void Update()
    {
        StateHandler();
        MoveDiractionTarget();

    }
    public void SetDeathState()
    { _navMashAgent.ResetPath();
        _currentState = State.Death;
    }
    private void StateHandler()
    {
        switch (_currentState)
        {

            case State.Roaming:
                _roamingTime -= Time.deltaTime;
                if (_roamingTime <= 0f)
                {
                    Roaming();
                    _roamingTime = _roamingTimeMax;

                }
                ChecCurrentState();

                break;

            case State.Chasing:
                ChasingTarget();
                ChecCurrentState();

                break;
            case State.Attacking:
                AttackingTarget();
                ChecCurrentState();
                break;
            case State.Death:
                break;
            default:
            case State.Idle:
                break;

        }
    }
    private void AttackingTarget()
    {
        if (Time.time >= _nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            _nextAttackTime = Time.time + _AttackRate;
        }
    }
    private void MoveDiractionTarget()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (_IsRunning)
            {
                ChangeFacingDirection(_lastPosition,transform.position);
            }else if (_currentState == State.Attacking)
            {
                if (Player.Instance == null) return;
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
            }
            _lastPosition = transform.position;
            _nextDirrectionTime=Time.time + _checkDirectionDuration;
        }
    }
    private void ChasingTarget()
    {
        if (Player.Instance == null) return;
        _navMashAgent.SetDestination(Player.Instance.transform.position);
    }
    public float GetRoamingAnimationSpeed()
    {
        return _navMashAgent.speed / _roamingSpeed;
    }
    private void ChecCurrentState()
    {   if (Player.Instance == null) return;
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Roaming;
        if (_isChasingEnemy)
        {
            if (distanceToPlayer <= _chasingDistance)
            {
               newState = State.Chasing;
            }

        }

        if (_isAttackingEnemy)
        {
            if(distanceToPlayer <= _attackingDistance)
            {
               
                if (Player.Instance.IsAlive())
                    newState = State.Attacking;
                else
                    newState = State.Roaming;
            }
        }


        if(newState != _currentState)
        {
            if(newState== State.Chasing)
            {
                _navMashAgent.ResetPath();
                _navMashAgent.speed = _chasingSpeed;
            }else if (newState == State.Roaming)
            {
                _roamingTime = 0f;
                _navMashAgent.speed = _roamingSpeed;
            }
            else if (newState == State.Attacking)
            {
                   _navMashAgent.ResetPath();

            }
                _currentState = newState;
        }
        
    }

    private void Roaming()
    {
        _startingposition = transform.position;
        _roamPosition = GetRoamingPosition();
        //ChangeFacingDirection(_startingposition, _roamPosition);
        _navMashAgent.SetDestination(_roamPosition);
    }
    private Vector3 GetRoamingPosition()
    {
        return _startingposition + Utils.GetRandomDir() * UnityEngine.Random.Range(_roamingDistanceMin, _roamingDistanceMax);
    }
    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
    

}
