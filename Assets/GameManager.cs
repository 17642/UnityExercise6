using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public enum VAR { Quest, SFX}

    public static GameManager instance;//GameManager 클래스가 가질 단 하나의 인스턴스
    private int score = 0;

    private bool quest = false;//퀘스트 토글

    public int SCORE{//score 프로퍼티
        get => score;
        set => score = value;
    }

    public bool Quest
    {
        get => quest;
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int score_)
    {
        score += score_;//점수 추가
        Debug.Log($"{score_} 점 추가");
    }

    public void ResetScore() {
        score = 0;//점수 리셋
        Debug.Log("점수 리셋");
    }

    public void ToggleVarible(VAR var,bool value)//변수 토글 함수.
    {
        if (var == VAR.Quest) quest = value;
    }
}
