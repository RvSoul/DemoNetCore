using Demo.Model.CM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domian
{
    public class BaseDM
    {
        public static IServiceProvider service = null;
        public TodoContext c;

        public BaseDM()
        {
           var scope= service.GetRequiredService<IServiceScopeFactory>().CreateScope();
           c= scope.ServiceProvider.GetService<TodoContext>();
        }
    }
}
