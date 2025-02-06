using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

// �� ��° retry�� stage�ֺ�� ���� -> stage Ȱ��ȭ �ϸ鼭 �÷��̾�� �� ��ǥ �ʱ�ȭ�ؾ���
// ��ǥ �̵��� ���� �״�η� �ٽ� �����ؼ� �� ���̴� ����.
public class ClickButton : MonoBehaviour
{
    public GameManager gameManager;
    public void OnClickRetryButton()
    {
        gameManager.heart = 3;
        gameManager.Hearts[0].SetActive(true);
        gameManager.Hearts[1].SetActive(true);
        gameManager.Hearts[2].SetActive(true);
        gameManager.Me.SetActive(true);
        gameManager.Gameover.SetActive(false);
        gameManager.Stages[gameManager.stageIndex].SetActive(true);

        Time.timeScale = 1;
    }
    public void OnClickHomeButton()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
        gameManager.score = 0;  // ���� ���� �� score �ʱ�ȭ
        if (gameManager.scoreText != null)
            gameManager.scoreText.text = "Score: " + gameManager.score;
    }

    
}
