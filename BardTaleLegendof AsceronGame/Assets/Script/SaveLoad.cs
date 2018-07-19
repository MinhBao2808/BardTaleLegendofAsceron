using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;

public class SaveLoad: MonoBehaviour {

	private static string GetSavePath (string name) {
		return Path.Combine(Application.persistentDataPath, name + ".save");
	}

	public static bool DoesSaveGameExist (string name) {
		return File.Exists(GetSavePath(name));
	}

	public static bool DeleteSave(string name) {
		try {
			File.Delete(GetSavePath(name));
		}
		catch (Exception) {
			return false;
		}
		return true;
	}

    public void SaveFile0() {
        SaveData save = CreateSaveObject();
		var path = Path.Combine(Application.streamingAssetsPath, @"Savefiles/");
        BinaryFormatter bf = new BinaryFormatter();
		FileStream saveFile = File.OpenWrite(path + "/Save0.save");
        bf.Serialize(saveFile, save);
        saveFile.Close();
    }

	public void SaveFile1() {
        SaveData save = CreateSaveObject();
        var path = Path.Combine(Application.streamingAssetsPath, @"Savefiles/");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.OpenWrite(path + "/Save1.save");
        bf.Serialize(saveFile, save);
        saveFile.Close();
    }

	public void SaveFile2() {
        SaveData save = CreateSaveObject();
        var path = Path.Combine(Application.streamingAssetsPath, @"Savefiles/");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.OpenWrite(path + "/Save2.save");
        bf.Serialize(saveFile, save);
        saveFile.Close();
    }

	public void SaveFile3() {
        SaveData save = CreateSaveObject();
        var path = Path.Combine(Application.streamingAssetsPath, @"Savefiles/");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.OpenWrite(path + "/Save3.save");
        bf.Serialize(saveFile, save);
        saveFile.Close();
    }

	public void SaveFile4() {
        SaveData save = CreateSaveObject();
        var path = Path.Combine(Application.streamingAssetsPath, @"Savefiles/");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.OpenWrite(path + "/Save4.save");
        bf.Serialize(saveFile, save);
        saveFile.Close();
    }

	public void CreateNewSave() {
		SaveData save = CreateSaveObject();
		var path = Path.Combine(Application.streamingAssetsPath, @"Savefiles/");
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fs = File.Create(path + "/" + "Save" + GameManager.instance.index + ".save");
		binaryFormatter.Serialize(fs, save);
		fs.Close();
		GameManager.instance.index += 1;
		PlayerPrefs.SetInt("c", GameManager.instance.index);
		for (int i = 0; i < GameManager.instance.index; i++) {
			SaveUIManager.instance.saveFileButton[i].SetActive(true);
			//SaveUIManager.instance.saveFileButton[i].GetComponentsInChildren<Text>()[0].text = "Save" + i;
		}
	}

	public static void LoadMultipleFiles(GameObject loadFileSaveButton,int[] arrayOfSave) {
		//Debug.Log("a");
		GameObject loadFileSavePanel = GameObject.Find("LoadFileSavePanel");
		var path = Path.Combine(Application.streamingAssetsPath, @"Savefiles/");
		DirectoryInfo directory = new DirectoryInfo(path);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		var files = directory.GetFiles().Where(o => o.Name.EndsWith(".save")).ToArray();
		var allFiles = new SaveData[files.Length];
		for (int i = 0; i < files.Length; i++) {
			arrayOfSave[i] = 1;
			FileStream fileStream = File.OpenRead(files[i].FullName);
			allFiles[i] = (SaveData)binaryFormatter.Deserialize(fileStream);
			InputManager.instance.loadGameButton[i].SetActive(true);
			InputManager.instance.loadGameButton[i].GetComponentsInChildren<Text>()[0].text = "Save " + i + " Player 1 level " + allFiles[i].levelPlayer1;
			fileStream.Close();
		}
	}

