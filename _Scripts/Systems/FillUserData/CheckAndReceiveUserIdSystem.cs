using System.Collections;
using System.Collections.Generic;
using DBDL.CommonDLL;
using Kuhpik;
using UnityEngine;

public class CheckAndReceiveUserIdSystem : ExpectedGameSystem, IIniting {

    void IIniting.OnInit() {
        if (player.UserId == -1) {
            game.MainServer.GetNewUserId(OnGetNewUserId);
        } else {
            game.MainServer.GetInfoForUserId(player.UserId, OnGetUserInfoForId);
        }
    }

    private void OnGetNewUserId(int userId) {
        player.UserId = userId;
        Bootstrap.SavePlayerData();
        
        game.MainServer.GetInfoForUserId(userId, OnGetUserInfoForId);
    }

    private void OnGetUserInfoForId(UserEntity userEntity, bool canReconnect) {
        FillUserData(userEntity);
        
        Printer.Print($"CAN RECONNECT: {canReconnect}");

        if (canReconnect) {
            Bootstrap.ChangeGameState(EGamestate.Reconnection);
        } else {
            Bootstrap.ChangeGameState(EGamestate.Menu);
        }
    }

    private void FillUserData(UserEntity userEntity) {
        // TODO
        Printer.Print($"FillUserData\nNickname: {userEntity.Nickname}\nUserId: {userEntity.UserId}\nCoinsCount: {userEntity.CoinsCount}\nEmeraldsCount: {userEntity.EmeraldsCount}");
    
        game.UserData = userEntity;
    }
    
}