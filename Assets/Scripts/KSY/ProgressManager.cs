using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;
    private string filePath;
    public ProgressData progressData = new ProgressData();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        filePath = Path.Combine(Application.persistentDataPath, "progress.json");
        Debug.LogWarning("progressData.stages �ʱ�ȭ�մϴ�.");
        InitializeOrLoadProgress();

        //  ������ stages �ʱ�ȭ Ȯ�� 
        if (progressData.stages == null)
        {
            Debug.LogWarning("progressData.stages�� null�̹Ƿ� �⺻ ������ �ʱ�ȭ�մϴ�.");
            InitializeProgress();
        }
    }

    void InitializeOrLoadProgress()
    {
        if (File.Exists(filePath))
        {
            LoadProgress();
        }
        else
        {
            Debug.Log("�ʱ�ȭ");
            InitializeProgress();
            SaveProgress();
        }
    }

    public void SaveProgress()
    {
        if (progressData == null)
        {
            Debug.LogError("progressData�� null�̹Ƿ� ������ �� ����!");
            return;
        }

        string json = JsonUtility.ToJson(progressData, true);

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Json ��ȯ ����! progressData�� ����ְų� ����ȭ ���� �߻�");
            return;
        }

        File.WriteAllText(filePath, json);
        Debug.Log($"progress.json ���� �Ϸ�! ���� ���: {filePath}\n{json}");
    }


    public void LoadProgress()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("����� progress.json ������ ����, �⺻������ �ʱ�ȭ�մϴ�.");
            InitializeProgress();
            return;
        }

        string json = File.ReadAllText(filePath);
        progressData = JsonUtility.FromJson<ProgressData>(json);

        if (progressData.stages == null)
        {
            Debug.LogWarning("�ε�� �����Ϳ��� stages�� null�Դϴ�. �⺻ ������ �ʱ�ȭ�մϴ�.");
            InitializeProgress();
        }

        if (progressData == null || progressData.unlockedStages == null)
        {
            Debug.LogWarning("�ε�� �����Ͱ� null�̹Ƿ� �ʱ�ȭ�մϴ�.");
            InitializeProgress();
        }

    }

    public bool AllPotionsCollected()
    {
        foreach (var stage in progressData.stages)
        {
            if (stage.Key != "stage_4" && stage.Value.collected_potions < stage.Value.total_potions)
            {
                return false; // �ϳ��� �� ���� ���������� �ִٸ� false ��ȯ
            }
        }

        // ��� ������ ������� stage4(Ending) �ر�
        Debug.LogWarning("���� �������� �ر�!");
        UnlockStage("stage4");
        SaveProgress(); // ������� ����
        return true;
    }




    void InitializeProgress()
    {
        if (progressData.stages == null)
        {
            progressData.stages = new Dictionary<string, StageProgress>
            {
                { "stage_1", new StageProgress { total_potions = 8, collected_potions = 0 } },
                { "stage_2", new StageProgress { total_potions = 8, collected_potions = 0 } },
                { "stage_3", new StageProgress { total_potions = 8, collected_potions = 0 } }
            };
        }


        if (progressData.unlockedStages == null)
        {
            progressData.unlockedStages = new Dictionary<string, bool>();
        }
        if (!progressData.unlockedStages.ContainsKey("stage1")) progressData.unlockedStages["stage1"] = true;
        if (!progressData.unlockedStages.ContainsKey("stage2")) progressData.unlockedStages["stage2"] = false;
        if (!progressData.unlockedStages.ContainsKey("stage3")) progressData.unlockedStages["stage3"] = false;
        if (!progressData.unlockedStages.ContainsKey("stage4")) progressData.unlockedStages["stage4"] = false;

        Debug.LogWarning("�������� �ʱ�ȭ �Ϸ�");
        Debug.Log($"Progress ���� ���: {Application.persistentDataPath}");

        SaveProgress();  // ������ �ʱ�ȭ ������ ����
    }

    public void UnlockStage(string stageKey)
    {
        if (!progressData.unlockedStages.ContainsKey(stageKey))
        {
            string alternativeKey = stageKey.Replace("_", ""); // "stage_1" �� "stage1"
            if (progressData.unlockedStages.ContainsKey(alternativeKey))
            {
                stageKey = alternativeKey;
            }
            else
            {
                Debug.LogWarning($"UnlockStage: {stageKey}�� �������� ����");
                return;
            }
        }

        Debug.Log($"�������� ��� ����");
        progressData.unlockedStages[stageKey] = true;
        SaveProgress(); // ���� ������ �����Ͽ� ����
    }


    public bool IsStageUnlocked(string stageKey)
    {
        // Ű �� ���� (��: "stage_1"�� ����� �����Ϳ� ������ "stage1" üũ)
        if (!progressData.unlockedStages.ContainsKey(stageKey))
        {
            string alternativeKey = stageKey.Replace("_", ""); // "stage_1" �� "stage1"
            if (progressData.unlockedStages.ContainsKey(alternativeKey))
            {
                return progressData.unlockedStages[alternativeKey];
            }
            return false;
        }
        return progressData.unlockedStages[stageKey];
    }



}
