using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly IConfiguration _configuration;

        private const string MinioAccessName = "MinioFileStorage";

        public MinioStorage(IConfiguration configuration)
        {
            _configuration = configuration;
            _storage = CreateStorage();
        }

        public async Task<byte[]> Get(string objectName, string bucketName)
        {
            var bktExistArgs = new BucketExistsArgs().WithBucket(bucketName);
            var found = await _storage.BucketExistsAsync(bktExistArgs);
            if (!found)
            {
                throw new DirectoryNotFoundException("Не найдена директория.");
            }

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
            var bktExistArgs = new BucketExistsArgs().WithBucket(bucketName);
            var found = await _storage.BucketExistsAsync(bktExistArgs);
            if (!found)
            {
                var mkBktArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await _storage.MakeBucketAsync(mkBktArgs);
            }

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
            var bktExistArgs = new BucketExistsArgs().WithBucket(bucketName);
            var found = await _storage.BucketExistsAsync(bktExistArgs);
            if (!found)
            {
                throw new DirectoryNotFoundException("Директория не найдена.");
            }

            var args = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            await _storage.RemoveObjectAsync(args);           
        }

        private MinioClient CreateStorage()
        {
            var endpoint = _configuration.GetSection(MinioAccessName).GetRequiredSection("Endpoint").Value;
            var accessKey = _configuration.GetSection(MinioAccessName).GetRequiredSection("AccessKey").Value;
            var secretKey = _configuration.GetSection(MinioAccessName).GetRequiredSection("SecretKey").Value;

            var minioCLient = new MinioClient();
            minioCLient.WithEndpoint(_configuration.GetSection(MinioAccessName).GetRequiredSection("Endpoint").Value)
                    .WithCredentials(accessKey, secretKey)
                    .Build();

            return minioCLient;
        }

    }
}