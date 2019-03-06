using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class LevelDef : ScriptableObject
{
    public int Number;

    [TextArea]
    public string DescriptionText;

    [TextArea(10, 10)]
    public string LevelLayout;

    public override string ToString()
    {
        return Number.ToString();
    }
}
