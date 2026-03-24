using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private FkashBlink _flashblink;

    private const string IS_DIE = "_IsDie";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _flashblink = GetComponent<FkashBlink>();
    }

    private void Start()
    {
        // Защита: подписываемся только если игрок существует
        if (Player.Instance != null)
        {
            Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        }
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_DIE, true);

        _flashblink.StopBlinking();
    }

    private void Update()
    {
        // Защита: если игрока (пока) нет, прерываем Update
        if (Player.Instance == null) return;

        animator.SetBool("_isRunning", Player.Instance.IsRunning());
        if (Player.Instance.IsAlive())
            AdjustPlayerFacingDirection();
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerScreenPos = Player.Instance.GetPlayerScreenPosition();
        if (!Pause.paused)
        {



            if (mousePos.x < playerScreenPos.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }


    private void OnDestroy()
    {
        // Защита: отписываемся только если ссылка на игрока ещё жива
        if (Player.Instance != null)
        {
            Player.Instance.OnPlayerDeath -= Player_OnPlayerDeath;
        }
    }
}
    