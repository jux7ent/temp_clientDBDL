using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
/*
public class BotsControllerSystem : GameSystem, IIniting, IDisposing {
    [SerializeField] private float howOftenHandleBotsSec = 0.1f;
    [SerializeField] private float arrivingPositionEPSSquare = 4f;
    
    [SerializeField] [BoxGroup("Choose movement")] private float botsWalkRadius = 10f;
    
    private BotsData botsData;
    
    void IIniting.OnInit() {
        
        botsData = Bootstrap.GetSystem<SpawnBotsSystem>().BotsData;

        if (botsData.CatchersBots.Count > 0) {
            Debug.Log($"Start Catchers bots handling");
            StartCoroutine(HandleCatchersBotsCoroutine(botsData.CatchersBots));
        }

        if (botsData.EscapersBots.Count > 0) {
            Debug.Log($"Start Escapers bots handling");
            StartCoroutine(HandleEscapersBotsCoroutine(botsData.EscapersBots));
        }
    }

    void IDisposing.OnDispose() {
        StopAllCoroutines();
    }

    private IEnumerator HandleCatchersBotsCoroutine(List<BotCharacter> catchers) {
        while (true) {
            for (int i = 0; i < catchers.Count; ++i) {
                HandleBotCatcher(catchers[i]);
            }
            yield return GameExtensions.GetWaitForSeconds(howOftenHandleBotsSec);
        }
    }

    private IEnumerator HandleEscapersBotsCoroutine(List<BotCharacter> escapers) {
        while (true) {
            for (int i = 0; i < escapers.Count; ++i) {
                HandleBotEscaper(escapers[i]);
            }
            yield return GameExtensions.GetWaitForSeconds(howOftenHandleBotsSec);
        }
    }

    private void HandleBotCatcher(BotCharacter catcher) {
        
    }

    private void HandleBotEscaper(BotCharacter escaper) {
        if (NavMeshAgentArrivedPosition(escaper.NavMeshAgent)) {
            escaper.Walk(GetRandomPositionOnNavMesh(escaper.transform.position));
        }
    }

    private Vector3 GetRandomPositionOnNavMesh(Vector3 startPoint) {
        startPoint += Random.insideUnitSphere * botsWalkRadius;
        
        NavMeshHit hit;
        NavMesh.SamplePosition(startPoint, out hit, botsWalkRadius, 1);
        return hit.position;
    }

    private bool NavMeshAgentArrivedPosition(NavMeshAgent agent) {
        return (agent.transform.position - agent.destination).sqrMagnitude < arrivingPositionEPSSquare;
    }

}*/