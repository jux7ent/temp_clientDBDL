using System.Collections.Generic;
using DBDL.CommonDLL;
using GameServer;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;

public class BotControllerSystem : GameSystem, IIniting, IUpdating {
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private NavMeshAgent botNavMeshAgentForCopy;

    private Dictionary<int, NavMeshAgent> botIdNavMeshDict = new Dictionary<int, NavMeshAgent>();

    private bool systemInited = false;
    private float lastPrepareDataTime = 0f;

    void IIniting.OnInit() {
        if (!systemInited) {
            game.GameServer.MessagesHandler.OnTakeBot += TakeBot;
            game.GameServer.MessagesHandler.OnReleaseBot += ReleaseBot;
            
            systemInited = true;
        }
    }

    void IUpdating.OnUpdate() {
        for (int i = 0; i < game.GameServer.PlayerUpdateCm.BotsUpdateData.BotsUpdateCms.Count; ++i) {
            Transform botTransform = botIdNavMeshDict[game.GameServer.PlayerUpdateCm.BotsUpdateData.BotsUpdateCms[i].BotId].transform;
            game.GameServer.PlayerUpdateCm.BotsUpdateData.BotsUpdateCms[i].LocationData.Update(
                (RWVector3)botTransform.position,
                (RWQuaternion)botTransform.rotation);
        }
        
        if (Time.time - lastPrepareDataTime > 0.1) {
            foreach (var pair in botIdNavMeshDict) {
                HandleBotMovement(pair.Value);
            }
            
            lastPrepareDataTime = Time.time;
        }
    }

    private void HandleBotMovement(NavMeshAgent agent) {
        if (Vector3.SqrMagnitude(agent.transform.position - agent.destination) < 3) {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 10f;
            randomDirection += agent.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
            Vector3 finalPosition = hit.position;

            agent.SetDestination(finalPosition);
        }
    }

    private void TakeBot(int botId) {
        if (!botIdNavMeshDict.ContainsKey(botId)) {
            botIdNavMeshDict[botId] = null;
            Debug.Log($"TakeBot: {botId}");
            Bootstrap.InvokeInMainThread(() => {
                NavMeshAgent agent = AddNavMeshAgent(botId);

                if (agent == null) {
                    Printer.Print($"ERROR AGENT == NULL. botId: {botId}");
                }
                
                botIdNavMeshDict[botId] = agent;
                
                if (game.PlayersList.Find(x => x.PlayerId == botId).IsCatcher) {
                    game.GameServer.PlayerUpdateCm.BotsUpdateData.BotsUpdateCms.Add(new BotUpdateData(botId));
                } else {
                    game.GameServer.PlayerUpdateCm.BotsUpdateData.BotsUpdateCms.Add(new BotUpdateData(botId));
                }
            });
        }
    }

    private void ReleaseBot(int botId) {
        if (botIdNavMeshDict.ContainsKey(botId)) {
            Debug.Log($"ReleaseBot: {botId}");
            
            Bootstrap.InvokeInMainThread(() => {
                RemoveNavMeshAgent(botId);

                botIdNavMeshDict.Remove(botId);

                int indexForRemove = game.GameServer.PlayerUpdateCm.BotsUpdateData.BotsUpdateCms.FindIndex(x => x.BotId == botId);
                game.GameServer.PlayerUpdateCm.BotsUpdateData.BotsUpdateCms.RemoveAt(indexForRemove);
            });
        }
    }

    private NavMeshAgent AddNavMeshAgent(int botId) {
        for (int i = 0; i < game.PlayersList.Count; ++i) {
            if (game.PlayersList[i].PlayerId == botId) {
                game.PlayersList[i].MyControlled = true;
                var navMeshAgent = game.PlayersList[i].gameObject.AddComponent<NavMeshAgent>();

                //navMeshAgent.GetCopyOf<NavMeshAgent>(Bootstrap.instance.config.BotsNavMeshAgentForCopy);

                return navMeshAgent;
            }
        }

        return null;
    }

    private void RemoveNavMeshAgent(int botId) {
        for (int i = 0; i < game.PlayersList.Count; ++i) {
            if (game.PlayersList[i].PlayerId == botId) {
                game.PlayersList[i].MyControlled = false;
                
                Destroy(game.PlayersList[i].gameObject.GetComponent<NavMeshAgent>());
                return;
            }
        }
    }
}