using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    [Header("Configurações")]
    public CanvasGroup canvasGroup;
    public float fadeDuration = 2f;        // tempo do fade in/out
    public float delayBeforeFadeOut = 2f;  // tempo que o aviso fica visível
    public string nextSceneName = "MainMenu"; // cena para carregar depois

    void Start()
    {
        // Começa invisível
        canvasGroup.alpha = 0;
        // Inicia a sequência
        StartCoroutine(PlayWarning());
    }

    System.Collections.IEnumerator PlayWarning()
    {
        // Fade IN
        yield return StartCoroutine(Fade(0, 1));

        // Espera um tempo visível
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // Fade OUT
        yield return StartCoroutine(Fade(1, 0));

        // Carrega a próxima cena
        SceneManager.LoadScene(nextSceneName);
    }

    System.Collections.IEnumerator Fade(float start, float end)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}
