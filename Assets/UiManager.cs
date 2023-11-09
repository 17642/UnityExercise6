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
    TMP_Text EnemyHealthDisp;//텍스트 필드
    //[SerializeField]
    //Enemy enemyState;//적 상태 확인
    [SerializeField]
    EnemyStateController controller;//상태 컨트롤러(상태 확인


    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Quest)//퀘스트 정보에 따라 버튼 이름 변경
        {
            QuestToggleButton.text = "Quest End";
            QuestDisplay.text = "Quest: True";//관련 정보 UI 출력
        }
        else
        {
            QuestToggleButton.text = "Quest Start";
            QuestDisplay.text = "Quest: False";
        }
        Scoreboard.text ="Score: " +GameManager.instance.SCORE;//GameManager의 점수 가져옴 

        EnemyStateDisp.text = "Enemy State: " + controller.State.GetType().ToString();//상태 가져옴
        EnemyHealthDisp.text = "Enemy Health: " + controller.enemy.ELife;
    }
}
