using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager
{
    // JSON으로 데이터를 저장하는 메서드
    public static void SaveToJson<T>(string filePath, T data)
    {
        // 객체를 JSON 문자열로 변환
        string json = JsonUtility.ToJson(data, true);  // true는 들여쓰기를 적용합니다.

        // 파일에 JSON 문자열을 저장
        File.WriteAllText(filePath, json);
    }

    // JSON으로 저장된 데이터를 로드하는 메서드
    public static T LoadFromJson<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            // 파일에서 JSON 문자열을 읽어들임
            string json = File.ReadAllText(filePath);


            return JsonUtility.FromJson<T>(json);
        }

        // 파일이 없으면 기본값 반환
        return default;
    }
}