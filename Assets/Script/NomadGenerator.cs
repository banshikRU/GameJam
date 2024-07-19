using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomadGenerator : MonoBehaviour
{
    [SerializeField] GameObject _nomads;
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(_nomads, new Vector3(Random.Range(-3, 3),gameObject.transform.position.y,0), Quaternion.identity);
        }
    }
}
