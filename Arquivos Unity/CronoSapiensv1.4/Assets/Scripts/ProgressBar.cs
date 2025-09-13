using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int maximum = 10;   // valor máximo da barra
    public int current = 0;    // valor atual da barra
    public Image mask;

    void Update()
    {
        // só atualiza se estiver em Play Mode e se a mask estiver atribuída
        if (Application.isPlaying && mask != null)
        {
            GetCurrentFill();
        }
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = Mathf.Clamp01(fillAmount);
    }

    // ?? Método para aumentar ou diminuir a barra
    public void AddProgress(int amount)
    {
        current += amount;
        current = Mathf.Clamp(current, 0, maximum); // nunca menor que 0 nem maior que máximo
    }
}
