using Common.Helpers;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Common.Implementations
{
    public class Job : IJob
    {
        public event ExceptionOnRunning OnExceptionOnRunning;

        public static List<Job> JobList = new List<Job>();

        public JobMethod ExcuteJobMethod { private set; get; }
        public JobType Type { private set; get; }
        public int IntervalOrTimmingSeconds { private set; get; }
        public string JobName { protected set; get; }

        private bool IsInit;
        private bool IsRunning;
        private Thread thRun = null;
        private Mutex mt = new Mutex();
        private bool IsRunToday = false;
        private int IntervalCounting = 0;
        public Job()
        {
            JobList.Add(this);
        }
        public void Init(JobType type, JobMethod method, int seconds)
        {
            LogHelper.GetLogger().Info(string.Format("Init job: {0}", this.JobName));
            lock (mt)
            {
                Type = type;
                ExcuteJobMethod = method;
                IntervalOrTimmingSeconds = seconds;
                IsInit = true;
            }
        }
        public void Stop()
        {
            LogHelper.GetLogger().Info(string.Format("Stop job: {0}", this.JobName));
            if (thRun != null)
            {
                thRun.Abort();
                IsRunning = false;
                thRun = null;
            }
        }
        public void Pause()
        {
            LogHelper.GetLogger().Info(string.Format("Pause job: {0}", this.JobName));
            if (thRun != null)
            {
                IsRunning = false;
            }
            else
            {
                throw new Exception("The thread is already Abort. Can't pause this thread");
            }
        }
        public void Resume()
        {
            LogHelper.GetLogger().Info(string.Format("Resume job: {0}", this.JobName));
            if (thRun != null)
            {
                IsRunning = true;
                IntervalCounting = 0;
                if ((int)DateTime.Now.TimeOfDay.TotalSeconds - IntervalOrTimmingSeconds < 0)
                {
                    IsRunToday = false;
                }
                else
                {
                    IsRunToday = true;
                }
            }
            else
            {
                throw new Exception("The thread is already Abort. Can't resume this thread");
            }
        }
        public void Start()
        {
            LogHelper.GetLogger().Info(string.Format("Starting job: {0}", this.JobName));
            IsRunning = true;

            IntervalCounting = 0;
            if ((int)DateTime.Now.TimeOfDay.TotalSeconds - IntervalOrTimmingSeconds < 0)
            {
                IsRunToday = false;
            }
            else
            {
                IsRunToday = true;
            }

            if (thRun == null)
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        // Check The job is running or not
                        while (!IsRunning || !IsInit)
                        {
                            IntervalCounting = 0;
                            Thread.Sleep(1000);
                        }

                        lock (mt)
                        {
                            try
                            {
                                if (Type == JobType.Interval)
                                {
                                    if (IntervalCounting >= IntervalOrTimmingSeconds)
                                    {
                                        ExcuteJobMethod?.Invoke();
                                        IntervalCounting = 0;
                                    }
                                    else
                                    {
                                        IntervalCounting++;
                                    }
                                }
                                else if (Type == JobType.Timming)
                                {
                                    // Reset run
                                    if ((int)DateTime.Now.TimeOfDay.TotalSeconds <= 1)
                                    {
                                        IsRunToday = false;
                                    }
                                    // Check it is in Running time
                                    if (!IsRunToday && ((int)DateTime.Now.TimeOfDay.TotalSeconds - IntervalOrTimmingSeconds) > 0 && ((int)DateTime.Now.TimeOfDay.TotalSeconds - IntervalOrTimmingSeconds) < 5)
                                    {
                                        ExcuteJobMethod?.Invoke();
                                        IsRunToday = true;
                                    }
                                }
                                else if (Type == JobType.OnlyOne)
                                {
                                    ExcuteJobMethod?.Invoke();
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                OnExceptionOnRunning?.Invoke(ex);
                                LogHelper.GetLogger().Error(ex.ToString());
                            }
                            finally
                            {
                                if(Type == JobType.OnlyOne)
                                {
                                    JobList.Remove(this);
                                }
                            }
                        }

                        Thread.Sleep(1000); // wait 1 seconds
                    }
                }));
                thRun = thread;
                thRun.IsBackground = true;
                thRun.Start();
            }
        }
    }
}
