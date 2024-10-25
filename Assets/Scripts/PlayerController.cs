using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPhysics
{

    [SerializeField] private float acceleration;
    [SerializeField] private float limits;
    [SerializeField] private GameObject ball;
    [SerializeField] private float ballImpulse;
    [SerializeField] private float startTime;
    
    private float currentTime;
    
    private Physics physics;
    private Physics ballPhysics;

    private Coroutine countDownCoroutine;

    
    private void Start()
    {
        currentTime = startTime;
        ballPhysics = ball.GetComponent<Physics>();
        physics = this.gameObject.GetComponent<Physics>();
        countDownCoroutine = StartCoroutine(StartCountdown());
    }
    
    void Update()
    {
        if (GameManager.Instance.ballOnBoard && Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(countDownCoroutine);
            countDownCoroutine = null;
            Unparent(ball);
            
            ShootBall();
        } 
        
        Movement();
    }
    
    private void CheckBorder()
    {
        if (transform.position.x < -limits)
        {
            physics.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(-limits, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > limits)
        {
            physics.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(limits, transform.position.y, transform.position.z);
        }
    }
    
    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            physics.ApplyForce(new Vector3(Input.GetAxis("Horizontal"),0,0)*acceleration);
            CheckBorder();
        }
        physics.ApplyFriction(); 
    }
    
    private void ShootBall()
    {
        ballPhysics.ApplyImpulse(new Vector3(0,1,0) * ballImpulse);
        GameManager.Instance.ballOnBoard = false;
    }
    
    private IEnumerator StartCountdown()
    {
        while (currentTime > 0)
        {
            Debug.Log("Tiempo restante: " + currentTime);
            yield return new WaitForSeconds(1f); // Espera 1 segundo
            currentTime--;
        }
        Unparent(ball);
        ShootBall();
    }
    
    private void Unparent(GameObject ballGameObject)
    {
        ballGameObject.transform.SetParent(null);
    }
    
}
