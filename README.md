
<h1 align="center">
  BookStoreProject-Backend
</h1>

<p align="center">
<img src="https://cdn.dribbble.com/users/432077/screenshots/2822920/bookstore-logo.jpg">
</p>

<h1 align="center">
  Table of Contents :question:
</h1>
<ul>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--introduction-books">Introduction</a></li>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--technologies-used-computer">Technologies Used</a></li>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--techniques-bulb">Techniques</a></li>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--database-tables-beginner">Database Tables</a></li>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--aop-techniques--">AOP Techniques</a></li>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--jwt---">JWT</a></li>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--autofac-dependency-resolver-techniques--">DependencyResolver</a></li>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--ioc--">IoC</a></li>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--entity-framework-core--">Entity Framework Core</a></li>
  <li><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/README.md#--autofac--">Autofac</a></li>
</ul>

<h1 align="center">
  Introduction :books:
</h1>
<p>I developed this project to improve myself and learn new technologies. Contact me to tell about my <em>shortcomings</em>, other <em>technologies</em> I can add. :telephone_receiver:</p>

<h1 align="center">
  Technologies Used :computer:
</h1>

<ul>                                         
  <li>.NET</li>                                  
  <li>ASP.NET for Restful api</li>               
  <li>Entity Framework Core</li>                 
  <li>Autofac</li>                               
  <li>Fluent Validation</li>                     
  <li>MsSql</li>                               
</ul>
  
<h1 align="center">
  Techniques :bulb:
</h1>

<ul>
  <li>Layered Architecture Design Pattern</li>
  <li>AOP</li>
  <li>JWT</li>
  <li>Autofac dependency resolver</li>
  <li>IOC</li>
</ul>

<h1 align="center">
  Database Tables :beginner:
</h1>

![DatabaseTables](https://user-images.githubusercontent.com/90088895/132098258-e338e4fc-1f88-4657-a2a9-8b8b507214ce.png)
<br>
<br>
<p>It's a bit confusing, but you'll understand. :relaxed:</p>

<h1 align="center">
  AOP Techniques
  </h1>
  <p>Aspect-Oriented Programming (AOP), also named Aspect-Oriented Software Development (AOSD), is an approach to software development that goes further in the direction of separation of concerns. Separation of concerns is one of the most important rules in software development. It states that the same concern should be solved in a single unit of code. This is also called modularization . In procedural programming, the unit of code is the procedure (or function, or method). In object-oriented programming , the unit of code is the class.

Some concerns cannot be implemented successfully using a pure procedural or object-oriented programming. An example of this is code security. If you want to secure objects and methods, you have to modify the code of each method. That's why security is called a crosscutting concern, because it crosscuts the unit of modularization of the programming paradigm, in this case the class.</p>


```csharp
  
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Entities;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        T Get(Expression<Func<T, bool>> filter);
        List<T> GetList(Expression<Func<T, bool>> filter=null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
  }
 ```
  
  <p>sing the AOP programming model, you could, instead of modifying each method, develop an aspect and 'apply' it on methods of interest.</p>

```csharp

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context=new TContext())
            {
               var addedEntity = context.Entry(entity);
               addedEntity.State = EntityState.Added;
               context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
```

<h1 align="center">
  JWT 
  </h1>
  <p>A JWT is a mechanism to verify the owner of some JSON data. It’s an encoded, URL-safe string that can contain an unlimited amount of data (unlike a cookie) and is cryptographically signed.</p>
  <p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/tree/master/Core/Utilities/Security/Jwt" target="_blank">You can find the JWT codes here.</a></p>
  
  ```csharp
        [SecuredOperation("Book.List,Admin")] //Attribute that makes JWT meaningful
        [PerformanceAspect(5)]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Book>> GetAll()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Book>>(_bookDal.GetList().ToList(), Messages.BooksListed);
        }
  ```
 
  ![YetkinizYok](https://user-images.githubusercontent.com/90088895/132128893-2eb2cfc9-4ca8-4463-926b-fa2131c43461.jpg)
  ![Token](https://user-images.githubusercontent.com/90088895/132129092-61b501ee-7271-4c0e-8d3f-5388aa2a82ba.jpg)
  ![List](https://user-images.githubusercontent.com/90088895/132129209-e4f25bf0-b9da-4b29-94e8-3185d032272a.jpg)

  <p><code>The part I marked is where we place the token</code></p>
  
  <h1 align="center">
  Autofac dependency resolver Techniques
  </h1>
  
<p>Provides a registration point for dependency resolvers that implement <code>IDependencyResolver</code> or the Common Service Locator IServiceLocator interface.</p>
<a href="https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.dependencyresolver?view=aspnet-mvc-5.2">DependencyResolver Class</a>
  <br>
  <p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/blob/master/Business/DependencyResolvers/Autofac/AutofacBusinessModule.cs">You can find the Autofac Dependency Resolver codes here.</a></p>
  
  ```csharp
  
   builder.RegisterType<BookManager>().As<IBookService>().SingleInstance();
   builder.RegisterType<EfBookDal>().As<IBookDal>().SingleInstance();
  ```
  
  <h1 align="center">
  IoC
  </h1>
  
 <p>Inversion of control is a software design principle. With Ioc, it is aimed to minimize the dependencies of the object instances in the application by providing management. It can also be described as the framework that does the creation and management of dependencies in your project rather than the developer.</p>
  
  <h1 align="center">
  Entity Framework Core
  </h1>
  
  ```csharp
  
   public class BookContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BookDatabase;Trusted_Connection=true");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Subheading> Subheadings { get; set; }
        public DbSet<SubheadingOfSubheading> SubheadingsOfSubheading { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
  ```
  
  <h1 align="center">
  Autofac
  </h1>
  
  <p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/tree/master/Core/Aspects/Autofac" >You can find the Autofac codes here.</a></p>
  
  <h3>Contents</h3>
  
  <ul>
  <li><p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/tree/master/Core/Aspects/Autofac/Caching"</a>Caching</p></li>
  <li><p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/tree/master/Core/Aspects/Autofac/Caching"</a>Exception</p></li>
  <li><p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/tree/master/Core/Aspects/Autofac/Caching"</a>Logging</p></li>
  <li><p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/tree/master/Core/Aspects/Autofac/Caching"</a>Performance</p></li>
  <li><p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/tree/master/Core/Aspects/Autofac/Caching"</a>Transaction</p></li>
  <li><p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/tree/master/Core/Aspects/Autofac/Caching"</a>Validation</p></li>
  </ul>

<h1 align="center">
  Fluent Validation
  </h1>
  <p><code>FluentValidation</code> is a validation library for .NET used to create strict type validation rules for your business objects.</p>
  
```csharp

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.BookName).NotEmpty();
            RuleFor(b => b.BookName).MinimumLength(1);
            RuleFor(b => b.CategoryId).NotEmpty();
            RuleFor(b => b.LanguageId).NotEmpty();
            RuleFor(b => b.AuthorId).NotEmpty();
            RuleFor(b => b.PublisherId).NotEmpty();
            RuleFor(b => b.Price).NotEmpty();
            RuleFor(b => b.Price).GreaterThan(0);
            RuleFor(b => b.StockQty).NotEmpty();
        }
    }
}
```
<p><a href="https://github.com/atakancigdem/BookStoreProject-Backend/tree/master/Business/ValidationRules/FluentValidation" >You can find the FluentValidation codes here.</a></p>

<h2 align="center">
  See you on the frontend :wave:
  </h2>
