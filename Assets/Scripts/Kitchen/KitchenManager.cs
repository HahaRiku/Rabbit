using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchEvent_handler;
using UnityEngine.UI;

public class KitchenManager : MonoBehaviour {
    public GameObject 食譜Prefab;
    public Sprite QuestionMark;
    public Sprite 焦;

    private enum Type {
        外觀,
        口感,
        香氣,
        經典
    }

    public struct RabbitChoice {
        public string name;
        public Sprite image;
        public Method method;
        public int successRate;
        public bool got;
        public GameObject instance;
    }

    private Text 外觀lv, 口感lv, 香氣lv, 經典lv;
    private Button 外觀Button, 口感Button, 香氣Button, 經典Button;
    private Animator IncreasePanelAni;
    private GameObject CloseDetect;
    private Text IPType;
    private Text IPCurrentLevel;
    private Text IPNextLevel;
    private Text IPPrice;
    private Button IPPriceButton;
    private GameObject ChoiceContent;
    private RectTransform CCRT;

    private Type currentType;

    private List<RabbitChoice> rabbitChoices = new List<RabbitChoice>();
    private int chosenRabbitIndex = 0;

    private GameObject ResultPanel;
    private GameObject Refrigerator, Oven;
    private Text RabbitName;
    private Image RabbitImage;
    private bool success = false;

    // Start is called before the first frame update
    void Start() {
        外觀Button = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        外觀lv = 外觀Button.transform.GetChild(1).GetComponent<Text>();
        口感Button = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        口感lv = 口感Button.transform.GetChild(1).GetComponent<Text>();
        香氣Button = transform.GetChild(0).GetChild(2).GetComponent<Button>();
        香氣lv = 香氣Button.transform.GetChild(1).GetComponent<Text>();
        經典Button = transform.GetChild(0).GetChild(3).GetComponent<Button>();
        經典lv = 經典Button.transform.GetChild(1).GetComponent<Text>();

        外觀lv.text = "Lv." + SystemVariables.waiguanValue.ToString();
        口感lv.text = "Lv." + SystemVariables.koganValue.ToString();
        香氣lv.text = "Lv." + SystemVariables.xianchiValue.ToString();
        經典lv.text = "Lv." + SystemVariables.classicValue.ToString();

        IncreasePanelAni = transform.GetChild(0).GetChild(7).GetComponent<Animator>();
        IPType = IncreasePanelAni.transform.GetChild(0).GetComponent<Text>();
        IPCurrentLevel = IncreasePanelAni.transform.GetChild(2).GetChild(1).GetComponent<Text>();
        IPNextLevel = IncreasePanelAni.transform.GetChild(2).GetChild(2).GetComponent<Text>();
        IPPrice = IncreasePanelAni.transform.GetChild(3).GetChild(1).GetComponent<Text>();
        IPPriceButton = IncreasePanelAni.transform.GetChild(3).GetComponent<Button>();

        CloseDetect = transform.GetChild(0).GetChild(6).gameObject;
        CloseDetect.SetActive(false);
        CheckTypeButton();

        ChoiceContent = transform.GetChild(0).GetChild(5).GetChild(0).gameObject;

        foreach(OneRabbit r in RabbitSystem.data.rabbitsList) {
            RabbitChoice rc;
            GameObject g = Instantiate(食譜Prefab, ChoiceContent.transform);
            g.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate() { SelectRecipe(g); });
            if(r.got) {
                g.transform.GetChild(1).gameObject.SetActive(false);
                g.transform.GetChild(0).gameObject.SetActive(true);
                g.transform.GetChild(0).GetComponent<Image>().sprite = r.image;
                rc.successRate = 100;
            }
            else {
                rc.successRate = GetSuccessRate(r.demands);
                if(rc.successRate != 0) {
                    g.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = rc.successRate.ToString();
                    g.transform.GetChild(0).gameObject.SetActive(false);
                    g.transform.GetChild(1).gameObject.SetActive(true);
                }
                else {
                    g.transform.GetChild(0).gameObject.SetActive(true);
                    g.transform.GetChild(1).gameObject.SetActive(false);
                    g.transform.GetChild(0).GetComponent<Image>().sprite = QuestionMark;
                }
            }
            g.transform.GetChild(2).gameObject.SetActive(false);
            rc.instance = g;
            rc.name = r.name;
            rc.method = r.method;
            rc.image = r.image;
            rc.got = r.got;
            rabbitChoices.Add(rc);
        }
        CCRT = ChoiceContent.GetComponent<RectTransform>();
        CCRT.offsetMin = new Vector2(CCRT.offsetMin.x, ((170 * Mathf.Ceil(rabbitChoices.Count / 3.0f) + 27) - 270) * -1);

