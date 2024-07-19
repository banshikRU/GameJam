using UnityEngine;
using System.Collections.Generic;

public class PointConnector : MonoBehaviour
{
    public LayerMask pointLayer; // Указывает слой для точек
    private List<string> FirstGroup = new List<string>() {"Paddock","Ferm","Mining","Hunting","WoodProcessing","Village","Nomads","Agriculture"};
    private List<string> SecondGroup = new List<string>() { "Quarry", "Village", "Sawmill", "Peasants", "Barn", "Market", "TownHall", "Blacksmith", "Craft", "MilitaryAffairs", "Border" };
    private List<GameObject> FirstGroupLines = new List<GameObject>() ;
    private List<GameObject> SecondGroupLines = new List<GameObject>();
    public  Material lineMaterial;
    private List<Transform> selectedPoints = new List<Transform>(); // Список выбранных точек для соединения
    private List<LineRenderer> lines = new List<LineRenderer>(); // Список линий
    private List<Point>allPoints = new List<Point>();
    private bool isNomadActivated;
    public static PointConnector instance;
    public IFabric selectedFabric;
    private void Start()
    {
        instance = this;
        isNomadActivated = false;
    }
    void Update()
    {
        // Проверяем, есть ли ввод от мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Получаем позицию мыши в мировых координатах
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Пускаем луч и проверяем, есть ли коллайдер точки в указанной позиции
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, pointLayer);

