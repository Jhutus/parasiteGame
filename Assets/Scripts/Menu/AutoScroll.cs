using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.2f; // Velocidad del desplazamiento
    private bool isScrolling = true;

    void Update()
    {
        if (isScrolling)
        {
            // Desplazamiento automático hacia abajo
            scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;

            // Reiniciar cuando llega al final
            if (scrollRect.verticalNormalizedPosition <= 0)
            {
                scrollRect.verticalNormalizedPosition = 1;
            }
        }

        void OnPointerEnter()
    {
    isScrolling = false;
    }

        void OnPointerExit()
    {
    isScrolling = true;
    }
    
    }

}
