using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour {

    private static SaveLoadManager _instance;
    private string fileExtension = ".sav";
    private string[] saveType = { "CheckPoint_", "ManualSave_" };
    private int saveNumber = 0;

    private int resWidth = 228;
    private int resHeight = 128;
    public static SaveLoadManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void Save()
    {
        string saveTime = System.DateTime.Now.ToString("yyyyMMdd_hhmmss_");

        int changedSaveNumber = saveNumber + 1000;
        string hexValue = changedSaveNumber.ToString("X");
        int hexNumber = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        string hexNumToString = hexNumber.ToString("X") + "_";
        saveNumber++;

        string fileName = saveType[0] + saveTime + hexNumToString;
        string path = Application.persistentDataPath + "/" + fileName;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path + fileExtension, FileMode.Create);
        PlayerData data = new PlayerData();
        bf.Serialize(stream, data);
        stream.Close();
        SaveScreenshot(path);
    }

    void SaveScreenshot(string path)
    {
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        Camera.main.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = path + ".png";
        System.IO.File.WriteAllBytes(filename, bytes);
    }
}

[Serializable]
public class PlayerData
{
    public int difficulty;
    public Character[] characters;
    public int currency;

    public PlayerData()
    {

    }
}

[Serializable]
public class Character
{
    public int level;
    public int exp;
    public int maxHP;
    public int hp;
    public int maxMP;
    public int mp;
    public int strength;
    public int dexterity;
    public int intelligence;
    public int vitality;
    public int endurance;
    public int wisdom;
    public int fireRes;
    public int lightningRes;
    public int iceRes;
    public int cosmosRes;
    public int chaosRes;
    public int[] skillIDs;
    public Equipment[] equipments;
}

[Serializable]
public class Equipment
{
    public Equipment()
    {

    }
}