using System;
using System.Collections.Generic;
using DBDL.CommonDLL;
using GameServer;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LobbySlot {
    [field: SerializeField] public TextMeshProUGUI Nickname { get; private set; }
    [SerializeField] private Transform slotParentTransform;

    private GameObject currSlotGameObj;
    private ECharacter currCharacter;
    private bool inited = false;

    public void UpdateSlot(GameConfig config, LobbyPlayerInfo lobbyPlayerInfo) {
        if (!inited || lobbyPlayerInfo.CharacterType != currCharacter) {
            if (lobbyPlayerInfo.CharacterType != currCharacter) {
                GameObject.Destroy(currSlotGameObj);
            }
            
            GameObject characterPrefab = config.GetPrefabByType(lobbyPlayerInfo.CharacterType);

            currSlotGameObj = GameObject.Instantiate(characterPrefab, slotParentTransform);
            currSlotGameObj.GetComponent<Animator>().SetTrigger("Idle");
            currCharacter = lobbyPlayerInfo.CharacterType;

            inited = true;
        }

        currCharacter = lobbyPlayerInfo.CharacterType;
        
        Nickname.text = lobbyPlayerInfo.Name;
        Nickname.color = lobbyPlayerInfo.ReadyState ? Color.green : Color.white;
    }

    public void ClearSlot() {
        if (inited) {
            GameObject.Destroy(currSlotGameObj);
        }
        
        inited = false;

        Nickname.text = "";
    }
}

public class LobbyHandlerSystem : GameSystem, IIniting {
    [SerializeField] private Button readyBtn;
    
    [SerializeField] private LobbySlot[] lobbyEscaperContainers;
    [SerializeField] private LobbySlot lobbyCatcherContainer;
    
    [SerializeField] private Dropdown charactersDropdown;

    private List<LobbyPlayerInfo> cachedEscapersList = new List<LobbyPlayerInfo>();
    private int usedLobbySlotIndex = -1;

    private LobbyInfoSM lobbyData;

    private bool systemInited = false;

    void IIniting.OnInit() {

        if (!systemInited) {
            InitDropdown();
            
            readyBtn.onClick.AddListener(() => {
                game.SetReadyToPlay(true);
            });

            game.GameServer.MessagesHandler.OnGetServerLobbyData += OnReceiveLobbyData;
            
            systemInited = true;
        }
    }

    private void InitDropdown() {
        List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
        List<string> characterNames = new List<string>();
        
        int escapersStartIndex = CharacterHelper.GetEscapersStartIndex();
        
        int startIndex = game.IsCatcher ? 0 : escapersStartIndex;
        int endIndex = game.IsCatcher ? escapersStartIndex : CharacterHelper.GetCharactersCount();
        for (int i = startIndex; i < endIndex; ++i) {
            characterNames.Add(Enum.GetName(typeof(ECharacter), i));
        }

        for (int i = 0; i < characterNames.Count; ++i) {
            dropdownOptions.Add(new Dropdown.OptionData());
            dropdownOptions[dropdownOptions.Count - 1].text = characterNames[i];
        }
        
        game.LobbyInfoCm.CharacterType = (ECharacter) startIndex;

        charactersDropdown.options = dropdownOptions;
        charactersDropdown.onValueChanged.AddListener(delegate(int index) {
            game.LobbyInfoCm.CharacterType = (ECharacter) (index + startIndex);
        });
    }

    private void OnReceiveLobbyData(LobbyInfoSM lobbyDataFromServer) {
        lobbyData = lobbyDataFromServer;
        game.LastLobbyInfoFromServer = lobbyDataFromServer;
        UpdateLobbyData();
    }

    private void UpdateLobbyData() {
        Bootstrap.InvokeInMainThread(() => {
            if (lobbyData.Players.Count > 5) {
                Debug.Log($"Error. Players in lobby: {lobbyData.Players.Count}");
                return;
            }

            LobbyPlayerInfo catcher = null;
            cachedEscapersList.Clear();
            for (int i = 0; i < lobbyData.Players.Count; ++i) {
                if (CharacterHelper.IsCatcher(lobbyData.Players[i].CharacterType)) {
                    catcher = lobbyData.Players[i];
                } else {
                    cachedEscapersList.Add(lobbyData.Players[i]); 
                }
            }

            for (int i = 0; i < lobbyEscaperContainers.Length; ++i) {
                if (i > cachedEscapersList.Count - 1) {
                    lobbyEscaperContainers[i].ClearSlot();
                } else {
                    lobbyEscaperContainers[i].UpdateSlot(config, cachedEscapersList[i]);
                }
            }

            if (catcher == null) {
                lobbyCatcherContainer.ClearSlot();
            } else {
                lobbyCatcherContainer.UpdateSlot(config, catcher);
            }
        });
    }
}