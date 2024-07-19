using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    IFabric fabric;
    public Vector3 fabricPosition;
    public static InputManager instance;
    private void Start()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent<IFabric>(out fabric))
                {
                    GameManager.isNomadBuilded = true;
                    fabric.ProductResourse();
                    fabricPosition = fabric.GetPosition();
                }
                else
                {
                        GameManager.isNomadBuilded = false;
                        //GameManager.instance.RestartNomads();

                }

            }
        }
    }
}
