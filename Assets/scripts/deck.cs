using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deck : MonoBehaviour
{
    void Start()
    {

    }

    public void use()
    {
        GameManager.instance.useCard.ForEach(card =>
        {
            string[] name = card.name.Split("-");
            int month = int.Parse(name[0]);
            int number = int.Parse(name[1]);
            int index = 4 * (month - 1) + (number == 1 ? 0 : 2);
            Image img = transform.GetChild(index).GetComponent<Image>();
            img.color = new Color(110 / 255f, 110 / 255f, 110 / 255f);
        });
    }

    public void all()
    {
        GameManager.instance.useCard.ForEach(card =>
        {
            string[] name = card.name.Split("-");
            int month = int.Parse(name[0]);
            int number = int.Parse(name[1]);
            int index = 4 * (month - 1) + (number == 1 ? 0 : 2);
            Image img = transform.GetChild(index).GetComponent<Image>();
            img.color = new Color(1.0f, 1.0f, 1.0f);
        });
    }
}
