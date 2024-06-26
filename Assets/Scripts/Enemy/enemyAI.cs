using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class enemyAI : MonoBehaviour
{
    //movement
    [SerializeField] private Rigidbody rb;
    public Vector3 moveDirection = Vector3.forward;
    [SerializeField] private float moveSpeed = 1.0f;
    
    //enemy attacks
    [SerializeField] private float attackCooldown = 1.0f;
    private float lastAttackCooldown = 0.0f;
    [SerializeField] private float enemyDamage = 0.5f;
    
    public EnemyState currentState = EnemyState.Idle;
    [SerializeField] private EnemyState previousState;
    
    private float origMoveSpeed;
    private bool isAlreadySlow = false;
    public bool isPoisoned = false;

    public enum EnemyState : int
    {
        Idle,
        Moving,
        Attack,
        Damaged,
        numStates
    }

    private Color[] stateColors = new Color[(int)EnemyState.numStates]
    {
        new Color(0, 0, 0),
        new Color(255, 0, 0),
        new Color(0, 0, 255),
        new Color(0, 255, 0)
    };


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        origMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        var r =GetComponent<Renderer>();
        if (r)
        {
            r.material.color = stateColors[(int)currentState];
        }
        
        switch (currentState)
        {
            case EnemyState.Idle:
                Move();
                break;
            case EnemyState.Moving:
                CalculateMovement();
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Damaged:
                StartCoroutine(ResetStateAfterDamaged());
                break;
            default:
                break;
        }
        
    }
    
    void Move()
    {
        currentState = EnemyState.Moving;
    }

    void CalculateMovement()
    {
        Vector3 movement = moveDirection.normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    private void OnTriggerStay(Collider other)
    {
        //attack slimes
        if (other.gameObject.CompareTag("Player") && Time.time - lastAttackCooldown >= attackCooldown)
        {
            currentState = EnemyState.Attack;
            Health slimeHealth = other.gameObject.GetComponent<Health>();
            slimeHealth.TakeDamage(enemyDamage);
            lastAttackCooldown = Time.time;
            if (slimeHealth.health <= 0)
            {
                currentState = EnemyState.Moving;
            }
        }else if (other.gameObject.CompareTag("Wall"))
        {
            currentState = EnemyState.Attack;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            previousState = currentState;
            currentState = EnemyState.Damaged;
        }
    }
    
    private IEnumerator ResetStateAfterDamaged()
    {
        yield return new WaitForSeconds(0.1f);
        currentState = previousState;
    }

    public void ApplySlowEffect(float duration, float factor)
    {

        StartCoroutine(SlowEffect(duration, factor));
        
    }

    private IEnumerator SlowEffect(float duration, float factor)
    {
        if (moveSpeed >= origMoveSpeed)
        {
            moveSpeed *= factor;
        }

        yield return new WaitForSeconds(duration);
        moveSpeed = origMoveSpeed;
    }

    public void AttackUnit(Health unitHealth)
    {
        if (Time.time - lastAttackCooldown >= attackCooldown)
        {
            currentState = EnemyState.Attack;
            unitHealth.TakeDamage(enemyDamage);
            lastAttackCooldown = Time.time;
        }
    }
    public void StopAttack()
    {
        currentState = EnemyState.Moving;
    }

    public void TakeDamage()
    {
        previousState = currentState;
        currentState = EnemyState.Damaged;
    }
    
}
