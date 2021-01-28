using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionsManager : MonoBehaviour {

    [Header("Prefab")]
    public GameObject elementPrefab = null;

    [Header("共用圖")]
    public Sprite 未知兔子 = null;
    public Sprite 未知管家 = null;
    public Sprite 兔子Tab_亮 = null;
    public Sprite 兔子Tab_暗 = null;
    public Sprite 管家Tab_亮 = null;
    public Sprite 管家Tab_暗 = null;

    private Image RabbitTab = null;
    private Image KeeperTab = null;
    private GameObject RabbitPanel = null;
    private GameObject KeeperPanel = null;
    private RabbitPopup rabbitPopup = null;
    private KeeperPopup keeperPopup = null;

    private CollectionTab tab = CollectionTab.Rabbit;

    // Start is called before the first frame update
    void Start() {
        RabbitTab = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>();
        KeeperTab = RabbitTab.transform.parent.GetChild(1).GetComponent<Image>();
        RabbitPanel = transform.GetChild(0).GetChild(0).gameObject;
        KeeperPanel = transform.GetChild(0).GetChild(1).gameObject;
        rabbitPopup = transform.GetChild(0).GetChild(4).GetComponent<RabbitPopup>();
        keeperPopup = transform.GetChild(0).GetChild(5).GetComponent<KeeperPopup>();

        SetPanel();
        UpdateUI();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void SetPanel() {
        //Set RabbitPanel
        Transform Content = RabbitPanel.transform.GetChild(0).GetChild(0);
        for (int i = 0; i < RabbitSystem.GetRabbitListCount(); i++) {
            GameObject g = Instantiate(elementPrefab, Content);
            Transform img = g.transform.GetChild(0);
            bool got = RabbitSystem.GetRabbitGotById(i);
            img.GetComponent<RectTransform>().sizeDelta = got ? new Vector2(80, 80) : new Vector2(未知兔子.texture.width, 未知兔子.texture.height);
            img.GetComponent<Image>().sprite = got ? RabbitSystem.GetRabbitSpriteById(i) : 未知兔子;
            if (!got)
                continue;
            int tmpIndex = i;
            g.GetComponent<Button>().onClick.AddListener(delegate () { ClickRabbit(tmpIndex); });
        }

        //Set KeeperPanel
        Content = KeeperPanel.transform.GetChild(0).GetChild(0);
        for(int i = 0; i < HouseKeeperSystem.GetKeeperCount(); i++) {
            GameObject g = Instantiate(elementPrefab, Content);
            Transform img = g.transform.GetChild(0);
            bool got = HouseKeeperSystem.GetGotByIndex(i);
            img.GetComponent<RectTransform>().sizeDelta = got ? new Vector2(80, 80) : new Vector2(未知管家.texture.width, 未知管家.texture.height);
            img.GetComponent<Image>().sprite = got ? HouseKeeperSystem.GetSpriteByIndex(i) : 未知管家;
            if (!got)
                continue;
            int tmpIndex = i;
            g.GetComponent<Button>().onClick.AddListener(delegate () { ClickKeeper(tmpIndex); });
        }
    }

    private void UpdateUI() {
        RabbitTab.sprite = tab == CollectionTab.Rabbit ? 兔子Tab_亮 : 兔子Tab_暗;
        KeeperTab.sprite = tab == CollectionTab.Keeper ? 管家Tab_亮 : 管家Tab_暗;
        RabbitPanel.SetActive(tab == CollectionTab.Rabbit);
        KeeperPanel.SetActive(tab == CollectionTab.Keeper);
    }

    private void ClickRabbit(int index) {
        Debug.Log("Click Rabbit index: " + index);
        rabbitPopup.Open(index);
    }

    private void ClickKeeper(int index) {
        Debug.Log("Click Keeper index: " + index);
        keeperPopup.Open(index);
    }

    public void OnClickRabbitTab() {
        if (tab == CollectionTab.Rabbit)
            return;
        tab = CollectionTab.Rabbit;
        UpdateUI();
    }

    public void OnClickKeeperTab() {
        if (tab == CollectionTab.Keeper)
            return;
        tab = CollectionTab.Keeper;
        UpdateUI();
    }

    public void OnClickBackBtn() {

    }
}

public enum CollectionTab {
    Rabbit,
    Keeper
}
