using DBDL.CommonDLL;
using GameServer;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class CharactersSettingsUpdator : GameSystem, IIniting {
    [SerializeField] private bool debug = false;
    [SerializeField] [ShowIf("debug")] private float debugMovementSpeed;
    [SerializeField] [ShowIf("debug")] private float debugAngularSpeed;
    [SerializeField] [ShowIf("debug")] private float debugFovRadius;
    [SerializeField] [ShowIf("debug")] private float debugFovAngle;

    private bool systemInited = false;

    private ControlCharacter controlCharacter;
    private FieldOfView fieldOfView;

    void IIniting.OnInit() {
        InitData();
        
        if (!systemInited) {
            game.GameServer.MessagesHandler.OnGetServerSceneUpdateData += OnGetServerUpdateData;
            
            systemInited = true;
        }
    }

    private void OnGetServerUpdateData(ServerUpdateStateSM serverWorldUpdate) {
        CharacterSettings characterSettings = serverWorldUpdate.Players[game.MyPlayerId].CharacterSettings;

        if (debug) {
            characterSettings.MovementSpeed = debugMovementSpeed;
            characterSettings.AngularSpeed = debugAngularSpeed;
            characterSettings.FovRadius = debugFovRadius;
            characterSettings.FovAngle = debugFovAngle;
        }
        
        
        controlCharacter.UpdateSpeed(characterSettings);
        fieldOfView.fovData.Update(characterSettings);
    }

    private void InitData() {
        controlCharacter = FindObjectOfType<ControlCharacter>();
        fieldOfView = FindObjectOfType<FieldOfView>();
    }
}