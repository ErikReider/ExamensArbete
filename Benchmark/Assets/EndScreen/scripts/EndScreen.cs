using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EndScreen : MonoBehaviour {
    void Start() {
        String resultPath = Directory.GetCurrentDirectory() + "/Results/";
        const String fileName = "result";
        // To prevent overriding the old results folder
        if (!Directory.Exists(resultPath)) Directory.CreateDirectory(resultPath);
        List<String> fpsData = FPSCounter.instance.averageFPSData.Select(s => s.ToString()).ToList();
        string[] files = Directory.GetFiles(resultPath, fileName + "*");
        // Gets the next index to use
        int index = 0;
        foreach (var file in files) {
            int fileIndex = int.Parse(file.Replace(resultPath + fileName, "").Replace(".txt", ""));
            if (fileIndex > index) index = fileIndex;
        }
        File.WriteAllLines(resultPath + fileName + (index + 1).ToString() + ".txt", fpsData);
    }
}