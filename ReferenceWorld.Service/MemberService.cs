using ReferenceWorld.Model;
using ReferenceWorld.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceWorld.Service
{
    public class MemberService
    {
        private readonly MemberRepository _memberRepository;

        public MemberService()
        {
            _memberRepository = new MemberRepository();
        }

        public IEnumerable<User> GetTeamFriends(string userGuid)
        {
            return _memberRepository.GetTeamFriends(userGuid);
        }
        public IEnumerable<User> SearchResultPeople(SearchModel model)
        {
            return _memberRepository.SearchResultPeople(model);
        }

        public IEnumerable<User> GetTeamFriends(string friendGuid, string myGuid)
        {
            return _memberRepository.GetTeamFriends(friendGuid, myGuid);
        }
        public int SendMessagesToUser(Message mes)
        {
            return _memberRepository.SendMessagesToUser(mes);
        }
        public int GetNewMessage(string userGuid)
        {
            return _memberRepository.GetNewMessage(userGuid);
        }
        public int DeleteMessage(string id)
        {
            return _memberRepository.DeleteMessage(id);
        }
        public int FollowFriend(Teams team)
        {
            return _memberRepository.FollowFriend(team);
        }
        public bool IsFollowFriend(Teams team)
        {
            return _memberRepository.IsFollowFriend(team);
        }
        public int IsSendFollowFriend(Teams team)
        {
            return _memberRepository.IsSendFollowFriend(team);
        }
        public int AgreeFollowFriend(string FriendGuid, string MyGuid)
        {
            return _memberRepository.AgreeFollowFriend(FriendGuid,MyGuid);
        }
        public IEnumerable<MessageEntity> GetNewMessagesList(string myGuid)
        {
            return _memberRepository.GetNewMessagesList(myGuid);
        }
        public User GetFriendHeadImage(string friendGuid)
        {
            return _memberRepository.GetFriendHeadImage(friendGuid);
        }
        public int DeletePicture(string id)
        {
            return _memberRepository.DeletePicture(id);
        }
        public void SendRequestFollowFriend(Teams team)
        {

        }

    }
}
