namespace CodeBase.ItemsSystem
{
    public class Test
    {
        public void Start()
        {
            Tool axe = new("axe", 100);
            axe.Durability++;
            
            Food apple = new("apple", 100);
            apple.Satiety++;

            Item rock = new("rock");
        }
    }
}