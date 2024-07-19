using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Point : MonoBehaviour
{
    [NonSerialized] public SpriteRenderer _renderer;
    [NonSerialized] public bool isPointCompleted;
    [NonSerialized] public bool isPointRequireded;
    [SerializeField] private GameObject generatedObject;
    [SerializeField] private Transform buildingPosition;
    [SerializeField] private float timeToBuild;
    [SerializeField] private string textToBuild;
    [SerializeField] private string textNededResourses;

    public List<Transform> childrenDots = new List<Transform>();
    public List<Transform> requiredDots = new List<Transform>();
    public List<Transform> conectingDots = new List<Transform>();
    private void Start()
    {
        isPointCompleted = false;
        isPointRequireded = false;
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void ResumStatus() // сбросить параметры точки 
    {
        gameObject.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
    }
    public void IAmSelectedDot() // увеличить точку 
    {
        gameObject.transform.localScale = new Vector3(1.9f, 1.9f, 1);
    }
    public void EnableDot()
    {
        _renderer.color = Color.white;
        if (generatedObject!=null)
        {
            BuildingObject(generatedObject, buildingPosition, timeToBuild, textToBuild);
        }
        else
        {
            StatusManager.Instance.SetStatus(textToBuild, 5f);
        }
    }
    public void CompleteDot()
    {
        _renderer.color = Color.yellow;
    }
    public void UncompleteDot()
    {
        _renderer.color = Color.gray;
    }
    public void TextIfResoursesDontHave()
    {
        StatusManager.Instance.SetStatus(textNededResourses,5f);
    }
    public void UnrequiredDot()
    {
        isPointRequireded = false;
    }
    public void BuildingObject(GameObject buildingObject, Transform buildingPosition, float timeToBuild, string textToBuild)
    {
        VillageGenerator.instance.BuildingObject(buildingObject, buildingPosition, timeToBuild,textToBuild);
    }

}