using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public class EnemyStateController : MonoBehaviour
{
    public Enemy enemy;
    public BaseEnemyState State;

    private void Awake()
    {
        enemy = new Enemy();
        State = new IdleState();//각 객체 생성;
        enemy.init();
    }


    private void Update()
    {
        State.StateUpdate(this, enemy);//상태 시간별 업데이트 함수
    }

    public void ChgState(BaseEnemyState state)
    {
        State = state;
        Debug.Log(state.GetType().ToString() + " 로 상태 변경");
    }
    public void Operate()
    {
        State.GetAttack(this, enemy);
    }

}
public class Enemy
{
    [SerializeField]
    public float counterAttackTime = 0.5f;
    [SerializeField]
    public float respawnTime = 1.0f;
    [SerializeField]//상수
    public int EnemyMaxLife = 2;

    int EnemyLife;
    public float counterAttackTimer { get; set; }
    public float respawnTimer { get; set; }

    public int ELife { get => EnemyLife; set => EnemyLife=value; }//읽기 전용 프로퍼티

    public void init()
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

    
}

public interface BaseEnemyState//기본상태
{
    void GetAttack(EnemyStateController controller, Enemy enemy);
    void StateUpdate(EnemyStateController controller, Enemy enemy);
}

class IdleState : BaseEnemyState
{
    public void GetAttack(EnemyStateController controller, Enemy enemy)
    {
        enemy.ELife-=1;
        Debug.Log("적 체력 1 감소");
        if(enemy.ELife == 0)
        {
            enemy.Die();
            controller.ChgState(new DeadState());
        }
        else
        {
            controller.ChgState(new CounterAttackState());
        }
    }
    public void StateUpdate(EnemyStateController controller, Enemy enemy)
    {
        //아무것도 안함
    }
}

class CounterAttackState: BaseEnemyState
{
    public void GetAttack(EnemyStateController controller, Enemy enemy)
    {
        Debug.Log("적의 반격");//체력 감소 X
    }
    public void StateUpdate(EnemyStateController controller, Enemy enemy)
    {
        enemy.counterAttackTimer -= Time.deltaTime;
        if (enemy.counterAttackTimer <= 0)//시간이 지났으면
        {
            controller.ChgState(new IdleState());//상태 변경
            enemy.counterAttackTimer = enemy.counterAttackTime;//반격시간 초기화
        }
    }
}

class DeadState: BaseEnemyState
{
    public void GetAttack(EnemyStateController controller, Enemy enemy)
    {
        Debug.Log("공격 대상을 찾을 수 없습니다.");
    }
    public void StateUpdate(EnemyStateController controller, Enemy enemy)
    {
        enemy.respawnTimer -= Time.deltaTime;
        if (enemy.respawnTimer <= 0)
        {
            controller.ChgState(new IdleState());
            enemy.respawnTimer= enemy.respawnTime;
        }
    }
}