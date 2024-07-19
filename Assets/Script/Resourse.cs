using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Resourse : MonoBehaviour
{
    public static Resourse instance;
    [SerializeField] TextMeshProUGUI tree;
    [SerializeField] TextMeshProUGUI rock;
    [SerializeField] TextMeshProUGUI metall;
    [SerializeField] TextMeshProUGUI people;
    [SerializeField] TextMeshProUGUI coin;
    public int Tree;
    public int Rock;
    public int Metall;
    public int People;
    public int Coin;
    const string nullRes = "0";
    const string gap = " ";
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        Tree = 0;
        Rock = 0 ;
        Metall = 0;
        People = 0;
        Coin = 0;
        UpdateResourseText();
    }
    public void UpdateResourseText()
    {
        tree.text ="x" + Tree  ;
        rock.text = "x" + Rock ;
        metall.text = "x" + Metall  ;
        coin.text = "x" + Coin  ;
    }
}
