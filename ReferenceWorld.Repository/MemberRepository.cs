using ReferenceWorld.Data.Dapper;
using ReferenceWorld.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceWorld.Repository
{
    public class MemberRepository
    {
        private readonly ISqlDatabaseProxy _databaseProxy;
        public MemberRepository()
        {
            _databaseProxy = new SqlDatabaseProxy(new SqlConnectionFactory(new ConnectionStringManager(DataConfig.GetPrimaryConnectionString())));
        }
        public IEnumerable<User> GetTeamFriends(string userGuid)
        {
            string sql = @" select u.* from rw_user as u 
                            inner join [rw_team ] as t on u.UserGuid=t.FriendGuid
                            where t.IsMatch=1 and t.MyGuid=@UserGuid
                            order by t.CreateTime desc ";
            return _databaseProxy.Query<User>(sql, new { UserGuid = userGuid });
        }

        public IEnumerable<User> SearchResultPeople(SearchModel model)
        {
            try
            {
                string strSql = @" select * from rw_user where UserState=0 {0} order by AddTime desc ";
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(model.Keyword))
                {
                    sb.AppendFormat(" and ((UserName like '%{0}%') or (FirstName like '%{0}%') or (LastName like '%{0}%') or (Address like '%{0}%'))", model.Keyword);
                }
                if (!string.IsNullOrEmpty(model.Person))
                {
                    sb.AppendFormat(" and Email like '%{0}%' ", model.Person);
                }
                if (!string.IsNullOrEmpty(model.Company))
                {
                    sb.AppendFormat(" and Company like '%{0}%' ", model.Company);
                }
                string sql = string.Format(strSql, sb.ToString());
                return _databaseProxy.Query<User>(sql, null);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<User> GetTeamFriends(string friendGuid, string myGuid)
        {
            string sql = @" select u.* from rw_user as u 
                            inner join [rw_team ] as t on u.UserGuid=t.FriendGuid
                            where t.MyGuid=@MyGuid and t.FriendGuid !=@FriendGuid
                            order by t.CreateTime desc ";
            return _databaseProxy.Query<User>(sql, new { MyGuid = myGuid, FriendGuid = friendGuid });
        }
        public int SendMessagesToUser(Message mes)
        {
            string sql = @"INSERT INTO [dbo].[rw_message ]
                                           ([FromUserGuid]
                                           ,[ToUserGuid]
                                           ,[FriendGuid]
                                           ,[Description]
                                           ,[SendType]
                                           ,[IsRead]
                                           ,[CreateTime])
                                     VALUES
                                           (@FromUserGuid
                                           ,@ToUserGuid
                                           ,@FriendGuid
                                           ,@Description
                                           ,@SendType
                                           ,@IsRead
                                           ,@CreateTime) ";
            return _databaseProxy.Execute(sql, mes);
        }
        public int GetNewMessage(string userGuid)
        {
            string sql = @" select Count(0) as iCount from [dbo].[rw_message ] where ToUserGuid=@ToUserGuid and IsRead=0  ";
            return _databaseProxy.Query<int>(sql, new { ToUserGuid = userGuid }).FirstOrDefault();
        }
        public int DeleteMessage(string id)
        {
            string sql = @" UPDATE [dbo].[rw_message ] SET [IsRead] = 1 WHERE Id=@id ";
            return _databaseProxy.Execute(sql, new { id = id });
        }
        public int FollowFriend(Teams team)
        {
            string sql = @" INSERT INTO [dbo].[rw_team ]
                                            ([FriendGuid]
                                            ,[MyGuid]
                                            ,[IsMatch]
                                            ,[CreateTime])
                                        VALUES
                                            (@FriendGuid
                                            ,@MyGuid 
                                            ,0
                                            ,@CreateTime) ";
            return _databaseProxy.Execute(sql, team);
        }

        public bool IsFollowFriend(Teams team)
        {
            string sql = @" select count(0) as iCount from [rw_team ] where IsMatch=1 and [FriendGuid]=@FriendGuid and [MyGuid]=@MyGuid ";
            int i = _databaseProxy.Query<int>(sql, new { FriendGuid = team.FriendGuid, MyGuid = team.MyGuid }).FirstOrDefault();
            return i > 0;
        }
        public int IsSendFollowFriend(Teams team)
        {
            string sql = @" select count(0) as iCount from [rw_team ] where IsMatch=0 and [FriendGuid]=@FriendGuid and [MyGuid]=@MyGuid ";
            return _databaseProxy.Query<int>(sql, new { FriendGuid = team.FriendGuid, MyGuid = team.MyGuid }).FirstOrDefault();
        }
        public int AgreeFollowFriend(string FriendGuid,string MyGuid)
        {
            string sql = @" UPDATE [rw_team ] SET IsMatch=1 where IsMatch=0 and [FriendGuid]=@FriendGuid and [MyGuid]=@MyGuid ";
            return _databaseProxy.Execute(sql, new { FriendGuid = FriendGuid, MyGuid = MyGuid });
        }
        public IEnumerable<MessageEntity> GetNewMessagesList(string myGuid)
        {
            string sql = @" select m.*,u.UserName,u.Email,u.HeadImage,u.UserGuid from rw_message as m
                            left join rw_user as u on m.FromUserGuid=u.UserGuid
                            where m.IsRead = 0 and  m.ToUserGuid=@ToUserGuid
                            order by m.CreateTime desc ";
            return _databaseProxy.Query<MessageEntity>(sql, new { ToUserGuid = myGuid });
        }
        public User GetFriendHeadImage(string friendGuid)
        {
            string sql = @" SELECT * FROM [dbo].[rw_user] WHERE [UserGuid]=@FriendGuid ";
            return _databaseProxy.Query<User>(sql, new { FriendGuid = friendGuid }).FirstOrDefault();
        }
        public int DeletePicture(string id)
        {
            string sql = @" Delete from [rw_photos] WHERE Id=@id ";
            return _databaseProxy.Execute(sql, new { id = id });
        }

    }
}
