using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class service : MonoBehaviour
{
    public Image[] images;
    public GameObject serviceCardList; // 강화할 카드 목록을 가진 부모 오브젝트
                                       // 이 변수에서 랜덤으로 3개를 뽑아 img1, 2, 3의 sprite 적용한다.
    public Image cardback;

    void OnEnable()
    {
        
    }

    public void setting()
    {
        // 현재가지고 있는 카드 제외 코드
        // 이 상점턴에서 나온 것들 제외
        // hover 도 만들어야한다!
        // hover를 세팅할때에는 모든 카드에 대해 오브젝트를 만들지 말고
        // sprite 이름으로 맵을 검색하여 텍스트를 가져온다
        // sprite 이름으로 이 게임에 모든 상호작용을 만드는 것이 편한거 같다.
        int[] numbers =
            Enumerable.Range(0, serviceCardList.transform.childCount).OrderBy(x => Random.value).Take(2).ToArray();

        for (int i = 0; i < 2; i++)
        {
            Sprite sprite = serviceCardList.transform.GetChild(numbers[i]).GetComponent<Image>().sprite;
            images[i].enabled = true;
            images[i].sprite = sprite;
        }
    }

    public void back()
    {
        for (int i = 0; i < 2; i++)
        {
            images[i].sprite = cardback.sprite;
        }
    }
}
