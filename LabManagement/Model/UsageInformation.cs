using System;

namespace LabManagement
{
    public class UsageInformation
    {
        private DateTime timeStartUsing;
        private DateTime timeFinishUsing;

        public UsageInformation(DateTime timeStartUsing, DateTime timeFinishUsing)
        {
            this.timeStartUsing = timeStartUsing;
            this.timeFinishUsing = timeFinishUsing;
        }

        public DateTime TimeStartUsing
        {
            get => timeStartUsing;
            set => timeStartUsing = value;
        }

        public DateTime TimeFinishUsing
        {
            get => timeFinishUsing;
            set => timeFinishUsing = value;
        }
    }
}