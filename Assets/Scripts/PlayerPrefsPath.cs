using UnityEngine;

public class PlayerPrefsPath : MonoBehaviour
{
    void Start()
    {
        string path = Application.persistentDataPath;
        Debug.Log("PlayerPrefs ���� ���: " + path);
    }
}
