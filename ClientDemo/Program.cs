

using DotNetCoreRpc.Client;
using DotNetCoreRpc.Core;
using IRpcService;
using IRpcService.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            //*注册DotNetCoreRpcClient核心服务
            services.AddDotNetCoreRpcClient()
            //*通信是基于HTTP的,内部使用的HttpClientFactory,自行注册即可
            .AddHttpClient("WebDemo", client => { client.BaseAddress = new Uri("http://localhost:13285/"); });

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            //*获取RpcClient使用这个类创建具体服务代理对象
            RpcClient rpcClient = serviceProvider.GetRequiredService<RpcClient>();

            //IPersonService是我引入的服务包interface，需要提供ServiceName,即AddHttpClient的名称
            IPersonService personService = rpcClient.CreateClient<IPersonService>("WebDemo");

            PersonDto personDto = new PersonDto
            {
                Id = 1,
                Name = "yi念之间",
                Address = "中国",
                BirthDay = new DateTime(2000, 12, 12),
                IsMarried = true,
                Tel = 88888888888
            };

            bool addFlag = personService.Add(personDto);
            Console.WriteLine($"添加结果=[{addFlag}]");

            var person = personService.Get(personDto.Id);
            Console.WriteLine($"获取person结果=[{person.ToJson()}]");

            var persons = personService.GetAll();
            Console.WriteLine($"获取persons结果=[{persons.ToList().ToJson()}]");

            personService.Delete(person.Id);
            Console.WriteLine($"删除完成");

            Console.ReadLine();
        }
    }
}
