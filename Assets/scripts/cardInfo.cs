using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class cardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoUI;
    TextMeshProUGUI title;
    TextMeshProUGUI content;
    bool status;

    public void judge(string cardName)
    {
        status = false;
        if (serviceAndUpgrade.titleCotent.ContainsKey(cardName))
        {
            status = true;
            title = infoUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            content = infoUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            set(serviceAndUpgrade.titleCotent[cardName]);
        }
    }

    void set(string[] info)
    {
        this.title.text = info[0];
        this.content.text = info[1];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(status)
            infoUI.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (status)
            infoUI.SetActive(true);
    }
}
