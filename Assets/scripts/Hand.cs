using System.Collections.Generic;

public class Hand 
{
    public string HandName { get; private set; }
    public float Chip { get; }
    public float Multiple { get; }

    public Hand(string handName, List<float> point)
    {
        this.HandName = handName;
        this.Chip = point[0];
        this.Multiple = point[1];
    }
   
}