        ResultPanel = transform.GetChild(1).gameObject;
        ResultPanel.SetActive(false);
        Refrigerator = transform.GetChild(1).GetChild(0).gameObject;
        Oven = transform.GetChild(1).GetChild(1).gameObject;
        RabbitName = transform.GetChild(1).GetChild(3).GetComponent<Text>();
        RabbitImage = transform.GetChild(1).GetChild(4).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Test() {
        SystemVariables.CarrotCount+=500;
    }

    private void IncreaseWaiguan() {
        SystemVariables.waiguanValue++;
        外觀lv.text = "Lv." + SystemVariables.waiguanValue.ToString();
        CheckTypeButton();
        CheckChoices();
    }

    private void IncreaseKogan() {
        SystemVariables.koganValue++;
        口感lv.text = "Lv." + SystemVariables.koganValue.ToString();
        CheckTypeButton();
        CheckChoices();
    }

    private void IncreaseXianchi() {
        SystemVariables.xianchiValue++;
        香氣lv.text = "Lv." + SystemVariables.xianchiValue.ToString();
        CheckTypeButton();
        CheckChoices();
    }

    private void IncreaseClassic() {
        SystemVariables.classicValue++;
        經典lv.text = "Lv." + SystemVariables.classicValue.ToString();
        CheckTypeButton();
        CheckChoices();
    }

    private void CheckTypeButton() {
        if (SystemVariables.waiguanValue == 5) {
            外觀Button.interactable = false;
        }
        if (SystemVariables.koganValue == 5) {
            口感Button.interactable = false;
        }
        if (SystemVariables.xianchiValue == 5) {
            香氣Button.interactable = false;
        }
        if (SystemVariables.classicValue == 5) {
            經典Button.interactable = false;
        }
    }

    public void Open外觀() {
        int value = SystemVariables.waiguanValue;
        IPType.text = "外觀";
        SetIPInfo(value);

        IncreasePanelAni.SetTrigger("Open外觀");
        currentType = Type.外觀;
        CloseDetect.SetActive(true);
    }

    public void Open口感() {
        int value = SystemVariables.koganValue;
        IPType.text = "口感";
        SetIPInfo(value);

        IncreasePanelAni.SetTrigger("Open口感");
        currentType = Type.口感;
        CloseDetect.SetActive(true);
    }

    public void Open香氣() {
        int value = SystemVariables.xianchiValue;
        IPType.text = "香氣";
        SetIPInfo(value);

        IncreasePanelAni.SetTrigger("Open香氣");
        currentType = Type.香氣;
        CloseDetect.SetActive(true);
    }

    public void Open經典() {
        int value = SystemVariables.classicValue;
        IPType.text = "經典";
        SetIPInfo(value);

        IncreasePanelAni.SetTrigger("Open經典");
        currentType = Type.經典;
        CloseDetect.SetActive(true);
    }

    private void SetIPInfo(int value) {
        IPCurrentLevel.text = value.ToString();
        IPNextLevel.text = (value + 1).ToString();
        int price;
        if (value == 1) {
            price = 500;
            IPPrice.text = "500";
        }
        else if (value == 2) {
            price = 1500;
            IPPrice.text = "1500";
        }
        else if (value == 3) {
            price = 2500;
            IPPrice.text = "2500";
        }
        else {
            price = 4000;
            IPPrice.text = "4000";
        }
        if (SystemVariables.CarrotCount < price) {
            IPPriceButton.interactable = false;
        }
        else {
            IPPriceButton.interactable = true;
        }
    }

    public void CloseFourType() {
        if(currentType == Type.外觀) {
            IncreasePanelAni.SetTrigger("Close外觀");
        }
        else if(currentType == Type.口感) {
            IncreasePanelAni.SetTrigger("Close口感");
        }
        else if(currentType == Type.香氣) {
            IncreasePanelAni.SetTrigger("Close香氣");
        }
        else {
            IncreasePanelAni.SetTrigger("Close經典");
        }
        CloseDetect.SetActive(false);
    }

