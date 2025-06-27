using System.IO;
using UnityEditor;
using UnityEngine;
using System;

public class BuildScript
{
    public static void PerformAndroidBuild()
    {
        // 0) 출력 폴더 체크
        var projectroot = Directory.GetParent(Application.dataPath).FullName;
        var outDir = Path.Combine(projectroot, "Builds", "Android");
        if (!Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // 1) 커맨드라인 인자 읽기
        string[] args = Environment.GetCommandLineArgs();
        string version = GetArg(args, "-appVersion") ?? "1.0.0";  // 또는 "-version"
        string code = GetArg(args, "-buildCode") ?? "100";

        // 2) 번들 버전 설정
        PlayerSettings.bundleVersion = version;
        PlayerSettings.Android.bundleVersionCode = int.Parse(code);

        // 3) APK 경로
        string outputPath = Path.Combine(outDir, "MyGame.apk");

        // 4) 빌드 실행
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
            if (args[i] == name) return args[i + 1];
        return null;
    }
}
