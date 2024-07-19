using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public float fadeSpeed = 2.0f;
    [SerializeField] private GameObject TrainingPanel1;
    [SerializeField] private GameObject Resourses;
    [SerializeField] private GameObject InGameStatus;
    [SerializeField] private GameObject TrainingPanel2;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject playText;
    private bool IsTraining;
    private bool IsTrainingFirst;
    public  Image mainMenuRenderer;
    public  Image playButtonRenderer;

    private bool isFading = false;
    void Start()
    {
        IsTraining = false;
        IsTrainingFirst = false;
        mainMenuRenderer = mainMenu.GetComponent<Image>();
        playButtonRenderer = mainMenu.GetComponent<Image>();
    }
    void Update()
    {
        if (isFading== true)
        {
            FadeOut();
        }
        if (IsTraining)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IsTrainingFirst == true)
                {
                    TrainingPanel2.SetActive(false);
                    IsTraining = false;
                    GameManager.isMainMenu = false;
                    GameManager.isGame = true;
                    InGameStatus.SetActive(true);
                }
                else
                {
                    IsTrainingFirst = true;
                    TrainingPanel1.SetActive(false);
                    TrainingPanel2.SetActive(true);
                }
               
            }
        }
    }
    public void Faiding()
    {
        isFading = true;
        //playText.SetActive(false);
        //mainMenu.SetActive(false);

        //StartTraining();
    }
    void FadeOut()
    {
        Fading(playButtonRenderer);
        Fading(mainMenuRenderer);
    }
    private void StartTraining()
    {
        Resourses.SetActive(true);
        IsTraining = true;
        TrainingPanel1.SetActive(true);
       
    }
    private void Fading(Image image)
    {
        Color currentColor = image.color;
        float newAlpha = Mathf.MoveTowards(currentColor.a, 0f, fadeSpeed * Time.deltaTime);
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        if (newAlpha == 0)
        {;
            playText.SetActive(false);
            isFading = false;
            StartTraining();
        }
    }
}
