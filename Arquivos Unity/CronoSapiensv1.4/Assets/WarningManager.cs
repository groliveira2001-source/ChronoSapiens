using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScreen : MonoBehaviour
{
    [Header("Configurações")]
    public float delay = 5f; // tempo em segundos que a tela vai ficar visível
    public string nextSceneName = "MainMenu"; // nome da próxima cena do jogo

    void Start()
    {
        // Chamará o método para trocar de cena depois do tempo configurado
        Invoke(nameof(LoadNextScene), delay);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
