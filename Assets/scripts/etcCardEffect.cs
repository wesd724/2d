using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class etcCardEffect
{

    public string HandName { get; private set; }
    public int Chip { get; set; }
    public int Multiple { get; set; }

    public string explain { get; set; }

    public etcCardEffect(int chip, int multiple)
    {
        this.Chip = chip;
        this.Multiple = multiple;
    }

    public void addMultiple(int n)
    {
        this.Multiple += n;
    }

    public void addChip(int n)
    {
        this.Chip += n;
    }

}
