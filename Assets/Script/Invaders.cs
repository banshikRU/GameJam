using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed ; // сколько клеток проходит в секунду
    private void Update()
    {
        float step = moveSpeed * Time.deltaTime;

        if (GameManager.isInvaderAttack)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);

            if (Vector3.Distance(transform.position, pointB.position) < 0.001f)
            {
                PointConnector.instance.DefeatedVillageCheckConnection();
                GameManager.isInvaderAttack = false;
                StatusManager.Instance.SetStatus("You are defeated!",7f);

            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, step);

            //if (Vector3.Distance(transform.position, pointA.position) < 0.001f)
            //{
            //    movingTowardsB = true;
            //}
        }
    }
}
