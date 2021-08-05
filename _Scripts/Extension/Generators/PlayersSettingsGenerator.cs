/*using System.IO;
using DBDL.CommonDLL;
using UnityEngine;

public class PlayersSettingsGenerator : MonoBehaviour {
    [SerializeField] private CharacterSettings[] characterSettingsArray;
    
    private const string Path = "Assets/_Scripts/ServerLibs/Resources/players_settings";

    public void Generate() {
        RWList<CharacterSettings> playerSettingsList = new RWList<CharacterSettings>();

        for (int i = 0; i < characterSettingsArray.Length; ++i) {
            playerSettingsList.Add(characterSettingsArray[i]);
        }

        BinaryStreamWriter writer = new BinaryStreamWriter(new MemoryStream());
        playerSettingsList.Write(writer);
        
        File.WriteAllBytes(Path, writer.GetBuffer());
    }
}*/
