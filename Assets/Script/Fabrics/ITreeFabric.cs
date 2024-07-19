using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITreeFabric : MonoBehaviour, IFabric
{
    public int amount;
    public int peoplesNeded;
    public LayerMask peoplesLayer;
    public void ProductResourse()
    {
        if (GetPeoplesOverFabrick())
        {
            Resourse.instance.Tree += amount;
            Resourse.instance.UpdateResourseText();
        }
        else
        {
            StatusManager.Instance.SetStatus("Not Enough Peoples", 5f);
        }
    }
    public bool GetPeoplesOverFabrick()
    {
        Collider2D[] hitPeoples = Physics2D.OverlapBoxAll(transform.position, new Vector2(2, 2), 0, peoplesLayer);
        Debug.Log(hitPeoples.Length);
        if (hitPeoples.Length > peoplesNeded)
        {
            return true;
        }
        else return false;
    }
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}
