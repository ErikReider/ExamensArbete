using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TestType { time, fps };

public abstract class Test : MonoBehaviour
{
    public abstract int benchmarkTime { get; }
    public abstract TestType benchmarkType { get; }
    public abstract int minFPS { get; }
    public abstract void onUpdate();

    private string testName;

    List<double> listOfFrameRate = new List<double>();

    protected void Awake()
    {
        testName = SceneManager.GetActiveScene().name;
    }

    private void done()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    protected void Update()
    {
        int currentFPS = (int)(1f / Time.unscaledDeltaTime);
        if ((benchmarkType == TestType.fps && Time.timeSinceLevelLoad <= benchmarkTime) || (benchmarkType == TestType.time && currentFPS >= minFPS))
        {
            onUpdate();
            listOfFrameRate.Add(currentFPS);
        }
        else if (FPSCounter.instance.averageFPSData.Count == 0 || FPSCounter.instance.averageFPSData.Last<testData>().testName != testName)
        {
            int fps = benchmarkType == TestType.fps ? (int)listOfFrameRate.Average() : -1;
            float timeUntil = benchmarkType == TestType.time ? Time.timeSinceLevelLoad : -1;

            FPSCounter.instance.averageFPSData.Add(new testData(testName, benchmarkType, fps, timeUntil));
            done();
        }
    }
}