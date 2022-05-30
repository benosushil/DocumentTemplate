using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentTemplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentTemplateController : ControllerBase
    {
        static Random random = new Random();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DocumentTemplateController> _logger;
        private readonly TemplateService _templateService;

        public DocumentTemplateController(ILogger<DocumentTemplateController> logger, TemplateService templateService)
        {
            _logger = logger;
            _templateService = templateService;
        }

        [HttpGet, Route("{documentId}")]
        public Template Get(string documentId)
        {
            var rng = HexaGenerator(24);
            var template = default(Template);
            try
            {
                template = _templateService.Get(documentId);
            }
            catch (Exception ex)
            {
                return null;
            }
            return template;
        }

        [HttpPost]
        public string Upsert([FromBody] Template document)
        {
            var documentId = HexaGenerator(24);
            if (document.Id == null)
            {

                _templateService.Create(new Template
                {
                    Id = documentId,
                    Json = document.Json
                });
                return documentId;
            }
            else
            {
                _templateService.Update(document.Id, new Template
                {
                    Id = document.Id,
                    Json = document.Json
                });
            }
            return document.Id;
        }

        private string HexaGenerator(int digit)
        {
            var result = string.Empty;
            for (int i = 0; i < digit; i++)
            {
                result += random.Next(16).ToString("X");
            }
            return result;
        }
    }
}
