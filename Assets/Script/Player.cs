using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum State
    {
        Idle,
        Run,
        Attack,
        Die
    }
    static public Player instance;
    public Arrow arrow;

    void ExitState()
    {
        switch(m_state)
        {
            case State.Attack:
                if (AttackCo_ != null)
                    StopCoroutine(AttackCo_);
                break;
        }

    }

    private State m_state;
    private State state
    {
        get { return m_state; }
        set
        {
            //Debug.Log("Previous State: " + state + " New State: " + value);

            ExitState();
            switch (value)
            {
                case State.Attack:
                    AttackCo_ = StartCoroutine(AttckCo());
                    break;

            }


            m_state = value;
        }
    }
    private Coroutine AttackCo_;

    public int maxHp = 10;
    public int curHp;
    public Joystick joystick;
    public float moveSpeed = 6;

    private Rigidbody rigidbody;
    private Vector3 movement;
    private Animator animator;

    private void Awake()
    {
        instance = this;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        curHp = maxHp;

        // 적이 있으면 공격 없으면 Idle
        ChangeState();
    }

    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        Move(x, y);

        // 아무 방향이나 움직이면 isWalking 을 true로 만든다.
        bool isWalking = x != 0f || y != 0f;
        animator.SetBool("IsWaking", isWalking);

        if (state == State.Run && isWalking == false)
        {
            // 상태 변경(적이 있으면 공격, 없으면 Idle)
            ChangeState();
            return;
        }   

        if (isWalking == true && state != State.Run)
            state = State.Run;

        // 이동방향을 바라본다(rotation 값을 조이스틱의 x,y방향으로 하는 방법)
        if (state == State.Run)
        {
            float heading = Mathf.Atan2(x, y);
            transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0);
        }
    }

    // 상태 변경(적이 있으면 공격, 없으면 Idle)
    void ChangeState()
    {
        if (Chick.chickList.Count > 0)
            state = State.Attack;
        else
            state = State.Idle;
    }

    private void Move(float x, float y)
    {
        movement.Set(x, 0f, y);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);
    }

    IEnumerator AttckCo()
    {
        while (true)
        {
            // 적을 찾는다.
            Chick chick = null;
            float distance = float.MaxValue;

            // 거리가 가장 가까운 애를 찾는다.
            foreach (Chick iter in Chick.chickList)
            {
                float iterDistance = Vector3.Distance(transform.position, iter.transform.position);
                if (iterDistance < distance)
                {
                    chick = iter;
                    distance = iterDistance;
                }
            }

            if (chick != null)
            {
                // 적을 바라본다.
                transform.LookAt(chick.transform.position);
                Debug.Log("pos: " + transform.position + " /eneny: " + chick.transform.position);

                // 공격 애니메이션
                animator.SetTrigger("Shoot");

                // 화살 발사 전 대기
                yield return new WaitForSeconds(0.3f);

                // 적에게 화살을 발사한다.
                Arrow newArrow = Instantiate(arrow);
                newArrow.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
                newArrow.transform.LookAt(new Vector3(chick.transform.position.x, 0.5f, chick.transform.position.z));
                newArrow.SetAttribute(myAtkAtt);
            }

            // 공격 딜레이만큼 대기
            yield return new WaitForSeconds(2);
        }
       
    }

    //private DropItem dropItem;
    private AttackAttribute myAtkAtt;
    private void OnTriggerEnter(Collider other)
    {
        print(other);

        if (other.tag == "Item")
        {
            DropItem dropItem = other.GetComponent<DropItem>();
            //print(dropItem.attribute + " /Damage: " + dropItem.posisonDamage);

            // 획득한 아이템 처리.
            //this.dropItem = dropItem;
            myAtkAtt = dropItem.attackAttribute;

            Destroy(other.gameObject, 0.2f);
        }
    }

   
}
