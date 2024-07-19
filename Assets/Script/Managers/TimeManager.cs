using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Globalization;
using System.Runtime.CompilerServices;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI _statusBar;
    public static TimeManager instance = null;
    private float _timeLeft = 0f;
    private void Awake()
    {
        if (instance == null)

            instance = this;
    }
    public IEnumerator StartTimerToInvaders(float _timeToInvaders,AudioClip InvadersAttackMusic)
    {

        _timeLeft = _timeToInvaders;
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;

                if (SoundManager.instance.efxSource.isPlaying == false)
                {
                    SoundManager.instance.PlaySingle(InvadersAttackMusic);
                }

            UpdateTimeToInvadersCome();
            yield return null;
        }
    }
    private void UpdateTimeToInvadersCome()
    {
        if (_timeLeft < 0)
        {
            _timeLeft = 0;
            _statusBar.text = "...";
        }
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    public void StartInvadersTimer(float _timeToInvaders,AudioClip InvadersAttackMusic)
    {
        StartCoroutine(StartTimerToInvaders(_timeToInvaders,InvadersAttackMusic));
    }


    public IEnumerator StartBuildingTimer(float _timeLf, Transform buildingPosition)
    {

        _timeLeft = _timeLf;
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;

            if (SoundManager.instance.efxSource.isPlaying == false)
            {
                SoundManager.instance.PlaySingle(VillageGenerator.instance._buildingMusic);
            }

            UpdateTimeText(buildingPosition);
            yield return null;
        }
    }
    private void UpdateTimeText(Transform buildingPosition)
    {
        if (_timeLeft < 0)
        {
            _timeLeft = 0;
            CheckForBuild(buildingPosition);
            _statusBar.text = ".....";
            PointConnector.instance.ClearAllDotsList();
            GameManager.isGame = true;
        }
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    public void StartBuildingCoro(float _time, AudioClip audioClip, Transform buildingPosition,string textToBuild)
    {
        StatusManager.Instance.SetStatus(textToBuild, _time);
        StartCoroutine(StartBuildingTimer(_time, buildingPosition));
    }
    private void CheckForBuild(Transform buildingPosition)
    {
        GameObject building = VillageGenerator.instance.buildingObjectsList.Dequeue();
        VillageGenerator.instance.ObjectGenerator(building, buildingPosition);
    }

}