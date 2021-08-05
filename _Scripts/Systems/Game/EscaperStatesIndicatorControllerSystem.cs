using System.Collections;
using System.Collections.Generic;
using GameServer;
using Kuhpik;
using UnityEngine;

public class EscaperStatesIndicatorControllerSystem : GameSystem, IIniting, IDisposing {
    [SerializeField] private EscaperStateIndicator[] escaperStateIndicators;

    private Dictionary<int, EscaperStateIndicator> escaperIdEscaperStateIndicator =
        new Dictionary<int, EscaperStateIndicator>();

    private bool systemInitiated = false;
    
    private int availableEscaperStateIndicatorIndex;

    void IIniting.OnInit() {
        escaperIdEscaperStateIndicator.Clear();
        availableEscaperStateIndicatorIndex = 0;
        
        InitIndicators();
        game.GameServer.MessagesHandler.OnGetServerSceneUpdateData += UpdateIcons;

        if (!systemInitiated) {
            systemInitiated = true;
        }
    }

    void IDisposing.OnDispose() {
        game.GameServer.MessagesHandler.OnGetServerSceneUpdateData -= UpdateIcons;
    }

    private void InitIndicators() {
        for (int i = 0; i < game.LastLobbyInfoFromServer.Players.Count; ++i) {
            if (!CharacterHelper.IsCatcher(game.LastLobbyInfoFromServer.Players[i].CharacterType)) {
                escaperIdEscaperStateIndicator.Add(game.LastLobbyInfoFromServer.Players[i].PlayerId,
                    escaperStateIndicators[availableEscaperStateIndicatorIndex]);
                escaperStateIndicators[availableEscaperStateIndicatorIndex].SetNickname(game.LastLobbyInfoFromServer.Players[i].Name);
                ++availableEscaperStateIndicatorIndex;
            }
        }
    }

    private void UpdateIcons(ServerUpdateStateSM serverUpdateState) {
        foreach (var escaperIdIndicator in escaperIdEscaperStateIndicator) {
            RWBaseEscaperUpdateState escaperUpdateState =
                serverUpdateState.Players[escaperIdIndicator.Key] as RWBaseEscaperUpdateState;
            EEscaperState state = escaperUpdateState.State;
            
            escaperIdIndicator.Value.UpdateState(state);
            escaperIdIndicator.Value.SetDieProgress(escaperUpdateState.DieProgress);
        }
    }
}