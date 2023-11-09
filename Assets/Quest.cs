using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    public void StartQuest()//GameManager의 Quest 켬 함수
    {
        GameManager.instance.ToggleVarible(GameManager.VAR.Quest,true);//퀘스트 여부 토글
        Debug.Log("퀘스트 시작");
        
    }

    public void EndQuest()
    {
        GameManager.instance.ToggleVarible(GameManager.VAR.Quest, false);
        GameManager.instance.AddScore(100);//퀘스트 점수 추가
        Debug.Log("퀘스트 끝");
        
    }

    public void ToggleQuest()
    {
        if (GameManager.instance.Quest) EndQuest();
        else StartQuest();
    }
}
