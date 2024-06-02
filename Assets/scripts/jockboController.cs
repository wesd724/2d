using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jockboController : MonoBehaviour
{
    public moveUI ui;
    public void open()
    {
        gameObject.SetActive(true);
        StartCoroutine(ui.moveUp(10));
    }

    public void close()
    {
        StartCoroutine(ui.moveDown(-915));
    }
}
