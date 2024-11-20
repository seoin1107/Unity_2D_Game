using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager
{
    // JSON���� �����͸� �����ϴ� �޼���
    public static void SaveToJson<T>(string filePath, T data)
    {
        // ��ü�� JSON ���ڿ��� ��ȯ
        string json = JsonUtility.ToJson(data, true);  // true�� �鿩���⸦ �����մϴ�.

        // ���Ͽ� JSON ���ڿ��� ����
        File.WriteAllText(filePath, json);
    }

    // JSON���� ����� �����͸� �ε��ϴ� �޼���
    public static T LoadFromJson<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            // ���Ͽ��� JSON ���ڿ��� �о����
            string json = File.ReadAllText(filePath);


            return JsonUtility.FromJson<T>(json);
        }

        // ������ ������ �⺻�� ��ȯ
        return default;
    }
}