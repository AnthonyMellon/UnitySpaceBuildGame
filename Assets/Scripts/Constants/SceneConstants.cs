using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneConstants
{
    public enum Scenes
    {
        BuildMenu,
    }

    public static Dictionary<Scenes, string> SceneNames = new Dictionary<Scenes, string>
    {
        { Scenes.BuildMenu, "BuildMenu" }
    };
}
