using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct testData
{
    public string testName;
    public int avgFPS;
    public float timeUntilMinFPS;
    public TestType testType;

    public testData(string testName, TestType testType, int avgFPS, float timeUntilMinFPS)
    {
        this.testName = testName;
        this.testType = testType;
        this.avgFPS = avgFPS;
        this.timeUntilMinFPS = timeUntilMinFPS;

    }

    public override string ToString() => testName + ": " + (testType == TestType.time ? timeUntilMinFPS : avgFPS).ToString();
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

    public Text _fpsText;
    public int fps = 0;
    [SerializeField] private float _hudRefreshRate = 0.25f;

    private float _timer;

    public List<testData> averageFPSData = new List<testData>();

    public void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = fps.ToString();
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
}