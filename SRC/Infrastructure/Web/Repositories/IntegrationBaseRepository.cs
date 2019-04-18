using Common;
using Common.Abstracts;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Web.Repositories
{
    public class IntegrationBaseRepository<T> : IIntegrationRepository<T> where T : BaseModel
    {
        public async Task<int> Delete(UserSession user, T value, IDbConnection conn = null, IDbTransaction trans = null)
        {
            Assembly assembly = null;
            if (!string.IsNullOrEmpty(GlobalConfiguration.IntegrationTemplate.ModelAssembly))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "ExtensionFolder", GlobalConfiguration.IntegrationTemplate.ModelAssembly);
                assembly = Assembly.Load(File.ReadAllBytes(path));
            }
            EventResultModel eventResultModel = null;
            if (conn != null && trans != null)
            {
                try
                {
                    var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);

                    // run berore record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Delete.BeforeRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    value.IsDeleted = true;
                    string cmd = QueriesCreatingHelper.CreateQueryUpdate(value);

                    var rs = await DALHelper.Execute(cmd, dbTransaction: trans, connection: conn);

                    // run after record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Delete.AfterRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    return rs == 1 ? 0 : -1;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger().Error(ex.Message);
                    return -1;
                }
            }
            else
            {
                try
                {
                    var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);

                    // run berore record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Delete.BeforeRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    value.IsDeleted = true;
                    string cmd = QueriesCreatingHelper.CreateQueryUpdate(value);

                    var rs = await DALHelper.Execute(cmd);

                    // run after record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Delete.AfterRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    return rs == 1 ? 0 : -1;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger().Error(ex.Message);
                    return -1;
                }
            }
        }

        public async Task<int> Update(UserSession user, T value, IDbConnection conn = null, IDbTransaction trans = null)
        {
            Assembly assembly = null;
            if (!string.IsNullOrEmpty(GlobalConfiguration.IntegrationTemplate.ModelAssembly))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "ExtensionFolder", GlobalConfiguration.IntegrationTemplate.ModelAssembly);
                assembly = Assembly.Load(File.ReadAllBytes(path));
            }
            EventResultModel eventResultModel = null;
            if (conn != null && trans != null)
            {
                try
                {
                    var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);

                    // run berore record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Update.BeforeRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    string cmd = QueriesCreatingHelper.CreateQueryUpdate(value);

                    var rs = await DALHelper.Execute(cmd, dbTransaction: trans, connection: conn);

                    // run after record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Update.AfterRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    return rs == 1 ? 0 : -1;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger().Error(ex.Message);
                    return -1;
                }
            }
            else
            {
                try
                {
                    var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);

                    // run berore record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Update.BeforeRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    string cmd = QueriesCreatingHelper.CreateQueryUpdate(value);

                    var rs = await DALHelper.Execute(cmd);

                    // run after record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Update.AfterRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    return rs == 1 ? 0 : -1;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger().Error(ex.Message);
                    return -1;
                }
            }
        }

        public async Task<int> Write(UserSession userLogin, T value, IDbConnection conn = null, IDbTransaction trans = null)
        {
            Assembly assembly = null;
            if (!string.IsNullOrEmpty(GlobalConfiguration.IntegrationTemplate.ModelAssembly))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "ExtensionFolder", GlobalConfiguration.IntegrationTemplate.ModelAssembly);
                assembly = Assembly.Load(File.ReadAllBytes(path));
            }
            EventResultModel eventResultModel = null;
            if (conn != null && trans != null)
            {
                try
                {
                    var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);

                    // run berore record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.BeforeRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, userLogin });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    string cmd = QueriesCreatingHelper.CreateQueryInsert(value);

                    var rs = await DALHelper.Execute(cmd, dbTransaction: trans, connection: conn);

                    // run after record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.AfterRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, userLogin });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    return rs == 1 ? 0 : -1;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger().Error(ex.Message);
                    return -1;
                }
            }
            else
            {
                try
                {
                    var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);

                    // run berore record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.BeforeRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, userLogin });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    string cmd = QueriesCreatingHelper.CreateQueryInsert(value);

                    var rs = await DALHelper.Execute(cmd);

                    // run after record event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.AfterRowEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, userLogin });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    return rs == 1 ? 0 : -1;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger().Error(ex.Message);
                    return -1;
                }
            }
        }
        public async Task<int> WriteArray(UserSession user, IEnumerable<T> value, IDbConnection conn = null, IDbTransaction trans = null)
        {
            Assembly assembly = null;
            if (!string.IsNullOrEmpty(GlobalConfiguration.IntegrationTemplate.ModelAssembly))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "ExtensionFolder", GlobalConfiguration.IntegrationTemplate.ModelAssembly);
                assembly = Assembly.Load(File.ReadAllBytes(path));
            }
            EventResultModel eventResultModel = null;
            if (conn != null && trans != null)
            {
                try
                {

                    var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);

                    // run berore event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.BeforeEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    foreach (var item in value)
                    {
                        // run berore record event
                        eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.BeforeRowEvent, typeof(BaseEvent<T>), new object[] { this, item, conn, trans, user });
                        if (!eventResultModel.Result)
                        {
                            throw new Exception(eventResultModel.ErrorMessage);
                        }
                        if (!eventResultModel.IsBubble)
                        {
                            continue;
                        }

                        string cmd = QueriesCreatingHelper.CreateQueryInsert(value);

                        var rs = await DALHelper.Execute(cmd, dbTransaction: trans, connection: conn);

                        // run after record event
                        eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.AfterRowEvent, typeof(BaseEvent<T>), new object[] { this, item, conn, trans, user });
                        if (!eventResultModel.Result)
                        {
                            throw new Exception(eventResultModel.ErrorMessage);
                        }
                        if (!eventResultModel.IsBubble)
                        {
                            continue;
                        }
                    }

                    // run after event
                    eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.AfterEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                    if (!eventResultModel.Result)
                    {
                        throw new Exception(eventResultModel.ErrorMessage);
                    }
                    if (!eventResultModel.IsBubble)
                    {
                        return 0;
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger().Error(ex.Message);
                    return -1;
                }
            }
            else
            {
                using (conn = DALHelper.GetConnection())
                {
                    conn.Open();
                    using (trans = conn.BeginTransaction())
                    {
                        try
                        {
                            var model = GlobalConfiguration.IntegrationTemplate.ModelTemplates.FirstOrDefault(m => m.Name == typeof(T).Name);

                            // run berore event
                            eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.BeforeEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                            if (!eventResultModel.Result)
                            {
                                throw new Exception(eventResultModel.ErrorMessage);
                            }
                            if (!eventResultModel.IsBubble)
                            {
                                return 0;
                            }

                            foreach (var item in value)
                            {
                                // run berore record event
                                eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.BeforeRowEvent, typeof(BaseEvent<T>), new object[] { this, item, conn, trans, user });
                                if (!eventResultModel.Result)
                                {
                                    throw new Exception(eventResultModel.ErrorMessage);
                                }
                                if (!eventResultModel.IsBubble)
                                {
                                    continue;
                                }

                                string cmd = QueriesCreatingHelper.CreateQueryInsert(value);

                                var rs = await DALHelper.Execute(cmd, dbTransaction: trans, connection: conn);

                                // run after record event
                                eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.AfterRowEvent, typeof(BaseEvent<T>), new object[] { this, item, conn, trans, user });
                                if (!eventResultModel.Result)
                                {
                                    throw new Exception(eventResultModel.ErrorMessage);
                                }
                                if (!eventResultModel.IsBubble)
                                {
                                    continue;
                                }
                            }

                            // run after event
                            eventResultModel = CommonHelper.RunEvent(assembly, model.Insert.AfterEvent, typeof(BaseEvent<T>), new object[] { this, value, conn, trans, user });
                            if (!eventResultModel.Result)
                            {
                                throw new Exception(eventResultModel.ErrorMessage);
                            }
                            if (!eventResultModel.IsBubble)
                            {
                                return 0;
                            }

                            trans.Commit();
                            return 0;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.GetLogger().Error(ex.Message);
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                            return -1;
                        }
                        finally
                        {
                            if (conn.State == System.Data.ConnectionState.Open)
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
