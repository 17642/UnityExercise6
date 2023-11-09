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
        State = new IdleState();//�� ��ü ����;
        enemy.init();
    }


    private void Update()
    {
        State.StateUpdate(this, enemy);//���� �ð��� ������Ʈ �Լ�
    }

    public void ChgState(BaseEnemyState state)
    {
        State = state;
        Debug.Log(state.GetType().ToString() + " �� ���� ����");
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
    [SerializeField]//���
    public int EnemyMaxLife = 2;

    int EnemyLife;
    public float counterAttackTimer { get; set; }
    public float respawnTimer { get; set; }

    public int ELife { get => EnemyLife; set => EnemyLife=value; }//�б� ���� ������Ƽ

    public void init()
    {
        counterAttackTimer = counterAttackTime;
        respawnTimer = respawnTime;

        EnemyLife = EnemyMaxLife;
    }

    //���� �Լ�
    public void Die()
    {
        if (GameManager.instance.Quest)//����Ʈ ���� ���� Ȯ��
        {
            GameManager.instance.AddScore(25);
            Debug.Log("����Ʈ ���� ���̹Ƿ� 25�� �߰�");
        }
        else
        {
            GameManager.instance.AddScore(30);
            Debug.Log("����Ʈ�� �����ϰ� ���� �����Ƿ� 30�� �߰�");
        }

    }

    
}

public interface BaseEnemyState//�⺻����
{
    void GetAttack(EnemyStateController controller, Enemy enemy);
    void StateUpdate(EnemyStateController controller, Enemy enemy);
}

class IdleState : BaseEnemyState
{
    public void GetAttack(EnemyStateController controller, Enemy enemy)
    {
        enemy.ELife-=1;
        Debug.Log("�� ü�� 1 ����");
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
        //�ƹ��͵� ����
    }
}

class CounterAttackState: BaseEnemyState
{
    public void GetAttack(EnemyStateController controller, Enemy enemy)
    {
        Debug.Log("���� �ݰ�");//ü�� ���� X
    }
    public void StateUpdate(EnemyStateController controller, Enemy enemy)
    {
        enemy.counterAttackTimer -= Time.deltaTime;
        if (enemy.counterAttackTimer <= 0)//�ð��� ��������
        {
            controller.ChgState(new IdleState());//���� ����
            enemy.counterAttackTimer = enemy.counterAttackTime;//�ݰݽð� �ʱ�ȭ
        }
    }
}

class DeadState: BaseEnemyState
{
    public void GetAttack(EnemyStateController controller, Enemy enemy)
    {
        Debug.Log("���� ����� ã�� �� �����ϴ�.");
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