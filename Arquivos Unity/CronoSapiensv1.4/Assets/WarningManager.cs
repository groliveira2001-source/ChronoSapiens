using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScreen : MonoBehaviour
{
    [Header("Configura��es")]
    public float delay = 5f; // tempo em segundos que a tela vai ficar vis�vel
    public string nextSceneName = "MainMenu"; // nome da pr�xima cena do jogo

    void Start()
    {
        // Chamar� o m�todo para trocar de cena depois do tempo configurado
        Invoke(nameof(LoadNextScene), delay);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
