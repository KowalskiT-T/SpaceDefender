using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerBehavior : MonoBehaviour
{

    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;


    Shooter shooter;

    Vector2 rawInput;

    Vector2 minBound;
    Vector2 maxBound;
    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }
    void Start()
    {
        InitBound();
    }

    void Update()
    {
        Movement();
    }


    void InitBound()
    {
        Camera mainCam = Camera.main;
        minBound = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCam.ViewportToWorldPoint(new Vector2(1, 1)); 
    }
    void Movement()
    {
        Vector3 delta = rawInput * playerSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBound.x + paddingLeft, maxBound.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBound.y + paddingBottom, maxBound.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
