using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;

    [SerializeField]
    private bool selfUpdate = false;


    public ShapeSettings shapeSettings;

    public ColourSettings colourSettings;

    shapeGenerator shapeGenerator;

    public bool shapeSettingsFoldout;
    public bool colourSettingsFoldout;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    private void OnValidate()
    {
        generatePlanet();
    }

    private void FixedUpdate()
    {
        if (selfUpdate)
        {
            shapeGenerator.settings.noiseSettings.centre.x += 0.005f;
            generatePlanet();
        }
        
    }

    void Initialize()
    {
        if (shapeGenerator == null)
        {
            shapeGenerator = new shapeGenerator(shapeSettings);
        }

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }
    public void generatePlanet()
    {
        Initialize();
        GenerateMesh();
        generateColours();
    }

    public void OnShapeSettingUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
        
    }
    public void OnColourSettingUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            generateColours();
        }
    } 

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }
    void generateColours()
    {
        foreach (MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colourSettings.planetColour;
        }
    }
}