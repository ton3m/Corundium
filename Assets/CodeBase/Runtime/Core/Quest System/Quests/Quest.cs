using System;
using UnityEngine;

[Serializable]
public class RewardItem 
{
    public GameObject Prefab;
    public int Count;
}

[Serializable]
public class DialogueLine
{
    [TextArea]
    public string Text;
    public string Answer;
}

public class Quest : ScriptableObject
{
    public event Action Completed = delegate { };
    public bool IsCompleted { get; private set; }

    [field: SerializeField] public DialogueLine[] DialogueBeforeQuest { get; private set; }
    [field: SerializeField] public string QuestTextLine { get; private set; }
    [field: SerializeField] public string QuestAnswerLine { get; private set; }
    [field: SerializeField] public string AfterQuestTextLine { get; private set; }
    [field: SerializeField] public RewardItem[] RewardItems { get; private set; }
    protected Transform RewardItemSpawnPoint { get; private set; }
    private int _currentDialogueLineIndex;

    public void Init(Transform rewardItemSpawnPoint)
    {
        RewardItemSpawnPoint = rewardItemSpawnPoint;

        IsCompleted = false;
        _currentDialogueLineIndex = 0;
    }

    public virtual void CheckComplete()
    {
        Completed?.Invoke();
        IsCompleted = true;
    }

    public DialogueLine GetDialogueLine()
    {
        if (DialogueBeforeQuest.Length - 1 < _currentDialogueLineIndex)
            return null;

        try
        {
            return DialogueBeforeQuest[_currentDialogueLineIndex];
        }
        finally
        {
            _currentDialogueLineIndex++;
        }
    }
}
