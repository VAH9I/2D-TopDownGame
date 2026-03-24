using UnityEngine;
using System;
public class DestroyPlants : MonoBehaviour
{
    public event  EventHandler OnDestrictableTakeDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Sword>())
        {   OnDestrictableTakeDamage?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
            NavMashSurfeceMenegment.Instance.RebakeNavmeshSurface();
        }
    }
}
