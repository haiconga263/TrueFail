using Common.Models;
using DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Users.UI.Models;

namespace Users.Queries
{
    public class AccessTokenQueries : BaseQueries, IAccessTokenQueries
    {
        public async Task<UserSession> Get(string accessToken)
        {
            UserSession result = null;
            string cmd = $@"SELECT t.*, a.*, r.* FROM `user_access_token` t
                            INNER JOIN `user_account` a ON t.user_id = a.id AND a.is_deleted = 0
                            LEFT JOIN `user_account_role` ar ON t.user_id = ar.user_account_id
                            LEFT JOIN `role` r ON r.id = ar.role_id
                            WHERE t.access_token = '{accessToken}'";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<UserAccessToken, UserAccount, Role, UserSession>(
                    (tkRs, uRs, rRs) =>
                    {
                        if (result == null)
                        {
                            result = new UserSession()
                            {
                                AccessToken = tkRs.AccessToken,
                                Id = uRs.Id,
                                Email = uRs.Email,
                                IsSuperAdmin = uRs.IsSuperAdmin,
                                IsUsed = uRs.IsUsed,
                                LanguageId = 1, //test,
                                LanguageCode = "vi", //test,
                                UserName = uRs.UserName,
                                SessionId = tkRs.Id,
                                LoginResult = 0,
                                LoginCaptionMessage = string.Empty
                            };
                        }

                        if (rRs != null)
                        {
                            var role = result.Roles.FirstOrDefault(r => r == rRs.Name);
                            if (role == null)
                            {
                                result.Roles.Add(rRs.Name);
                            }
                        }
                        return result;
                    }
                );
                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<UserAccessToken, UserAccount, Role, UserSession>(
                    (tkRs, uRs, rRs) =>
                    {
                        if (result == null)
                        {
                            result = new UserSession()
                            {
                                AccessToken = tkRs.AccessToken,
                                Id = uRs.Id,
                                Email = uRs.Email,
                                IsSuperAdmin = uRs.IsSuperAdmin,
                                IsUsed = uRs.IsUsed,
                                LanguageId = 1, //test,
                                LanguageCode = "vi", //test,
                                UserName = uRs.UserName,
                                SessionId = tkRs.Id,
                                LoginResult = 0,
                                LoginCaptionMessage = string.Empty
                            };
                        }

                        if (rRs != null)
                        {
                            var role = result.Roles.FirstOrDefault(r => r == rRs.Name);
                            if (role == null)
                            {
                                result.Roles.Add(rRs.Name);
                            }
                        }
                        return result;
                    }
                );
                    return result;
                }
            }
        }

        public async Task<IEnumerable<UserSession>> Gets()
        {
            List<UserSession> result = new List<UserSession>();
            string cmd = $@"SELECT t.*, a.*, r.* FROM `user_access_token` t
                            INNER JOIN `user_account` a ON t.user_id = a.id AND a.is_deleted = 0
                            LEFT JOIN `user_account_role` ar ON t.user_id = ar.user_account_id
                            LEFT JOIN `role` r ON r.id = ar.role_id";
            if (DbConnection != null)
            {
                var rd = await DbConnection.QueryMultipleAsync(cmd, transaction: DbTransaction);
                rd.Read<UserAccessToken, UserAccount, Role, UserSession>(
                    (tkRs, uRs, rRs) =>
                    {
                        var session = result.FirstOrDefault(s => s.SessionId == tkRs.Id);
                        if (session == null)
                        {
                            session = new UserSession()
                            {
                                AccessToken = tkRs.AccessToken,
                                Id = uRs.Id,
                                Email = uRs.Email,
                                IsSuperAdmin = uRs.IsSuperAdmin,
                                IsUsed = uRs.IsUsed,
                                LanguageId = 1, //test,
                                LanguageCode = "vi", //test,
                                UserName = uRs.UserName,
                                SessionId = tkRs.Id,
                                LoginResult = 0,
                                LoginCaptionMessage = string.Empty
                            };
                            result.Add(session);
                        }

                        if (rRs != null)
                        {
                            var role = session.Roles.FirstOrDefault(r => r == rRs.Name);
                            if (role == null)
                            {
                                session.Roles.Add(rRs.Name);
                            }
                        }
                        return session;
                    }
                );
                return result;
            }
            else
            {
                using (var conn = DALHelper.GetConnection())
                {
                    var rd = await conn.QueryMultipleAsync(cmd);
                    rd.Read<UserAccessToken, UserAccount, Role, UserSession>(
                        (tkRs, uRs, rRs) =>
                        {
                            var session = result.FirstOrDefault(s => s.SessionId == tkRs.Id);
                            if (session == null)
                            {
                                session = new UserSession()
                                {
                                    AccessToken = tkRs.AccessToken,
                                    Id = uRs.Id,
                                    Email = uRs.Email,
                                    IsSuperAdmin = uRs.IsSuperAdmin,
                                    IsUsed = uRs.IsUsed,
                                    LanguageId = 1, //test,
                                    LanguageCode = "vi", //test,
                                    UserName = uRs.UserName,
                                    SessionId = tkRs.Id,
                                    LoginResult = 0,
                                    LoginCaptionMessage = string.Empty
                                };
                                result.Add(session);
                            }

                            if (rRs != null)
                            {
                                var role = session.Roles.FirstOrDefault(r => r == rRs.Name);
                                if (role == null)
                                {
                                    session.Roles.Add(rRs.Name);
                                }
                            }
                            return session;
                        }
                    );
                    return result;
                }
            }
        }
    }
}
