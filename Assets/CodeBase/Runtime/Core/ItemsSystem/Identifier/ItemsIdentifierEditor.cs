using UnityEditor;

namespace CodeBase.ItemsSystem
{
    [CustomEditor(typeof(ItemsDataInitializerSO))]
    public class ItemsIdentifierEditor : ButtonEditor
    {
        private void OnEnable()
        {
            SetButtonName("Reload & Identify");
        }
    }
}