using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kuhpik;
using Kuhpik.Pooling;
using UnityEngine;
using UnityEngine.UI;

/*
namespace WorldIcons {
    internal class WorldIcon {
        public Image Image { get; private set; }
        public Button Button { get; private set; }

        public GameObject GameObject { get; private set; }
        
        public RectTransform RectTransform { get; private set; }

        public WorldIcon(GameObject gameObject) {
            GameObject = gameObject;
            Button = gameObject.GetComponent<Button>();
            Image = gameObject.GetComponent<Image>();
            RectTransform = Image.rectTransform;
        }
    }
    
    public class WorldIconsController : GameSystem, IIniting, IUpdating {
    [SerializeField] private GameObject worldLabelImagePrefab;
    [SerializeField] private RectTransform worldIconsCanvas;
    [SerializeField] private Vector2 offset;

    private CampFire[] campFires;

    private Dictionary<Transform, WorldIcon> worldIconTransformSprite = new Dictionary<Transform, WorldIcon>();
    
    private List<KeyValuePair<Transform, WorldIcon>> worldIconTransformSpriteList =
        new List<KeyValuePair<Transform, WorldIcon>>();

    private Vector2 normalizedForScreenOffset;

    void IIniting.OnInit() {
        normalizedForScreenOffset =
            GameExtensions.UI.NormalizeResolutionVector2(offset, new Vector2(1920f, 1080f), worldIconsCanvas.sizeDelta);
        InitWorldIconObjAndSpriteDict();
    }

    void IUpdating.OnUpdate() {
        MoveWorldIconsOnPositions();
    }

    private void InitWorldIconObjAndSpriteDict() {
        worldIconTransformSprite.Clear();
        worldIconTransformSpriteList.Clear();
        
        InitCampFiresWorldIcons();
    }

    private void InitCampFiresWorldIcons() {
        campFires = 
            GameObject.FindGameObjectsWithTag(Constants.Tags.CampFire).Select(x => x.GetComponent<CampFire>()).ToArray();

        for (int i = 0; i < campFires.Length; ++i) {
            WorldIcon worldIcon = GetFromPoolAndInitWorldIconImage();
            
            worldIcon.Image.sprite =
                game.playerCharacterType == ECharacterType.Catcher ?
                    config.WorldIconsConfig.Data[EWorldIconType.CampFire].CatcherSprite :
                    config.WorldIconsConfig.Data[EWorldIconType.CampFire].HiderSprite;

            worldIcon.RectTransform.anchoredPosition =
                game.MainCamera.WorldToScreenPoint(campFires[i].transform.position);

            worldIconTransformSprite.Add(campFires[i].transform, worldIcon);
            worldIconTransformSpriteList.Add(new KeyValuePair<Transform, WorldIcon>(campFires[i].transform, worldIcon));
        }
    }

    private WorldIcon GetFromPoolAndInitWorldIconImage() {
        WorldIcon result = new WorldIcon(PoolingSystem.GetObject(worldLabelImagePrefab.gameObject));
        result.GameObject.transform.parent = worldIconsCanvas;
        return result;
    }

    private void MoveWorldIconsOnPositions() {
        for (int i = 0; i < worldIconTransformSpriteList.Count; ++i) {
            worldIconTransformSpriteList[i].Value.RectTransform.position =
                WorldToCanvasPosition(worldIconTransformSpriteList[i].Key.position) + normalizedForScreenOffset;
        }
    }

    private Vector2 WorldToCanvasPosition(Vector3 worldPoint) {
        return Vector2.Scale(game.MainCamera.WorldToViewportPoint(worldPoint), worldIconsCanvas.sizeDelta);
    }
}
}
*/