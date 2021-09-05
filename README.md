
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
  <li>Introduction</li>
  <li>Technologies Used</li>
  <li>Techniques</li>
  <li>Database Tables</li>
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
  JWT Techniques
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
  <p align="center">
  
  ![YetkinizYok](https://user-images.githubusercontent.com/90088895/132128893-2eb2cfc9-4ca8-4463-926b-fa2131c43461.jpg)
  
  </p>
  
  <h1 align="center">
  Autofac dependency resolver Techniques)
  </h1>

<h2 align="center">
  See you on the frontend :wave:
  </h2>
