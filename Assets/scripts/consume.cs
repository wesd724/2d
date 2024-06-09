using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class consume : MonoBehaviour
{
    public Image[] images;
    public TextMeshProUGUI[] texts;
    public GameObject consumeCardList; // ��ȭ�� ī�� ����� ���� �θ� ������Ʈ
    // �� �������� �������� 3���� �̾� img1, 2, 3�� sprite �����Ѵ�.
    public Image cardback;

    void OnEnable()
    {
        
    }

    public void setting()
    {
        int[] numbers =
            Enumerable.Range(0, consumeCardList.transform.childCount).OrderBy(x => Random.value).Take(3).ToArray();

        for (int i = 0; i < 3; i++)
        {
            Transform child = consumeCardList.transform.GetChild(numbers[i]);
            Sprite sprite = child.GetComponent<Image>().sprite;
            images[i].enabled = true;
            images[i].sprite = sprite;

            int price = check(sprite.name);
            texts[i].text = $"${price}";
            images[i].GetComponent<cardInfo>().judge(sprite.name[^2..]);
        }
    }

    public void back()
    {
        for (int i = 0; i < 3; i++)
        {
            images[i].enabled = true;
            images[i].sprite = cardback.sprite;
        }
    }

    public int check(string name)
    {
        if (name[^1] == 's' || name == "empty")
        {
            return Random.Range(2, 4);
        }
        else if (name[^1] == 'g')
        {
            return Random.Range(2, 5);
        }
        return Random.Range(1, 3);
    }
}
