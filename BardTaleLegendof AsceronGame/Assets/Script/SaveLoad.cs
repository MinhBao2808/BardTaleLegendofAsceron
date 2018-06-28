using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad: MonoBehaviour {
    public void SaveGame() {
        SaveData save = CreateSaveObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(saveFile, save);
        saveFile.Close();
        Debug.Log(DataManager.instance.playerPosition);
        Debug.Log("Save created"+saveFile.Name);
    }

    public void LoadGame() {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream saveFile = File.Open(Application.persistentDataPath + "/gamesave.save",FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(saveFile);
            saveFile.Close();
            GameObject[] playerObject = GameObject.FindGameObjectsWithTag("PlayerUnit");
            for (var i = 0; i < playerObject.Length; i++) {
                if (playerObject[i].gameObject.name == "Player1") {
                    playerObject[i].gameObject.GetComponent<PlayerStat>().attack = save.attackPlayer1;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().defense = save.defensePlayer1;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().health = save.healthPlayer1;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().magic = save.magicPlayer1;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().mana = save.manaPlayer1;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().speed = save.speedPlayer1;
                }
                else if (playerObject[i].gameObject.name == "Player2") {
                    playerObject[i].gameObject.GetComponent<PlayerStat>().attack = save.attackPlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().defense = save.defensePlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().health = save.healthPlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().magic = save.magicPlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().mana = save.manaPlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().speed = save.speedPlayer2;
                }
            }
            DataManager.instance.playerPosition = new Vector3(save.xPlayerPosition, save.yPlayerPosition, save.zPlayerPosition);
			GameManager.instance.level = save.gameLevel;
            GameManager.instance.LoadMapScene();
            Debug.Log(DataManager.instance.playerPosition);
        }
    }

    private SaveData CreateSaveObject() {
        SaveData saveData = new SaveData();
        DataManager.instance.playerPosition = PlayerMovement.instance.ReturnPlayerPosition();
        saveData.xPlayerPosition = DataManager.instance.playerPosition.x;
        saveData.yPlayerPosition = DataManager.instance.playerPosition.y;
        saveData.zPlayerPosition = DataManager.instance.playerPosition.z;
        saveData.attackPlayer1 = DataManager.instance.attackPlayer1;
        saveData.attackPlayer2 = DataManager.instance.attackPlayer2;
        saveData.defensePlayer1 = DataManager.instance.defensePlayer1;
        saveData.defensePlayer2 = DataManager.instance.defensePlayer2;
        saveData.healthPlayer1 = DataManager.instance.healthPlayer1;
        saveData.healthPlayer2 = DataManager.instance.healthPlayer2;
        saveData.magicPlayer1 = DataManager.instance.magicPlayer1;
        saveData.magicPlayer2 = DataManager.instance.magicPlayer2;
        saveData.manaPlayer1 = DataManager.instance.manaPlayer1;
        saveData.manaPlayer2 = DataManager.instance.manaPlayer2;
        saveData.speedPlayer1 = DataManager.instance.speedPlayer1;
        saveData.speedPlayer2 = DataManager.instance.speedPlayer2;
		saveData.gameLevel = DataManager.instance.gameLevel;
        return saveData;
    }
}
