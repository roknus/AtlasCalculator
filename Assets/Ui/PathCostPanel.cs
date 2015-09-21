﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PathCostPanel : MonoBehaviour {

    public Text Red;
    public Text Green;
    public Text Blue;
    public Text Tot;
	
	public Text Purple;

    public Text Proficency;

    public Text Greatness;

    public PathStatsPanel StatPanel;

    public void SetPanel(NodePath path)
    {
        if (path == null) return;

        Red.text        = path.Red.ToString();
        Green.text      = path.Green.ToString();
        Blue.text       = path.Blue.ToString();
        Tot.text        = path.TotSparks.ToString();
        Proficency.text = path.Proficency.ToString();
		Purple.text 	= path.PurpleSparks.ToString ();
        Greatness.text  = path.Greatness.ToString();

        StatPanel.SetPanel(path);
    }

    public void Clean()
    {
        Red.text = "0";
        Green.text = "0";
        Blue.text = "0";
        Tot.text = "0";
        Purple.text = "0";
        Proficency.text = "0";
        Greatness.text = "0";

        StatPanel.Clean();
    }
}
