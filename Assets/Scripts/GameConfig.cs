using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfig
{
    public static float InfectionRate = 50;
    public static float ReInfectionTime = 1;
    public static float MaxInfectionTime = 20;
    public static float CureRate = 20;
    public static float CureDelayTime = 1;
    public static int PeopleLayerMask = 1 << 9;
}
