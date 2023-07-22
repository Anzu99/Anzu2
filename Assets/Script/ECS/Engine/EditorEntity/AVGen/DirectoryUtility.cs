
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DirectoryUtility
{
    public static string[] GetChildFolder(string directory)
    {
        string[] res = { };

        try
        {
            if (Directory.Exists(directory))
            {
                res = Directory.GetDirectories(directory);
                if (res.Length == 0) Debug.LogError("ANZU ERROR: " + directory + " contains 0 folder");
            }
            else
            {
                Debug.LogError("ANZU ERROR: " + directory + " not found");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("ANZU ERROR: " + e.Message);
        }

        return res;
    }
    public static string[] GetChildFolderName(string directory)
    {
        List<string> res = new List<string>();
        try
        {
            if (Directory.Exists(directory))
            {
                string[] dirTmp = Directory.GetDirectories(directory);
                if (dirTmp.Length == 0) Debug.LogError("ANZU ERROR: " + directory + " contains 0 folder");
                foreach (var item in dirTmp)
                {
                    string name = Path.GetFileName(item);
                    res.Add(name);
                }
            }
            else
            {
                Debug.LogError("ANZU ERROR: " + directory + " not found");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("ANZU ERROR: " + e.Message);
        }

        return res.ToArray();
    }
    public static string[] GetAllFileInFolder(string directory, string extension = "cs")
    {
        List<string> res = new List<string>();
        try
        {
            if (Directory.Exists(directory))
            {
                string[] nameTmp = Directory.GetFiles(directory, $"*.{extension}", SearchOption.AllDirectories);
                if (nameTmp.Length == 0) Debug.LogError("ANZU ERROR: " + directory + " contains 0 file");
                foreach (var item in nameTmp)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item);
                    res.Add(fileName);
                }
            }
            else
            {
                Debug.LogError("ANZU ERROR: " + directory + " not found");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("ANZU ERROR: " + e.Message);
        }

        return res.ToArray();
    }
    public static string[] GetAllFileInFolderAndChild(string directory, string extension = "cs")
    {
        List<string> res = new List<string>();
        try
        {
            if (Directory.Exists(directory))
            {
                FindCSFilesRecursive(directory);
                void FindCSFilesRecursive(string folderPath)
                {
                    string[] files = Directory.GetFiles(folderPath, $"*.{extension}");

                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        res.Add(fileName);
                    }

                    string[] subDirectories = Directory.GetDirectories(folderPath);

                    foreach (string subDir in subDirectories)
                    {
                        FindCSFilesRecursive(subDir);
                    }
                }
                string[] files = Directory.GetFiles(directory, "*.cs");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("ANZU ERROR: " + e.Message);
        }
        foreach (var item in res)
        {
            UnityEngine.Debug.Log(item);
        }
        return res.ToArray();
    }
}