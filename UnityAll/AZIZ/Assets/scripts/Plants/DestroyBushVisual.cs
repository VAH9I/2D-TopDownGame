using UnityEngine;

public class DestroyBushVisual : MonoBehaviour
{
    [SerializeField] private DestroyPlants destrictableplatnt;
    [SerializeField] private GameObject DuthBseEfectVariant;
    private void Start()
    {
        destrictableplatnt.OnDestrictableTakeDamage += Destrictableplatnt_OnDestrictableTakeDamage;


    }
    private void   Destrictableplatnt_OnDestrictableTakeDamage(object sender, System.EventArgs e)
    {
        ShowDeathVFX();
    }
    private void ShowDeathVFX()
    {
        Instantiate(DuthBseEfectVariant, transform.position, Quaternion.identity);
    }
    private void OnDestroy()
    {
        destrictableplatnt.OnDestrictableTakeDamage -= Destrictableplatnt_OnDestrictableTakeDamage;
    }

}
