﻿using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SAM.Core.CQRS.Validation.Interfaces;
using SAM.Repository.Repositories.Interfaces;
using SAM.Search.Services.Commands.CreateSearch;

namespace SAM.Search.Tests.Services.Commands.CreateSearch
{
    [TestFixture]
    public class CreateSearchHandlerTests
    {
        private MockRepository _mockRepository;

        private Mock<IRepository> _repository;
        private Mock<IMapper> _mapper;
        private Mock<IRequestValidator<CreateSearchRequest, CreateSearchResponse>> _validator;

        private CreateSearchHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);

            _repository = _mockRepository.Create<IRepository>();
            _mapper = _mockRepository.Create<IMapper>();
            _validator = _mockRepository.Create<IRequestValidator<CreateSearchRequest, CreateSearchResponse>>();

            _handler = new CreateSearchHandler(
                _repository.Object,
                _mapper.Object,
                _validator.Object);
        }

        [Test]
        public async Task HandleValidatedRequestAsync_ValidRequest_SuccessResponse()
        {
            // arrange
            var request = new CreateSearchRequest
            {
                PatientId = 1,
                MatchEngineId = 1,
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

            // act
            var response = await _handler.HandleValidatedRequestAsync(request);

            // assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);

            _repository.Verify(
                x => x.GetByIdAsync<SAM.Repository.Models.Patient>(request.PatientId),
                Times.Once);

            _repository.Verify(
                x => x.CommitAsync(),
                Times.Never);
        }

        [Test]
        public async Task HandleValidatedRequestAsync_InvalidPatientId_FailedValidResponse()
        {
            // arrange
            var request = new CreateSearchRequest
            {
                PatientId = 1,
                MatchEngineId = 1,
            };

            _repository.Setup(x =>
                    x.GetByIdAsync<SAM.Repository.Models.Patient>(request.PatientId))
                .ReturnsAsync((SAM.Repository.Models.Patient)null);

            // act
            // assert
            await _handler.Invoking(x => x.HandleValidatedRequestAsync(request))
                .Should().ThrowAsync<ArgumentException>();

            _repository.Verify(
                x => x.GetByIdAsync<SAM.Repository.Models.Patient>(request.PatientId),
                Times.Once);

            _repository.Verify(
                x => x.CommitAsync(),
                Times.Never);
        }
    }
}
