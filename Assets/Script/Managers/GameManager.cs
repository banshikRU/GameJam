using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [NonSerialized]public List<Nomads> nomads;
    [SerializeField] private GameObject[] clouds;
    [SerializeField] private Transform cloudsPosition;
    public static bool isNomadBuilded;
    public static bool isMainMenu;
    public static bool isGame;
    public static bool isInvaderAttack;
    public static bool isNomaded;
    public static Vector3 fabricPosition;
    [SerializeField] private Transform villageCenter;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       isNomadBuilded = false;
        isMainMenu = true;

        isGame = false; 
        isInvaderAttack = false;
        CloudGenerator();
       // StartInvaderAttack();
    }
    private void CloudGenerator()
    {
        for (int i = 0; i < 16; i++)
        {
            Instantiate(clouds[Random.Range(0, clouds.Length - 1)], new Vector3(cloudsPosition.position.x, Random.Range(cloudsPosition.position.y-1, cloudsPosition.position.y +2), 0f), Quaternion.identity);
        }
    }
    private void StartInvaderAttack()
    {
       // Invaders.Instance.StartInvadersAttack(villageCenter.position);
    }
    //public void  RestartNomads()
    //{
    //    if (nomads.Count != 0)
    //    {
    //        foreach (var nomad in nomads)
    //        {
    //            nomad.RestartMovementSpeed();
    //        }
    //    }

    //}

}
