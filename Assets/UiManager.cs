using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditorInternal;

public class UiManager : MonoBehaviour
{
    
    [SerializeField]
    Text Scoreboard;
    [SerializeField]
    Text QuestToggleButton;
    [SerializeField]
    TMP_Text QuestDisplay;
    [SerializeField]
    TMP_Text EnemyStateDisp;
    [SerializeField]
    TMP_Text EnemyHealthDisp;//�ؽ�Ʈ �ʵ�
    [SerializeField]
    Enemy enemyState;//�� ���� Ȯ��

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Quest)//����Ʈ ������ ���� ��ư �̸� ����
        {
            QuestToggleButton.text = "Quest End";
            QuestDisplay.text = "Quest: True";//���� ���� UI ���
        }
        else
        {
            QuestToggleButton.text = "Quest Start";
            QuestDisplay.text = "Quest: False";
        }
        Scoreboard.text ="Score: " +GameManager.instance.SCORE;//GameManager�� ���� ������ 

        EnemyStateDisp.text = "Enemy State: "+ Enemy.States.GetName(typeof(Enemy.States),enemyState.CurrentState);//���� ������
        EnemyHealthDisp.text = "Enemy Health: " + ((Enemy)enemyState).ELife;
    }
}
