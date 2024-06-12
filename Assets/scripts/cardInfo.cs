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

    void OnEnable()
    {
        status = false;
    }

    public void judge(string cardName)
    {
        status = false;
        if (serviceAndUpgrade.titleContent.ContainsKey(cardName))
        {
            status = true;
            title = infoUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            content = infoUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            set(serviceAndUpgrade.titleContent[cardName]);
        }
    }

    void set(string[] info)
    {
        this.title.text = info[0];
        this.content.text = info[1];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(status || infoUI.activeSelf)
            infoUI.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (status)
            infoUI.SetActive(true);
    }

    public void setStatus(bool status)
    {
        this.status = status;
    }
}
