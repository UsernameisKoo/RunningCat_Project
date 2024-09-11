using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform finishTransform; // Finish 오브젝트의 위치를 가리키는 Transform
    public GameObject stage1; // Stage1 오브젝트 stage1만 구현 중
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
        if (heart > 0 && Me.layer == 10)
        {
            heart--;
            Hearts[heart].SetActive(false);

            // 죽음 -> 홈으로 돌아감
            if (heart == 0)
            {
                Invoke("GameOver", 0.5f);
            }
        }
    }

    void GameOver()
    {
        Me.SetActive(false);
        Stages[stageIndex].SetActive(false); // 현재 스테이지 비활성화
        Gameover.SetActive(true); // 게임오버를 실행 시킴
    }

    public void StageClear()
    {
        Gameclear.SetActive(true);
    }
}