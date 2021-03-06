using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColoredCubes : Test
{
    public override int benchmarkTime => 30;

    public override TestType benchmarkType => TestType.fps;

    public override int minFPS => 0;

    public GameObject myPrefab;
    public int cubeRows = 0;
    public Camera mainCamera;

    private List<GameObject> cubes = new List<GameObject>();
    GameObject parent;
    void createBlocks()
    {
        // Instantiate new cubes
        for (var x = 0; x < cubeRows; x++)
        {
            for (var z = 0; z < cubeRows; z++)
            {
                GameObject cube = Instantiate(myPrefab, new Vector3(x, Random.Range(-0.1f, 0.1f), z), Quaternion.identity);
                cube.transform.SetParent(parent.transform);
                Material material = new Material(cube.GetComponent<MeshRenderer>().material.shader);
                material.SetColor("_Color", Random.ColorHSV());
                cube.GetComponent<MeshRenderer>().material = material;
                cubes.Add(cube);
            }
        }
    }

    void randomizeCubes()
    {
        foreach (var cube in cubes)
        {
            var pos = cube.transform.position;
            cube.transform.position = new Vector3(pos.x, Random.Range(-0.1f, 0.1f), pos.z);
            cube.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
        }
    }

    void Start()
    {
        parent = GetComponent<Transform>().gameObject;
        // Center tha camera
        mainCamera.transform.position = new Vector3(cubeRows * 0.5f - 0.5f, cubeRows + 0.5f, cubeRows * 0.5f - 0.5f);
        createBlocks();
    }

    public override void onUpdate()
    {
        randomizeCubes();
    }
}