using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class consume : MonoBehaviour
{
    public Image[] images;
    public GameObject consumeCardList; // ��ȭ�� ī�� ����� ���� �θ� ������Ʈ
    // �� �������� �������� 3���� �̾� img1, 2, 3�� sprite �����Ѵ�.
 
    void OnEnable()
    {
        int[] numbers = 
            Enumerable.Range(0, consumeCardList.transform.childCount).OrderBy(x => Random.value).Take(3).ToArray();

        for(int i = 0; i < 3; i++)
        {
            Sprite sprite = consumeCardList.transform.GetChild(numbers[i]).GetComponent<Image>().sprite;
            images[i].sprite = sprite;
        }
    }
}
