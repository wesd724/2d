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
        if (this.Multiple > 6)
            this.Multiple = 6;
    }

    public void setMultiple(int m)
    {
        this.Multiple = m;
    }

    public void addChip(int n)
    {
        this.Chip += n;
    }

    public void setChip(int n)
    {
        this.Chip = n;
    }

    public void setCoin(int m)
    {
        if (m > 5)
            m = 5;
        this.Multiple = m;
    }
}
