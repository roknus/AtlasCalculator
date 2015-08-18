using UnityEngine;
using System.Collections;

public class Cost
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int Spare { get; set; }
    public int Tot {
        get
        {
            return R + G + B + Spare;
        }
    }

	public int Purple { get; set; }

    public Cost (int r, int g, int b, int spare = 0, int purple = 0)
    {
        R = r;
        G = g;
        B = b;
        Spare = spare;
		Purple = purple;
    }

    public Cost (Cost other)
    {
        R = other.R;
        G = other.G;
        B = other.B;
        Spare = other.Spare;
		Purple = other.Purple;
    }

    public static Cost operator +(Cost c1, Cost c2)
    {
        return new Cost(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B, c1.Spare + c2.Spare, c1.Purple + c2.Purple);
    }
    public static Cost operator -(Cost c1, Cost c2)
    {
		return new Cost(c1.R - c2.R, c1.G - c2.G, c1.B - c2.B, c1.Spare - c2.Spare, c1.Purple - c2.Purple);
    }
    public static bool operator <(Cost c1, Cost c2)
    {
        return c1.Tot < c2.Tot;
    }
    public static bool operator >(Cost c1, Cost c2)
    {
        return c1.Tot > c2.Tot;
    }

    // Tell if the Cost is negative even with Spare sparks
    public bool isNegative()
    {
        int spare = Spare;
        if (R < 0) spare += R;
        if (G < 0) spare += G;
        if (B < 0) spare += B;

        return spare < 0;
    }
}

