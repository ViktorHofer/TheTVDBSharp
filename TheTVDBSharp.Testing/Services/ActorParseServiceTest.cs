﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Testing.Services
{
    [TestClass]
    public class ActorParseServiceTest
    {
        private readonly IActorParseService actorParseService = new ActorParseService();

        [TestMethod]
        public void Parse_Actors_76156_Test()
        {
            var actorCollectionRaw = SampleDataHelper.Open(SampleDataHelper.SampleData.SeriesFull_76156_Actors);
            var actors = actorParseService.Parse(actorCollectionRaw);

            Assert.IsNotNull(actors);
            Assert.AreEqual(18, actors.Count);
            Assert.AreEqual((uint)43640, actors.First().Id);
            Assert.AreEqual("Zach Braff", actors.First().Name);
            Assert.AreEqual(0, actors.First().SortOrder);
        }
    }
}