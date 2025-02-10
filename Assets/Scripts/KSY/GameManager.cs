using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform finishTransform;
    public GameObject stage1;
    public int stageIndex;
    public GameObject Me;
    public GameObject[] Stages;
    public GameObject Home;
    public GameObject Gameover;
    public GameObject Gameclear;
    public GameObject[] Hearts;
    public int heart;
    public int score = 0;
    public TMP_Text scoreText;
    private string currentStageKey = "stage_1"; // �⺻��



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;  // �� ���� �� �ڵ����� ȣ��
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("�� �ε��: " + scene.name);
        FindScoreText(); // ���� �ε�� �� scoreText �ٽ� ã��
        ResetScore();    // �������� ���� �� ���ھ� �ʱ�ȭ
    }

    public string GetCurrentStageKey()
    {
        return currentStageKey;
    }

    public void SetCurrentStageKey(string newStageKey)
    {
        currentStageKey = newStageKey;
        Debug.Log($"���� �������� ����: {currentStageKey}");
    }

    void FindScoreText()
    {
        GameObject textObject = GameObject.Find("scoreText/scoreText");
        if (textObject != null)
        {
            Debug.Log("textObject Name: " + textObject.name);
            scoreText = textObject.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogWarning("ScoreText�� ã�� �� �����ϴ�! ���� scoreText ������Ʈ�� �ִ��� Ȯ���ϼ���.");
        }
    }

    public void ResetScore()
    {
        score = 0;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        Debug.Log("�������� ����� - Score �ʱ�ȭ �Ϸ�");
    }


    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        Debug.Log("Score: " + score);
    }

    public void HeartDown()
    {
        if (heart > 0 && Me.layer == 10)
        {
            heart--;
            Hearts[heart].SetActive(false);

            if (heart == 0)
            {
                Invoke("GameOver", 0.5f);
            }
        }
    }

  

    void GameOver()
    {
        Me.SetActive(false);
        Stages[stageIndex].SetActive(false);
        Gameover.SetActive(true);
        
    }


    public void StageClear()
    {
        Gameclear.SetActive(true);
    }
}
