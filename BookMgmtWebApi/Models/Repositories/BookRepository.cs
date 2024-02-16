namespace BookMgmtWebApi.Models.Repositories
{
    public static class BookRepository
    {
        private static List<Book> books = new List<Book>()
        {
            new() {BookId=1, Title="Book1",Genre="Horror",Author="Author1",Price=10},
            new() {BookId=2, Title="Book2",Genre="Comedy",Author="Author2",Price=20},
            new() {BookId=3, Title="Book3",Genre="Horror",Author="Author3",Price=30},
            new() {BookId=4, Title="Book4",Genre="Drama",Author="Author4",Price=40},
            new() {BookId=5, Title="Book5",Genre="Crime",Author="Author5",Price=50}
        };

        public static List<Book> GetBooks()
        {
            return books;
        }

        public static bool BookExists(int id)
        {
            return books.Any(b => b.BookId == id);
        }

        public static Book? GetBookByid(int id)
        {
            return books.FirstOrDefault(b => b.BookId == id);
        }

        public static void AddBook(Book book)
        {
            int maxId = books.Max(b => b.BookId);
            book.BookId = maxId + 1;
            books.Add(book);
        }

        public static Book? GetBookByProperties(string? name, string? genre, string? author)
        {
            return books.FirstOrDefault(b => !string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(b.Title) &&
                b.Title.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(genre) &&
                !string.IsNullOrWhiteSpace(b.Genre) &&
                b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(author) &&
                !string.IsNullOrWhiteSpace(b.Author) &&
                b.Title.Equals(author, StringComparison.OrdinalIgnoreCase));
        }

        public static void UpdateBook(Book book)
        {
            var bookToUpdate = books.First(b => b.BookId == book.BookId);
            bookToUpdate.Title = book.Title;
            bookToUpdate.Genre = book.Genre;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Price = book.Price;
        }

        public static void DeleteBook(int bookId)
        {
            var book = GetBookByid(bookId);
            if (book != null)
            {
                books.Remove(book);
            }
        }
    }
}
