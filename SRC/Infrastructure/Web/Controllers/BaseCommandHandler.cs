using Autofac;
using Common;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using DAL;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Web.Controls;

namespace Web.Controllers
{
    public abstract class BaseCommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : BaseCommand<TResult>
    {
        public static ICacheProvider CachingCommandTransaction = GlobalConfiguration.Container.Resolve<ICacheProvider>();


        protected BaseCommandHandler()
        {
        }

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            //Validation
            Type typeValidation = typeof(TCommand);
            Assembly assembly = Assembly.GetAssembly(typeValidation);
            var validatorBaseType = typeof(AbstractValidator<>).MakeGenericType(typeValidation);
            var validatorType = assembly.GetTypes().FirstOrDefault(type => type.IsSubclassOf(validatorBaseType));
            if (validatorType != null)
            {
                var validator = Activator.CreateInstance(validatorType);
                var method = validatorBaseType.GetMethods().Where(m => m.Name == "Validate" && m.GetParameters()[0].ParameterType == typeValidation).First();
                ValidationResult results = (ValidationResult)method.Invoke(validator, new object[] { request });
                if(!results.IsValid)
                {
                    List<APIResult> returnErrors = new List<APIResult>();
                    foreach (var error in results.Errors)
                    {
                        returnErrors.Add(new APIResult()
                        {
                            Result = -1,
                            ErrorMessage = error.ErrorMessage
                        });
                    }
                    throw new BusinessException(JsonConvert.SerializeObject(returnErrors));
                }
            }
            if (request.CmdStyle == CommandStyles.Normal)
            {
                return await HandleCommand(request, cancellationToken);
            }
            else if(request.CmdStyle == CommandStyles.Transaction)
            {
                if(CachingCommandTransaction.TryGetValue(request.CommandId, out CachingCommandTransactionModel transModel))
                {
                    return await HandleCommandTransaction(request, transModel, cancellationToken);
                }
                else
                {
                    var conn = DALHelper.GetConnection();
                    conn.Open();
                    var trans = conn.BeginTransaction();
                    transModel = new CachingCommandTransactionModel()
                    {
                        Connection = conn,
                        Transaction = trans
                    };
                    if(CachingCommandTransaction.TrySetValue(request.CommandId, transModel))
                    {
                        return await HandleCommandTransaction(request, transModel, cancellationToken);
                    }
                    else
                    {
                        throw new BusinessException("Common.CachingData.SetError");
                    }
                }
            }
            else if(request.CmdStyle == CommandStyles.CommitTransaction)
            {
                if (!CachingCommandTransaction.TryGetValue(request.CommandId, out CachingCommandTransactionModel transModel))
                {
                    throw new BusinessException("Common.CachingData.GetError");
                }

                transModel.Transaction.Commit();
                transModel.Transaction.Dispose();
                transModel.Connection.Dispose();

                //Remove TransModel
                CachingCommandTransaction.TryRemoveCaching<CachingCommandTransactionModel>(request.CommandId);
                return default(TResult);
            }
            else //Rollback Transaction
            {
                if (!CachingCommandTransaction.TryGetValue(request.CommandId, out CachingCommandTransactionModel transModel))
                {
                    throw new BusinessException("Common.CachingData.GetError");
                }

                transModel.Transaction.Rollback();
                transModel.Transaction.Dispose();
                transModel.Connection.Dispose();

                //Remove TransModel
                CachingCommandTransaction.TryRemoveCaching<CachingCommandTransactionModel>(request.CommandId);
                return default(TResult);
            }
        }

        public abstract Task<TResult> HandleCommand(TCommand request, CancellationToken cancellationToken);
        public virtual Task<TResult> HandleCommandTransaction(TCommand request, CachingCommandTransactionModel trans, CancellationToken cancellationToken)
        {
            return default(Task<TResult>);
        }

        public T CreateBuild<T>(T obj, UserSession userSession) where T : BaseModel
        {
            obj.CreatedDate = DateTime.Now;
            obj.CreatedBy = userSession.Id;
            obj.IsDeleted = false;
            return obj;
        }

        public T UpdateBuild<T>(T obj, UserSession userSession) where T : BaseModel
        {
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = userSession.Id;
            return obj;
        }

        public T DeleteBuild<T>(T obj, UserSession userSession) where T : BaseModel
        {
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = userSession.Id;
            obj.IsDeleted = true;
            return obj;
        }
    }
}
