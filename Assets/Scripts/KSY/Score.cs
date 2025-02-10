using UnityEngine;

public class Score : MonoBehaviour
{
    public enum PotionType { Normal, Big, Rainbow } // ���� ����
    public PotionType potionType = PotionType.Normal;

    public int normalPotionValue = 10;
    public int bigPotionValue = 20;
    public int rainbowPotionValue = 50;

    public int normalPotionProgress = 1;
    public int bigPotionProgress = 2;
    public int rainbowPotionProgress = 5;

    private ProgressManager progressManager;

    void Start()
    {
        progressManager = FindAnyObjectByType<ProgressManager>();

        if (progressManager == null)
        {
            Debug.LogError("ProgressManager�� ������ ã�� �� �����ϴ�.");
            return;
        }

        string currentStageKey = GameManager.Instance.GetCurrentStageKey();

        if (progressManager.progressData.stages.ContainsKey(currentStageKey))
        {
            progressManager.progressData.stages[currentStageKey].newCollected = 0;
            progressManager.SaveProgress();
            Debug.Log($"[{currentStageKey}] newCollected�� 0���� �ʱ�ȭ");
        }
        else
        {
            Debug.LogWarning($"[{currentStageKey}] �������� �����Ͱ� ��� �ʱ�ȭ�� �� �����ϴ�.");
        }
    }

 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && progressManager != null)
        {
            // ���� ���� ���� �������� �������� (GameManager���� ���� �������� ���������� ����)
            string currentStageKey = GameManager.Instance.GetCurrentStageKey();

            if (!progressManager.progressData.stages.ContainsKey(currentStageKey))
            {
                Debug.LogWarning($"'{currentStageKey}' �������� �����Ͱ� ��� �ʱ�ȭ�մϴ�.");
                progressManager.progressData.stages[currentStageKey] = new StageProgress { total_potions = 8, collected_potions = 0, newCollected = 0, };
                progressManager.SaveProgress();
            }

            StageProgress stage = progressManager.progressData.stages[currentStageKey];

            int potionProgress = potionType switch
            {
                PotionType.Normal => normalPotionProgress,
                PotionType.Big => bigPotionProgress,
                PotionType.Rainbow => rainbowPotionProgress,
                _ => 0
            };


            stage.newCollected = Mathf.Min(stage.newCollected + potionProgress, stage.total_potions);

            Debug.Log($"[{currentStageKey}] ���� ���� ���൵: {stage.collected_potions}, �߰��� ��: {potionProgress}, ��� ��: {stage.newCollected}");

            if (stage.newCollected > stage.collected_potions)
            {
                stage.collected_potions = stage.newCollected;
                progressManager.progressData.stages[currentStageKey] = stage;
                progressManager.SaveProgress();

                Debug.Log($"[{currentStageKey}] ���� ���൵ ������Ʈ �Ϸ�! ���� ���൵: {stage.collected_potions}/{stage.total_potions}");
            }

            // ���� �߰�
            int scoreValue = potionType switch
            {
                PotionType.Normal => normalPotionValue,
                PotionType.Big => bigPotionValue,
                PotionType.Rainbow => rainbowPotionValue,
                _ => 0
            };

            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject); // ���� ���� ����
        }
    }
}
