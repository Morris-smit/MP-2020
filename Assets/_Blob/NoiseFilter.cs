using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter 
{
    Noise noise = new Noise();
    NoiseSettings settings;

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float NoiseEvaluate(Vector3 p)
    {
        float noiseValue = (noise.Evaluate(p * settings.roughness + settings.centre) + 1) * 0.5f;
        return noiseValue * settings.strenth;
    }

}


