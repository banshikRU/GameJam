using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ResoursesNeded : MonoBehaviour
{
    [SerializeField] int tree;
    [SerializeField] int rock;
    [SerializeField] int metall;
    [SerializeField] int coin;
    private bool isBuilded;
    private void Start()
    {
        isBuilded = false;
    }
    public bool IsObjectHaveAllNededResourses()
    {
        if (!isBuilded == true)
        {
            if (tree <= Resourse.instance.Tree && rock <= Resourse.instance.Rock && metall <= Resourse.instance.Metall && coin <= Resourse.instance.Coin)
            {
                isBuilded = true;
                RefreshAllResourses();
                return true;
            }
            else return false;
        } return true;

    }
    private void RefreshAllResourses()
    {
        Resourse.instance.Tree -=  tree;
        Resourse.instance.Rock -= rock;
        Resourse.instance.Metall -= metall;
        Resourse.instance.Coin -= coin;
        Resourse.instance.UpdateResourseText();
    }
}
