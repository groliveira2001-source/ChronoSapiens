using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasbeenDiscard;
    public bool hasbeenPlayed;
    public int handIndex;

    private GameManager gm;

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

        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (hasbeenDiscard)
        {
            Invoke("MoveToDiscardPile", 0.5f);
            hasbeenDiscard = false;
        }

        if (!isPlayable) return; // se não pode mais ser jogada, ignora o hover

        // Hover sutil
        if (isHovered)
        {
            Vector3 targetPos = originalPosition + new Vector3(0, hoverHeight, 0);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * hoverSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * hoverScale, Time.deltaTime * hoverSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * hoverSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * hoverSpeed);
        }
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
        if (!hasbeenPlayed)
        {
            isPlayable = false; // desativa hover depois do clique
            isHovered = false;  // garante que não volte ao estado de hover
            transform.localScale = originalScale; // volta à escala normal

            transform.position += Vector3.up * 0.4f;
            hasbeenPlayed = true;
            gm.avaliableCardsSlots[handIndex] = true;
            Invoke("MoveTotestdeckPile", 2f);
        }
    }

    void MoveTotestdeckPile()
    {
        gm.testdeckPile.Add(this);
        gm.UpdateProgress(gameObject.tag);
        gameObject.SetActive(false);
    }

    void MoveToDiscardPile()
    {
        gm.DiscardPile.Add(this);
        gm.avaliableCardsSlots[handIndex] = true;
        gameObject.SetActive(false);
    }
}
