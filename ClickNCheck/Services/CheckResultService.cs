using ClickNCheck.Data;
using ClickNCheck.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public class CheckResultService
    {
       
        private readonly ClickNCheckContext _context;
        private readonly UploadService _uploadService;

        public CheckResultService(ClickNCheckContext context, UploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        //THe function below will save the results to azure blobs
        public async Task<Results> SaveResult(int CheckID, string resultStatus, string resultDescription, byte[] resultFiles)
        {
            var checkResult = _context.Result.FirstOrDefault();
            checkResult.ServicesID = CheckID;
            checkResult.CheckStatus.name=resultStatus;
            checkResult.resultDescription = resultDescription;
            string resultFilesURL = "";

            foreach (var files in resultFiles)
            {
                if (resultFiles.Length <= 0)
                {
                    continue;
                }

                using (var ms = new MemoryStream(resultFiles))
                {
                    var fileBytes = ms.ToArray();
                    resultFilesURL = await _uploadService.UploadCheckResults(resultFiles.ToString(), fileBytes, null);
                }
            }

            checkResult.resultFilesURL = resultFilesURL; 
            _context.Result.Add(checkResult);

            return checkResult;
        }
    }
}
