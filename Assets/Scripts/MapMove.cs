using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public GameObject backgroundPrefab; // �� �����Ű��
    public int poolSize = 3;
    public float speed = 0.5f; //
    public float backgroundWidth;

    private List<GameObject> backgroundPool;
    private float resetPosition;
    private float startPosition;

    // Update is called once per frame

    void Start()
    {
        if (backgroundPrefab == null)
        {
            Debug.LogError("Background prefab is not assigned!");
            return;
        }

        backgroundPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(backgroundPrefab);
            obj.SetActive(true);  // �ʱ⿡ Ȱ��ȭ ���·� ����
            obj.transform.position = new Vector3(i * backgroundWidth, 0, 0);  // �ʱ� ��ġ ����
            backgroundPool.Add(obj);
        }

        if (backgroundPool.Count > 0)
        {
            SpriteRenderer spriteRenderer = backgroundPool[0].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                backgroundWidth = spriteRenderer.bounds.size.x;
            }
            else
            {
                Debug.LogError("SpriteRenderer component not found on background prefab!");
                return;
            }
        }

        resetPosition = -backgroundWidth;
        startPosition = backgroundWidth * (poolSize - 1);

        // ��� ��� ������ ����  �� ȭ��, �� ��° ȭ�� x�κ� �������� ����

        // �ʱ� ��� ����
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bg = backgroundPool[i];
            bg.SetActive(true);
            bg.transform.position = new Vector3(i * backgroundWidth, 0, 0);
        }
    }

    void Update() // 
    {
        foreach(GameObject bg in backgroundPool) 
        {
            if (bg.activeSelf) // ��� ���� ������Ʈ�� ���� Ȱ��ȭ ���� 
            {
                bg.transform.Translate(Vector3.left * speed * Time.deltaTime); // ���� update�ڵ�
                if (bg.transform.position.x <= resetPosition) // �� ��� x��ǥ�� 
                {
                    bg.SetActive(false);
                    ActivateBackground(startPosition);
                }
            }
        }
    }

    void ActivateBackground(float xposition) 
    {
        foreach (GameObject bg in backgroundPool)
        {
            if (!bg.activeSelf)
            {
                bg.SetActive(true);
                bg.transform.position =new Vector3(xposition, 0, 0);
                return;
            }
        }
    }
}
