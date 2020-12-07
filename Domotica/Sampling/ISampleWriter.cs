using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Sampling
{
    public interface ISampleWriter
    {
        Task ProvideSample(DateTime timestamp, float temperature);

        void StartNewFile();
    }
}
