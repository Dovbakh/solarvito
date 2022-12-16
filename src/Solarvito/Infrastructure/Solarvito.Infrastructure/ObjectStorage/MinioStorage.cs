using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Minio;
using Minio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reactive.Linq;
using System.Reflection.PortableExecutable;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Solarvito.Infrastructure.ObjectStorage
{  
    public class MinioStorage : IObjectStorage
    {
        protected MinioClient _storage { get; }

        string endpoint = "127.0.0.1:9000";
        string accessKey = "7d0OSrPdBpU0Iabo";
        string secretKey = "53jc7XIX5o5BfqxhE2ToCBJQcZCiu80f";

        public MinioStorage()
        {
            _storage = new MinioClient();
            _storage.WithEndpoint(endpoint)
                    .WithCredentials(accessKey, secretKey)
                    .Build();

        }

        public async Task CreateBucket(string bucketName)
        {
            var bktExistArgs = new BucketExistsArgs().WithBucket(bucketName);
            var found = await _storage.BucketExistsAsync(bktExistArgs);
            if (!found)
            {
                var mkBktArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await _storage.MakeBucketAsync(mkBktArgs);
            }
        }

        public async Task<byte[]> Get(string objectName, string bucketName)
        {
            await CreateBucket(bucketName);

            MemoryStream memoryStream = new MemoryStream();

            var args = new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithCallbackStream((stream) => stream.CopyTo(memoryStream));

            await _storage.GetObjectAsync(args);

            return memoryStream.ToArray();
        }

        public async Task Upload(string objectName, string bucketName, string contentType, byte[] bytes)
        {
            await CreateBucket(bucketName);

            using (var memoryStream = new MemoryStream(bytes))
            {

                var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(memoryStream)
                .WithObjectSize(memoryStream.Length)
                .WithContentType(contentType);

                await _storage.PutObjectAsync(args);
            } 
        }

        public async Task Delete(string objectName, string bucketName)
        {
            await CreateBucket(bucketName);

            var args = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            await _storage.RemoveObjectAsync(args);           
        }
    }
}