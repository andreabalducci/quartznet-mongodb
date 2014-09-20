using System;
using System.Collections.Specialized;
using Common.Logging;
using NUnit.Framework;
using Quartz.Tests.Integration.Impl;

namespace Quartz.Impl.MongoDB.Tests.UnitTests
{
    [System.ComponentModel.Category("integration")]
    [TestFixture]
    public class MongoDbSmokeTests
    {
        private bool clearJobs = true;
        private bool scheduleJobs = true;
        private bool clustered = true;
        private ILoggerFactoryAdapter oldAdapter;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            // set Adapter to report problems
            oldAdapter = LogManager.Adapter;
            LogManager.Adapter = new FailFastLoggerFactoryAdapter();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            // default back to old
            LogManager.Adapter = oldAdapter;
        }

        [Test]
        public void RunMongoDbSmokeTests()
        {
            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory(BuildProperties());
            IScheduler sched = sf.GetScheduler();
            SmokeTestPerformer performer = new SmokeTestPerformer();
            performer.Test(sched, clearJobs, scheduleJobs);

            Assert.IsEmpty(FailFastLoggerFactoryAdapter.Errors, "Found error from logging output");
        }


        private NameValueCollection BuildProperties()
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
