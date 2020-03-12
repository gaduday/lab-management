namespace LabManagement
{
    public class User
    {
        // private int id;
        private string name;
        private string username;
        private string password;
        private bool isOnline;

        public User()
        {
            
        }
        public User(string name, string username, string password, bool isOnline)
        {
            // this.id = id;
            this.name = name;
            this.username = username;
            this.password = password;
            this.isOnline = isOnline;
        }

        // public int Id
        // {
        //     get => id;
        //     set => id = value;
        // }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Username
        {
            get => username;
            set => username = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public bool IsOnline
        {
            get => isOnline;
            set => isOnline = value;
        }

        public override string ToString()
        {
            return string.Format("Name = {0} / Username = {1} / Password = {2}", name, username, password);
        }
    }
}