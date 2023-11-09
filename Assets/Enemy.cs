using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Enemy : EnemyState
{
    [SerializeField]//상수
    int EnemyMaxLife = 2;
    [SerializeField]
    float counterAttackTime = 0.5f;
    [SerializeField]
    float respawnTime = 1.0f;

    float counterAttackTimer;
    float respawnTimer;
    int EnemyLife;

    public int ELife { get { return EnemyLife; } }//읽기 전용 프로퍼티

    private void Start()
    {
        counterAttackTimer = counterAttackTime;
        respawnTimer = respawnTime;

        EnemyLife = EnemyMaxLife;
    }

    //동작 함수
    public void Die()
    {
        if (GameManager.instance.Quest)//퀘스트 수행 여부 확인
        {
            GameManager.instance.AddScore(25);
            Debug.Log("퀘스트 수행 중이므로 25점 추가");
        }
        else
        {
            GameManager.instance.AddScore(30);
            Debug.Log("퀘스트를 수행하고 있지 않으므로 30점 추가");
        }

    }

    public void GetAttack()
    {
        if (currentState == States.Idle)
        {
            EnemyLife--;//적이 idle 상태일 경우 체력 감소
            Debug.Log("적이 공격받음. 체력 1 감소");
            if (EnemyLife == 0)//EnemyLife가 0인 경우:
            {
                Die();//적 사망 처리
                ChgState(States.Dead);//사망으로 상태를 변경
            }
            else//적이 아직 살아있을 경우
            {
                ChgState(States.CounterAttack); //반격으로 상태 변경
            }
        } else if(currentState == States.CounterAttack) //적이 반격 상태일 경우
        {
            Debug.Log("적의 반격");//체력은 감소하지 않음
        }
        else //적이 사망 상태인 경우
        {
            Debug.Log("공격할 적이 없습니다");//체력은 감소하지 않음
        }
    }

    //상태 함수
    protected override void IdleState()
    {
        return;//아무것도 안함
    }
    protected override void CounterAttackState()
    {
        counterAttackTimer -=Time.deltaTime;//반격 시간
        if (counterAttackTimer <= 0 )
        {
            ChgState(States.Idle);
            counterAttackTimer = counterAttackTime;//반격 시간 초기화
        }
    }
    protected override void DeadState()
    {
        respawnTimer -= Time.deltaTime;//리스폰 시간
        if (respawnTimer <= 0)
        {
            ChgState(States.Idle);//적 상태 변경
            respawnTimer = respawnTime;//리스폰 시간 초기화
            EnemyLife = EnemyMaxLife;//적 체력 초기화
        }
    }
}

public abstract class  EnemyState: MonoBehaviour
{
    public enum States { Idle, CounterAttack, Dead}//적 상태 3가지
    private States initialState = States.Idle;//초기 상태//Idle
    protected States currentState;

    public States CurrentState { get { return currentState; } }//currentState의 프로퍼티(읽기 전용)

    protected virtual void Awake()
    {
        currentState= initialState;//현재 상태를 초기 상태로 설정
    }

    protected virtual void Update()
    {
        UpdateState();
    }
    protected virtual void UpdateState() { //상태에 따른 State 업데이트 함수
        switch(currentState)
        {
            case States.Idle:
                IdleState();
                break;
            case States.CounterAttack:
                CounterAttackState();
                break;
            case States.Dead:
                DeadState();
                break;

        }
    }

    public void ChgState(States state) {//상태 변경 기본 함수
        currentState = state;

        string logger = "상태 ";
        switch (currentState)
        {
            case States.Idle:
                logger += "Idle";
                break;
            case States.CounterAttack:
                logger += "CounterAttack";
                break;
            case States.Dead:
                logger += "Dead";
                break;

        }
        logger += " 으로 변환됨.";
        Debug.Log(logger);
    }

    //각 상태에 대한 함수
    protected abstract void IdleState();
    protected abstract void CounterAttackState();
    protected abstract void DeadState();

}