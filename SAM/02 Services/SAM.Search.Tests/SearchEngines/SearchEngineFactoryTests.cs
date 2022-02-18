using System;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SAM.Search.Enums;
using SAM.Search.SearchEngines;

namespace SAM.Search.Tests.SearchEngines
{
    [TestFixture]
    public class SearchEngineFactoryTests
    {
        private SearchEngineFactory _factory;

        [SetUp]
        public void SetUp()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);

            var mapper = mockRepository.Create<IMapper>();
            var logger = mockRepository.Create<ILogger<ISearchEngineFactory>>();

            _factory = new SearchEngineFactory(
                mapper.Object,
                logger.Object);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void Validate_MatchEngineTypeIsValid_InstanceCreated(byte matchEngineTypeId)
        {
            // arrange
            var matchEngineType = (MatchEngineType)matchEngineTypeId;

            // act
            var result = _factory.GetClient(matchEngineType);

            // assert
            result.Should().NotBeNull();
        }

        [Test]
        [TestCase(0)]
        [TestCase(byte.MinValue)]
        [TestCase(byte.MaxValue)]
        public void Validate_MatchEngineTypeIsInvalid_ExceptionThrown(byte matchEngineTypeId)
        {
            // arrange
            var matchEngineType = (MatchEngineType)matchEngineTypeId;

            // act
            // assert
            _factory.Invoking(x => x.GetClient(matchEngineType))
                .Should().Throw<NotImplementedException>();
        }
    }
}
