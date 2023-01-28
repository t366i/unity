using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chick : MonoBehaviour
{
    static public List<Chick> chickList = new List<Chick>();
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    public float maxHp = 2;
    private bool isLive;
    private float m_curHp;
    public float curHp
    {
        get { return m_curHp; }
        set
        {
            m_curHp = value;

            // HP 바 갱신

        }
    }
    public GameObject booldEffectGo;
    public DropItem dropItem;

    enum State
    {
        Idle,
        Run,
        Attck,
        OnDamage,
        Die
    }
   
    void ExitState()
    {
        switch(m_state)
        {
            case State.Attck:
                if (attackCo != null)
                    StopCoroutine(attackCo);
                break;

            case State.OnDamage:
                if (onDamaeCo != null)
                    StopCoroutine(onDamaeCo);
                break;

            case State.Run:
                if (runCo != null)
                    StopCoroutine(runCo);
                break;

            case State.Die:
                if (dieCo != null)
                    StopCoroutine(dieCo);
                break;
        }
    }

    State m_state;
    State state
    {
        get { return m_state; }
        set
        {
            Debug.Log(name + "Pre: " + m_state + " /new: " + value);
            ExitState();
            switch (value)
            {
                case State.Idle:
                    break;
                case State.Run:
                    runCo = StartCoroutine(RunCo());
                    break;
                case State.Attck:
                    attackCo = StartCoroutine(AttackCo());
                    break;
                case State.OnDamage:
                    onDamaeCo = StartCoroutine(OnDamageCo());
                    break;
                case State.Die:
                    dieCo = StartCoroutine(DieCo());
                    break;
                default:
                    break;
            }

            m_state = value;
        }
    }
    Coroutine runCo;
    Coroutine onDamaeCo;
    Coroutine dieCo;
    Coroutine attackCo;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        chickList.Add(this);
    }

    void Start()
    {
        isLive = true;
        curHp = maxHp;
        state = State.Run;
    }

    private float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, Player.instance.transform.position);
    }

    private IEnumerator AttackCo()
    {
        // Attack Animation
        animator.SetTrigger("Attack");

        // 잠시 기다렸다가(공격 전 딜레이)
        yield return new WaitForSeconds(1f);

        // 플레이어에게 달려든다.
        navMeshAgent.SetDestination(Player.instance.transform.position);
        navMeshAgent.speed = 10;

        // 달려드는 시간, 공격 후 딜레이 동안 대기
        yield return new WaitForSeconds(2f);

        // Run 스테이트로 변경
        state = State.Run;
    }

    private IEnumerator RunCo()
    {
        animator.SetBool("IsWalking", true);
        while (isLive)
        {
            // 플레이어가 가까이에 있다면 Attack 스테이트로 바꾼다.
            if (GetPlayerDistance() < 2)
            { 
                state = State.Attck;
                yield break; // return
            }

            // 플레이어를 도착지점으로 함
            navMeshAgent.SetDestination(Player.instance.transform.position);
            navMeshAgent.speed = 3;
            navMeshAgent.angularSpeed = 100;

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator OnDamageCo()
    {
        // 피 이펙트 생성
        GameObject newBloodEffect = Instantiate(booldEffectGo);
        newBloodEffect.transform.position = transform.position;
        Destroy(newBloodEffect, 0.5f);

        // 도착위치 변경
        float power = 2;
        navMeshAgent.speed = 10;
        Vector3 desPos = transform.position + transform.forward * -power;
        navMeshAgent.SetDestination(desPos);
        navMeshAgent.angularSpeed = 0;

        // 밀려날 동안 대기
        yield return new WaitForSeconds(0.5f);

        // 스테이트 변경
        state = State.Run;
    }

    IEnumerator DieCo()
    {
        isLive = false;

        // 도착지점을 현재 지점으로 한다.
        navMeshAgent.SetDestination(transform.position);

        // 죽는 애니메이션
        animator.SetTrigger("Die");

        // 죽는 시간동안 대기
        yield return new WaitForSeconds(2f);

        // 아이템 드랍
        DropItem newDropItem = Instantiate(dropItem);
        newDropItem.transform.position = transform.position;

        chickList.Remove(this);

        // 파괴
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLive == false)
            return;

        //print(other + " curHp: " + curHp);

        // 화살일 경우
        if (other.tag == "PlayerMissile")
        {
            Arrow arrow = other.GetComponent<Arrow>();
            if (arrow.atkAtt != null && arrow.atkAtt.type == AttackAttribute.Type.Poison)
                StartCoroutine(PoisonCo(arrow.atkAtt));

            // 화살을 없앤다.
            Destroy(other.gameObject);

            curHp = curHp - 1;

            if (curHp > 0)
                state = State.OnDamage;
            else
                state = State.Die;
        }
    }

    private IEnumerator PoisonCo(AttackAttribute atkAtt)
    {
        float startTime = Time.time;
        while (startTime + atkAtt.duration > Time.time)
        {
            curHp = curHp - atkAtt.damage;
            Debug.Log(name + " PoisonCo curHp: " + curHp);
            // 독 이펙트 생성.
            GameObject newPosionEffect = Instantiate(atkAtt.effectGo);
            newPosionEffect.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            newPosionEffect.transform.SetParent(transform);
            Destroy(newPosionEffect, 1);

            // 독 색으로 변경
            SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
            Material material = renderer.materials[0];
            material.color = Color.green;
            yield return new WaitForSeconds(0.1f);
            material.color = Color.white;

            yield return new WaitForSeconds(atkAtt.interval);
        }
    }
}
