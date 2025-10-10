using UnityEngine;
using TMPro;
using System.Collections;

public class FeedbackManager : MonoBehaviour
{
    [Header("Refer�ncias de UI")]
    public GameObject feedbackPanel;
    public TMP_Text feedbackText;

    [Header("Configura��es")]
    public float displayTime = 3f;

    private Coroutine currentFeedback;

    private void Start()
    {
        if (feedbackPanel != null)
            feedbackPanel.SetActive(false);
    }

    public void ShowFeedback(string message)
    {
        if (currentFeedback != null)
            StopCoroutine(currentFeedback);

        currentFeedback = StartCoroutine(DisplayFeedback(message));
    }

    private IEnumerator DisplayFeedback(string message)
    {
        if (feedbackPanel == null || feedbackText == null)
        {
            Debug.LogWarning("FeedbackManager: refer�ncias de UI n�o atribu�das!");
            yield break;
        }

        feedbackText.text = message;
        feedbackPanel.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        feedbackPanel.SetActive(false);
    }
}
