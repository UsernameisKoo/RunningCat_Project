using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    public string currentStageKey; // ���� �������� (��: "stage1")
    public string nextStageKey; // ���� �������� (��: "stage2")

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{currentStageKey} Ŭ����! {nextStageKey} �������� ��� ����");

            // ProgressManager�� ���� ���� �������� ��� ����
            ProgressManager.Instance.UnlockStage(nextStageKey);
        }
    }
}
