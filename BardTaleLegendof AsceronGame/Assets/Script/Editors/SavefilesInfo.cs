using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavefilesInfo : MonoBehaviour {

    private FileInfo fileInfo;
    public Text eventName;
    public Text location;
    public Text date;
    public Text saveType;
    public Text playTime;

    public void ParseFileInfo(FileInfo info)
    {
        fileInfo = info;
        OnUpdate();
    }

	void OnUpdate()
    {
        DateTime time = fileInfo.CreationTime;
        date.text = time.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        string[] nameSplit = fileInfo.Name.Split('_');
        if(nameSplit[0] == "Autosave")
        {
            saveType.text = "Autosave";
        }
        else
        {
            saveType.text = "Manual Save";
        }
        playTime.text = nameSplit[1].Substring(0, 4) + ":" 
            + nameSplit[1].Substring(4,2) + ":" 
            + nameSplit[1].Substring(6,2);
        Debug.Log(date.text);
    }

    public void OnClick()
    {
        Debug.Log(fileInfo.FullName);
        ScreenManager.Instance.TriggerLoadingFadeOut(1);
        SaveLoadManager.Instance.Load(fileInfo.FullName);
    }
}
