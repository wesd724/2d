using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class shopDeck : MonoBehaviour
{
    public Image[] images;
    public Image cardback;

    public void setting()
    {
        int[] numbers =
            numbers = Enumerable.Range(0, 20).OrderBy(x => Random.value).Take(5).ToArray();

        for (int i = 0; i < 5; i++)
        {
            Sprite sprite = GameManager.instance.deck[numbers[i]].GetComponent<SpriteRenderer>().sprite;
            images[i].sprite = sprite;
            images[i].GetComponent<changeMyCard>().setNumbers(numbers);
        }
    }

    public void back()
    {
        for (int i = 0; i < 5; i++)
        {
            images[i].sprite = cardback.sprite;
        }
    }
}
