using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GuiManager : MonoBehaviour

{
 [SerializeField] private ScorePlayer scorePlayer;
 [SerializeField] private TextMeshProUGUI _payText1;
 [SerializeField] private TextMeshProUGUI _payText2;
 [SerializeField] private TextMeshProUGUI _payText3;
 [SerializeField]private Sword _sword;
 [SerializeField]private Player _player;
 [SerializeField] private TextMeshProUGUI _hpValueText;
 [SerializeField] private TextMeshProUGUI _dmgValueText;
 [SerializeField] private TextMeshProUGUI _quantityValueText;
 public GameObject StartGui;
 public GameObject SetingsGui;
 public GameObject shopOpenGui;
 public GameObject PauseGui;
 public Animator[] myAnimator;
 public static bool isRestarting = false;
 private bool _shopIsOpen = false;
 public void Restart()
 {
  isRestarting = true; // Запоминаем, что мы делаем именно рестарт
  SceneManager.LoadScene(SceneManager.GetActiveScene().name);
 }


 public void menuBackBaton()
 {
  StartGui.SetActive(true);
  SetingsGui.SetActive(false);
  
 }

 public void menuSetingsBaton()
 {
  StartGui.SetActive(false);
  SetingsGui.SetActive(true);
 }
 public void menuBaton()
 {
  StartGui.SetActive(true);
  Time.timeScale = 0;
  PauseGui.SetActive(false);
  
 }
 private void Update()
 {
  
 }

 public void PlayeGame()
 {
  StartGui.SetActive(false);
  Time.timeScale = 1;
  Pause.paused = false;
  
 
  Animator[] animators = FindObjectsOfType<Animator>();
  foreach (Animator anim in animators) 
  {
   anim.enabled = true;
  }
  
 }
 public void Start()
 {
  if (isRestarting == true)
  {
   // Если мы перезапустили игру после смерти:
   StartGui.SetActive(false); // Прячем меню
   Time.timeScale = 1;        // Запускаем время
   isRestarting = false;   
   Pause.paused = false;// Сбрасываем флажок для следующих разов
  }
  else
  {
   // Если это самый первый запуск игры:
   StartGui.SetActive(true);  // Убеждаемся, что меню видно
   Time.timeScale = 0;        // Ставим на паузу
  }
 }


 public void shopOpen()
 {
  if (_shopIsOpen== false)
  {
   foreach (Animator anim in myAnimator)
   {
    anim.enabled = false; 
   }
   
   Time.timeScale = 0;
   _shopIsOpen = true;
   shopOpenGui.SetActive(true);
  }
  else
  {
   foreach (Animator anim in myAnimator)
   {
    anim.enabled = true;
   }

   Time.timeScale = 1;
   _shopIsOpen = false;
   shopOpenGui.SetActive(false);
  }
 }

 public void shopBuydamage()
 {
  if (_shopIsOpen == true)
  {
   int price;
   if (int.TryParse(_payText1.text, out price))
   {
    if (scorePlayer.scoreHero >= price)
    {
     scorePlayer.scoreHero -= price;
     scorePlayer.UpdateScoreText();
     _sword._damageAmount += 1;
     Debug.Log($"Now you damage = {_sword._damageAmount}");
    }
    else
    {
     Debug.Log($"You don't have enough money to buy this damage");
    }

   }
  }
 }
 public void shopBuyHp()
 {
  if (_shopIsOpen == true)
  {
   int price;
   if (int.TryParse(_payText2.text, out price))
   {
    if (scorePlayer.scoreHero >= price)
    {
     scorePlayer.scoreHero -= price;
     scorePlayer.UpdateScoreText();
     _player._maxHealth += 1;
     Debug.Log($"Now you Health Point = {_player._maxHealth}");
    }
    else
    {
     Debug.Log($"You don't have enough money to buy this Health Point");
    }

   }
  }
 }
 public void shopBuyKD()
  {
   if (_shopIsOpen == true)
   {
    int price;
    if (int.TryParse(_payText3.text, out price))
    {
     if (scorePlayer.scoreHero >= price)
     {
      scorePlayer.scoreHero -= price;
      scorePlayer.UpdateScoreText();
      _player._DashColdownTime -= 0.2f;
      Debug.Log($"Coldown = {_player._DashColdownTime}");
     }
     else
     {
      Debug.Log($"You don't have enough money to buy this Coldown Duration");
     }
 
    }
   }
  }
   

  
 public void OnHpSliderChanged(float value)
 {
  POLYAEnemy.GameSettings.EnemyMaxHP = value;
  _hpValueText.text = value.ToString("0"); // "0" убирает лишние дроби (100.4 -> 100)
 }

 public void OnDmgSliderChanged(float value)
 {
  POLYAEnemy.GameSettings.EnemyDamage = value;
  _dmgValueText.text = value.ToString("0");
 }

 public void OnQuantitySliderChanged(float value)
 {
  POLYAEnemy.GameSettings.EnemyCount = Mathf.RoundToInt(value); // Для количества нужны целые числа
  _quantityValueText.text = POLYAEnemy.GameSettings.EnemyCount.ToString();
 }
 
 
 public void Exit()
 {
  Application.Quit();
 }
}
