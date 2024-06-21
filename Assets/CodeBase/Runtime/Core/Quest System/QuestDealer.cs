
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestDealer : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _questRewardItemSpawnPoint;
    [field: SerializeField] public Transform PointForTip { get; private set; }
    [Header("UI")]
    [SerializeField] private CanvasGroup _ui;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _answerButton;
    [SerializeField] private TMP_Text _answerButtonText;
    [SerializeField] private TMP_Text _dialogueText;

    [Header("QUEST Settings")]
    [SerializeField] private Quest[] _quests;
    [SerializeField] private QuestItemsProgressUIController _itemsProgressUIController;
    private IPauseService _pauseService;
    private ICursorService _cursorService;
    private int _currentQuestIndex;
    private bool _isDialogueActive;


    [Inject]
    public void Construct(IPauseService pauseService, ICursorService cursorService)
    {
        _pauseService = pauseService;
        _cursorService = cursorService;
    }

    private void Start()
    {
        InitQuests();

        _currentQuestIndex = 0;
    }

    

    public void Interact()
    {
        if (_isDialogueActive || _quests[_currentQuestIndex].IsCompleted)
            return;
        
        _cursorService.SetCursorVisible();
        _pauseService.PauseActivated?.Invoke();

        _exitButton.onClick.AddListener(HideDialogue);
        _answerButton.onClick.AddListener(UpdateDialogueWindow);
        
        UpdateDialogueWindow();
        ShowDialogueWindow();
    }


    private void OnDisable()
    {
        _exitButton.onClick.RemoveAllListeners();
        _answerButton.onClick.RemoveAllListeners();
    }

    private void InitQuests()
    {
        foreach (var quest in _quests)
        {
            quest.Init(_questRewardItemSpawnPoint);
        }
    }

    private void UpdateDialogueWindow()
    {
        DialogueLine line = _quests[_currentQuestIndex].GetDialogueLine();

        if(line == null)
            ShowQuest();
        else if(line != null)
            ShowNextLine(line);
    }

    private void ShowDialogueWindow()
    {
        _isDialogueActive = true;
        _ui.gameObject.SetActive(true);
    }

    private void HideDialogue()
    {
        _cursorService.SetCursorInvisible();
        _pauseService.PauseDeActivated?.Invoke();

        _isDialogueActive = false;
        _ui.gameObject.SetActive(false);

        _exitButton.onClick.RemoveAllListeners();
        _answerButton.onClick.RemoveAllListeners();
    }

    private void ShowNextLine(DialogueLine line)
    {
        _dialogueText.text = line.Text;
        _answerButtonText.text = line.Answer;
    }

    private void ShowQuest()
    {
        Quest currentQuest = _quests[_currentQuestIndex];
        // set up ui text above ded
        _itemsProgressUIController.SetUp();
        
        _dialogueText.text = currentQuest.QuestTextLine;
        _answerButtonText.text = currentQuest.QuestAnswerLine;

        _answerButton.onClick.RemoveAllListeners();
        _answerButton.onClick.AddListener(currentQuest.CheckComplete);
        _answerButton.onClick.AddListener(() => _dialogueText.text = currentQuest.AfterQuestTextLine);

        // if complete переходить на след квест
        _quests[_currentQuestIndex].Completed += SetNextQuest;
        
    }

    private void SetNextQuest()
    {
        _quests[_currentQuestIndex].Completed -= SetNextQuest;

        _answerButton.onClick.RemoveAllListeners();
        _answerButton.onClick.AddListener(HideDialogue);

        if(_quests.Length - 1 < _currentQuestIndex + 1)
        {
            return;
        }

        _currentQuestIndex++;
    }
}
