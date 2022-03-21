using System;

namespace EntityLayer
{
    public class User
    {
        [PrimaryKey]
        public int UserID{get;set;}
        public string UserName{get;set;}
        public string UserSurname{get;set;}
        public string UserPassword{get;set;}
        public string UserEmail{get;set;}
    }
}
