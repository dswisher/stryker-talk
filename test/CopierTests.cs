
using System.Threading;
using System.Threading.Tasks;

using Amazon.S3;
using Amazon.S3.Model;
using FluentAssertions;
using Moq;
using Xunit;

namespace Lightning.Test
{
    public class CopierTests
    {
        private const string Key = "my/fun/file.zip";

        private readonly Mock<IAmazonS3> s3 = new Mock<IAmazonS3>();
        private readonly CancellationToken stoppingToken = default(CancellationToken);

        private readonly Copier copier;

        private CopyObjectRequest copyRequest;


        public CopierTests()
        {
            copier = new Copier(s3.Object);
        }


        [Theory]
        [InlineData(2)]
        public async Task NewFileCopiedToNormalBucket(int age)
        {
            // Arrange
            // Capture the request, so we can assert on it
            s3.Setup(x => x.CopyObjectAsync(It.IsAny<CopyObjectRequest>(), stoppingToken))
                .Callback<CopyObjectRequest, CancellationToken>((req, tok) => copyRequest = req);

            // Act
            await copier.Copy(Key, age, stoppingToken);

            // Assert
            s3.Verify(x => x.CopyObjectAsync(It.IsAny<CopyObjectRequest>(), stoppingToken));
        }


        [Theory]
        [InlineData(90)]
        public async Task OldFileCopiedToGlacierBucket(int age)
        {
            // Arrange
            // Capture the request, so we can assert on it
            s3.Setup(x => x.CopyObjectAsync(It.IsAny<CopyObjectRequest>(), stoppingToken))
                .Callback<CopyObjectRequest, CancellationToken>((req, tok) => copyRequest = req);

            // Act
            await copier.Copy(Key, age, stoppingToken);

            // Assert
            s3.Verify(x => x.CopyObjectAsync(It.IsAny<CopyObjectRequest>(), stoppingToken));
        }
    }
}
