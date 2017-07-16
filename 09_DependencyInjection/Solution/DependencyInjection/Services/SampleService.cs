using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services
{
    public class Sample : ISampleTransient, ISampleScoped, ISampleSingleton, ISampleInterface
    {
        private Random _random;
        private int _current;

        public Sample()
        {
            _random = new Random();
            _current = _random.Next();
        }

        public int GetNumber()
        {
            return _current;
        }
    }


    public class SampleService
    {
        public ISampleTransient SampleTransient { get; private set; }
        public ISampleScoped SampleScoped { get; private set; }
        public ISampleSingleton SampleSingleton { get; private set; }

        public SampleService(ISampleTransient sampleTransient, ISampleScoped sampleScoped, ISampleSingleton sampleSingleton)
        {
            SampleTransient = sampleTransient;
            SampleScoped = sampleScoped;
            SampleSingleton = sampleSingleton;
        }
    }
}
