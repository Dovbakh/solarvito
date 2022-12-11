using Minio;
using Minio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Solarvito.Infrastructure.ObjectStorage
{
    public class MinioStorage : IObjectStorage
    {
        public async Task<byte[]> Create()
        {
            var endpoint = "127.0.0.1:9000";
            var accessKey = "7d0OSrPdBpU0Iabo";
            var secretKey = "53jc7XIX5o5BfqxhE2ToCBJQcZCiu80f";


            // Make a new bucket called mymusic.
            var bucketName = "mymusic-folder"; //<==== change this
            var location = "us-east-1";
            // Upload the zip file
            var objectName = "print2.png";
            // The following is a source file that needs to be created in
            // your local filesystem.
            var filePath = "D:\\print3.png";
            var contentType = "image/png";

            try
            {
                var minio = new MinioClient()
                    .WithEndpoint(endpoint)
                    .WithCredentials(accessKey, secretKey)
                    .Build();


                //            var bktExistArgs = new BucketExistsArgs()
                //.WithBucket(bucketName);
                //            var found = await minio.BucketExistsAsync(bktExistArgs);
                //            if (!found)
                //            {
                //                var mkBktArgs = new MakeBucketArgs()
                //                    .WithBucket(bucketName)
                //                    .WithLocation(location);
                //                await minio.MakeBucketAsync(mkBktArgs);
                //            }

                //var putObjectArgs = new PutObjectArgs()
                //    .WithBucket(bucketName)
                //    .WithObject(objectName)
                //    .WithFileName(filePath)
                //    .WithContentType(contentType);
                //await minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                MemoryStream memoryStream = new MemoryStream();

                var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithCallbackStream((stream) => stream.CopyTo(memoryStream));
                //.WithFile(filePath);
                var stat = await minio.GetObjectAsync(args);

                

                return memoryStream.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            


            


        }
    }
}
