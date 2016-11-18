using System;

namespace AdminTools
{
    public class Ban
    {
        public string Name
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Reason
        {
            get;
            set;
        }

        public DateTime TimeIssued
        {
            get;
            set;
        }

        public string BannedBy
        {
            get;
            set;
        }
    }
}
