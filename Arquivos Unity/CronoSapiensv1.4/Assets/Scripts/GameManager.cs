using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> testdeckPile = new List<Card>();
    public List<Card> DiscardPile = new List<Card>();

    public Transform[] cardSlots;

    public Transform drawOrigin;

    public bool[] avaliableCardsSlots;

    public ProgressBar progressBar;

    public Image Gameover;
    public Image YouWin;

    public FeedbackManager feedbackManager;
    void Start()
    {
        if (Gameover != null)
        {
            Gameover.gameObject.SetActive(false);
        }

        if (YouWin != null)
        {
            YouWin.gameObject.SetActive(false);
        }

    }

    public void UpdateProgress(string tag, Card card = null) // 👈 adicione o parâmetro opcional
    {
        if (tag == "Certa")
        {
            progressBar.current += 2;
        }
        else if (tag == "MeioCerta")
        {
            progressBar.current += 1;
        }
        else if (tag == "Errada")
        {
            progressBar.current -= 1;

            // 👇 mostra o feedback, se houver um motivo configurado
            if (card != null && feedbackManager != null)
            {
                string message = string.IsNullOrEmpty(card.invalidReason)
                    ? "Jogada incorreta!"
                    : card.invalidReason;

                feedbackManager.ShowFeedback(message);
            }
        }


        progressBar.current = Mathf.Clamp(progressBar.current, 0, progressBar.maximum);


        if (progressBar.current <= 0)
        {
            ShowGameOver();
        }

        else if (progressBar.current >= 10)
        {
            ShowYouWin();
        }
    }

    public void DrawCard()
    {
        // Verifica se ainda há cartas no deck
        if (deck.Count < 1)
        {
            Debug.Log("Não há cartas suficientes no deck para comprar.");
            return;
        }

        // Percorre todos os slots disponíveis
        for (int i = 0; i < avaliableCardsSlots.Length; i++)
        {
            // Se o slot está livre e há cartas no deck
            if (avaliableCardsSlots[i] && deck.Count > 0)
            {
                // Escolhe uma carta aleatória do deck
                Card randCard = deck[Random.Range(0, deck.Count)];

                // Ativa e posiciona a carta
                randCard.gameObject.SetActive(true);
                randCard.handIndex = i;
                randCard.transform.position = cardSlots[i].position;
                randCard.ResetOriginalTransform();
                randCard.hasbeenPlayed = false;

                // Marca o slot como ocupado
                avaliableCardsSlots[i] = false;

                // Remove a carta do deck
                deck.Remove(randCard);

            }

        }
    }

    public void Shuflle()
    {
        // Cartas jogadas vão de volta pro deck
        if (testdeckPile.Count >= 1)
        {
            foreach (Card card in testdeckPile)
            {
                deck.Add(card);
            }
            testdeckPile.Clear();
        }

        // Cartas descartadas também voltam pro deck
        if (DiscardPile.Count >= 1)
        {
            foreach (Card card in DiscardPile)
            {
                deck.Add(card);
            }
            DiscardPile.Clear();
        }

        // Embaralha a lista do deck
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // ?? Método chamado no fim do turno para descartar cartas que sobraram na mão
    public void DiscardDeck()
    {
        // Procura em cada slot se existe uma carta ativa
        foreach (Card card in FindObjectsOfType<Card>())
        {
            // Só descarta se a carta estiver ativa e ainda não tiver sido jogada
            if (card.gameObject.activeSelf && !card.hasbeenPlayed)
            {
                card.hasbeenDiscard = true;
            }
        }
    }

    public void ShowGameOver()
    {
        if (Gameover != null)
        {
            Gameover.gameObject.SetActive(true);
        }
    }

    public void ShowYouWin()
    {
        if (YouWin != null)
        {
            YouWin.gameObject.SetActive(true);
        }
    }

}
