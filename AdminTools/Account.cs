using System;

namespace AdminTools
{
    public enum Privilege
    {
        User,
        Moderator,
        Administrator,
        Owner
    }

    public class Account
    {
        public string Name
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public Privilege Level
        {
            get;
            set;
        }

        public Ban Ban
        {
            get;
            set;
        }
    }
}
