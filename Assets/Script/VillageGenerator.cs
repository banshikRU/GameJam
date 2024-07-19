using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using System.Globalization;

public class VillageGenerator : MonoBehaviour
{
    public Queue<GameObject> buildingObjectsList = new Queue<GameObject>();
    public Queue<GameObject> buildedObject = new Queue<GameObject>();
    public static VillageGenerator instance;
    public AudioClip _buildingMusic;
    public AudioClip _InvadersAttackClip;
    [SerializeField] GameObject _nomads;
    [SerializeField] GameObject _home;
    [SerializeField] private TextMeshProUGUI _statusText;
    private void Start()
    {
        instance = this;
    }
    public void BuildingObject(GameObject buildingObject,Transform buildingPosition,float timeToBuild,string textToBuild)
    {
        if (!buildedObject.Contains(buildingObject))
        {
            GameManager.isGame = false;
            buildingObjectsList.Enqueue(buildingObject);
            TimeManager.instance.StartBuildingCoro(timeToBuild,_buildingMusic, buildingPosition,textToBuild);
        }else { GameManager.isGame = true; }

    }
    public void ObjectGenerator(GameObject buildingObject,Transform buildingPosition)
    {
        Instantiate(buildingObject,buildingPosition);
        TimeManager.instance.StopAllCoroutines();
       // CheckIfAnyGenerated();
       // CheckIfNomadGenerated(buildingObject);


    }
    public void CheckIfNomadGenerated(GameObject buildingObject)
    {
        if (buildingObject.name =="NomadsGenerator")
        {
            StatusManager.Instance.SetStatus("The invaders are close!Rather, learn something new!",15);
            TimeManager.instance.StartInvadersTimer(15, _InvadersAttackClip);
            GameManager.isInvaderAttack = true;
        }
    }
    public void CheckIfAnyGenerated()
    {
            StatusManager.Instance.SetStatus("The invaders are close!Rather, learn something new!", 15);
            TimeManager.instance.StartInvadersTimer(15, _InvadersAttackClip);
            GameManager.isInvaderAttack = true;

    }
}
