using ReferenceWorld.Data.Dapper;
using ReferenceWorld.Model;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web;
namespace ReferenceWorld.Repository
{
    public class UserRepository
    {
        private readonly ISqlDatabaseProxy _databaseProxy;
        public UserRepository()
        {
            _databaseProxy = new SqlDatabaseProxy(new SqlConnectionFactory(new ConnectionStringManager(DataConfig.GetPrimaryConnectionString())));
        }
        public IEnumerable<User> GetUsers()
        {
            string sql = @" select * from [dbo].[rw_user] ";
            return _databaseProxy.Query<User>(sql);
        }
        public User GetLoginUserInfo(string username, string userpwd)
        {
            string sql = @" select top 1 * from [dbo].[rw_user] where UserState=0 and Email=@username and [Password]=@userpwd ";
            return _databaseProxy.Query<User>(sql, new { username = username, userpwd = userpwd }).FirstOrDefault();
        }
        public UserEntity GetUserInfo(string userGuid)
        {
            string sql = @" select  * from [dbo].[rw_user] where UserState=0 and [UserGuid]=@UserGuid ";
            return _databaseProxy.Query<UserEntity>(sql, new { UserGuid = userGuid}).FirstOrDefault();
        }
        public User GeExistUserInfo(string value, int type)
        {
            string sql = @" select top 1 * from [dbo].[rw_user] where UserState=0 {0} ";
            string sqlWhere = string.Empty;
            if (type == 1)
            {
                sqlWhere = " and UserName=@value ";
            }
            else
            {
                sqlWhere = " and Email=@value ";
            }
            string strSql = string.Format(sql, sqlWhere);
            return _databaseProxy.Query<User>(strSql, new { value = value}).FirstOrDefault();
        }
        public Member GetLoginUserAvatar(string id)
        {
            string sql = @" select top 1 a.* ,t.iSum,t.iCount
                            from [dbo].[rw_user] as a  
                            left join 
                            (select b.UserGuid,COUNT(0) as iCount,SUM(CONVERT(int,ISNULL(b.Star,0))) as iSum from rw_comment as b
                            where b.UserGuid=@UserGuid
                            group by b.UserGuid) as t on a.UserGuid=t.UserGuid
                            where a.UserState=0 and a.[UserGuid]=@UserGuid ";
            return _databaseProxy.Query<Member>(sql, new { UserGuid = id }).FirstOrDefault();
        }
        public int AddUser(UserEntity user)
        {
            string sql = @"INSERT INTO [dbo].[rw_user] ([UserGuid],[FirstName],[LastName],[UserName],[Email],[Password],[HeadImage],[UserType],[UserState],[Address],[Company],[AddTime])
                                                 VALUES(@UserGuid,@FirstName,@LastName,@UserName,@Email,@Password,@HeadImage,@UserType,@UserState,@Address,@Company,@AddTime) ";
            return _databaseProxy.Execute(sql, user);
        }
        public int AddUserAvatar(string userId, string saveName)
        {
            string sql = @"update [dbo].[rw_user] set [HeadImage]=@saveName where Id=@Id ";
            return _databaseProxy.Execute(sql, new { saveName= saveName,Id=userId });
        }
        public int CreateCommonToUser(Comment comment)
        {
            string sql = @" INSERT INTO [dbo].[rw_comment]
                                               ([UserGuid]
                                               ,[Content]
                                               ,[Star]
                                               ,[CommentName]
                                               ,[CreateTime])
                                         VALUES
                                               (@UserGuid
                                               ,@Content
                                               ,@Star
                                               ,@CommentName
                                               ,@CreateTime) ";
            return _databaseProxy.Execute(sql, comment);
        }

        public IEnumerable<Comment> GetCommentsByUser(string userGuid)
        {
            string sql = @"select  * from rw_comment where UserGuid=@UserGuid order by CreateTime desc";
            return _databaseProxy.Query<Comment>(sql, new { UserGuid = userGuid });
        }

        public int InsertPicture(Photos photo)
        {
            string sql = @" INSERT INTO [dbo].[rw_photos ]
                                                   ([UserGuid]
                                                   ,[Image]
                                                   ,[CreateTime])
                                             VALUES
                                                   (@UserGuid
                                                   ,@Image
                                                   ,@CreateTime) ";
            return _databaseProxy.Execute(sql, photo);
        }
        public IEnumerable<Photos> GetPhotosByUser(string userGuid)
        {
            string sql = @" select  * from [rw_photos] where UserGuid=@UserGuid order by CreateTime desc ";
            return _databaseProxy.Query<Photos>(sql, new { UserGuid = userGuid });
        }
        public int CreateIntroductionToUser(Introduction duction)
        {
            string sql = @" INSERT INTO [dbo].[rw_introduction]
                                               ([UserGuid]
                                               ,[Description]
                                               ,[CreateTime])
                                          VALUES
                                               (@UserGuid
                                               ,@Description
                                               ,@CreateTime) ";
            return _databaseProxy.Execute<Introduction>(sql, duction);
        }
        public Introduction GetLoginIntroduction(string id)
        {
            string sql = @" select top 1 * from rw_introduction where UserGuid=@UserGuid order by Id desc  ";
            return _databaseProxy.Query<Introduction>(sql, new { UserGuid = id }).FirstOrDefault();
        }

    }
}
