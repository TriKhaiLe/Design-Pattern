namespace Command
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string bookId, string title)
        {
            BookId = bookId;
            Title = title;
            IsAvailable = true;
        }
    }

    public class LibraryUser
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<Book> BorrowedBooks { get; set; }

        public LibraryUser(string userId, string name)
        {
            UserId = userId;
            Name = name;
            BorrowedBooks = new List<Book>();
        }
    }

    public class Library
    {
        public void BorrowBook(Book book, LibraryUser user)
        {
            if (book.IsAvailable)
            {
                book.IsAvailable = false;
                user.BorrowedBooks.Add(book);
                Console.WriteLine($"Sach {book.Title} da duoc muon boi {user.Name}");
            }
            else
            {
                Console.WriteLine($"Sach {book.Title} khong co san");
            }
        }

        public void ReturnBook(Book book, LibraryUser user)
        {
            if (user.BorrowedBooks.Remove(book))
            {
                book.IsAvailable = true;
                Console.WriteLine($"Sach {book.Title} da duoc tra boi {user.Name}");
            }
        }

        public void RenewBook(Book book, LibraryUser user)
        {
            if (user.BorrowedBooks.Contains(book))
            {
                Console.WriteLine($"Sach {book.Title} da duoc gia han boi {user.Name}");
            }
        }
    }

    public interface LibraryCommand
    {
        void Execute();
    }

    public class BorrowBookCommand : LibraryCommand
    {
        private Library _library;
        private Book _book;
        private LibraryUser _user;

        public BorrowBookCommand(Library library, Book book, LibraryUser user)
        {
            _library = library;
            _book = book;
            _user = user;
        }

        public void Execute()
        {
            _library.BorrowBook(_book, _user);
        }
    }

    public class ReturnBookCommand : LibraryCommand
    {
        private Library _library;
        private Book _book;
        private LibraryUser _user;

        public ReturnBookCommand(Library library, Book book, LibraryUser user)
        {
            _library = library;
            _book = book;
            _user = user;
        }

        public void Execute()
        {
            _library.ReturnBook(_book, _user);
        }
    }

    public class RenewBookCommand : LibraryCommand
    {
        private Library _library;
        private Book _book;
        private LibraryUser _user;

        public RenewBookCommand(Library library, Book book, LibraryUser user)
        {
            _library = library;
            _book = book;
            _user = user;
        }

        public void Execute()
        {
            _library.RenewBook(_book, _user);
        }
    }

    public class LibraryInvoker
    {
        private List<LibraryCommand> _commands = new List<LibraryCommand>();

        public void AddCommand(LibraryCommand command)
        {
            _commands.Add(command);
        }

        public void ExecuteCommands()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
            _commands.Clear();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            Book book1 = new Book("B001", "Sach 1");
            LibraryUser user1 = new LibraryUser("U001", "Nguyen Van Z");

            LibraryInvoker invoker = new LibraryInvoker();

            invoker.AddCommand(new BorrowBookCommand(library, book1, user1));
            invoker.AddCommand(new RenewBookCommand(library, book1, user1));
            invoker.AddCommand(new ReturnBookCommand(library, book1, user1));

            invoker.ExecuteCommands();
        }
    }
}
