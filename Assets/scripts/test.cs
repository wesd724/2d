using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public List<Sprite> serviceImages = new List<Sprite>();
    public List<Sprite> deckImages = new List<Sprite>();

    public void change()
    {
        GameManager.discardStack = 20;
        TextManager.instance.cash.text = "5";
        for (int i = 0; i < serviceImages.Count; i++)
        {
            GameManager.instance.serviceDeck[i].sprite = serviceImages[i];
        }

        for (int i = 0; i < deckImages.Count; i++)
        {
            GameManager.instance.deck[i].GetComponent<SpriteRenderer>().sprite = deckImages[i];
        }
        for (int i = 0; i < 7; i++)
            serviceAndUpgrade.services["������"].addMultiple();

        TextManager.instance.setJokboCount("���ȱ���", 1);
        TextManager.instance.setJokboCount("����", 2);
        TextManager.instance.setJokboCount("��", 6);
        TextManager.instance.setJokboCount("�˸�", 5);
        TextManager.instance.setJokboCount("����", 3);
        TextManager.instance.setJokboCount("���", 3);
        TextManager.instance.setJokboCount("����", 4);
        TextManager.instance.setJokboCount("��", 8);



        //GameManager.discardStack_s = 20;
        //TextManager.instance.setAllJokbo(3);
    }
}
