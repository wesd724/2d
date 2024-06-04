using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jockboController : MonoBehaviour
{
    public moveUI ui;
    public GameObject infoUI;
    public void open()
    {
        gameObject.SetActive(true);
        infoUI.SetActive(true);
        StartCoroutine(ui.moveUp(10));
    }

    public void close()
    {
        infoUI.SetActive(false);
        StartCoroutine(ui.moveDown(-915));
    }
}
