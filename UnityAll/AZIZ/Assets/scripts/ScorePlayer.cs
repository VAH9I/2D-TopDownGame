using System;
using UnityEngine;
using TMPro; // 1. ДОБАВЛЯЕМ БИБЛИОТЕКУ ДЛЯ ТЕКСТА
using Random = UnityEngine.Random;

public class ScorePlayer : MonoBehaviour
{
    // 2. СОЗДАЕМ ПОЛЕ ДЛЯ НАШЕГО ТЕКСТА (оно появится в Инспекторе)
    [SerializeField] private TextMeshProUGUI _scoreText; 

    public int scoreHero = 0; // (заодно поправил опечатку scroe)

    private void Start()
    {
        // 3. Чтобы в самом начале игры выводился 0, а не "New Text"
        UpdateScoreText(); 
    }

    private void AddScore(object sender, EventArgs e)
    {
        scoreHero += Random.Range(3, 14);
        
        // 4. ОБНОВЛЯЕМ ТЕКСТ НА ЭКРАНЕ
        UpdateScoreText(); 
        
        Debug.Log(scoreHero);
    }

    // Отдельный метод, чтобы не писать одно и то же дважды
// Меняем private на public
    public void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Баланс: " + scoreHero.ToString(); 
        }
    }
    

    private void OnEnable()
    {
        EnemyEntity.OnAnyDeath += AddScore;
    }

    private void OnDisable()
    {
        EnemyEntity.OnAnyDeath -= AddScore;
    }
}