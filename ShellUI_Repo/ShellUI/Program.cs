using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using HedgehogService.Contract;
using MonkeyService.Contract;


namespace ShellUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var hedgehogProxyFactory = new ChannelFactory<IHedgehogService1>();
            var hedgehogChannel = hedgehogProxyFactory.CreateChannel();
            var resultHedgehog = hedgehogChannel.GetData(10);

            var monkeyProxyFactory = new ChannelFactory<IMonkeyService1>();
            var monkeyChannel = monkeyProxyFactory.CreateChannel();
            var resultMonkey = monkeyChannel.GetData(10);
        }
    }
}
