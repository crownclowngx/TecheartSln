using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Utils;
using TecheartSln.Plug.Drive.Domain.Response;
using TecheartSln.Plug.Drive.Scene.Request;
using TecheartSln.Plug.Drive.Scene.Response;

namespace TecheartSln.Plug.Drive.Scene
{
    public class ZhiBoUtils
    {
        /// <summary>
        /// 根据userid 查询这个用户下的所有子用户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static BaseResponse<GetUsersNewResponse> GetSubUsersNew(long userid)
        {
            var responsestr = HttpUtils.PostJson("http://39.107.99.199:30000/api/User/GetSubUsersNew", new { UserIds = new List<long>() { userid }, PageNumber = 1, PageCount = 10000, });
            var response = JsonUtils.Deserialize<BaseResponse<GetUsersNewResponse>>(responsestr);
            return response;
        }

        /// <summary>
        /// 根据用户id 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static BaseResponse<UserResponse> GetUserByUserId(long userid)
        {
            var responsestr = HttpUtils.PostJson("http://39.107.99.199:30000/api/User/GetUser", new { UserId = userid });
            var response = JsonUtils.Deserialize<BaseResponse<UserResponse>>(responsestr);
            return response;
        }

        /// <summary>
        /// 创建员工，主播不能通过该接口创建
        /// </summary>
        /// <param name="userCreateRequest"></param>
        /// <returns></returns>
        public static BaseResponse<UserResponse> CreateUser(CreateUserRequest userCreateRequest)
        {
            var responsestr = HttpUtils.PostJson("http://39.107.99.199:30000/api/User/CreateUser", userCreateRequest);
            var response = JsonUtils.Deserialize<BaseResponse<UserResponse>>(responsestr);
            return response;
        }

        /// <summary>
        /// 创建主播
        /// </summary>
        /// <param name="Anchor"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public static BaseResponse<CreateAnchorResponse> CreateAnchor(Anchor Anchor , User User)
        {
            var responsestr = HttpUtils.PostJson("http://39.107.99.199:30000/api/Anchor/CreateAnchor", new { Anchor = Anchor, User = User });
            var response = JsonUtils.Deserialize<BaseResponse<CreateAnchorResponse>>(responsestr);
            return response;
        }

 
        public static BaseResponse<UserResponse> UpdatePassword(String password,long uderid)
        {
            var responsestr = HttpUtils.PostJson("http://39.107.99.199:30000/api/User/UpdatePassWord", new { PassWord= password, UserId= uderid });
            var response = JsonUtils.Deserialize<BaseResponse<UserResponse>>(responsestr);
            return response;
        }
        /// <summary>
        /// 根据usertype获取他下面的字usertype
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        public static UserTypeSub GetUserSubTypeByUserType(int userType)
        {
            UserTypeSub userTypeSub = new UserTypeSub();
            if (userType == 0)
            {
                userTypeSub.Id = 1;
                userTypeSub.Desc = "运营经理";
            }
            if (userType == 1)
            {
                userTypeSub.Id = 2;
                userTypeSub.Desc = "星探组长";
            }
            if (userType == 2)
            {
                userTypeSub.Id = 3;
                userTypeSub.Desc = "星探";
            }
            if (userType == 3)
            {
                userTypeSub.Id = 999;
                userTypeSub.Desc = "主播";
            }
            return userTypeSub;
        }

        public static int Get主播Id()
        {
            return 999;
        }
    }

    public class UserTypeSub
    {
        public int Id { get; set; }

        public String Desc { get; set; }
    }
}
