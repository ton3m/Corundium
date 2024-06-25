namespace CodeBase.ItemsSystem
{
    public class Tool : ValueItem
    {
        public Tool(string id, float durability) : base(id, durability)
        {
        }

        public float Durability
        {
            get => Value;
            set => Value = value;
        }
    }
}