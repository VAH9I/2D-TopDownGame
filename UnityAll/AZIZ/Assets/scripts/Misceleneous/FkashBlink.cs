using UnityEngine;

public class FkashBlink : MonoBehaviour
{
    
    [SerializeField] private MonoBehaviour _damageobjects;
    [SerializeField] private Material blinkMaterial;
    [SerializeField] private float blinkDuration = 0.2f;
    private float _blinkTimer;
    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;
    private bool _isBlinking;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;

        _isBlinking = true;
    }
    private void Start()
    {
        if (_damageobjects is Player)
        {
            (_damageobjects as Player).OnFlashBlink += Player_OnFlashBlink;
        }

    }
    private void Player_OnFlashBlink(object sender,System.EventArgs e)
    {
        SetBlinkingMaterial();
    }
    private void Update()
    {
        if(_isBlinking)
        {
            _blinkTimer -= Time.deltaTime;
            if(_blinkTimer<0)
            {
                SetDefaultMaterial();
            }
        }
    }
    private void SetBlinkingMaterial()
    {
        _blinkTimer = blinkDuration;
        _spriteRenderer.material = blinkMaterial;
    }

    private void SetDefaultMaterial()
    {
        _spriteRenderer.material = _defaultMaterial;
     
    }
    public void StopBlinking()
    {
               
        SetDefaultMaterial();
        _isBlinking = false;
    }
    private void OnDestroy()
    {
        if (_damageobjects is Player)
        {
            (_damageobjects as Player).OnFlashBlink -= Player_OnFlashBlink;
        }
    }
}

