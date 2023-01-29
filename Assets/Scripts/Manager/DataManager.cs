using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataManager : BaseManager
{
    
    private ILoader MappingClass(FileName file, string text)
    {
        ILoader loader = null;
        switch (file)
        {
            case FileName.ItemData:
                loader = JsonUtility.FromJson<ItemStat>(text);
                break;
        }
        if (loader != null)
            loader.MakeDictionary();

        return loader;
    }

    private void DataLoad(FileName file)
    {
        string fileName = Enum.GetName(typeof(FileName), file);
        TextAsset textAsset = Resources.Load<TextAsset>("DataSet/" + fileName);
        if (textAsset == null)
            return;
        ILoader loader = MappingClass(file, textAsset.text);
        if (loader != null)
            DataSet.Add(file, loader);
    }

    public void DataWrite(string fileName, UnityEngine.Object obj)
    {
        string text = JsonUtility.ToJson(obj);
        Debug.Log("Wrtie : " + text);
        File.WriteAllText( Application.persistentDataPath + "/" + fileName + ".json" ,text);
    }
    // https://geukggom.tistory.com/9  
    public T DataRead<T>(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            Debug.Log("Read : " + text);
            return JsonUtility.FromJson<T>(text);
        }
        return default;
    }


    public Dictionary<FileName, ILoader> DataSet = new Dictionary<FileName, ILoader>();


    public override void Init()
    {
        for (int i = 0; i < (int)FileName.MAX; i++)
            DataLoad((FileName)i);
    }

    public override void Clear()
    {

    }

}
