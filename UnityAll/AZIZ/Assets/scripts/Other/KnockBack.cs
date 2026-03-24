using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float _knockBackForce = 3f;
    [SerializeField] private float _knockBackMovingTimeMax=0.3f;
    private float _knockBackMovingTime;
    private Rigidbody2D _rigidbody2D;
    public bool IsGettingKnockBack { get; private set; }
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _knockBackMovingTime-= Time.deltaTime;
        if (_knockBackMovingTime <= 0)
            StopKnockBackMovement();
    }
    public void GetKnokedBack(Transform damageSource)
    {   IsGettingKnockBack = true;
        _knockBackMovingTime =_knockBackMovingTimeMax;
        Vector2 difference = (transform.position - damageSource.position).normalized*_knockBackForce/_rigidbody2D.mass;
        _rigidbody2D.AddForce(difference, ForceMode2D.Impulse);
    }
    public void StopKnockBackMovement()
    {
        IsGettingKnockBack = false;
        _rigidbody2D.linearVelocity = Vector2.zero;
        
    }


}
