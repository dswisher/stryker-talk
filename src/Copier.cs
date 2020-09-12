
using System.Threading;
using System.Threading.Tasks;

using Amazon.S3;
using Amazon.S3.Model;

namespace Lightning
{
    public class Copier
    {
        private readonly IAmazonS3 s3;
        private readonly string liveBucketName;

        public Copier(IAmazonS3 s3)
        {
            this.s3 = s3;

            liveBucketName = "live-bucket";
        }


        public async Task Copy(string key, int age, CancellationToken stoppingToken)
        {
            string archiveBucketName;
            if (age > 30)
            {
                archiveBucketName = "glacier-bucket";
            }
            else
            {
                archiveBucketName = "normal-bucket";
            }

            var copyRequest = new CopyObjectRequest()
            {
                SourceBucket = liveBucketName,
                SourceKey = key,
                DestinationBucket = archiveBucketName,
                DestinationKey = key,
            };

            await s3.CopyObjectAsync(copyRequest, stoppingToken);
        }
    }
}
