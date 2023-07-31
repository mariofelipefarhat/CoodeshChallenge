global using NUnit.Framework;
using Coodesh.Application.Commands;
using Coodesh.Infrastructure.Models.Transaction;
using Coodesh.Infrastructure.Persistence.Transaction;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Text;

namespace Coodesh.Tests.Commands;

[TestFixture]
public class CreateTransactionCommandHandlerTests
{
    private Mock<ITransactionRepository> _transactionRepositoryMock;
    private Mock<IValidator<CreateTransactionCommand>> _validatorMock;
    private CreateTransactionCommandHandler _commandHandler;

    [SetUp]
    public void Setup()
    {
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _validatorMock = new Mock<IValidator<CreateTransactionCommand>>();
        _commandHandler = new CreateTransactionCommandHandler(_transactionRepositoryMock.Object, _validatorMock.Object);
    }

    [Test]
    public void Handle_ValidCommandWithValidData_ExecutesSuccessfully()
    {
        var formFileMock = new Mock<IFormFile>();
        var fileBytes = Encoding.UTF8.GetBytes(GetSampleData());
        formFileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(fileBytes));
        formFileMock.Setup(f => f.Length).Returns(fileBytes.Length);
        formFileMock.Setup(f => f.FileName).Returns("SampleData.txt");
        formFileMock.Setup(f => f.ContentType).Returns("text/plain");

        var command = new CreateTransactionCommand(formFileMock.Object);

        _validatorMock.Setup(validator => validator.ValidateAsync(command, default))
            .ReturnsAsync(new ValidationResult());

        var result = _commandHandler.Handle(command, default).Result;

        Assert.That(result.IsError, Is.False);
        _transactionRepositoryMock.Verify(repository => repository.AddRange(It.IsAny<List<TransactionModel>>()), Times.Once);
        _transactionRepositoryMock.Verify(repository => repository.SaveChanges(), Times.Once);
    }

    [Test]
    public void Handle_InvalidCommand_ReturnsValidationErrors()
    {
        var formFileMock = new Mock<IFormFile>();
        var fileBytes = Encoding.UTF8.GetBytes(GetSampleData());
        formFileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(fileBytes));
        formFileMock.Setup(f => f.Length).Returns(fileBytes.Length);
        formFileMock.Setup(f => f.FileName).Returns("SampleData.txt");
        formFileMock.Setup(f => f.ContentType).Returns("text/plain");

        var command = new CreateTransactionCommand(formFileMock.Object);

        var validationErrors = new List<ValidationFailure>
        {
            new ValidationFailure("Error1", "Validation Error 1"),
            new ValidationFailure("Error2", "Validation Error 2")
        };

        var validationResult = new FluentValidation.Results.ValidationResult(validationErrors);

        _validatorMock.Setup(validator => validator.ValidateAsync(command, default))
            .ReturnsAsync(validationResult);

        var result = _commandHandler.Handle(command, default).Result;
        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.True);
            Assert.That(result.Errors, Has.Count.EqualTo(validationErrors.Count));
        });
    }

    [Test]
    public void Handle_EmptyCommand_ReturnsValidationError()
    {// Passing null as IFormFile to simulate an empty command.
        var command = new CreateTransactionCommand(null); 

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Command", "Command cannot be null or empty.")
        });

        _validatorMock.Setup(validator => validator.ValidateAsync(command, default))
            .ReturnsAsync(validationResult);

        var result = _commandHandler.Handle(command, default).Result;

        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.True);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void Handle_InvalidDataInFile_ReturnsValidationErrors()
    {
        var formFileMock = new Mock<IFormFile>();
        var fileBytes = Encoding.UTF8.GetBytes("Invalid Data\nInvalid Line\n");
        formFileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(fileBytes));
        formFileMock.Setup(f => f.Length).Returns(fileBytes.Length);
        formFileMock.Setup(f => f.FileName).Returns("InvalidData.txt");
        formFileMock.Setup(f => f.ContentType).Returns("text/plain");

        var command = new CreateTransactionCommand(formFileMock.Object);

        var validationErrors = new List<ValidationFailure>
        {
            new ValidationFailure("Invalid Line Data Pattern", "Invalid Data"),
            new ValidationFailure("Invalid Line Data Pattern", "Invalid Line")
        };

        var validationResult = new FluentValidation.Results.ValidationResult(validationErrors);

        _validatorMock.Setup(validator => validator.ValidateAsync(command, default))
            .ReturnsAsync(validationResult);

        var result = _commandHandler.Handle(command, default).Result;
        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.True);
            Assert.That(result.Errors, Has.Count.EqualTo(validationErrors.Count));
        });
    }

    [Test]
    public void Handle_ValidCommandWithDifferentFileFormat_ReturnsValidationError()
    {
        var formFileMock = new Mock<IFormFile>();
        var fileBytes = Encoding.UTF8.GetBytes("Invalid Data Format");
        formFileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(fileBytes));
        formFileMock.Setup(f => f.Length).Returns(fileBytes.Length);
        formFileMock.Setup(f => f.FileName).Returns("InvalidFormat.csv"); // Assuming invalid format CSV file.
        formFileMock.Setup(f => f.ContentType).Returns("text/csv"); // Assuming invalid format CSV content type.

        var command = new CreateTransactionCommand(formFileMock.Object);

        var validationResult = new FluentValidation.Results.ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("File Format", "Only text/plain file format is allowed.")
        });

        _validatorMock.Setup(validator => validator.ValidateAsync(command, default))
            .ReturnsAsync(validationResult);

        var result = _commandHandler.Handle(command, default).Result;

        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.True);
            Assert.That(result.Errors, Has.Count.EqualTo(1));
        });
    }

    private static string GetSampleData()
    {
        return File.Exists("./Files/sales.txt") ? File.ReadAllText("./Files/sales.txt", Encoding.UTF8) :
            throw new FileNotFoundException("sales.txt not found.");
    }
}