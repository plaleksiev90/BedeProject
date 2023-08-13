using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedeBackEndTestProject.Utilities
{
    class Properties
    {
        //Base url and paths
        public const string bedeBaseUrl = "http://localhost:9000/api/";
        public const string bedeBooksPath = "books/";

        //Error messages
        public const string createBookErrorMessageNoId = "Book.Id should be a positive integer!";
        public const string createBookErrorMessageNoAuthor = "Book.Author is a required field.";
        public const string createBookErrorMessageNoTitle = "Book.Title is a required field";
        public const string createBookErrorMessageMaxLengthForAuthor = "Book.Author should not exceed 30 characters!";
        public const string createBookErrorMessageMaxLengthForTitle = "Book.Title should not exceed 100 characters!";
        public const string updateBookErrorMessageId = "Book.Id cannot be updated!";
    }
}
