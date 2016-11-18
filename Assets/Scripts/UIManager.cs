using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIManager : MonoBehaviour {

    [SerializeField]
    Fighter fighter1, fighter2;
    [SerializeField]
    Text p1Name, p2Name, timer;
    [SerializeField]
    Scrollbar p1Life, p2Life;
    [SerializeField]
    BattleController battleCtrl;

    // Use this for initialization
    void Start () {
        p1Name.text = fighter1.GetName();

        p2Name.text = fighter2.GetName();
	}
	
	// Update is called once per frame
	void Update () {
        if (p1Life.size > fighter1.GetHealthPercent())
            p1Life.size = fighter1.GetHealthPercent();

        if (p2Life.size > fighter2.GetHealthPercent())
            p2Life.size = fighter2.GetHealthPercent();

        //if()
        timer.text = battleCtrl.roundTime.ToString();
    }
}
