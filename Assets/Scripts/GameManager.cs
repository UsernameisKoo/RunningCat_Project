using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform finishTransform; // Finish ������Ʈ�� ��ġ�� ����Ű�� Transform
    public GameObject stage1; // Stage1 ������Ʈ stage1�� ���� ��
    public GameManager gameManager;
    public int stageIndex;
    public GameObject Me;
    public GameObject[] Stages;
    public GameObject Home;
    public GameObject Gameover;
    public GameObject Gameclear;
    public GameObject[] Hearts;
    public int heart;
 

    public void HeartDown()
    {
        if (heart > 0)
        {
            heart--;
            Hearts[2 - heart].SetActive(false);

            // ���� -> Ȩ���� ���ư�
            if (heart == 0)
            {
                Invoke("GameOver", 2);
            }
        }
    }


    void GameOver()
    {
        Me.SetActive(false);
        Stages[stageIndex].SetActive(false); // ���� �������� ��Ȱ��ȭ
        Gameover.SetActive(true); // ���ӿ����� ���� ��Ŵ
    }

    public void StageClear()
    {
        Gameclear.SetActive(true);
    }
}