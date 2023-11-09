using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public enum VAR { Quest, SFX}

    public static GameManager instance;//GameManager Ŭ������ ���� �� �ϳ��� �ν��Ͻ�
    private int score = 0;

    private bool quest = false;//����Ʈ ���

    public int SCORE{//score ������Ƽ
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
        score += score_;//���� �߰�
        Debug.Log($"{score_} �� �߰�");
    }

    public void ResetScore() {
        score = 0;//���� ����
        Debug.Log("���� ����");
    }

    public void ToggleVarible(VAR var,bool value)//���� ��� �Լ�.
    {
        if (var == VAR.Quest) quest = value;
    }
}
