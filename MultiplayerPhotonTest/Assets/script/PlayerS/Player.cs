using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [SerializeField] Transform targetedEnemy;

    private bool enemyClicked = false;
    private bool isWalking = false;
    private bool isAttacking = false;

    private NavMeshAgent navMeshAgent;

    [SerializeField] float shootingDistance = 10;

    [Header("Events")]
    [Space]

    [SerializeField] UnityEvent OnAttackEvent;

    [SerializeField] UnityEvent OnMoveEvent;
    [SerializeField] UnityEvent OnMoveEndEvent;

    private float nextAttack;
    [SerializeField] float attackRate = 5;

    [SerializeField] Quaternion direction;

    PhotonView view;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        isAttacking = false;
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            nextAttack -= Time.deltaTime;
            direction = gameObject.transform.rotation;


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        targetedEnemy = hit.transform;
                        enemyClicked = true;
                    }
                    else
                    {
                        isAttacking = false;
                        isWalking = true;

                        OnMoveEvent.Invoke();
                        enemyClicked = false;
                        navMeshAgent.destination = hit.point;
                        navMeshAgent.isStopped = false;
                    }
                }
            }

            if (enemyClicked == true)
            {
                MoveAndShoot();
            }

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                OnMoveEndEvent.Invoke();
                isWalking = false;
            }
            else
            {
                if (!isAttacking)
                {
                    OnMoveEvent.Invoke();
                    isWalking = true;
                }

            }
        }


    }

    void MoveAndShoot()
    {
        if (view.IsMine)
        {
            if (targetedEnemy == null)
            {
                isAttacking = false;
                return;
            }

            navMeshAgent.destination = targetedEnemy.position;

            if (navMeshAgent.remainingDistance >= shootingDistance)
            {
                navMeshAgent.isStopped = false;
                enemyClicked = false;
                isWalking = true;

            }

            if (navMeshAgent.remainingDistance <= shootingDistance && targetedEnemy != null && enemyClicked)
            {
                transform.LookAt(navMeshAgent.destination);

                navMeshAgent.isStopped = true;
                isAttacking = true;


                if (nextAttack <= 0 && targetedEnemy != null && enemyClicked && isAttacking)
                {
                    isAttacking = true;
                    nextAttack = attackRate;
                    OnMoveEndEvent.Invoke();
                    OnAttackEvent.Invoke();

                    Debug.Log("ATTACk");
                    enemyClicked = false;
                }
            }
        }

    }

    public Quaternion GetDirection()
    {
            return direction;     
        
    }
}
