using UnityEditor;
using UnityEngine;
using System;

// Unity 에디터에서 빌드 자동화를 위한 스크립트
public class BuildScript 
{
    // Jenkins에서 호출할 메서드
    // Android 빌드를 수행하며, 커맨드라인 인자로 버전과 빌드 코드를 받음
    public static void PerformAndroidBuild() 
    {
        // 커맨드라인 인자로 전달된 전체 문자열 배열 가져오기
        string[] args = Environment.GetCommandLineArgs();
        // -version 인자가 있으면 해당 값을, 없으면 기본값 "1.0.0" 사용
        string version = GetArg(args, "-version") ?? "1.0.0";
        // -buildCode 인자가 있으면 해당 값을, 없으면 기본값 "100" 사용
        string code = GetArg(args, "-buildCode") ?? "100";

        // PlayerSettings에 버전 정보와 빌드 코드 설정
        PlayerSettings.bundleVersion = version;
        PlayerSettings.Android.bundleVersionCode = int.Parse(code);

        // 실제로 빌드를 수행하는 부분
        // Main.unity 씬을 Android용 APK로 빌드하여 지정된 경로에 저장
        BuildPipeline.BuildPlayer(
            new[] { "Assets/Scenes/Main.unity" },
            "Builds/Android/MyGame.apk",
            BuildTarget.Android,
            BuildOptions.None
        );
    }

    // 커맨드라인 인자에서 특정 이름의 값을 찾아 반환하는 함수
    private static string GetArg(string[] args, string name) 
    {
        for (int i = 0; i < args.Length; i++) {
            if (args[i] == name && i + 1 < args.Length) return args[i + 1];
        }
        return null;
    }
}
