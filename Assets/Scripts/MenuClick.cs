using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickHomeButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnClickStage1()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickStage2()
    {
        SceneManager.LoadScene(2);
    }
    public void OnClickStage3()
    {
        SceneManager.LoadScene(3);
    }
}
