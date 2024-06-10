using UnityEditor;

namespace CodeBase.Inventory
{
    [CustomEditor(typeof(ItemsIdentifierSO))]
    public class ItemsIdentifierEditor : ButtonEditor
    {
        private void OnEnable()
        {
            SetButtonName("Reload & Identify");
        }
    }
}