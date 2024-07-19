using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nomads : MonoBehaviour
{
    private float mMovementSpeed;
    public bool bIsGoingRight;
    public float mRaycastingDistance = 1f;
    public bool isPatrol;
    public int speed;
    private SpriteRenderer _mSpriteRenderer;

    void Start()
    {
        mMovementSpeed = Random.Range(2, 6);
        isPatrol = false;
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            bIsGoingRight = false;
        }
        else bIsGoingRight = true;
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _mSpriteRenderer.flipX = bIsGoingRight;
    //    GameManager.instance.nomads.Add(this);
    }
    public void RestartMovementSpeed()
    {
        mMovementSpeed = Random.Range(2, 6);

    }

    void Update()
    {
        
        if (GameManager.isNomadBuilded == false)
        {
            Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;
            directionTranslation *= Time.deltaTime * mMovementSpeed;
            transform.Translate(directionTranslation);
        }
        if (GameManager.isNomadBuilded == true)
        {
            Vector3 moveDirection = (InputManager.instance.fabricPosition - transform.position).normalized;
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Terrain")
        {
            bIsGoingRight = !bIsGoingRight;
            _mSpriteRenderer.flipX = bIsGoingRight;
            RestartMovementSpeed();
        }
    }

}
