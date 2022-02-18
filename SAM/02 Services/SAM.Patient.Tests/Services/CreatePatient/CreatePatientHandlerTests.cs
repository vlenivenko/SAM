using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SAM.Core.CQRS.Validation.Interfaces;
using SAM.Patient.Services.Commands.CreatePatient;
using SAM.Repository.Repositories.Interfaces;

namespace SAM.Patient.Tests.Services.CreatePatient
{
    public class CreatePatientHandlerTests
    {
        private MockRepository _mockRepository;

        private Mock<IRepository> _repository;
        private Mock<IMapper> _mapper;
        private Mock<IRequestValidator<CreatePatientRequest, CreatePatientResponse>> _validator;

        private CreatePatientHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);

            _repository = _mockRepository.Create<IRepository>();
            _mapper = _mockRepository.Create<IMapper>();
            _validator = _mockRepository.Create<IRequestValidator<CreatePatientRequest, CreatePatientResponse>>();

            _handler = new CreatePatientHandler(
                _repository.Object,
                _mapper.Object,
                _validator.Object);
        }

        [Test]
        public async Task HandleValidatedRequestAsync_Valid()
        {
            // arrange
            var request = new CreatePatientRequest
            {
                FirstName = "Vladislav",
                LastName = "Lenivenko",
                DateOfBirth = new DateOnly(1989, 08, 10),
                DiseaseType = "Allergy",
            };

            var patient = new SAM.Repository.Models.Patient
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                DiseaseType = request.DiseaseType,
            };

            _mapper.Setup(x =>
                    x.Map<SAM.Repository.Models.Patient>(request))
                .Returns(patient);

            // act
            var response = await _handler.HandleValidatedRequestAsync(request);

            // assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);

            _mapper.Verify(
                x => x.Map<SAM.Repository.Models.Patient>(request),
                Times.Once);

            _repository.Verify(
                x => x.AddAsync(It.IsAny<SAM.Repository.Models.Patient>()),
                Times.Once);

            _repository.Verify(
                x => x.CommitAsync(),
                Times.Once);
        }
    }
}
