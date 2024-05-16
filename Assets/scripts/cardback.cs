using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cardback : MonoBehaviour
{
    public GameObject deckButton;
    public GameObject watchDeck;
    public moveUI ui;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        StartCoroutine(ui.moveUp());
    }
}
