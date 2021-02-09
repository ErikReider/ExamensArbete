using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class EndScreen : MonoBehaviour
{
    void writeResults()
    {
        List<String> fpsData = FPSCounter.instance.averageFPSData.Select(s => s.ToString()).ToList();
        if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES3)
        {
            if (FPSCounter.instance.averageFPSData.Count == 0) return;
            String resultPath = Directory.GetCurrentDirectory() + "/Results/";
            String fileName = "result_" + SystemInfo.graphicsDeviceType + "_";
            // To prevent overriding the old results folder
            if (!Directory.Exists(resultPath)) Directory.CreateDirectory(resultPath);
            string[] files = Directory.GetFiles(resultPath, fileName + "*");
            // Gets the next index to use
            int index = 0;
            foreach (var file in files)
            {
                String number = file.Replace(resultPath + fileName, "").Replace(".txt", "");
                if (!number.All(char.IsDigit)) continue;
                int fileIndex = int.Parse(number);
                if (fileIndex > index) index = fileIndex;
            }
            File.WriteAllLines(resultPath + fileName + (index + 1).ToString() + ".txt", fpsData);
        }
        else
        {
            foreach (var data in fpsData)
            {
                print(data);
            }
        }
    }
    void Start()
    {
        writeResults();
        Application.Quit();
    }
}