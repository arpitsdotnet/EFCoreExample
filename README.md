# EF-Core Implementation in Code-First Approach & Seed Data

While working with the EF-Core Code-First approach, we create the classes for our domain entities first. Later, we create the database from our code by using migrations. This is the opposite of Data-First approach where we design our database first and then create the classes which match our database design.

## How Does It Work?
-	**Model First:** First we create a class where we write properties as columns of a data table.
-	**Database Linked Instance:** A `DbSet<TEntity>` can be used to query and save instances of our Model. LINQ queries against a `DbSet<TEntity>` will be translated into queries against the database.
-	**Seed Data:** When you want your data-table prefilled with data, `OnModelCreating` override method is required.
-	**Adding the Migration:** Using Terminal we can add or update our migrations from classes to database.

## Example
1.	Add a Database Connection into your project’s `Program.cs` file, you can define your connection string in `appsettings.json` file

```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
app.Run();
```

2.	Create your Model with getter and setter properties

```
public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    public string CategoryName { get; set; }

    public int CategoryDisplayOrder { get; set; }
}
```

`[Key]` attribute will indicate the EF-Core to create the field with identity key and primary key constraints.
`[Required]` attribute will make the field required and Not-Null field.

3.	Create ApplicationDbContext class to manage DbContext entities

```
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
}
```

4.	To add seed data prefilled into your `Category` table

```
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
    	new Category { CategoryId = 1, CategoryName = "Action", CategoryDisplayOrder = 1 },
    	new Category { CategoryId = 2, CategoryName = "SciFi", CategoryDisplayOrder = 2 },
    	new Category { CategoryId = 3, CategoryName = "History", CategoryDisplayOrder = 3 }
        );
    }
}
```

5.	To open a **Package Manager Console**, Go to **Tools > NuGet Package Manager > Package Manager Console**

![](https://github.com/arpitsdotnet/EFCoreExample/blob/master/assets/images/2024-09-07%20(1).png)


6.	When **Package Manage Console** tab opened; type `add-migration <migration_name>`, this will create a `migration_name.cs` file which you can assess to check if correct table is being created.

> [!TIP]
> If the above comment throws error; Make sure you have a package installed `Microsoft.EntityFrameworkCore.Tools`

![](https://github.com/arpitsdotnet/EFCoreExample/blob/master/assets/images/2024-09-07%20(2).png)


7.	To reflect all changes to database; type `update-database`.

![](https://github.com/arpitsdotnet/EFCoreExample/blob/master/assets/images/2024-09-07%20(3).png)



•	Now you can check your table in Database

![](https://github.com/arpitsdotnet/EFCoreExample/blob/master/assets/images/2024-09-07%20(4).png)

