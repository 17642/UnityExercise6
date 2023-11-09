using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    public void StartQuest()//GameManager�� Quest �� �Լ�
    {
        GameManager.instance.ToggleVarible(GameManager.VAR.Quest,true);//����Ʈ ���� ���
        Debug.Log("����Ʈ ����");
        
    }

    public void EndQuest()
    {
        GameManager.instance.ToggleVarible(GameManager.VAR.Quest, false);
        GameManager.instance.AddScore(100);//����Ʈ ���� �߰�
        Debug.Log("����Ʈ ��");
        
    }

    public void ToggleQuest()
    {
        if (GameManager.instance.Quest) EndQuest();
        else StartQuest();
    }
}
