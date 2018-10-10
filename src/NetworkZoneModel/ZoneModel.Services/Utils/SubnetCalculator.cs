using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Services.Utils
{
    public class SubnetCalculator : ISubnetCalculator
    {
        public string GetSubnetOffset(string ip,byte mask,  int index)
        {
            var parsed = IPNetwork.Parse(ip);

            var subnets = parsed.Subnet(mask);

            if (subnets.Count <= index)
            {
                throw new InvalidOperationException($"Subnet at index {index} too high. The ip {ip} with mask {mask} has only {subnets.Count} available subnets");
            }

            var subnet = subnets[index];

            return $"{subnet.Network}/{subnet.Cidr}";
        }
    }
}
