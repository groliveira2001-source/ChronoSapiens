using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public class DialogueEvent
    {
        public int progressThreshold; // valor da barra (ex: 2, 5, 9)
        [TextArea(2, 4)]
        public string dialogueText;   // texto mostrado
    }

    public List<DialogueEvent> dialogueEvents = new List<DialogueEvent>();

    [Header("UI de Diálogo")]
    public GameObject dialogueCanvas;
    public Text dialogueText;
    public Button continueButton;

    private bool isDialogueActive = false;
    private int lastTriggeredThreshold = -1;

    private void Start()
    {
        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);

        if (continueButton != null)
            continueButton.onClick.AddListener(CloseDialogue);
    }

    public void CheckForDialogue(int currentProgress)
    {
        // Verifica se há algum evento para o valor atual da barra
        foreach (var evt in dialogueEvents)
        {
            if (currentProgress >= evt.progressThreshold && lastTriggeredThreshold < evt.progressThreshold)
            {
                TriggerDialogue(evt);
                lastTriggeredThreshold = evt.progressThreshold;
                break;
            }
        }
    }

    void TriggerDialogue(DialogueEvent evt)
    {
        if (dialogueCanvas == null || dialogueText == null) return;

        Time.timeScale = 0f; // pausa o jogo
        isDialogueActive = true;

        dialogueCanvas.SetActive(true);
        dialogueText.text = evt.dialogueText;
    }

    void CloseDialogue()
    {
        if (!isDialogueActive) return;

        dialogueCanvas.SetActive(false);
        Time.timeScale = 1f; // volta ao jogo
        isDialogueActive = false;
    }
}

