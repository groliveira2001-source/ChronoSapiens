using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasbeenDiscard;
    public bool hasbeenPlayed;
    public int handIndex;

    private GameManager gm;

    [Header("Feedback")]
    [TextArea(2, 4)]
    public string invalidReason; // Texto explicativo se a carta for errada

    // Guardar posição e escala originais
    private Vector3 originalPosition;
    private Vector3 originalScale;

    // Configuração do hover
    [SerializeField] private float hoverHeight = 2.0f;  // Quanto a carta sobe
    [SerializeField] private float hoverScale = 1.3f;   // Zoom
    [SerializeField] private float hoverSpeed = 10f;    // Velocidade da transição

    private bool isHovered = false;
    private bool isPlayable = true; // controla se o hover pode acontecer

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        if (gm == null)
        {
            Debug.LogError("GameManager não encontrado na cena!");
        }

        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (hasbeenDiscard)
        {
            Invoke(nameof(MoveToDiscardPile), 0.5f);
            hasbeenDiscard = false;
        }

        if (!isPlayable) return;

        // Hover sutil
        Vector3 targetPos = isHovered
            ? originalPosition + new Vector3(0, hoverHeight, 0)
            : originalPosition;

        Vector3 targetScale = isHovered
            ? originalScale * hoverScale
            : originalScale;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * hoverSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * hoverSpeed);
    }


    private void OnMouseEnter()
    {
        if (isPlayable)
            isHovered = true;
    }

    private void OnMouseExit()
    {
        if (isPlayable)
            isHovered = false;
    }

    private void OnMouseDown()
    {
        if (hasbeenPlayed) return;

        isPlayable = false;
        isHovered = false;
        transform.localScale = originalScale;

        transform.position += Vector3.up * 0.4f;
        hasbeenPlayed = true;
        gm.avaliableCardsSlots[handIndex] = true;

        // espera um pouco antes de mover pro test deck
        Invoke(nameof(MoveTotestdeckPile), 2f);
    }

    void MoveTotestdeckPile()
    {
        gm.testdeckPile.Add(this);
        gm.UpdateProgress(gameObject.tag, this); // Envia referência da carta
        gameObject.SetActive(false);
    }

    void MoveToDiscardPile()
    {
        gm.DiscardPile.Add(this);
        gm.avaliableCardsSlots[handIndex] = true;
        gameObject.SetActive(false);
    }

    public void ResetOriginalTransform()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;
        isPlayable = true;
        isHovered = false;
        hasbeenPlayed = false; // garante reset completo
    }
}
