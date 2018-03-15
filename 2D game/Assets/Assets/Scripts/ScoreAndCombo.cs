using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndCombo : MonoBehaviour {
    private static ScoreAndCombo ornek;
    public static ScoreAndCombo Ornek
    {
        get
        {
            if (ornek == null)
            {


                ornek = GameObject.FindObjectOfType<ScoreAndCombo>();
            }

            return ornek;
        }
    }
    [SerializeField]
    private Text comboText;
    [SerializeField]
    private Text goldText;
    [SerializeField]
    private Text FinalGold;
    private  int gold;
    
    public  int Gold
    {
        get { return gold; }
        set { gold = value; }
    }
    private  int totalGold;

    public  int TotalGold
    {
        get { return totalGold; }
        set { totalGold = value; }
    }
    private  int combo;

    public  int Combo
    {
        get { return combo; }
        set { combo = value; }
    }

    private void Awake()
    {
       
        ResetScore();



        if (FinalGold != null && SaveLoad.Load()!=null)
        {
            FinalGold.text = SaveLoad.Load().gold.ToString();
        }
            
        





    }
    
    public void SetGoldText()
    {
        goldText.text = Gold.ToString();
    }
    public void SetComboText()
    {
        comboText.text = Combo.ToString();
    }
    void ResetScore()
    {
        Gold = 0;
        Combo = 0;
        TotalGold = 0;
    }

    public int EndScore()
    {
        return TotalGold = Gold * (Combo+1);
        
    }


}
