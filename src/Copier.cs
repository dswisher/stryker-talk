
using System;
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


        public async Task Copy(string key, DateTime lastModified, CancellationToken stoppingToken)
        {
            string archiveBucketName;
            if (DateTime.UtcNow - lastModified > TimeSpan.FromDays(30))
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
