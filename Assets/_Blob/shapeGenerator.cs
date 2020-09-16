using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shapeGenerator
{
    public ShapeSettings settings;

    NoiseFilter noiseFilter;

    public shapeGenerator(ShapeSettings settings)
    {
        this.settings = settings;
        noiseFilter = new NoiseFilter(settings.noiseSettings);
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float elevation = noiseFilter.NoiseEvaluate(pointOnUnitSphere);
        return pointOnUnitSphere * settings.planetRadius * (1 + elevation);
    }
}
