using System.Collections.Specialized;
using Common.Logging;
using NUnit.Framework;

namespace Quartz.Impl.MongoDB.Tests.UnitTests
{
    public class AbstractMongoJobStoreTests
    {
        protected const bool ClearJobs = true;
        ILoggerFactoryAdapter _oldAdapter;
        protected const bool ScheduleJobs = true;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            // set Adapter to report problems
            _oldAdapter = LogManager.Adapter;
            LogManager.Adapter = new FailFastLoggerFactoryAdapter();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            // default back to old
            LogManager.Adapter = _oldAdapter;
        }

        protected NameValueCollection BuildProperties()
        {
            var properties = new NameValueCollection();

            properties["quartz.scheduler.instanceName"] = "TestScheduler";
            properties["quartz.scheduler.instanceId"] = "instance_one";
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "10";
            properties["quartz.threadPool.threadPriority"] = "Normal";
            properties["quartz.jobStore.misfireThreshold"] = "60000";
            properties["quartz.jobStore.type"] = "Quartz.Impl.MongoDB.JobStore, Quartz.Impl.MongoDB";
            return properties;
        }
    }
}