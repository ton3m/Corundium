namespace CodeBase.ItemsSystem
{
    public class Food : ValueItem
    {
        public Food(string id, float satiety) : base(id, satiety)
        {
        }

        public float Satiety
        {
            get => Value;
            set => Value = value;
        }
    }
}