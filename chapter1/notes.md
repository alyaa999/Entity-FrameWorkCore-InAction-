# Entity-FrameWorkCore-InAction
Entity frame work InAction - Jon P Smith Foreword by Julie Lerman 

## Introduction to EntityFramework Core :
* EF Core, is a library that software developers can use to
access database
* EF Core is
designed as an object-relational mapper (O/RM). O/RMs work by mapping between
two worlds: the relational database, with its own API

*  EF Core isn’t the first version of Entity Framework; an existing, non-Core, Entity
Framework library is known as EF6.x
*  6 In EF6, you could use an EDMX/database designer to design your database visually, an option known as design-first. EF Core doesn’t support this
design-first approach in any form, and there are no plans to add it

* 6 In EF6, you could use an EDMX/database designer to design your database visually, an option known as design-first. EF Core doesn’t support this
design-first approach in any form, and there are no plans to add it

* The Books table name comes from the DbSet<Book>
Books property shown in figure 1.5. The Author table name hasn’t got a
DbSet<T> property in figure 1.5, so EF Core uses the name of the class.

* You must have a class that inherits from the EF Core class DbContext. This
class holds the information and configuration for accessing your database


* The database connection string holds information about the database 
    * How to find the database server
    * The name of the database
    * Authorization to access the database


* In a console application, you configure
EF Core’s database options by
overriding the OnConfiguring method.
In this case, you tell it you’re using an
SQL Server database by using the
UseSqlServer method.
* By creating a property called Books
of type DbSet<Book>, you tell EF Core
that there’s a database table named
Books, and it has the columns and
keys as found in the Book class.

* Our database has a table called Author, but you purposely didn’t create a property for that table.
EF Core finds that table by finding a navigational property of type Author in the Book class.


* the modeling steps that EF Core uses on our AppDbContext, which
happens the first time you create an instance of the AppDbContext. (After that, the
model is cached, so that subsequent instances are created quickly.) The following text
provides a more detailed description of the process:
    * EF Core looks at the application’s DbContext and finds all the public DbSet<T>
properties. From this data, it defines the initial name for the one table it finds:
Books.
  * EF Core looks through all the classes referred to in DbSet<T> and looks at its
properties to work out the column names, types, and so forth. It also looks for
special attributes on the class and/or properties that provide extra modeling
information.
  * EF Core looks for any classes that the DbSet<T> classes refer to. In our case, the
Book class has a reference to the Author class, so EF Core scans that class too , carries out the same search on the properties of the Author class as it did on the
Book class in step 2. It also takes the class name, Author, as the table name.
  *  For the last input to the modeling process, EF Core runs the virtual method
OnModelCreating inside the application’s DbContext. In this simple application, you don’t override the OnModelCreating method, but if you did, you
could provide extra information via a fluent API to do more configuration of
the modeling.
  *  EF Core creates an internal model of the database based on all the information
it gathered. This database model is cached so that later accesses will be quicker.
Then this model is used for performing all database accesses.

* The process to read data from the database is as follows:

  * The query db.Books.AsNoTracking().Include(book => book.Author) accesses
the DbSet<Book> property in the application’s DbContext and adds a .Include
(book => book.Author) at the end to ask that the Author parts of the relationship are loaded too. This is converted by the database provider into an SQL
command to access the database. The resulting SQL is cached to avoid the cost
of retranslation if the same database access is used again.



*  the code includes the command AsNoTracking, EF Core knows to suppress the creation of a tracking snapshot. Tracking snapshots are used for spotting
changes to data, as you’ll see in the example of editing the WebUrl database column
in section 1.9.3. Because this query is read-only, suppressing the tracking snapshot
makes the command faster.

*  I’ve described most of the steps, but here is a blow-by-blow account of how the
author’s WebUrl column is updated:
   * The application uses a LINQ query to find a single book with its author information. EF Core turns the LINQ query into an SQL command to read the rows
where the Title is Quantum Networking, returning an instance of both the Book
and the Author classes, and checks that only one row was found.
   * The LINQ query doesn’t include the .AsNoTracking method you had in the
   previous read versions, so the query is considered to be a tracked query. Therefore, EF Core creates a tracking snapshot of the data loaded.

   * Then the code changes the WebUrl property in the Author class of the book. When SaveChanges is called, the Detect Changes stage compares all the classes that were returned from a tracked query with the tracking snapshot. From this,it can detect what has changed—in this case, only the WebUrl property of the Author class, which has a primary key of 3.
    
    * As a change is detected, EF Core starts a transaction. Every database update is
done as an atomic unit: if multiple changes to the database occur, either they all
succeed, or they all fail. This fact is important, because a relational database
could get into a bad state if only part of an update were applied.
    *   The update request is converted by the database provider to an SQL command
that does the update. If the SQL command is successful, the transaction is committed, and the SaveChanges method returns; otherwise, an exception is

* EF Core has good documentation (https://docs.microsoft.com/en-us/ef/core/index),

* Also, EF Core isn’t that good at bulk commands. Normally, tasks such as bulk-loading
large amounts of data and deleting all the rows in a table can be implemented quicker
by raw SQL

* EF Core no longer supports the EDMX/database designer approach that earlier forms of EF used.
