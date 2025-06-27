using UnityEditor;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformAndroidBuild()
    {
        string[] args = Environment.GetCommandLineArgs();
        string version = GetArg(args, "-version") ?? "1.0.0";
        string code = GetArg(args, "-buildCode") ?? "100";

        PlayerSettings.bundleVersion = version;
        PlayerSettings.Android.bundleVersionCode = int.Parse(code);

        string outputPath = "Builds/Android/MyGame.apk";

        try
        {
            Debug.Log("=== Unity 빌드 시작 ===");

            BuildReport report = BuildPipeline.BuildPlayer(
                new[] { "Assets/Scenes/Main.unity" }, 
                outputPath,
                BuildTarget.Android,
                BuildOptions.None
            );

            if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                Debug.LogError("❌ 빌드 실패: " + report.summary.result);
                throw new Exception("Build failed: " + report.summary.result);
            }

            Debug.Log("✅ 빌드 성공! " + outputPath);
        }
        catch (Exception e)
        {
            Debug.LogError("🔥 빌드 중 예외 발생: " + e);
        }
        finally
        {
            Debug.Log("=== Unity 빌드 종료 ===");
        }
    }

    private static string GetArg(string[] args, string name)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && i + 1 < args.Length)
                return args[i + 1];
        }
        return null;
    }
}
