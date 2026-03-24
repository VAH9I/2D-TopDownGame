using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
  
    [SerializeField] private GameObject shopGui;
    [SerializeField]private GameObject ShopOpenGui;
private bool IsOnShop = false;
    private void Start()
    {
       
    }


    private void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            shopGui.SetActive(true);
            IsOnShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            shopGui.SetActive(false);
            ShopOpenGui.SetActive(false);
            IsOnShop = false;
            
        }
    }
}

