using static BedeBackEndTestProject.Utilities.Properties;
using BedeBackEndTestProject.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BedeBackEndTestProject.Tests
{
    internal class UpdateBookTests : BaseStepsTest
    {
        [TestCase(TestName = "Update book Id")]
        public async Task PutBookIdBadRequest()
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

            int uniqueIdUpdate = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + new Random().Next(1000, 9999);

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueIdUpdate,
                Title = "Dear Me Update",
                Description = "Test Description Update",
                Author = "Bratq Grim Update"
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBookUpdated.Content.Should().Contain(updateBookErrorMessageId);

        }

        [TestCase(TestName = "Update book without Description successfully")]
        public async Task PutBookWithoutDescriptionSuccesfully()
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

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me Update",
                Author = "Bratq Grim Update"
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBookUpdated.Content.ToString());

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Author.Should().Be(bookUpdated.Author);
            responseString.Description.Should().Be(null);
            responseString.Id.Should().Be(uniqueId);
            responseString.Title.Should().Be(bookUpdated.Title);
        }

        [TestCase(TestName = "Update book Author successfully")]
        public async Task PutBookAuthorSuccesfully()
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

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Description = "Test Description",
                Author = "Bratq Grim Update"
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBookUpdated.Content.ToString());

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Author.Should().Be(bookUpdated.Author);
            responseString.Description.Should().Be(bookUpdated.Description);
            responseString.Id.Should().Be(uniqueId);
            responseString.Title.Should().Be(bookUpdated.Title);
        }

        [TestCase(TestName = "Update book witout Author")]
        public async Task PutBookWithoutAuthor()
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

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Description = "Test Description"
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBookUpdated.Content.ToString());

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBookUpdated.Content.Should().Contain(createBookErrorMessageNoAuthor);
        }

        [TestCase(TestName = "Update book Title successfully")]
        public async Task PutBookTitleSuccesfully()
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

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me Updated",
                Description = "Test Description",
                Author = "Bratq Grim"
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBookUpdated.Content.ToString());

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Author.Should().Be(bookUpdated.Author);
            responseString.Description.Should().Be(bookUpdated.Description);
            responseString.Id.Should().Be(uniqueId);
            responseString.Title.Should().Be(bookUpdated.Title);
        }

        [TestCase(TestName = "Update book without Title")]
        public async Task PutBookWithoutTitle()
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

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Description = "Test Description",
                Author = "Bratq Grim"
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBookUpdated.Content.ToString());

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBookUpdated.Content.Should().Contain(createBookErrorMessageNoTitle);
        }

        [TestCase(TestName = "Update book Description successfully")]
        public async Task PutBookDescriptionSuccesfully()
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

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Description = "Test Description Updated",
                Author = "Bratq Grim"
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBookUpdated.Content.ToString());

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Author.Should().Be(bookUpdated.Author);
            responseString.Description.Should().Be(bookUpdated.Description);
            responseString.Id.Should().Be(uniqueId);
            responseString.Title.Should().Be(bookUpdated.Title);
        }

        [TestCase(TestName = "Update book Author with Max length successfully")]
        public async Task PutBookAuthorWithMaxLengthSuccesfully()
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

            string maxLengthString = "dijasdijdsajijajdijdsaijdpijdd";

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Description = "Test Description",
                Author = maxLengthString
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBookUpdated.Content.ToString());

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Author.Should().Be(bookUpdated.Author);
            responseString.Description.Should().Be(bookUpdated.Description);
            responseString.Id.Should().Be(uniqueId);
            responseString.Title.Should().Be(bookUpdated.Title);
        }

        [TestCase(TestName = "Update book Author with Over Max length Bad Request")]
        public async Task PutBookAuthorWithOverMaxLengthBadRequest()
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

            string overMaxLengthString = "dijasdijdsajijajdijdsaijdpijdda";

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Title = "Dear Me",
                Description = "Test Description",
                Author = overMaxLengthString
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBookUpdated.Content.Should().Contain(createBookErrorMessageMaxLengthForAuthor);
        }

        [TestCase(TestName = "Update book Title with Max length successfully")]
        public async Task PutBookTitleWithMaxLengthSuccesfully()
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

            string maxLengthString = "dijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddafsfghgs";

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Title = maxLengthString,
                Description = "Test Description",
                Author = "Valeri Bojinov"
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            BookModel responseString = JsonConvert.DeserializeObject<BookModel>(responseBookUpdated.Content.ToString());

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Author.Should().Be(bookUpdated.Author);
            responseString.Description.Should().Be(bookUpdated.Description);
            responseString.Id.Should().Be(uniqueId);
            responseString.Title.Should().Be(bookUpdated.Title);
        }

        [TestCase(TestName = "Update book Title with Over Max length")]
        public async Task PutBookTitleWithОOverMaxLengthSuccesfully()
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

            string maxLengthString = "dijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddadijasdijdsajijajdijdsaijdpijddafsfghgsа";

            BookModel bookUpdated = new BookModel
            {
                Id = uniqueId,
                Title = maxLengthString,
                Description = "Test Description",
                Author = "Valeri Bojinov"
            };


            RestRequest restRequestUpdated = new(bedeBaseUrl + bedeBooksPath + uniqueId, Method.Put);

            string jSonObjectUpdated = JsonConvert.SerializeObject(bookUpdated);

            restRequestUpdated.AddStringBody(jSonObjectUpdated, DataFormat.Json);
            restRequestUpdated.AddHeader("Accept", "application/json");
            restRequestUpdated.AddHeader("Content-Type", "application/json");

            RestResponse responseBookUpdated = await restClient.ExecutePutAsync(restRequestUpdated);

            responseBookUpdated.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBookUpdated.Content.Should().Contain(createBookErrorMessageMaxLengthForTitle);
        }
    }
}