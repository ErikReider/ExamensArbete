using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct testData
{
    public string testName;
    public int avgFPS;
    public testData(string testName, int avgFPS)
    {
        this.testName = testName;
        this.avgFPS = avgFPS;
    }
}

public class FPSCounter : MonoBehaviour
{
    public static FPSCounter instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public List<string> TestOrder;

    public Text _fpsText;
    [SerializeField] private float _hudRefreshRate = 0.25f;

    private float _timer;

    public List<testData> averageFPSData = new List<testData>();

    public void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = fps.ToString();
            _timer = Time.unscaledTime + _hudRefreshRate;
        }

        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("CubesBenchmark");
        }
        else if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("Test2");
        }
    }
}
