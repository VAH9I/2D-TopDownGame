using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    [SerializeField] private GameObject shopUi;
   private bool shopOpen;
    
    
    private void shopOn()
    {
        Time.timeScale = 0;
        shopOpen = true;
        shopUi.SetActive(true);
        
        
    }

    private void shopOff()
    {
        Time.timeScale = 1;
        shopOpen = false;
        shopUi.SetActive(false);
    }
    
}
