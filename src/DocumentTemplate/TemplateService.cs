using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DocumentTemplate
{
    public class TemplateService
    {
        private readonly IMongoCollection<Template> _template;

        public TemplateService(ITemplateDBSettings settings)
        {
            var connString = settings.ConnectionString;
            var atlasSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            atlasSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(atlasSettings);

            var dbs = client.ListDatabaseNames().ToList();

            foreach (var db in dbs)
            {
                Console.WriteLine(db);
            }

            var database = client.GetDatabase(settings.DatabaseName);

            _template = database.GetCollection<Template>(settings.CollectionName);
        }

        public List<Template> Get() =>
            _template.Find(template => true).ToList();

        public Template Get(string id) =>
            _template.Find<Template>(template => template.Id == id).FirstOrDefault();

        public Template Create(Template template)
        {
            _template.InsertOne(template);
            return template;
        }

        public void Update(string id, Template templateIn) =>
            _template.ReplaceOne(template => template.Id == id, templateIn);

        public void Remove(Template templateIn) =>
            _template.DeleteOne(template => template.Id == templateIn.Id);

        public void Remove(string id) =>
            _template.DeleteOne(template => template.Id == id);
    }
}
