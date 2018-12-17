using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceWorld.Model
{
    public class Message
    {
        public long Id { get; set; }
        public string FromUserGuid { get; set; }
        public string ToUserGuid { get; set; }
        public string FriendGuid { get; set; }
        public string Description { get; set; }
        public int IsRead { get; set; }
        public int SendType { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class MessageEntity: Message
    {
        public string UserGuid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HeadImage { get; set; }
        public string FriendUserName { get; set; }
        public string FriendCompany { get; set; }
        public string FriendHeadImage { get; set; }
    }
    public class Teams
    {
        public long Id { get; set; }
        public string FriendGuid { get; set; }
        public string MyGuid { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class PartUserInfo
    {
        public string UserGuid { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
    }
    public class SearchModel
    {
        public string Keyword { get; set; }
        public string Person { get; set; }
        public string Company { get; set; }
    }
}
