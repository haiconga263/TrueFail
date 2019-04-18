using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface IJob
    {
        void Init(JobType type, JobMethod method, int seconds);
        void Start();
        void Stop();
        void Pause();
        void Resume();
    }
}
