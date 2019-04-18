using Abivin.Integration.UI;
using Abivin.Integration.UI.Interfaces;
using Common.Exceptions;
using Common.Helpers;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Abivin.Integration.Commands.OrderCommand
{
    public class ChangeOrderStatusCommandHandler : BaseCommandHandler<ChangeOrderStatusCommand, int>
    {
        private readonly IAbivinOrderQueries abivinOrderQueries = null;
        public ChangeOrderStatusCommandHandler(IAbivinOrderQueries abivinOrderQueries)
        {
            this.abivinOrderQueries = abivinOrderQueries;
        }
        public override async Task<int> HandleCommand(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            if(request.OrderCode == null)
            {
                throw new BusinessException("Dữ liệu đầu vào không đúng");
            }
            if(!Const.AbivinPublicKey.Equals(request.Key))
            {
                throw new BusinessException("Key không đúng");
            }

            var signCheck = $"{request.Key}{Const.AbivinPrivateKey}{request.OrderCode}".CreateMD5();
            if (signCheck != request.Sign)
            {
                throw new BusinessException("Chữ ký không đúng");
            }

            var order = await abivinOrderQueries.GetByCode(request.OrderCode);
            if(order == null)
            {
                throw new BusinessException("Order không tồn tại");
            }

            //todo business

            return 0;
        }
    }
}
