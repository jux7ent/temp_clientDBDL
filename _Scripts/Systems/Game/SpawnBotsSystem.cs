using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
/*
public class BotsData {
    public List<BotCharacter> EscapersBots { get; private set; }
    public List<BotCharacter> CatchersBots { get; private set; }

    public BotsData() {
        EscapersBots = new List<BotCharacter>();
        CatchersBots = new List<BotCharacter>();
    }

    public void Clear() {
        EscapersBots.Clear();
        CatchersBots.Clear();
    }
}

public class SpawnBotsSystem : GameSystem, IIniting {

    public BotsData BotsData;

    private bool systemInited = false;

    void IIniting.OnInit() {
        if (!systemInited) {
            BotsData = new BotsData();
            
            systemInited = true;
        }
        
        SpawnBots();
    }

    private void SpawnBots() {
        BotsData.Clear();
        
        int howManyEscapersNeeded =
            config.EscapersCountOnMap - (game.playerCharacterType == ECharacterType.Escaper ? 1 : 0);
        int howManyCatchersNeeded =
            config.CatcherCountOnMap - (game.playerCharacterType == ECharacterType.Catcher ? 1 : 0);

        for (int i = 0; i < howManyCatchersNeeded; ++i) {
            BotsData.CatchersBots.Add(InitBot(ECharacterType.Catcher));
        }
        
        for (int i = 0; i < howManyEscapersNeeded; ++i) {
            BotsData.EscapersBots.Add(InitBot(ECharacterType.Escaper));
        }
    }

    private BotCharacter InitBot(ECharacterType characterType) {
        int characterSkinInt = 
            characterType == ECharacterType.Escaper ? 
                (int) GameExtensions.Random.GetRandomElementFromEnum<EEscapers>() :
                (int) GameExtensions.Random.GetRandomElementFromEnum<ECatchers>();
        
        GameObject randomPrefab = config.GetPrefabByTypes(characterType, characterSkinInt);
        BotCharacter initedBot = Instantiate(randomPrefab, Vector3.zero, Quaternion.identity).AddComponent<BotCharacter>();

   //     CharacterData characterData = config.GetCharacterDataByTypes(characterType, characterSkinInt);
     //   initedBot.SetMovementData(characterData.MovementData);
        
        Actions.CharacterSpawned?.Invoke(initedBot.transform);

        return initedBot;
    }
}*/