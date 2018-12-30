using DataProcessing.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.B2B.Query
{
    public class B2BSearchBlock : IB2BSearchBlock
    {
        public readonly IBusinessToBusinessRepository _businessToBusinessRepository;

        public B2BSearchBlock(IBusinessToBusinessRepository businessToBusinessRepository)
        {
            _businessToBusinessRepository = businessToBusinessRepository;
        }

        public SearchBlock BindSearchBlock()
        {
            var result = _businessToBusinessRepository.GetFilterBlocks();
            result.Wait();
            var filterOptions = result.Result;
            SearchBlock searchBlock = new SearchBlock()
            {
                Area = filterOptions.Area.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                BusinessCategory = filterOptions.BusinessCategory.Select(x => new SelectListItem()
                {
                    Value = $"{ x.Id }",
                    Text = x.Name
                }).AsEnumerable(),
                City = filterOptions.City.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                Country = filterOptions.Country.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                Desigination = filterOptions.Desigination.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                State = filterOptions.State.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable()
            };
            /*
            var client = new RestClient();
            client.BaseUrl = new Uri("http://localhost:5555/api/v1/resources/B2B/SearchBlock");
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");

            var request = new RestRequest();
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var response = client.Execute(request);
            RootObject myClass = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Two ways to get the result:
                string rawResponse = response.Content;
                myClass = new JsonDeserializer().Deserialize<RootObject>(response);
            }

            SearchBlock searchBlock = new SearchBlock()
            {
                Area = myClass.Area.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                BusinessCategory = myClass.Category.Select(x => new SelectListItem()
                {
                    Value = $"{ x.Id }",
                    Text = x.Value
                }).AsEnumerable(),
                City = myClass.City.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                Country = myClass.Country.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                Desigination = myClass.Designation.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable(),
                State = filterOptions.State.Select(x => new SelectListItem()
                {
                    Value = x,
                    Text = x
                }).AsEnumerable()
            };
            */
            return searchBlock;
        }
    }

    public class RootObject
    {
        public List<string> Area { get; set; }
        public List<Category> Category { get; set; }
        public List<string> City { get; set; }
        public List<string> Country { get; set; }
        public List<string> Designation { get; set; }
        public List<string> State { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}