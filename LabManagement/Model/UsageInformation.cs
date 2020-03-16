using System;

namespace LabManagement
{
    public class UsageInformation
    {
        private string timeStartUsing;
        private string timeFinishUsing;
        private int computerId;
        private string userUsername;
        
        public UsageInformation(string timeStartUsing, string timeFinishUsing, int computerId, string userUsername)
        {
            this.timeStartUsing = timeStartUsing;
            this.timeFinishUsing = timeFinishUsing;
            this.computerId = computerId;
            this.userUsername = userUsername;
        }

        public string TimeStartUsing
        {
            get => timeStartUsing;
            set => timeStartUsing = value;
        }

        public string TimeFinishUsing
        {
            get => timeFinishUsing;
            set => timeFinishUsing = value;
        }

        public int ComputerId
        {
            get => computerId;
            set => computerId = value;
        }

        public string UserUsername
        {
            get => userUsername;
            set => userUsername = value;
        }
    }
}