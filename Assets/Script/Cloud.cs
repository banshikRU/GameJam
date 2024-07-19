using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float mMovementSpeed;
    public LayerMask boardLayer;
    public bool bIsGoingRight;
    public float mRaycastingDistance = 1f;
    public bool isPatrol;
    public int speed;
    private SpriteRenderer _mSpriteRenderer;
    void Start()
    {
        mMovementSpeed = Random.Range(1, 6);
        isPatrol = false;
        int a = Random.Range(0, 3);
        if (a == 0)
        {
            bIsGoingRight = false;
        }
        else bIsGoingRight = true;
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _mSpriteRenderer.flipX = bIsGoingRight;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CloudsWall")
        {
            bIsGoingRight = !bIsGoingRight;
            _mSpriteRenderer.flipX = bIsGoingRight;
            RestartMovementSpeed();
        }
    }
    private void Update()
    {
        Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime * mMovementSpeed;
        transform.Translate(directionTranslation);
    }
    public void RestartMovementSpeed()
    {
        mMovementSpeed = Random.Range(2, 5);
        int a = Random.Range(0, 2);
    }

}
