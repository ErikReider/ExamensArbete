using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Test : MonoBehaviour {
    public abstract int benchmarkTime { get; }
    public abstract void onUpdate();

    private string testName;

    List<double> listOfFrameRate = new List<double>();

    protected void Awake() {
        testName = SceneManager.GetActiveScene().name;
    }

    private void done() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    protected void Update() {
        if (Time.timeSinceLevelLoad <= benchmarkTime) {
            onUpdate();
            listOfFrameRate.Add((int) (1f / Time.unscaledDeltaTime));
        } else {
            if (FPSCounter.instance.averageFPSData.Count == 0 || FPSCounter.instance.averageFPSData.Last<testData>().testName != testName) {
                FPSCounter.instance.averageFPSData.Add(new testData(testName, (int) listOfFrameRate.Average()));
            }
            done();
        }
    }
}