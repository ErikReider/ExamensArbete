using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColoredCone : Test
{

    public override int benchmarkTime => 0;

    public override TestType benchmarkType => TestType.time;

    public override int minFPS => 10;

    public GameObject myPrefab;
    public int itemsPerRow = 4;
    public Camera mainCamera;

    private int iteration = 0;

    // Adds a new row with two more cubes to create a cone
    void buildRow()
    {
        float moveBy = (float)360 / itemsPerRow;
        int spacing = itemsPerRow / 6;
        float rot = 0.0f;
        for (var i = 0; i < itemsPerRow; i++, rot += moveBy)
        {
            float pos = (rot * Mathf.PI) / 180;
            float x = Mathf.Sin(pos) * spacing;
            float y = Mathf.Cos(pos) * spacing;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, -rot));
            GameObject cube = Instantiate(myPrefab, new Vector3(x, y, iteration), rotation);
            cube.transform.SetParent(GetComponent<Transform>());
            Material material = new Material(cube.GetComponent<MeshRenderer>().material.shader);
            material.SetColor("_Color", UnityEngine.Random.ColorHSV());
            cube.GetComponent<MeshRenderer>().material = material;
        }
        iteration++;
        itemsPerRow += 2;
    }

    void Start()
    {
        // Center the camera
        mainCamera.transform.position = new Vector3(0, 0, -(float)itemsPerRow / 3);
    }

    public override void onUpdate()
    {
        buildRow();
        // Follow the new row and still stay in view of all cubes in the new row
        mainCamera.transform.position = new Vector3(0, 0, mainCamera.transform.position.z + Functions.sqrt(itemsPerRow) * 0.01f);
    }
}