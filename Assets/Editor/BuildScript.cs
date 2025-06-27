using UnityEditor;
using UnityEngine;
using System;

public class BuildScript
{
    public static void PerformAndroidBuild()
    {
        string[] args = Environment.GetCommandLineArgs();
        string version = GetArg(args, "-appVersion") ?? "1.0.0";
        string code = GetArg(args, "-buildCode") ?? "100";

        PlayerSettings.bundleVersion = version;
        PlayerSettings.Android.bundleVersionCode = int.Parse(code);

        string outputPath = "Builds/Android/MyGame.apk";
        var report = BuildPipeline.BuildPlayer(
            new[] { "Assets/Scenes/Main.unity" },
            outputPath,
            BuildTarget.Android,
            BuildOptions.None
        );

        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            throw new Exception("Build failed: " + report.summary.result);
        Debug.Log("✅ 빌드 성공: " + outputPath);
    }

    private static string GetArg(string[] args, string name)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == name) return args[i + 1];
        }
        return null;
    }
}
