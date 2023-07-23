

using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

class AppDbContext : DbContext {

 public AppDbContext( DbContextOptions<AppDbContext>options) : base(options)
 {

 }
              public DbSet<Book>  Books {get ; set;}  
              public DbSet <BookAuthor> BookAuthors {get ; set;}

              public DbSet <Author>  Authors  {get; set;}
              public DbSet <PriceOffer> PriceOffers {get; set;}
              public DbSet<Review>   Reviews {get; set;}
              public DbSet <Tag>      tag  {get; set;} 



}