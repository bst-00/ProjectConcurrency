/**
Made by Bruno van der Stel (1005978) and Yara Hammoud (0983695)
**/

namespace booksforall;
public class Customer
{
    // feel free to add necessary fields
    private Book? _currentBook; // Book currently held by the customer
    private readonly int _id;
    public Customer(int customerId) // feel free to change this constructor
    {
        _currentBook = null;
        _id = customerId;
    }

    public Book? GetCurrentBook()
    {
        return _currentBook;
    }
    // this is the work that the customer does
    public void DoWork() // feel free to add code to this method, 
                //  but DO NOT remove the existing one
                // do not alter the order of the instructions.
    {

        // the customer will come to the library when the book is ready
        // the customer picks up a book that he requested

        // Acquire lock for counter
        Program.counter_sem_full.WaitOne();
        Program.mutex.WaitOne();
        
        _currentBook = Program.counter.First();
            
        Program.counter.RemoveFirst();
            
        Console.WriteLine($"Customer {_id} is about to read the book {_currentBook.BookId}");

        // Release lock for counter
        Program.mutex.ReleaseMutex();
        Program.counter_sem_empty.Release();

        // the customer will take the book to read
        Thread.Sleep(new Random().Next(100, 500));

        //////////////////////////////DROPPING OFF BOOK AT DROPOFF COUNTER///////////////////////////////////////////
        // Acquire lock for dropoff
        Program.dropoff_sem_empty.WaitOne();
        Program.mutex.WaitOne();
        //the customer will return the book to the dropoff
        Console.WriteLine($"Customer {_id} is dropping off the book {_currentBook.BookId}");
            
        Program.dropoff.AddFirst(_currentBook);
        // Release Lock for dropoff
        Program.mutex.ReleaseMutex();
        Program.dropoff_sem_full.Release();

        _currentBook = null;

        Console.WriteLine($"Customer {_id} is leaving the library");

    }
}