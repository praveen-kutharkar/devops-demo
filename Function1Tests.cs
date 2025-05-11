using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace devops_demo.Tests
{
    public class Function1Tests
    {
        private readonly Mock<ILogger<Function1>> _mockLogger;
        private readonly Function1 _function;

        public Function1Tests()
        {
            _mockLogger = new Mock<ILogger<Function1>>();
            _function = new Function1(_mockLogger.Object);
        }

        [Fact]
        public void Run_ReturnsOkObjectResult_WithExpectedMessage()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            var expectedMessagePart = "Welcome to Azure Functions!, run-4 CICD test-4";

            // Act
            var result = _function.Run(mockHttpRequest.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnMessage = Assert.IsType<string>(okResult.Value);
            Assert.Contains(expectedMessagePart, returnMessage);
            Assert.Contains("Current Server Time is:", returnMessage);
        }

        [Fact]
        public void Run_LogsInformationMessage()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            _function.Run(mockHttpRequest.Object);

            // Assert
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("C# HTTP trigger function processed a request.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
