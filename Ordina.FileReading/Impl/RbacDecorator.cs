using System.Threading.Tasks;
using Decor;

namespace Ordina.FileReading
{
    internal class RbacDecorator : IDecorator
    {
        private readonly IRbacService _rbacService;

        public RbacDecorator(IRbacService rbacService)
        {
            _rbacService = rbacService;
        }

        public async Task OnInvoke(Call call)
        {

            var path = call.Arguments[0] as string;
            _rbacService.ThrowWhenCantReadContent(path);

            await call.Next();
        }
    }
}