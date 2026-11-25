
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[InitializeOnLoad]
public static class RF3DProjectBootstrap
{
    static RF3DProjectBootstrap()
    {
        EditorApplication.delayCall += EnsureStarterScene;
    }

    static void EnsureStarterScene()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;
        const string folder = "Assets/Scenes";
        const string scenePath = folder + "/Main.unity";
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        if (File.Exists(scenePath)) return;

        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.name = "Pitch";
        ground.transform.position = new Vector3(0, -0.25f, 0);
        ground.transform.localScale = new Vector3(24, 0.5f, 40);
        ground.GetComponent<Renderer>().sharedMaterial = MakeMaterial(new Color(0.08f, 0.34f, 0.12f));

        var ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.name = "Football";
        ball.transform.position = new Vector3(0, 0.35f, 0);
        ball.transform.localScale = Vector3.one * 0.7f;
        var rb = ball.AddComponent<Rigidbody>();
        rb.mass = 0.43f;
        ball.AddComponent<FootballBall>();

        var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player_Real_Madrid_Prototype";
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Renderer>().sharedMaterial = MakeMaterial(new Color(0.85f, 0.85f, 0.85f));
        player.AddComponent<CharacterController>();
        player.AddComponent<FootballPlayerController>();

        var opponent = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        opponent.name = "Player_Barcelona_Placeholder";
        opponent.transform.position = new Vector3(0, 1, 7);
        opponent.GetComponent<Renderer>().sharedMaterial = MakeMaterial(new Color(0.55f, 0.04f, 0.08f));

        var camObj = new GameObject("Main Camera");
        camObj.tag = "MainCamera";
        var cam = camObj.AddComponent<Camera>();
        camObj.transform.position = new Vector3(0, 13, -17);
        camObj.transform.rotation = Quaternion.Euler(28, 0, 0);
        cam.fieldOfView = 60;

        var lightObj = new GameObject("Directional Light");
        var light = lightObj.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.2f;
        lightObj.transform.rotation = Quaternion.Euler(50, -30, 0);

        var match = new GameObject("Match Manager");
        match.AddComponent<MatchManager>();
        match.AddComponent<GameModeManager>();

        var db = new GameObject("Roster Database");
        db.AddComponent<RosterDatabase>();

        EditorSceneManager.SaveScene(scene, scenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(scenePath, true) };
        AssetDatabase.SaveAssets();
        Debug.Log("REAL FOOTBALL 3D: escena inicial creada. Los modelos son placeholders.");
    }

    static Material MakeMaterial(Color color)
    {
        var shader = Shader.Find("Standard");
        var mat = new Material(shader);
        mat.color = color;
        return mat;
    }
}
#endif
