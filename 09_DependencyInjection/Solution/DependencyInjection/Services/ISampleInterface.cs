﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services
{
    public interface ISampleInterface
    {
        int GetNumber();
    }

    public interface ISampleTransient : ISampleInterface
    {
    }
    public interface ISampleScoped : ISampleInterface
    {
    }
    public interface ISampleSingleton : ISampleInterface
    {
    }
}
