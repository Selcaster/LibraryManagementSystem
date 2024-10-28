namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var catalog = new LibraryCatalog();
            var book1 = new Book(BookGenre.Science, "Ensiklopediya", 1969, new LibraryLocation(1, 1));
            var book2 = new Book(BookGenre.Fiction, "FerhadinXeyallari", 2024, new LibraryLocation(3, 1));

            catalog.AddBook(book1);
            catalog.AddBook(book2);

            try
            {
                var book = catalog[1];
                book.DisplayInfo();
            }
            catch (CustomBookError ex)
            {
                Console.WriteLine(ex.Message);
            }

            catalog.RemoveBook(2);

            try
            {
                var book = catalog[2];
            }
            catch (CustomBookError ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
