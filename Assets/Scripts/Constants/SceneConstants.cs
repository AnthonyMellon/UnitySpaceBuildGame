using System.Collections.Generic;

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
