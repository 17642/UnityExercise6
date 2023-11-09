using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Enemy : EnemyState
{
    [SerializeField]//���
    int EnemyMaxLife = 2;
    [SerializeField]
    float counterAttackTime = 0.5f;
    [SerializeField]
    float respawnTime = 1.0f;

    float counterAttackTimer;
    float respawnTimer;
    int EnemyLife;

    public int ELife { get { return EnemyLife; } }//�б� ���� ������Ƽ

    private void Start()
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

    public void GetAttack()
    {
        if (currentState == States.Idle)
        {
            EnemyLife--;//���� idle ������ ��� ü�� ����
            Debug.Log("���� ���ݹ���. ü�� 1 ����");
            if (EnemyLife == 0)//EnemyLife�� 0�� ���:
            {
                Die();//�� ��� ó��
                ChgState(States.Dead);//������� ���¸� ����
            }
            else//���� ���� ������� ���
            {
                ChgState(States.CounterAttack); //�ݰ����� ���� ����
            }
        } else if(currentState == States.CounterAttack) //���� �ݰ� ������ ���
        {
            Debug.Log("���� �ݰ�");//ü���� �������� ����
        }
        else //���� ��� ������ ���
        {
            Debug.Log("������ ���� �����ϴ�");//ü���� �������� ����
        }
    }

    //���� �Լ�
    protected override void IdleState()
    {
        return;//�ƹ��͵� ����
    }
    protected override void CounterAttackState()
    {
        counterAttackTimer -=Time.deltaTime;//�ݰ� �ð�
        if (counterAttackTimer <= 0 )
        {
            ChgState(States.Idle);
            counterAttackTimer = counterAttackTime;//�ݰ� �ð� �ʱ�ȭ
        }
    }
    protected override void DeadState()
    {
        respawnTimer -= Time.deltaTime;//������ �ð�
        if (respawnTimer <= 0)
        {
            ChgState(States.Idle);//�� ���� ����
            respawnTimer = respawnTime;//������ �ð� �ʱ�ȭ
            EnemyLife = EnemyMaxLife;//�� ü�� �ʱ�ȭ
        }
    }
}

public abstract class  EnemyState: MonoBehaviour
{
    public enum States { Idle, CounterAttack, Dead}//�� ���� 3����
    private States initialState = States.Idle;//�ʱ� ����//Idle
    protected States currentState;

    public States CurrentState { get { return currentState; } }//currentState�� ������Ƽ(�б� ����)

    protected virtual void Awake()
    {
        currentState= initialState;//���� ���¸� �ʱ� ���·� ����
    }

    protected virtual void Update()
    {
        UpdateState();
    }
    protected virtual void UpdateState() { //���¿� ���� State ������Ʈ �Լ�
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

    public void ChgState(States state) {//���� ���� �⺻ �Լ�
        currentState = state;

        string logger = "���� ";
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
        logger += " ���� ��ȯ��.";
        Debug.Log(logger);
    }

    //�� ���¿� ���� �Լ�
    protected abstract void IdleState();
    protected abstract void CounterAttackState();
    protected abstract void DeadState();

}