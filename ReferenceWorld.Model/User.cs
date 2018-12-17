using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceWorld.Model
{
    public class User
    {
        public long Id { get; set; }
        public string UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HeadImage { get; set; }
        public int UserType { get; set; }
        public int UserState { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public DateTime AddTime { get; set; }
    }
    public class UserEntity : User
    {
        public string Password { get; set; }
    }
    public class Member : User
    {
        public int iSum { get; set; }
        public int iCount { get; set; }
        public int Star { get; set; }
    }

    public class Comment
    {
        public long Id { get; set; }
        public string UserGuid { get; set; }
        public string Content { get; set; }
        public string Star { get; set; }
        public string CommentName { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class Photos
    {
        public long Id { get; set; }
        public string UserGuid { get; set; }
        public string Image { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class Introduction
    {
        public long Id { get; set; }
        public string UserGuid { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class CompanyEntity
    {
        public string Company { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }

}
