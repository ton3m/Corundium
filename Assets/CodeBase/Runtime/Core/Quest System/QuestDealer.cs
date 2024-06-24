
using CodeBase.Runtime.Core.Quest_System.Quests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestDealer : MonoBehaviour, IInteractable
{
    [field: SerializeField] public Transform PointForTip { get; private set; }
    
    [Header("UI")]
    [SerializeField] private CanvasGroup _ui;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _answerButton;
    [SerializeField] private TMP_Text _answerButtonText;
    [SerializeField] private TMP_Text _dialogueText;

    [Header("QUEST Settings")]
    [SerializeField] private Quest[] _quests;
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
        _currentQuestIndex = 0;
        _quests[_currentQuestIndex].Init();
    }

    public void Interact()
    {
        if (_isDialogueActive || _quests[_currentQuestIndex].IsCompleted)
            return;
        
        _cursorService.SetCursorVisible();
        _pauseService.PauseActivated?.Invoke();

        _exitButton.onClick.AddListener(DisableDilogue);
        _answerButton.onClick.AddListener(UpdateDialogueWindow);
        
        UpdateDialogueWindow();
        SetDialogueWindow(visibleMode: true);
    }

    private void DisableDilogue()
    {
        _cursorService.SetCursorInvisible();
        _pauseService.PauseDeActivated?.Invoke();

        _exitButton.onClick.RemoveAllListeners();
        _answerButton.onClick.RemoveAllListeners();
        
        SetDialogueWindow(visibleMode: false);
    }


    private void OnDisable()
    {
        _exitButton.onClick.RemoveAllListeners();
        _answerButton.onClick.RemoveAllListeners();
    }

    private void UpdateDialogueWindow()
    {
        DialogueLine line = _quests[_currentQuestIndex].GetDialogueLine();
        
        if(line == null)
            ShowQuest();
        else
            ShowNextLine(line);
    }

    private void SetDialogueWindow(bool visibleMode)
    {
        _isDialogueActive = visibleMode;
        _ui.gameObject.SetActive(visibleMode);
    }

    private void ShowNextLine(DialogueLine line)
    {
        _dialogueText.text = line.Text;
        _answerButtonText.text = line.Answer;
    }

    private void CheckQuest(Quest quest)
    {
        quest.CheckComplete();
        _dialogueText.text = quest.AfterQuestTextLine();
        
        _answerButton.onClick.RemoveAllListeners();
        _answerButton.onClick.AddListener(DisableDilogue);
    }
    
    private void ShowQuest()
    {
        Quest currentQuest = _quests[_currentQuestIndex];
        currentQuest.SetUpUI();
        
        _dialogueText.text = currentQuest.QuestTextLine;
        _answerButtonText.text = currentQuest.QuestAnswerLine;

        _answerButton.onClick.RemoveAllListeners();
        _answerButton.onClick.AddListener(() => CheckQuest(currentQuest));
        
        _quests[_currentQuestIndex].Completed += SetNextQuest;
    }

    private void SetNextQuest()
    {
        _quests[_currentQuestIndex].Completed -= SetNextQuest;

        _answerButton.onClick.RemoveAllListeners();
        _answerButton.onClick.AddListener(DisableDilogue);

        if(_quests.Length - 1 < _currentQuestIndex + 1)
        {
            return;
        }

        _currentQuestIndex++;
    }
}
