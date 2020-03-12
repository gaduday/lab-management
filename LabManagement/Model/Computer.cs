namespace LabManagement
{
    public class Computer
    {
        private int id;
        private bool isUsing;
        private string usingUsername;

        public Computer(int id, bool isUsing)
        {
            this.id = id;
            this.isUsing = isUsing;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public bool IsUsing
        {
            get => isUsing;
            set => isUsing = value;
        }

        public string UsingUsername
        {
            get => usingUsername;
            set => usingUsername = value;
        }

        public override string ToString()
        {
            string status = !isUsing ? " is available to use!" : " is being used!";
            return $"{id}/ Status:{status}";
        }
    }
}