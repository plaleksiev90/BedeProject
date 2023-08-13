using BedeBackEndTestProject.Models;
using NUnit.Framework;
using RestSharp;
using System;
using static BedeBackEndTestProject.Utilities.Properties;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;

namespace BedeBackEndTestProject.Tests
{
    internal class DeleteBookTests : BaseStepsTest
    {
        [TestCase(TestName = "Delete book by Id successfully")]
        public async Task DeleteBookByIdSuccesfully()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Description = "Test Description",
                Author = "Bratq Grim"
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBook.Content.ToString());

            responseBook.StatusCode.Should().Be(HttpStatusCode.OK);

            int responseBookId = responseString.Id;

            RestRequest restDeleteRequest = new(bedeBaseUrl + bedeBooksPath + responseBookId, Method.Delete);

            RestResponse deleteResponseBook = await restClient.ExecuteAsync(restDeleteRequest);

            deleteResponseBook.StatusCode.Should().Be(HttpStatusCode.NoContent);

        }

        [TestCase(TestName = "Delete book by not existing Id Bad Request")]
        public async Task DeleteBookByNotExistingIdBadRequest()
        {
            int notExistingBookId = 12345;
            string deleteBookNotFound = "Book with id " + notExistingBookId + " not found!";

            RestRequest restDeleteRequest = new(bedeBaseUrl + bedeBooksPath + notExistingBookId, Method.Delete);

            RestResponse deleteResponseBook = await restClient.ExecuteAsync(restDeleteRequest);

            deleteResponseBook.StatusCode.Should().Be(HttpStatusCode.NotFound);
            deleteResponseBook.Content.Should().Contain(deleteBookNotFound);

        }

    }
}
