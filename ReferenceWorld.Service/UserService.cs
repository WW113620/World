using ReferenceWorld.Model;
using ReferenceWorld.Repository;
using System.Collections.Generic;

namespace ReferenceWorld.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetLoginUserInfo(string username,string userpwd)
        {
            return _userRepository.GetLoginUserInfo(username, userpwd);
        }
        public UserEntity GetUserInfo(string userGuid)
        {
            return _userRepository.GetUserInfo(userGuid);
        }
        public User GeExistUserInfo(string value, int type)
        {
            return _userRepository.GeExistUserInfo(value, type);
        }

        public Member GetLoginUserAvatar(string id)
        {
            return _userRepository.GetLoginUserAvatar(id);
        }

        public int AddUser(UserEntity user)
        {
            return _userRepository.AddUser(user);
        }
        public int AddUserAvatar(string userId,string saveName)
        {
            return _userRepository.AddUserAvatar(userId, saveName);
        }
        public int CreateCommonToUser(Comment comment)
        {
            return _userRepository.CreateCommonToUser(comment);
        }
        public IEnumerable<Comment> GetCommentsByUser(string userGuid)
        {
            return _userRepository.GetCommentsByUser(userGuid);
        }
        public int InsertPicture(Photos photo)
        {
            return _userRepository.InsertPicture(photo);
        }
        public IEnumerable<Photos> GetPhotosByUser(string userGuid)
        {
            return _userRepository.GetPhotosByUser(userGuid);
        }
        public int CreateIntroductionToUser(Introduction duction)
        {
            return _userRepository.CreateIntroductionToUser(duction);
        }
        public Introduction GetLoginIntroduction(string id)
        {
            return _userRepository.GetLoginIntroduction(id);
        }
    }
}
