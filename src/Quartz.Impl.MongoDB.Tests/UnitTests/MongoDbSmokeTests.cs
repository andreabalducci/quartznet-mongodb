using System;
using System.Collections.Specialized;
using Common.Logging;
using NUnit.Framework;
using Quartz.Tests.Integration.Impl;

namespace Quartz.Impl.MongoDB.Tests.UnitTests
{
    [TestFixture(Category = "integration")]
    public class MongoDbSmokeTests : AbstractMongoJobStoreTests
    {
        [Test]
        public void RunMongoDbSmokeTests()
        {
            // First we must get a reference to a scheduler
            var sf = new StdSchedulerFactory(BuildProperties());
            var sched = sf.GetScheduler();
            var performer = new SmokeTestPerformer();
            performer.Test(sched, ClearJobs, ScheduleJobs);

            Assert.IsEmpty(FailFastLoggerFactoryAdapter.Errors, "Found error from logging output");
        }
    }
}
