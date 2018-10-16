using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZoneModel.Services.Options;

namespace ZoneModel.Services.Contracts
{
    public interface IVerbHandler<T> where T : OptionResultBase
    {
        bool CanHandle(OptionResultBase command);
        Task Handle(T command);
    }
}
