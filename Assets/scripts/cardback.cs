using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cardback : MonoBehaviour
{
    public GameObject deckButton;
    public GameObject watchDeck;
    public moveUI ui;
    public deck deck;

    private void OnMouseEnter()
    {
        deckButton.SetActive(true);
    }

    private void OnMouseExit()
    {
        deckButton.SetActive(false);
    }

    public void OnMouseDown()
    {
        watchDeck.SetActive(true);
        deck.use();
        StartCoroutine(ui.moveUp(76));
    }
}
