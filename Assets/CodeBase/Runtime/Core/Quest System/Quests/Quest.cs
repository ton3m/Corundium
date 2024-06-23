using System;
using UnityEngine;

namespace CodeBase.Runtime.Core.Quest_System.Quests
{
    [Serializable]
    public class DialogueLine
    {
        [TextArea]
        public string Text;
        public string Answer;
    }

    public abstract class Quest : MonoBehaviour
    {
        public event Action Completed = delegate { };
        public bool IsCompleted { get; private set; }

        [field: SerializeField] public DialogueLine[] DialogueBeforeQuest { get; private set; }
        [field: SerializeField] public string QuestTextLine { get; private set; }
        [field: SerializeField] public string QuestAnswerLine { get; private set; }
        [field: SerializeField] public string NoCompleteQuestLine { get; private set; }
        [field: SerializeField] public string CompleteQuestLine { get; private set; }
        private int _currentDialogueLineIndex;

        public void Init()
        {
            IsCompleted = false;
            _currentDialogueLineIndex = 0;
        }

        public virtual void SetUpUI()
        {
        }
        
        private void OnDisable()
        {
            Completed = null;
        }

        public virtual void CheckComplete()
        {
            Completed?.Invoke();
            IsCompleted = true;
        }

        public string AfterQuestTextLine()
        {
            return IsCompleted ? CompleteQuestLine : NoCompleteQuestLine;
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
}