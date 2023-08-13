using BedeBackEndTestProject.Models;
using static BedeBackEndTestProject.Utilities.Properties;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BedeBackEndTestProject.Tests
{
    internal class GetBookTests : BaseStepsTest
    {
        [TestCase(TestName = "GET all books successfully")]
        public async Task GetAllBookingsIdsSuccessfully()
        {
            RestResponse restResponse = await actions.GetRequest(bedeBaseUrl + bedeBooksPath);
          
            restResponse.StatusCode.Should().Be(HttpStatusCode.OK); 
            restResponse.ContentType.Should().Be("application/json");
            restResponse.Should().NotBeNull();
        }

        [TestCase(TestName = "GET book by Id successfully")]
        public async Task GetBookByIdSuccessfully()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Description = "Test Description",
                Author = "Bratq Grim"
            };

            RestRequest restRequestCreateBook = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequestCreateBook.AddStringBody(jSonObject, DataFormat.Json);
            restRequestCreateBook.AddHeader("Accept", "application/json");
            restRequestCreateBook.AddHeader("Content-Type", "application/json");

            RestResponse responseCreatedBook = await restClient.ExecutePostAsync(restRequestCreateBook);

            BookModel responseCreatedBookString = JsonConvert.DeserializeObject<BookModel>(responseCreatedBook.Content.ToString());

            responseCreatedBook.StatusCode.Should().Be(HttpStatusCode.OK);
            int createdBookId = responseCreatedBookString.Id;

            RestResponse restResponse = await actions.GetRequest(bedeBaseUrl + bedeBooksPath + createdBookId);

            BookModel responseGetBookByIdString = JsonConvert.DeserializeObject<BookModel>(restResponse.Content.ToString());

            restResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            restResponse.ContentType.Should().Be("application/json");
            responseGetBookByIdString.Id.Should().Be(book.Id);
            responseGetBookByIdString.Title.Should().Be(book.Title);          
            responseGetBookByIdString.Description.Should().Be(book.Description);
            responseGetBookByIdString.Author.Should().Be(book.Author);
        }

        [TestCase(TestName = "GET book by Title successfully")]
        public async Task GetBookByTitleSuccessfully()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = "Dexter's Laboratory",
                Description = "Test Description",
                Author = "Bratq Grim"
            };

            RestRequest restRequestCreateBook = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequestCreateBook.AddStringBody(jSonObject, DataFormat.Json);
            restRequestCreateBook.AddHeader("Accept", "application/json");
            restRequestCreateBook.AddHeader("Content-Type", "application/json");

            RestResponse responseCreatedBook = await restClient.ExecutePostAsync(restRequestCreateBook);

            BookModel responseCreatedBookString = JsonConvert.DeserializeObject<BookModel>(responseCreatedBook.Content.ToString());

            responseCreatedBook.StatusCode.Should().Be(HttpStatusCode.OK);
            string createdBookTitle = responseCreatedBookString.Title;
            string queryParam = "title";

            RestRequest restRequestGetBookByTitle = actions.AddQueryParameterToGetRequest(queryParam, createdBookTitle, new RestRequest(bedeBaseUrl + bedeBooksPath, Method.Get));

            RestResponse restResponse = await restClient.ExecuteAsync(restRequestGetBookByTitle);

            BookModel[] responseGetBooksByTitleArray = JsonConvert.DeserializeObject<BookModel[]>(restResponse.Content.ToString());

            restResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            foreach (var matchingBook in responseGetBooksByTitleArray)
            {
                matchingBook.Title.Should().Contain(createdBookTitle);
            }
        }

        [TestCase(TestName = "GET book by not existing Title")]
        public async Task GetBookByNotExistingTitle()
        {
            
            string createdBookTitle = "habibi";

            string queryParam = "title";

            RestRequest restRequestGetBookByTitle = actions.AddQueryParameterToGetRequest(queryParam, createdBookTitle, new RestRequest(bedeBaseUrl + bedeBooksPath, Method.Get));

            RestResponse restResponse = await restClient.ExecuteAsync(restRequestGetBookByTitle);

            restResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            restResponse.ContentType.Should().Be("application/json");
            restResponse.Content.Should().Be("[]");
        }

    }
}

