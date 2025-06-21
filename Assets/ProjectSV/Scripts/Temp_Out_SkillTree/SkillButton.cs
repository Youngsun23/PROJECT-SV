//using TMPro;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class SkillButton : MonoBehaviour, IPointerClickHandler
//{
//    public bool IsUnlockedSlot => isUnlockedSlot;
//    [SerializeField] private Image icon;
//    [SerializeField] private TextMeshProUGUI levelText;
//    [SerializeField] private int index;
//    [SerializeField] private Image highlighted;
//    private SkillPanel skillPanel;
//    [SerializeField] private bool isUnlockedSlot;
//    [SerializeField] private Sprite lockedSlotIcon;
//    [SerializeField] private Sprite emptySlotIcon;
//    [SerializeField] private GameObject lockedSkillIcon;
//    private Button button;

//    private void Awake()
//    {
//        skillPanel = transform.parent.GetComponent<SkillPanel>();
//        button = transform.GetComponent<Button>();
//    }

//    private void Start()
//    {
//        if (skillPanel == null)
//            return;

//        skillPanel.Initialize();

        
//    }

//    public void SetIndex(int _index)
//    {
//        index = _index;
//    }

//    public void Set(SkillTag skillTag)
//    {
//        Skill skill = GameDataManager.Singleton.GetSkillGameData(skillTag);
//        icon.sprite = skill.Icon;
//        int currentLevel = UserDataManager.Singleton.GetUserDataSkillLevelDictionary(skillTag);
//        lockedSkillIcon.SetActive(false);

//        if (currentLevel <= 0)
//        {
//            icon.color = Color.gray;
//            if(currentLevel < 0)
//            {
//                lockedSkillIcon.SetActive(true);
//            }
//        }
//        else
//        {
//            levelText.text = currentLevel.ToString();
//            icon.color = Color.white;
//        }

//        if (currentLevel == skill.MaxLevel)
//        {
//            levelText.color = Color.green;
//        }

//        //if (skill.RequiredUnlockPoint == UserDataManager.Instance.UserData.skillCurrentUnlockPointDictionary[skill.SkillTag])
//        //{
//        //    lockedSkillIcon.SetActive(false);
//        //    UserDataManager.Instance.UpdateUserDataSkillLevel(skill.SkillTag);
//        //    SkillPopup.Instance.UIUpdate(GameDataManager.Instance.skillGameData[index].SkillTag);
//        //}

//        button.interactable = true;
//    }

//    public void SetDefault()
//    {
//        if (!isUnlockedSlot)
//            icon.sprite = lockedSlotIcon;
//            // lockedSkillIcon.SetActive(true);
//        else
//            icon.sprite = emptySlotIcon;
//            // lockedSkillIcon.SetActive(false);

//        levelText.text = "";

//        button.interactable = false;
//    }

//    public void UnlockSlot()
//    {
//        isUnlockedSlot = true;
//        SetDefault();
//    }

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        if (!button.interactable)
//            return;

//        skillPanel.OnClick(index);
//    }

//    public void Highlight(bool tf)
//    {
//        highlighted.gameObject.SetActive(tf);
//    }
//}
