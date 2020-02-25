using System.Collections.Generic;
using GraphQL.Types;

namespace CleanArchitecture.WebUI.GraphQL
{
  public class AuthorQuery: ObjectGraphType
  {
    public AuthorQuery()
    {
      Field<AuthorType>(
        "Author",
        arguments: new QueryArguments(
          new QueryArgument<IdGraphType> { Name = "id", Description = "The ID of the Author." }),
        resolve: context =>
        {
          return new Author
          {
            Name = "Single Author", Books = new List<Book>()
          };
        });

      Field<ListGraphType<AuthorType>>(
        "Authors",
        resolve: context =>
        {
          var authors = new List<Author>()
          {
            new Author { Books = new List<Book>(), Name = "Second Author" },
            new Author { Books = new List<Book>(), Name = "First Author" }
          };
          return authors;
        });
    }
  }

  public class Book
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public bool Published { get; set; }

    public string Genre { get; set; }

    public string AuthorId { get; set; }

    public Author Author { get; set; }
  }

  public class Author
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; }
  }

  public class BookType : ObjectGraphType<Book>
  {
    public BookType()
    {
      Name = "Book";

      Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the Book.");
      Field(x => x.Name).Description("The name of the Book");
      Field(x => x.Genre).Description("Book genre");
      Field(x => x.Published).Description("If the book is published or not");
    }
  }

  public class AuthorType : ObjectGraphType<Author>
  {
    public AuthorType()
    {
      Name = "Author";

      Field(x => x.Id, type: typeof(IdGraphType)).Description("Author's ID.");
      Field(x => x.Name).Description("The name of the Author");
      Field(x => x.Books, type: typeof(ListGraphType<BookType>)).Description("Author's books");
    }
  }
}
