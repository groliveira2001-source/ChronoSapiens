using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    [Header("Configura��es")]
    public CanvasGroup canvasGroup;
    public float fadeDuration = 2f;        // tempo do fade in/out
    public float delayBeforeFadeOut = 2f;  // tempo que o aviso fica vis�vel
    public string nextSceneName = "MainMenu"; // cena para carregar depois

    void Start()
    {
        // Come�a invis�vel
        canvasGroup.alpha = 0;
        // Inicia a sequ�ncia
        StartCoroutine(PlayWarning());
    }

    System.Collections.IEnumerator PlayWarning()
    {
        // Fade IN
        yield return StartCoroutine(Fade(0, 1));

        // Espera um tempo vis�vel
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // Fade OUT
        yield return StartCoroutine(Fade(1, 0));

        // Carrega a pr�xima cena
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
