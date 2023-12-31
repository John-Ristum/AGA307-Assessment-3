using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { Patrol, Chase, Attack, Damage, Die}
public class Enemy : GameBehaviour
{
    public EnemyState state;

    Animator anim;

    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer;

    //patrol
    Vector3 destPoint;
    public bool walkpointSet;
    [SerializeField] float range = 15;
    [SerializeField] float waitTime = 1f;
    public bool patrolSet = true;           //Used in animator to revert to patrol state once animation is done

    public float attackDistance = 1f;

    public int health = 80;

    public int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        state = EnemyState.Patrol;
        StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        //test code
        //if (Input.GetKeyDown("1"))
        //    anim.SetTrigger("atk1");
        //if (Input.GetKeyDown("2"))
        //    anim.SetTrigger("atk2");
        //if (Input.GetKeyDown("3"))
        //    StartCoroutine(Die());
        //if (Input.GetKeyDown("4"))
        //    state = EnemyState.Chase;
        //if (Input.GetKeyDown("5"))
        //    _PLAYER.Hit();


        if (state == EnemyState.Die)
            return;

        if (state == EnemyState.Chase)
        {
            agent.speed = 5f;
            //get distance between me and player
            float distToPlayer = Vector3.Distance(transform.position, _PLAYER.transform.position);
            //Set the destination to that of the player
            agent.SetDestination(_PLAYER.transform.position);

            if (distToPlayer <= attackDistance)
                Attack();

        }

        transform.LookAt(new Vector3(_PLAYER.transform.localPosition.x, transform.position.y, _PLAYER.transform.localPosition.z));
        //Patrol();

        if (!patrolSet)
            SetPatrolState();
    }

    IEnumerator Patrol()
    {
        Debug.Log("patrol state endered");
        if (state != EnemyState.Patrol)
        {
            StopAllCoroutines();
            yield break;
        }
            

        if (!walkpointSet)
            SearchForDest();
        if (walkpointSet)
            agent.SetDestination(destPoint);
        if (Vector3.Distance(transform.position, destPoint) < 1)
        {
            walkpointSet = false;
            randomNumber = Random.Range(0, 5);
        }
            

        //Determines if enemy goes in for attack or continues to patrol
        

        yield return new WaitForSeconds(waitTime);

        if (randomNumber == 4)
        {
            state = EnemyState.Chase;
        }
        else if (randomNumber < 4)
        {
            
            StartCoroutine(Patrol());
        }
            
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if(Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkpointSet = true;
        }
    }

    public void SetPatrolState()
    {
        patrolSet = true;
        state = EnemyState.Patrol;
        agent.speed = 1.5f;
        StartCoroutine(Patrol());
    }

    void Attack()
    {
        StopAllCoroutines();
        agent.speed = 0f;
        state = EnemyState.Attack;
        anim.SetTrigger("atk" + Random.Range(1, 3));
        randomNumber = 0;
    }

    public void Hit(int _damage)
    {
        StopAllCoroutines();
        state = EnemyState.Damage;
        agent.speed = 0f;
        transform.LookAt(new Vector3(_PLAYER.transform.localPosition.x, transform.position.y, _PLAYER.transform.localPosition.z));
        anim.SetTrigger("dmgL");
        health -= _damage;
        if (health <= 0)
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        anim.SetTrigger("die");

        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }
}
