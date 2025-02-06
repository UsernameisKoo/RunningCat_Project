using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro�� ����ϴ� ���
using UnityEngine.UIElements;

public class ProgressBar : MonoBehaviour
{
    public string stageKey; // �������� Ű (��: "stage_1")
    public ProgressManager progressManager;
    public UnityEngine.UI.Slider progressBar;
    public TextMeshProUGUI progressText; // TextMeshPro UI ��� ��
    // public Text progressText; // �Ϲ� UnityEngine.UI.Text ��� ��

    void Start()
    {
        Debug.Log("ProgressBar Start() ȣ���");

        progressManager = FindFirstObjectByType<ProgressManager>();

        if (progressManager == null)
        {
            Debug.LogError("ProgressManager�� ã�� �� �����ϴ�.");
            return;
        }

        if (progressManager.progressData == null)
        {
            Debug.LogError("progressData�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        if (progressManager.progressData.stages == null)
        {
            Debug.LogError("stages Dictionary�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        if (progressBar == null)
        {
            Debug.LogError("ProgressBar�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        if (progressText == null)
        {
            Debug.LogError("ProgressText�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        Debug.Log("Start() ���� �Ϸ�");
    }



    void Update()
    {
        if (string.IsNullOrEmpty(stageKey))
        {
            Debug.LogError("stageKey�� �������� �ʾҽ��ϴ�.");
            return;
        }

        if (progressManager.progressData.stages.ContainsKey(stageKey))
        {
            StageProgress stage = progressManager.progressData.stages[stageKey];

            float progress = (float)stage.collected_potions / (float)stage.total_potions;
            progressBar.value = progress;
            progressText.text = $"{stage.collected_potions} / {stage.total_potions}";
        }
        else
        {
            Debug.LogWarning($"�������� Ű '{stageKey}' �� stages�� �������� �ʽ��ϴ�.");
        }
    }
}