	public void LoadSave0() {
		var path = Path.Combine(Application.streamingAssetsPath, @"Savefiles/");
		BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Open(Application.streamingAssetsPath + @"/Savefiles/Save0.save", FileMode.Open);
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
					playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer1;
					playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer1;
                }
                else if (playerObject[i].gameObject.name == "Player2") {
                    playerObject[i].gameObject.GetComponent<PlayerStat>().attack = save.attackPlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().defense = save.defensePlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().health = save.healthPlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().magic = save.magicPlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().mana = save.manaPlayer2;
                    playerObject[i].gameObject.GetComponent<PlayerStat>().speed = save.speedPlayer2;
					playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer2;
					playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer2;
                }
            }
            DataManager.instance.playerPosition = new Vector3(save.xPlayerPosition, save.yPlayerPosition, save.zPlayerPosition);
			GameManager.instance.level = save.gameLevel;
            GameManager.instance.LoadMapScene();
    }

	public void LoadSave1() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Open(Application.streamingAssetsPath + @"/Savefiles/Save1.save", FileMode.Open);
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
                playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer1;
                playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer1;
            }
            else if (playerObject[i].gameObject.name == "Player2") {
                playerObject[i].gameObject.GetComponent<PlayerStat>().attack = save.attackPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().defense = save.defensePlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().health = save.healthPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().magic = save.magicPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().mana = save.manaPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().speed = save.speedPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer2;
            }
        }
        DataManager.instance.playerPosition = new Vector3(save.xPlayerPosition, save.yPlayerPosition, save.zPlayerPosition);
        GameManager.instance.level = save.gameLevel;
        GameManager.instance.LoadMapScene();
    }

	public void LoadSave2() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Open(Application.streamingAssetsPath + @"/Savefiles/Save2.save", FileMode.Open);
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
                playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer1;
                playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer1;
            }
            else if (playerObject[i].gameObject.name == "Player2") {
                playerObject[i].gameObject.GetComponent<PlayerStat>().attack = save.attackPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().defense = save.defensePlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().health = save.healthPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().magic = save.magicPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().mana = save.manaPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().speed = save.speedPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer2;
            }
        }
        DataManager.instance.playerPosition = new Vector3(save.xPlayerPosition, save.yPlayerPosition, save.zPlayerPosition);
        GameManager.instance.level = save.gameLevel;
        GameManager.instance.LoadMapScene();
    }

	public void LoadSave3() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Open(Application.streamingAssetsPath + @"/Savefiles/Save3.save", FileMode.Open);
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
                playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer1;
                playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer1;
            }
            else if (playerObject[i].gameObject.name == "Player2") {
                playerObject[i].gameObject.GetComponent<PlayerStat>().attack = save.attackPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().defense = save.defensePlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().health = save.healthPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().magic = save.magicPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().mana = save.manaPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().speed = save.speedPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer2;
            }
        }
        DataManager.instance.playerPosition = new Vector3(save.xPlayerPosition, save.yPlayerPosition, save.zPlayerPosition);
        GameManager.instance.level = save.gameLevel;
        GameManager.instance.LoadMapScene();
    }

	public void LoadSave4() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Open(Application.streamingAssetsPath + @"/Savefiles/Save4.save", FileMode.Open);
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
                playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer1;
                playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer1;
            }
            else if (playerObject[i].gameObject.name == "Player2") {
                playerObject[i].gameObject.GetComponent<PlayerStat>().attack = save.attackPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().defense = save.defensePlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().health = save.healthPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().magic = save.magicPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().mana = save.manaPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().speed = save.speedPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().currentExp = save.currentExpPlayer2;
                playerObject[i].gameObject.GetComponent<PlayerStat>().playerLv = save.levelPlayer2;
            }
        }
        DataManager.instance.playerPosition = new Vector3(save.xPlayerPosition, save.yPlayerPosition, save.zPlayerPosition);
        GameManager.instance.level = save.gameLevel;
        GameManager.instance.LoadMapScene();
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
		saveData.currentExpPlayer1 = DataManager.instance.currentExpPlayer1;
		saveData.currentExpPlayer2 = DataManager.instance.currentExpPlayer2;
		saveData.levelPlayer1 = DataManager.instance.levelPlayer1;
		saveData.levelPlayer2 = DataManager.instance.levelPlayer2;
		saveData.gameLevel = DataManager.instance.gameLevel;
        return saveData;
    }
}
