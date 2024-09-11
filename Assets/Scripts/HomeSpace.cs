using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSpace : MonoBehaviour
{
    public GameObject Me;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("flicker_false", 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(4);
        }
        
    }

    void flicker_false()
    {
        Me.SetActive(false);
        Invoke("flicker_true", 0.2f);
    }
    void flicker_true()
    {
        Me.SetActive(true);
        Invoke("flicker_false", 0.45f);
    }
}
