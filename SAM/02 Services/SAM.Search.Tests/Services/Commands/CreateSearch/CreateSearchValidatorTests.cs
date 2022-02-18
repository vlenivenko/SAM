using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SAM.Repository.Repositories.Interfaces;
using SAM.Search.SearchEngines;
using SAM.Search.SearchEngines.Clients;
using SAM.Search.Services.Commands.CreateSearch;

namespace SAM.Search.Tests.Services.Commands.CreateSearch
{
    [TestFixture]
    public class CreateSearchValidatorTests
    {
        private MockRepository _mockRepository;

        private Mock<IRepository> _repository;
        private Mock<ISearchEngineFactory> _searchEngineFactory;

        private CreateSearchValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);

            _repository = _mockRepository.Create<IRepository>();
            _searchEngineFactory = _mockRepository.Create<ISearchEngineFactory>();

            _validator = new CreateSearchValidator(
                _repository.Object,
                _searchEngineFactory.Object);
        }

        [Test]
        public async Task Validate_RequestIsValid_ValidationSuccess()
        {
            // arrange
            var request = new CreateSearchRequest
            {
                PatientId = 1,
                MatchEngineId = Enums.MatchEngineType.MatchEngine1,
            };

            var patient = new SAM.Repository.Models.Patient
            {
                Id = request.PatientId,
                FirstName = "Vladislav",
                LastName = "Lenivenko",
                DateOfBirth = new DateOnly(1989, 08, 10),
                DiseaseType = "Allergy",
            };

            _repository.Setup(x =>
                    x.GetByIdAsync<SAM.Repository.Models.Patient>(request.PatientId))
                .ReturnsAsync(patient);

            var searchEngine = _mockRepository.Create<ISearchEngineClient>();
            _searchEngineFactory.Setup(x =>
                    x.GetClient(request.MatchEngineId))
                .Returns(searchEngine.Object);

            // act
            var result = await _validator.ValidateAsync(request);

            // assert
            result.PassedValidation.Should().BeTrue();
        }

        [Test]
        public async Task Validate_PatientDoesNotExist_ValidationFailed()
        {
            // arrange
            var request = new CreateSearchRequest
            {
                PatientId = 1,
                MatchEngineId = Enums.MatchEngineType.MatchEngine1,
            };

            // act
            var result = await _validator.ValidateAsync(request);

            // assert
            result.PassedValidation.Should().BeFalse();
            result.Response.ErrorResponse.Error.Message.Should().Be(CreateSearchValidator.PatientDoesNotExistErrorMessage);
        }

        [Test]
        public async Task Validate_SearchEngineDoesNotExist_ValidationFailed()
        {
            // arrange
            var request = new CreateSearchRequest
            {
                PatientId = 1,
                MatchEngineId = Enums.MatchEngineType.MatchEngine1,
            };

            var patient = new SAM.Repository.Models.Patient
            {
                Id = request.PatientId,
                FirstName = "Vladislav",
                LastName = "Lenivenko",
                DateOfBirth = new DateOnly(1989, 08, 10),
                DiseaseType = "Allergy",
            };

            _repository.Setup(x =>
                    x.GetByIdAsync<SAM.Repository.Models.Patient>(request.PatientId))
                .ReturnsAsync(patient);

            _searchEngineFactory.Setup(x =>
                    x.GetClient(request.MatchEngineId))
                .Throws(new NotImplementedException());

            // act
            var result = await _validator.ValidateAsync(request);

            // assert
            result.PassedValidation.Should().BeFalse();
            result.Response.ErrorResponse.Error.Message.Should().Be(CreateSearchValidator.SearchEngineDoesNotExistErrorMessage);
        }
    }
}
