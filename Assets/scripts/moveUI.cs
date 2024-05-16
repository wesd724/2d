using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUI : MonoBehaviour
{
    public deck deck;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator moveUp()
    {
        deck.use();
        while (transform.position.y <= 610)
        {
            transform.position += Vector3.up * 10;
            yield return null;
        }
    }

    public IEnumerator moveDown()
    {
        while (transform.position.y >= -500)
        {
            transform.position += Vector3.down * 10;
            yield return null;
        }
        transform.parent.gameObject.SetActive(false);
    }
}
