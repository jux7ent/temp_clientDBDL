using DBDL.CommonDLL;
using Kuhpik;
using Server;
using UnityEngine;

public class LoadSceneDataSystem : GameSystem, IIniting {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera uiCamera;

    private bool systemInited = false;

    void IIniting.OnInit() {
        if (!systemInited) {
            game.SetSceneData(mainCamera, uiCamera);
            systemInited = true;
        }
        
        NetworkLogger.Instance.LogNetworkStateTMP(ENetworkClientState.Disconnected);
    }
}