    public void IncreaseTypeValue() {
        if(currentType == Type.外觀) {
            UseCarrot(SystemVariables.waiguanValue);
            IncreaseWaiguan();
        }
        else if(currentType == Type.口感) {
            UseCarrot(SystemVariables.koganValue);
            IncreaseKogan();
        }
        else if(currentType == Type.香氣) {
            UseCarrot(SystemVariables.xianchiValue);
            IncreaseXianchi();
        }
        else {
            UseCarrot(SystemVariables.classicValue);
            IncreaseClassic();
        }
        CloseFourType();
    }

    private void UseCarrot(int level) {
        if(level == 1) {
            SystemVariables.CarrotCount -= 500;
        }
        else if(level == 2) {
            SystemVariables.CarrotCount -= 1500;
        }
        else if(level == 3) {
            SystemVariables.CarrotCount -= 2500;
        }
        else {
            SystemVariables.CarrotCount -= 4000;
        }
    }

    private int GetSuccessRate(Demand[] demands) {
        for(int i = 0; i < demands.Length; i++) {
            if(SystemVariables.waiguanValue >= demands[i].外觀Demand && SystemVariables.koganValue >= demands[i].口感Demand &&
                SystemVariables.xianchiValue >= demands[i].香氣Demand && SystemVariables.classicValue >= demands[i].經典Demand) {
                return demands[i].percentage;
            }
        }
        return 0;
    }

    private void CheckChoices() {
        for (int i = 0; i < rabbitChoices.Count; i++) {
            if(rabbitChoices[i].successRate != 100) {
                RabbitChoice tempRC = rabbitChoices[i];
                tempRC.successRate = GetSuccessRate(RabbitSystem.GetDemands(rabbitChoices[i].name));
                if(tempRC.successRate != 0) {
                    tempRC.instance.transform.GetChild(0).gameObject.SetActive(false);
                    tempRC.instance.transform.GetChild(1).gameObject.SetActive(true);
                    tempRC.instance.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = tempRC.successRate.ToString();
                }
                
                rabbitChoices[i] = tempRC;
            }
        }
    }

    public void SelectRecipe(GameObject g) {
        rabbitChoices[chosenRabbitIndex].instance.transform.GetChild(2).gameObject.SetActive(false);
        int count = 0;
        foreach(RabbitChoice rc in rabbitChoices) {
            if(rc.instance == g) {
                g.transform.GetChild(2).gameObject.SetActive(true);
                chosenRabbitIndex = count;
                return;
            }
            count++;
        }
        return;
    }

    public void Cooking() {
        if(rabbitChoices[chosenRabbitIndex].successRate == 100) {
            CookingSuccess();
        }
        else if(rabbitChoices[chosenRabbitIndex].successRate == 50) {
            if(Random.Range(0, 10) < 5) {
                CookingSuccess();
            }
            else {
                CookingFailed();
            }
        }
        else if(rabbitChoices[chosenRabbitIndex].successRate == 25) {
            if(Random.Range(0, 20) < 5) {
                CookingSuccess();
            }
            else {
                CookingFailed();
            }
        }
        ResultPanel.SetActive(true);
    }

    private void CookingSuccess() {
        if(rabbitChoices[chosenRabbitIndex].method == Method.Refrigerator) {
            Refrigerator.SetActive(true);
            Oven.SetActive(false);
        }
        else {
            Oven.SetActive(true);
            Refrigerator.SetActive(false);
        }
        RabbitName.text = rabbitChoices[chosenRabbitIndex].name;
        RabbitImage.sprite = rabbitChoices[chosenRabbitIndex].image;
        success = true;
    } 

    private void CookingFailed() {
        Refrigerator.SetActive(false);
        Oven.SetActive(false);
        RabbitName.text = "烤焦啦~~~";
        RabbitImage.sprite = 焦;
        success = false;
    }

    public void ResultConfirm() {
        ResultPanel.SetActive(false);
        rabbitChoices[chosenRabbitIndex].instance.transform.GetChild(2).gameObject.SetActive(false);
        if(success) {
            RabbitSystem.SetRabbitGotOrNot(rabbitChoices[chosenRabbitIndex].name, true);
            RabbitChoice rc = rabbitChoices[chosenRabbitIndex];
            rc.successRate = 100;
            rc.instance.transform.GetChild(0).gameObject.SetActive(true);
            rc.instance.transform.GetChild(1).gameObject.SetActive(false);
            rc.instance.transform.GetChild(0).GetComponent<Image>().sprite = rc.image;
            rabbitChoices[chosenRabbitIndex] = rc;
        }
    }
}
