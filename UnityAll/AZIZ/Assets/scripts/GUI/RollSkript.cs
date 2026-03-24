using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private float xVelocity = 0.05f; // Скорость по горизонтали
    [SerializeField] private float yVelocity = 0f;    // Скорость по вертикали

    void Update()
    {
        // Получаем текущие координаты текстуры
        Rect uvRect = backgroundImage.uvRect;
        
        // ВОТ ЗДЕСЬ ИЗМЕНЕНИЯ: меняем deltaTime на unscaledDeltaTime
        uvRect.x += xVelocity * Time.unscaledDeltaTime;
        uvRect.y += yVelocity * Time.unscaledDeltaTime;
        
        // Применяем новые координаты обратно
        backgroundImage.uvRect = uvRect;
    }
}