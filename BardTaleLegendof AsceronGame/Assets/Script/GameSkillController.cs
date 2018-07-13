using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameSkillController : MonoBehaviour {
    private string path;
    private string jsonString;
    private GameSkillArray skillArray;

    void Start() {
        path = Application.streamingAssetsPath + "/gameskill.json";
        jsonString = File.ReadAllText(path);
        skillArray  = JsonUtility.FromJson<GameSkillArray>(jsonString);
        foreach (var gameskill in skillArray.gameSkill) {
            //Debug.Log(gameskill.Name);
        }
    }


}

