using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }
    [SerializeField] private Sword sword;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // Защита от ошибки при рестарте сцены
        if (Player.Instance == null) return;

        if (Player.Instance.IsAlive())
            FollowMousePosition();
    }

    public Sword GetActiveWeapon()
    {
        return sword;
    }

    private void FollowMousePosition()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerScreenPos = Player.Instance.GetPlayerScreenPosition();
        if (!Pause.paused)
        {



            if (mousePos.x < playerScreenPos.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}


