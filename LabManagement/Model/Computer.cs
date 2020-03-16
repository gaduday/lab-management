namespace LabManagement
{
    public class Computer
    {
        private int id;
        private bool isUsing;
        private string usingUsername;

        public Computer(int id, bool isUsing, string usingUsername)
        {
            this.id = id;
            this.isUsing = isUsing;
            this.usingUsername = usingUsername;
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
            string status = !isUsing ? " is available to use!" : " is being used! " /*+ Program.userUsing + "!"*/;
            return $"{id}/ Status:{status}";
        }
    }
}