            if (hit.collider != null)
            {
                if (GameManager.isGame && GameManager.isMainMenu == false)
                {
                    NomadInitialization(hit.transform);
                    selectedPoints.Add(hit.transform);
                    if (selectedPoints.Count == 1)// проверка первого нажатия
                    {
                        if (!IsDotCompleted(hit.transform))
                        {
                            if (!IsSelectedButtonHasAnyConnect(selectedPoints[0]))// есть ли какой- то конект у кнопки
                            {
                                selectedPoints.Clear();
                                StatusManager.Instance.SetStatus("Not enough connections!",3f);
                            }
                            else
                            {
                                if (IsDotHaveAllRequiredDots(selectedPoints[0]) || IsDotRequired(selectedPoints[0]))// проверка, есть ли у точки все необходимые точки 
                                {

                                    SelectThisDot(selectedPoints[0]);
                                }
                                else
                                {
                                    ClearAllDotsList();
                                }
                            }
                        }
                        else
                        {
                            ClearAllDotsList();
                        }
                    }
                    else if (selectedPoints.Count == 2) // проверка второго нажатия
                    {
                        if (!ChildrenThisDotFromDot(hit.transform))
                        {
                            StatusManager.Instance.SetStatus("Incorrect connection!", 5f);
                            ClearAllDotsList();
                            return;
                        }
                        else
                        {
                            if (IsDotTooHaveConection(hit.transform))
                            {
                                StatusManager.Instance.SetStatus("Incorrect connection!", 5f);
                                ClearAllDotsList();
                                return;
                            }
                        }
                        if (!IsHaveRequiredResoursesForBuildingDot(selectedPoints[1]))
                        {
                            TextNededResourses(selectedPoints[1]);
                            ClearAllDotsList();
                            return;
                        }

                    }
                    if (selectedPoints.Count == 2) // соеденение точек
                    {
                        if (!IsSecondDotToo(selectedPoints[1]))
                        {
                            ConnectTwoDots();
                            AddPointToAllPointList(selectedPoints[1]);
                            AddPointToAllPointList(selectedPoints[0]);
                            IsDotCompleted(selectedPoints[0]);
                            IsDotCompleted(selectedPoints[1]);
                            if (IsDotHaveAllRequiredDots(hit.transform))
                            {
                                EnableDot(hit.transform);
                            }
                            ConnectPoints(selectedPoints[0], selectedPoints[1]);


                        }
                        else
                        {
                            StatusManager.Instance.SetStatus("The same point is selected!", 5f);
                            ClearAllDotsList();
                        }

                    }

                }
                else
                {
                    ClearAllDotsList();
                }
            }
            else
            {
                ClearAllDotsList();
            }
        }
    }

    // Метод для соединения двух точек линией
    void ConnectPoints(Transform point1, Transform point2)
    {
        // Создаем новую линию и добавляем ее в список линий
        GameObject lineObject = new GameObject("Line");
        IsLineWasFirstGroup(lineObject, point1,point2);

        lineObject.transform.SetParent(gameObject.transform);
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lines.Add(lineRenderer);
        lineRenderer.sortingLayerName = "Lines";

        // Устанавливаем координаты начальной и конечной точек линии
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, point1.position);
        lineRenderer.SetPosition(1, point2.position);
        ClearAllDotsList();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }
    private bool IsSecondDotToo(Transform dot)// проверка была ли нажата таже самая точка
    {
        if (dot == selectedPoints[0])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsSelectedButtonHasAnyConnect(Transform dot) // есть ли у этой точки какие- то конекты, сразу проверяем nomads
    {
        Point point;
        dot.TryGetComponent<Point>(out point);
        if (dot.gameObject.name == "Nomads")
        {
            return true;
        }
        if (point.conectingDots.Count != 0)
        {
            return true;
        }
        else return false;
    }
    public void ClearAllDotsList() // очистить весь выбранный список 
    {

        ResumStatusForAllDots();
        selectedPoints.Clear();
    }
    private void ResumStatusForAllDots() //сбросить все увеличенные кнопки 
    {
        foreach (var dot in selectedPoints)
        {
            Point point;
            dot.TryGetComponent<Point>(out point);
            point.ResumStatus();
        }
    }
    private void SelectThisDot(Transform dot) // отметить, что точка выбрана и увеличена
    {
        Point point;
        dot.TryGetComponent<Point>(out point);
        point.IAmSelectedDot();
    }
    private bool ChildrenThisDotFromDot(Transform dot) //проверка есть ли данная точка в дочерях у другой 
    {
        Point point;
        selectedPoints[0].TryGetComponent<Point>(out point);
        if (point.childrenDots.Contains(dot))
        {
            return true;
        }
        else return false;

    }
    private bool IsDotHaveAllRequiredDots(Transform dot) // есть ли у точки все требуемые точки 
    {
        if (dot.gameObject.name == "Nomads")
        {
            return true;
        }
        Point point;
        dot.TryGetComponent<Point>(out point);
        if (new HashSet<Transform>(point.conectingDots).SetEquals(point.requiredDots))
        {
            point.isPointRequireded = true;
            return true;
        }
        else return false;
    }
    private void EnableDot(Transform dot) // включение точки 
    {
        Point point;
        dot.TryGetComponent<Point>(out point);
        point.EnableDot();
    }
    private void NomadInitialization(Transform dot) // инициализация поселенцев
    {
        if (isNomadActivated == false)
        {
            if (dot.gameObject.name == "Nomads")
            {
                isNomadActivated = true;
                Point point;
                dot.TryGetComponent<Point>(out point);
                point.EnableDot();
            }
        }
    }
    private void ConnectTwoDots()
    {
        Point point;
        selectedPoints[0].TryGetComponent<Point>(out point);
        point.conectingDots.Add(selectedPoints[1]);
        selectedPoints[1].TryGetComponent<Point>(out point);
        point.conectingDots.Add(selectedPoints[0]);
    }
    public bool IsDotCompleted(Transform dot)
    {
        Point point;
        dot.TryGetComponent<Point>(out point);
        HashSet<Transform> numbers = new HashSet<Transform>(point.childrenDots);
        numbers.UnionWith(point.requiredDots);
        if (new HashSet<Transform>(point.conectingDots).SetEquals(numbers))
        {
            point.isPointCompleted = true;
            point.CompleteDot();
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool IsDotRequired(Transform dot)
    {
        Point point;
        dot.TryGetComponent<Point>(out point);
        return point.isPointRequireded;
    }
    public void AddPointToAllPointList(Transform dot)
    {
        Point point;
        dot.TryGetComponent<Point>(out point);
        if (!allPoints.Contains(point))
        {
            allPoints.Add(point);
        }
    }
    public bool IsVillageCompleted()
    {
        foreach (var point in allPoints)
        {
            if (point.gameObject.name == "Village")
            {
                if (IsDotCompleted(point.transform))
                {
                    return true;
                }
                else return false;
            }
        }
        return false;
    }
    public void ResumePoint(Point point)
    {
        point.conectingDots.Clear();
        point.UncompleteDot();
        point.UnrequiredDot();
        allPoints.Remove(point);

    }
    public void DefeatedVillageCheckConnection()
    {
        if (IsVillageCompleted())
        {
            if (IsTownHallCompleted())
            {
                
            }
            else
            {
                ClearAllLinesInGroup(SecondGroupLines);
                ClearAllDotsInGroup(SecondGroup);
            }
        }
        else
        {
            ClearAllLinesInGroup(FirstGroupLines);
            ClearAllDotsInGroup(FirstGroup);
        }
    }
    public void ClearAllLinesInGroup(List<GameObject>lines)
    {
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
    }
    public void IsLineWasFirstGroup(GameObject line,Transform point1,Transform point2)
    {
        if (FirstGroup.Contains(point1.gameObject.name) && FirstGroup.Contains(point2.gameObject.name))
        {
            FirstGroupLines.Add(line);
        }
    }
    public void IsLineWasSecondGroup(GameObject line, Transform point1, Transform point2)
    {
        if (SecondGroup.Contains(point1.gameObject.name) && SecondGroup.Contains(point2.gameObject.name))
        {
            SecondGroupLines.Add(line);
        }
    }
    public void ClearAllDotsInGroup(List<string> dotsInGroup)
    {
        foreach (var point in allPoints)
        {
            foreach (var dots in dotsInGroup)
            {
                if (point.gameObject.name == dots)
                {
                    ResumePoint(point);
                    allPoints.Remove(point);
                }
            }
        }
    }
    public bool IsTownHallCompleted()
    {
        foreach (var point in allPoints)
        {
            if (point.gameObject.name == "TownHall")
            {
                if (IsDotCompleted(point.transform))
                {
                    return true;
                }
                else return false;
            }
        }
        return false;
    }
    public bool IsHaveRequiredResoursesForBuildingDot(Transform dot)
    {
        ResoursesNeded point;
        dot.TryGetComponent<ResoursesNeded>(out point);
        Point pointt;
        dot.TryGetComponent<Point>(out pointt);
        if (point.IsObjectHaveAllNededResourses() )
        {
            return true;
        }
        else return false;
    }
    public bool IsDotTooHaveConection(Transform dot)
    {
        Point point;
        dot.TryGetComponent<Point>(out point);
        if (point.conectingDots.Contains(selectedPoints[0]))
        {
            return true;
        }
        else return false;

    }
    public void TextNededResourses(Transform dot)
    {
        Point point;
        dot.TryGetComponent<Point>(out point);
        point.TextIfResoursesDontHave();
    }
}
