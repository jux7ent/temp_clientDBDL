using DBDL.CommonDLL;
using GameServer;
using Kuhpik;
using UnityEngine;

public class RecieveDataFromServerAndSpawnObjects : ExpectedGameSystem, IIniting, IDisposing {
    [SerializeField] private GameObject lobbyScreenContainer;
    
    private bool objectsSpawned = false;

    void IIniting.OnInit() {
        game.MainCamera.gameObject.SetActive(false);
        lobbyScreenContainer.SetActive(true);
        
        Printer.Print("OnInit get server scene init data");
        game.GameServer.MessagesHandler.OnGetServerSceneInitData += OnGetServerSceneInitData;
    }
    
    void IDisposing.OnDispose() {
        game.MainCamera.gameObject.SetActive(true);
        lobbyScreenContainer.SetActive(false);
    }

    private void OnGetServerSceneInitData(InitWorldStateSM initWorldState) {
        Printer.Print("OnGerServerInitData");
        Bootstrap.InvokeInMainThread(() => {
            SpawnObjectsFromServerData(initWorldState);
            Ready();
        });

        game.GameServer.MessagesHandler.OnGetServerSceneInitData -= OnGetServerSceneInitData;
    }

    private void SpawnObjectsFromServerData(InitWorldStateSM initWorldState) {
        if (objectsSpawned) return;

        SpawnPlayers(initWorldState);
        SpawnCages(initWorldState);
        SpawnCampFires(initWorldState);
        SpawnHatch(initWorldState);
        SpawnMedkits(initWorldState);
        FillGates(initWorldState);

        objectsSpawned = true;
    }

    private void SpawnPlayers(InitWorldStateSM initWorldState) {
        var players = initWorldState.Players;

        for (int i = 0; i < players.Count; ++i) {
            GameObject player = Instantiate(
                config.PrefabsData.Characters[players[i].CharacterType],
                players[i].LocationData.Position,
                players[i].LocationData.Rotation);

            BasePlayerInfo playerInfo;

            if (CharacterHelper.IsCatcher(players[i].CharacterType)) {
                playerInfo = player.AddComponent<BaseCatcherInfo>();
            } else {
                playerInfo = player.AddComponent<BaseEscaperInfo>();
            }

            playerInfo.PlayerId = players[i].Id;
            if (game.MyPlayerId == playerInfo.PlayerId) {
                game.IsCatcher = playerInfo.IsCatcher;
                playerInfo.MyControlled = true;
            }

            game.PlayersList.Add(playerInfo);
            game.PlayerIdInfoDict.Add(playerInfo.PlayerId, playerInfo);
        }
    }

    private void SpawnCages(InitWorldStateSM initWorldState) {
        var cages = initWorldState.Cages;
        for (int i = 0; i < cages.Count; ++i) {
            GameObject cageObj
                = Instantiate(
                    config.PrefabsData.Cage,
                    cages[i].LocationData.Position,
                    cages[i].LocationData.Rotation);

            Cage cage = cageObj.GetComponent<Cage>();
            cage.SetValues(cages[i].Id);
            game.StaticObjects.Add(cage.Id, cage);
        }
    }

    private void SpawnCampFires(InitWorldStateSM initWorldState) {
        var campFires = initWorldState.CampFires;
        for (int i = 0; i < campFires.Count; ++i) {
            CampFire campFire = Instantiate(
                config.PrefabsData.CampFire,
                campFires[i].LocationData.Position,
                campFires[i].LocationData.Rotation).GetComponent<CampFire>();

            campFire.SetValues(campFires[i].Id);
            game.StaticObjects.Add(campFire.Id, campFire);
        }
    }

    private void SpawnHatch(InitWorldStateSM initWorldState) {
        Hatch hatch = Instantiate(
            config.PrefabsData.Hatch,
            initWorldState.Hatch.LocationData.Position,
            initWorldState.Hatch.LocationData.Rotation).GetComponent<Hatch>();

        hatch.SetValues(initWorldState.Hatch.Id);
        game.StaticObjects.Add(hatch.Id, hatch);
    }

    private void SpawnMedkits(InitWorldStateSM initWorldStateSm) {
        for (int i = 0; i < initWorldStateSm.Medkits.Count; ++i) {
            Medkit medkit = Instantiate(
                config.PrefabsData.Medkit,
                initWorldStateSm.Medkits[i].LocationData.Position,
                initWorldStateSm.Medkits[i].LocationData.Rotation).GetComponent<Medkit>();

            medkit.SetValues(initWorldStateSm.Medkits[i].Id);
            game.StaticObjects.Add(medkit.Id, medkit);
        }
    }

    private void FillGates(InitWorldStateSM initWorldStateSm) {
        Gate[] gates = FindObjectsOfType<Gate>();

        for (int i = 0; i < gates.Length; ++i) {
            gates[i].SetValues(initWorldStateSm.Gates[i].Id);
            game.StaticObjects.Add(gates[i].Id, gates[i]);
        }
    }
}