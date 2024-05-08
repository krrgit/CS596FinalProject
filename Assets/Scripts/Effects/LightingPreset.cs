using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Lighting/Lighting Preset", order = 0)]
public class LightingPreset : ScriptableObject
{
    public Gradient ambientColor;
    public Gradient directionalColor;
    public Gradient moonColor;
    public AnimationCurve sunPositionCurve;
    public AnimationCurve sunXRot;
    public AnimationCurve sunYRot;
}
