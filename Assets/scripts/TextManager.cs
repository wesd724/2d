using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager instance = null;

    public TMP_Text goal;
    public TMP_Text score;
    public TextMeshProUGUI handName;
    public TMP_Text chip;
    public TMP_Text multiple;
    public TMP_Text handCountText;
    public TMP_Text discardCountText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    void Start()
    {
        score.text = "0";
        handName.text = "";
        chip.text = "0";
        multiple.text = "0";
        handCountText.text = "3";
        discardCountText.text = "4";
    }
}
