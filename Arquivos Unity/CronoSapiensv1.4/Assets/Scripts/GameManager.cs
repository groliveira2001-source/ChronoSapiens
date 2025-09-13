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
    public bool[] avaliableCardsSlots;

    public ProgressBar progressBar;

    public Image Gameover;
    public Image YouWin;

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
    
    public void UpdateProgress(string tag)
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
        }

        // garante que o valor fique dentro dos limites
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
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < avaliableCardsSlots.Length; i++)
            {
                if (avaliableCardsSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = cardSlots[i].position;
                    randCard.hasbeenPlayed = false;
                    avaliableCardsSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
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
