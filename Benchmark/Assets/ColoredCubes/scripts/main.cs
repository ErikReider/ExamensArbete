using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main : Test
{
    public override int benchmarkTime { get { return 5; } }

    public GameObject myPrefab;
    public int cubeRows = 0;
    public int timesToRepeat = 1;
    public Camera mainCamera;

    private List<GameObject> cubes = new List<GameObject>();

    void createBlocks()
    {
        for (var i = 0; i < cubeRows; i++)
        {
            for (var j = 0; j < cubeRows; j++)
            {
                GameObject cube = Instantiate(myPrefab, new Vector3(i, Random.Range(-0.1f, 0.1f), j), Quaternion.identity);
                cube.transform.SetParent(GetComponent<Transform>());
                cube.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
                cubes.Add(cube);
            }
        }
    }

    void randomizeCubes()
    {
        for (var i = 0; i < timesToRepeat; i++)
        {
            foreach (var cube in cubes)
            {
                Vector3 pos = cube.transform.position;
                cube.transform.position = new Vector3(pos.x, Random.Range(-0.1f, 0.1f), pos.z);
                cube.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
            }
        }
    }

    void Start()
    {
        mainCamera.transform.position = new Vector3(cubeRows * 0.5f, cubeRows, cubeRows * 0.5f);
        createBlocks();
    }

    public override void onUpdate()
    {
        randomizeCubes();
    }
}
