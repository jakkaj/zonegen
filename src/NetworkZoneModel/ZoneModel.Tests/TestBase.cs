using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using ZoneModel.Services;

namespace ZoneModel.Tests
{
    public class TestBase
    {
        public static ServiceProvider Services { get; set; }
        public TestBase()
        {
            Services = AppServices.Services;
        }

        public static T Resolve<T>()
        {
            return Services.GetService<T>();
        }
    }
}
