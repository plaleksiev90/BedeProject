using BedeBackEndTestProject.Models;
using NUnit.Framework;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static BedeBackEndTestProject.Utilities.Properties;
using RestSharp;
using FluentAssertions;
using System;
using System.Net;
using System.Collections.Generic;

namespace BedeBackEndTestProject.Tests
{
    internal class CreateBookTests : BaseStepsTest
    {
        [TestCase(TestName = "Create book successfully")]
        public async Task PostBookSuccesfully()
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
            responseString.Id.Should().Be(book.Id);
            responseString.Title.Should().Be(book.Title);
            responseString.Description.Should().Be(book.Description);
            responseString.Author.Should().Be(book.Author);      
        }

        [TestCase(TestName = "Create book with existing Id")]
        public async Task PostBookWithExistingIdSuccesfully()
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

            responseBook.StatusCode.Should().Be(HttpStatusCode.OK);

            RestRequest restRequestExistingId = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBookExistingId = await restClient.ExecutePostAsync(restRequestExistingId);

            responseBookExistingId.StatusCode.Should().Be(HttpStatusCode.Conflict);

        }

        [TestCase(TestName = "Create more than one book")]
        public async Task PostBooksBadRequest()
        {
            int uniqueIdOne = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);
            int uniqueIdTwo = (int)(DateTime.UtcNow - new DateTime(1971, 2, 2)).TotalSeconds + new Random().Next(1000, 9999);

            List<BookModel> books = new List<BookModel>
            {
                new BookModel
                {
                    Id = uniqueIdOne,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    Author = "Author 1"
                },
                new BookModel
                {
                    Id = uniqueIdTwo,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    Author = "Author 2"
                }
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(books);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            responseBook.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestCase(TestName = "Create book without Id")]
        public async Task PostBookWithoudIdBadRequest()
        {
            BookModel book = new BookModel
            {
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

            responseBook.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBook.Content.Should().Contain(createBookErrorMessageNoId);
        }

        [TestCase(TestName = "Create book without author")]
        public async Task PostBookWithoutAuthorBadRequest()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Description = "Test Description"
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            responseBook.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBook.Content.Should().Contain(createBookErrorMessageNoAuthor);
        }

        [TestCase(TestName = "Create book without title")]
        public async Task PostBookWithoutTitleBadRequest()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Description = "Test Description",
                Author = "Bratq Grim"
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            responseBook.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBook.Content.Should().Contain(createBookErrorMessageNoTitle);
        }

        [TestCase(TestName = "Create book without description")]
        public async Task PostBookWithoutDescriptionSuccessfully()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
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
            responseString.Id.Should().Be(book.Id);
            responseString.Title.Should().Be(book.Title);
            responseString.Description.Should().Be(null);
            responseString.Author.Should().Be(book.Author);
        }

        [TestCase(TestName = "Create book with 0 for Id")]
        public async Task PostBookWithZeroForIdBadRequest()
        {

            BookModel book = new BookModel
            {
                Id = 0,
                Title = "Dear Me",
                Author = "Bratq Grim"
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            responseBook.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBook.Content.Should().Contain(createBookErrorMessageNoId);
        }

        [TestCase(TestName = "Create book with negative value for Id")]
        public async Task PostBookWithNegativeValueForIdBadRequest()
        {

            BookModel book = new BookModel
            {
                Id = -1,
                Title = "Dear Me",
                Author = "Bratq Grim"
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            responseBook.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBook.Content.Should().Contain(createBookErrorMessageNoId);
        }

        [TestCase(TestName = "Create book with Max length for Author")]
        public async Task PostBookWithMaxLengthForAuthorSuccessfully()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);
            string maxLengthString = "dijasdijdsajijajdijdsaijdpijdd";

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Author = maxLengthString
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBook.Content.ToString());

            responseBook.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Id.Should().Be(book.Id);
            responseString.Title.Should().Be(book.Title);
            responseString.Description.Should().Be(null);
            responseString.Author.Should().Be(book.Author);
        }

        [TestCase(TestName = "Create book with more than Max length for Author")]
        public async Task PostBookWithMoreThanMaxLengthForAuthorBadRequest()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);
            string overMaxLengthString = "dijasdijdsajijajdijdsaijdpijdda";

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Author = overMaxLengthString
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            responseBook.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBook.Content.Should().Contain(createBookErrorMessageMaxLengthForAuthor);
        }

        
        [TestCase(TestName = "Create book with Max length for Title")]
        public async Task PostBookWithMaxLengthForTitleBadRequest()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);
            string maxLengthString = "dijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddafsfghgs";

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = maxLengthString,
                Author = "Test Author"
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBook.Content.ToString());

            responseBook.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Id.Should().Be(book.Id);
            responseString.Title.Should().Be(book.Title);
            responseString.Description.Should().Be(null);
            responseString.Author.Should().Be(book.Author);
        }

        [TestCase(TestName = "Create book with more than Max length for Title")]
        public async Task PostBookWithMoreThanMaxLengthForTitleBadRequest()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);
            string overMaxLengthString = "dijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddafsfghgsa";

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = overMaxLengthString,
                Author = "Valeri Bojinov"
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            responseBook.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBook.Content.Should().Contain(createBookErrorMessageMaxLengthForTitle);
        }

        [TestCase(TestName = "Create book with big string for Description")]
        public async Task PostBookWithBigStringForDescriptionBadRequest()
        {
            int uniqueId = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);
            string bigString = "dijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddafsfghgsadijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddafsfghgsadijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddafsfghgsa";

            BookModel book = new BookModel
            {
                Id = uniqueId,
                Title = "Some Title",
                Author = "Valeri Bojinov",
                Description = bigString
            };

            RestRequest restRequest = new(bedeBaseUrl + bedeBooksPath, Method.Post);

            string jSonObject = JsonConvert.SerializeObject(book);

            restRequest.AddStringBody(jSonObject, DataFormat.Json);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            RestResponse responseBook = await restClient.ExecutePostAsync(restRequest);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBook.Content.ToString());

            responseBook.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Id.Should().Be(book.Id);
            responseString.Title.Should().Be(book.Title);
            responseString.Description.Should().Be(book.Description);
            responseString.Author.Should().Be(book.Author);
        }
    }
}
