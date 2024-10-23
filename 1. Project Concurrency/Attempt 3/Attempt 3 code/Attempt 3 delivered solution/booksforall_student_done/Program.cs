/**
Made by Bruno van der Stel (1005978) and Yara Hammoud (0983695)
**/

using System;

namespace booksforall
{
    internal class Program
    {
        //feel free to change the following values and if needed add variables
        public static int n_threads = 1000;// feel free to change this value

        private static readonly string studentname1 = "Bruno van der Stel";   //name and surname of the student1
        private static readonly string studentnum1 = "1005978";    //student number
        private static readonly string studentname2 = "Yara Hammoud";   //name and surname of the student2
        private static readonly string studentnum2 = "0983695";    //student number2


        // do not alter the following lines of code 
        // if you do, put them back as they were before submitting
        public static int n_books = n_threads;
        public static int n_customers = n_threads;
        public static readonly Clerk[] clerks = new Clerk[n_threads];
        public static readonly Customer[] customers = new Customer[n_threads];
        public static LinkedList<Book> counter = new();
        public static LinkedList<Book> dropoff = new();

        // Added variables
        private static List<Thread> clerk_threads = new List<Thread>();
        private static List<Thread> customer_threads = new List<Thread>();
        public static Mutex mutex = new Mutex();

        // We need a couple semaphores to track the number of free slots in the buffer 
        // and the number of full slots in the buffer
        public static Semaphore counter_sem_empty = new Semaphore(n_books, n_books);
        public static Semaphore counter_sem_full = new Semaphore(0, n_books);
        public static Semaphore dropoff_sem_empty = new Semaphore(n_books, n_books);
        public static Semaphore dropoff_sem_full = new Semaphore(0, n_books);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, we are starting our new pickup LIBRARY!");
            InitLibrary(); //do not alter this method
            
            //init the customers
            InitCustomers(); //do not alter this call

            //init the clerks
            InitClerks(); //do not alter this call

            //init records
            Clerk.initRecords(dropoff); //do not alter this line
            //clean the dropoff
            dropoff.Clear(); //do not alter this line

            //start the clerks
            StartClerks(); //do not alter this call

            //start the customers
            StartCustomers(); //do not alter this call
            // DO NOT CHANGE THE CODE ABOVE
            // use the space below to add your code if needed

            //We need a join method to wait for all clerk & customers threads to complete
            ClerksCustomersJoin();

            // DO NOT CHANGE THE CODE BELOW
            //the library is closing, DO NOT ALTER the following lines
            Console.WriteLine("Book left in the library " + Clerk.checkBookInInventory());

            if (counter.Count != 0)
            {
                Console.WriteLine("Books left and not picked up: " + counter.Count);
            }
            else
            {
                Console.WriteLine("Books left and not picked up: NOTHING LEFT!");
            }

            Console.WriteLine("Books left on the dropoff and not processed: " + dropoff.Count);
            // the lists should be empty
            Console.WriteLine("Name: " + studentname1 + " Student number: " + studentnum1);
            Console.WriteLine("Name: " + studentname2 + " Student number: " + studentnum2);

        }
        public static void InitLibrary() //do not alter this method
        {
            //a huge load of books arrives to the library, all at once.
            //init the books
            for (int i = 0; i < n_books; i++)
            {
                Book book = new Book(i);    //books are all different
                dropoff.AddLast(book);      //we load the books in the dropoff just
                                            // for easy access of the clerks
            }
        }
        public static void InitCustomers() // feel free to alter this method if needed
        {
            //init the customers
            for (int i = 0; i < customers.Length; i++) {
                customers[i] = new Customer(i);
            }
            //init a thread foreach customer
            foreach (Customer cu in customers) {
                Thread thread = new Thread(() =>
                {
                    cu.DoWork();
                });
                customer_threads.Add(thread);
            }

        }
        public static void InitClerks() // feel free to alter this method if needed
        {
            //init the clerks
            for (int i = 0; i < clerks.Length; i++) {
                clerks[i] = new Clerk(i);
            }
            //init a thread foreach clerk
            foreach (Clerk cl in clerks) {
                Thread thread = new Thread(() =>
                {
                    cl.DoWork();
                });
                clerk_threads.Add(thread);
            }

        }
        public static void StartClerks() // feel free to alter this method if needed
        {
            //start the clerks
            foreach (var thread in clerk_threads) {
                thread.Start();
            }
        }
        public static void StartCustomers() // feel free to alter this method if needed
        {
            //start the customers
            foreach (var thread in customer_threads) {
                thread.Start();
            }
        }
        public static void ClerksCustomersJoin()
        {
            //wait for all threads of clerks and customers to complete
            foreach (var thread in clerk_threads) {
                thread.Join();
            }
            foreach (var thread in customer_threads) {
                thread.Join();
            }
        }

    }

    public class Book // do not alter this class
    {
        public int BookId { get; set; } // Book identifier should always be something
        public Book(int bookId)
        {
            BookId = bookId;
        }
    }

    public class BookRecord // do not alter this class
    {
        public Book Book { get; set; }
        public bool IsBorrowed { get; set; } // True for borrowed, False for returned

        public BookRecord(Book book, bool isBorrowed)
        {
            Book = book;
            IsBorrowed = isBorrowed;
        }
    }
}