using Bogus;
using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Infra.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Extensions;

public static class WebApplicationExtensions
{
    private const short AuthorsToGenerate = 10;
    private const short PublishersToGenerate = 10;
    private const short BooksToGenerate = 50;

    public static void ExecuteMigrations(this WebApplication app)
    {
        const int retryCount = 5;

        var migrateDbPolicyHandle = Policy.Handle<Exception>()
            .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

        migrateDbPolicyHandle.Execute(async () =>
        {
            await using var scope = app.Services.CreateAsyncScope();
            var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            await applicationContext.Database.MigrateAsync();

            await InsertAuthors(applicationContext);
            await InsertCategories(applicationContext);
            await InsertPublishers(applicationContext);
            await InsertBooks(applicationContext);
        });
    }

    private static async Task InsertAuthors(ApplicationContext applicationContext)
    {
        var existingAuthors = await applicationContext.Authors.AnyAsync();
        if (existingAuthors)
            return;

        var authors = new Faker<Author>()
            .CustomInstantiator(x => new Author(x.Name.FullName(), ""))
            .Generate(AuthorsToGenerate);

        applicationContext.Authors.AddRange(authors);
        await applicationContext.SaveChangesAsync();
    }

    private static async Task InsertPublishers(ApplicationContext applicationContext)
    {
        var existingPublishers = await applicationContext.Publishers.AnyAsync();
        if (existingPublishers)
            return;

        var publishers = new Faker<Publisher>()
            .CustomInstantiator(x => new Publisher(x.Company.CompanyName(), ""))
            .Generate(PublishersToGenerate);

        applicationContext.Publishers.AddRange(publishers);
        await applicationContext.SaveChangesAsync();
    }

    private static async Task InsertCategories(ApplicationContext applicationContext)
    {
        var existingCategories = await applicationContext.Categories.AnyAsync();
        if (existingCategories)
            return;

        var parentArtsAndMusic = new Category("Arts & Music", "Arts & Music");
        applicationContext.Categories.Add(new Category("Art History", "Art History", parentArtsAndMusic));
        applicationContext.Categories.Add(new Category("Calligraphy", "Calligraphy", parentArtsAndMusic));
        applicationContext.Categories.Add(new Category("Drawing", "Drawing", parentArtsAndMusic));
        applicationContext.Categories.Add(new Category("Fashion", "Fashion", parentArtsAndMusic));
        applicationContext.Categories.Add(new Category("Film", "Film", parentArtsAndMusic));
        applicationContext.Categories.Add(parentArtsAndMusic);

        var biographiesParent = new Category("Biographies", "Biographies");
        applicationContext.Categories.Add(new Category("Ethnic & Cultural", "Ethnic & Cultural", biographiesParent));
        applicationContext.Categories.Add(new Category("Europe", "Europe", biographiesParent));
        applicationContext.Categories.Add(new Category("Historical", "", biographiesParent));
        applicationContext.Categories.Add(new Category("Leaders & Notable People", "Leaders & Notable People", biographiesParent));
        applicationContext.Categories.Add(new Category("Military", "Military", biographiesParent));
        applicationContext.Categories.Add(biographiesParent);

        var bussinesParent = new Category("Business", "Business");
        applicationContext.Categories.Add(new Category("Careers", "Careers", bussinesParent));
        applicationContext.Categories.Add(new Category("Economics", "Economics", bussinesParent));
        applicationContext.Categories.Add(new Category("Finance", "Finance", bussinesParent));
        applicationContext.Categories.Add(new Category("Industries", "Industries", bussinesParent));
        applicationContext.Categories.Add(new Category("International", "International", bussinesParent));
        applicationContext.Categories.Add(bussinesParent);

        var comicsParent = new Category("Comics", "Comics");
        applicationContext.Categories.Add(new Category("Comic Books", "Comic Books", comicsParent));
        applicationContext.Categories.Add(new Category("Comic Strips", "Comic Strips", comicsParent));
        applicationContext.Categories.Add(new Category("Dark Horse", "Dark Horse", comicsParent));
        applicationContext.Categories.Add(new Category("DC Comics", "DC Comics", comicsParent));
        applicationContext.Categories.Add(new Category("Fantasy", "Fantasy", comicsParent));
        applicationContext.Categories.Add(comicsParent);

        var computersAndTechParent = new Category("Computers & Tech", "Computers & Tech");
        applicationContext.Categories.Add(new Category("Apple", "Apple", computersAndTechParent));
        applicationContext.Categories.Add(new Category("CAD", "CAD", computersAndTechParent));
        applicationContext.Categories.Add(new Category("Certification", "Certification", computersAndTechParent));
        applicationContext.Categories.Add(new Category("Computer Science", "Computer Science", computersAndTechParent));
        applicationContext.Categories.Add(new Category("Databases", "Databases", computersAndTechParent));
        applicationContext.Categories.Add(computersAndTechParent);

        var cookingParent = new Category("Cooking", "Cooking");
        applicationContext.Categories.Add(new Category("Asian", "Asian", cookingParent));
        applicationContext.Categories.Add(new Category("Baking", "Baking", cookingParent));
        applicationContext.Categories.Add(new Category("BBQ", "BBQ", cookingParent));
        applicationContext.Categories.Add(new Category("Culinary Arts", "Culinary Arts", cookingParent));
        applicationContext.Categories.Add(new Category("Desserts", "Desserts", cookingParent));
        applicationContext.Categories.Add(cookingParent);

        var eduAndReferenceParent = new Category("Edu & Reference", "Edu & Reference");
        applicationContext.Categories.Add(new Category("Almanacs & Yearbooks", "Almanacs & Yearbooks", eduAndReferenceParent));
        applicationContext.Categories.Add(new Category("Atlases & Maps", "Atlases & Maps", eduAndReferenceParent));
        applicationContext.Categories.Add(new Category("Catalogs", "Catalogs", eduAndReferenceParent));
        applicationContext.Categories.Add(new Category("Colleges", "Colleges", eduAndReferenceParent));
        applicationContext.Categories.Add(new Category("Continuing Education", "Continuing Education", eduAndReferenceParent));
        applicationContext.Categories.Add(eduAndReferenceParent);

        var entertainmentParent = new Category("Entertainment", "Entertainment");
        applicationContext.Categories.Add(new Category("Brain Teasers", "Brain Teasers", entertainmentParent));
        applicationContext.Categories.Add(new Category("Entertainers", "Entertainers", entertainmentParent));
        applicationContext.Categories.Add(new Category("Games", "Games", entertainmentParent));
        applicationContext.Categories.Add(new Category("Humor", "Humor", entertainmentParent));
        applicationContext.Categories.Add(new Category("Movies", "Movies", entertainmentParent));
        applicationContext.Categories.Add(entertainmentParent);

        var healthAndFitnessParent = new Category("Health & Fitness", "Health & Fitness");
        applicationContext.Categories.Add(new Category("Aging", "Aging", healthAndFitnessParent));
        applicationContext.Categories.Add(new Category("Alternative Medicine", "Alternative Medicine", healthAndFitnessParent));
        applicationContext.Categories.Add(new Category("Beauty, Grooming & Style", "Beauty, Grooming & Style", healthAndFitnessParent));
        applicationContext.Categories.Add(new Category("Children's Health", "Children's Health", healthAndFitnessParent));
        applicationContext.Categories.Add(new Category("Disease & Ailments", "Disease & Ailments", healthAndFitnessParent));
        applicationContext.Categories.Add(healthAndFitnessParent);

        var historyParent = new Category("History", "History");
        applicationContext.Categories.Add(new Category("African", "African", historyParent));
        applicationContext.Categories.Add(new Category("Ancient", "Ancient", historyParent));
        applicationContext.Categories.Add(new Category("Asian", "Asian", historyParent));
        applicationContext.Categories.Add(new Category("Black History", "Black History", historyParent));
        applicationContext.Categories.Add(historyParent);

        var hobbiesAndCraftsParent = new Category("Hobbies & Crafts", "Hobbies & Crafts");
        applicationContext.Categories.Add(new Category("Antiques", "Antiques", hobbiesAndCraftsParent));
        applicationContext.Categories.Add(new Category("Clay", "Clay", hobbiesAndCraftsParent));
        applicationContext.Categories.Add(new Category("Collecting", "Collecting", hobbiesAndCraftsParent));
        applicationContext.Categories.Add(new Category("Fashion", "Fashion", hobbiesAndCraftsParent));
        applicationContext.Categories.Add(new Category("Jewelry Making", "Jewelry Making", hobbiesAndCraftsParent));
        applicationContext.Categories.Add(hobbiesAndCraftsParent);

        var homeAndGardenParent = new Category("Home & Garden", "Home & Garden");
        applicationContext.Categories.Add(new Category("Architecture", "Architecture", homeAndGardenParent));
        applicationContext.Categories.Add(new Category("Flowers", "Flowers", homeAndGardenParent));
        applicationContext.Categories.Add(new Category("Fruit", "Fruit", homeAndGardenParent));
        applicationContext.Categories.Add(new Category("Home Decorating", "Home Decorating", homeAndGardenParent));
        applicationContext.Categories.Add(new Category("Home Improvement", "Home Improvement", homeAndGardenParent));
        applicationContext.Categories.Add(homeAndGardenParent);

        var horrorParent = new Category("Horror", "Horror");
        applicationContext.Categories.Add(new Category("Ghosts", "Ghosts", horrorParent));
        applicationContext.Categories.Add(new Category("Paranormal", "Paranormal", horrorParent));
        applicationContext.Categories.Add(new Category("Supernatural", "Supernatural", horrorParent));
        applicationContext.Categories.Add(new Category("Vampires", "Vampires", horrorParent));
        applicationContext.Categories.Add(new Category("Zombies", "Zombies", horrorParent));
        applicationContext.Categories.Add(horrorParent);

        var kidsParent = new Category("Kids", "Kids");
        applicationContext.Categories.Add(new Category("Action & Adventure", "Action & Adventure", kidsParent));
        applicationContext.Categories.Add(new Category("Activities, Crafts & Games", "Activities, Crafts & Games", kidsParent));
        applicationContext.Categories.Add(new Category("Activity Books", "Activity Books", kidsParent));
        applicationContext.Categories.Add(new Category("Animals", "Animals", kidsParent));
        applicationContext.Categories.Add(kidsParent);

        var literatureAndFictionParent = new Category("Literature & Fiction", "Literature & Fiction");
        applicationContext.Categories.Add(new Category("Anthologies", "Anthologies", literatureAndFictionParent));
        applicationContext.Categories.Add(new Category("Classics", "Classics", literatureAndFictionParent));
        applicationContext.Categories.Add(new Category("Contemporary", "Contemporary", literatureAndFictionParent));
        applicationContext.Categories.Add(new Category("Foreign Language", "Foreign Language", literatureAndFictionParent));
        applicationContext.Categories.Add(new Category("Genre Fiction", "Genre Fiction", literatureAndFictionParent));
        applicationContext.Categories.Add(literatureAndFictionParent);

        var medicalParent = new Category("Medical", "Medical");
        applicationContext.Categories.Add(new Category("Administration", "Administration", medicalParent));
        applicationContext.Categories.Add(new Category("Allied Health", "Allied Health", medicalParent));
        applicationContext.Categories.Add(new Category("Basic Sciences", "Basic Sciences", medicalParent));
        applicationContext.Categories.Add(new Category("Clinical", "Clinical", medicalParent));
        applicationContext.Categories.Add(new Category("Dentistry", "Dentistry", medicalParent));
        applicationContext.Categories.Add(medicalParent);

        var mysteriesParent = new Category("Mysteries", "Mysteries");
        applicationContext.Categories.Add(new Category("Conspiracy", "Conspiracy", mysteriesParent));
        applicationContext.Categories.Add(new Category("Crime", "Crime", mysteriesParent));
        applicationContext.Categories.Add(new Category("Detective", "Detective", mysteriesParent));
        applicationContext.Categories.Add(new Category("Mysteries", "Mysteries", mysteriesParent));
        applicationContext.Categories.Add(new Category("Suspense", "Suspense", mysteriesParent));
        applicationContext.Categories.Add(mysteriesParent);

        var parentingParent = new Category("Parenting", "Parenting");
        applicationContext.Categories.Add(new Category("Adoption", "Adoption", parentingParent));
        applicationContext.Categories.Add(new Category("Aging Parents", "Aging Parents", parentingParent));
        applicationContext.Categories.Add(new Category("Child Care", "Child Care", parentingParent));
        applicationContext.Categories.Add(new Category("Family Activities", "Family Activities", parentingParent));
        applicationContext.Categories.Add(new Category("Family Health", "Family Health", parentingParent));
        applicationContext.Categories.Add(parentingParent);

        var religionParnet = new Category("Religion", "Religion");
        applicationContext.Categories.Add(new Category("Agnosticism", "Agnosticism"));
        applicationContext.Categories.Add(new Category("Astrology", "Astrology"));
        applicationContext.Categories.Add(new Category("Atheism", "Atheism"));
        applicationContext.Categories.Add(new Category("Buddhism", "Buddhism"));
        applicationContext.Categories.Add(new Category("Christian", "Christian"));
        applicationContext.Categories.Add(religionParnet);

        var romanceParent = new Category("Romance", "Romance");
        applicationContext.Categories.Add(new Category("Anthologies", "Anthologies", romanceParent));
        applicationContext.Categories.Add(new Category("Contemporary", "Contemporary", romanceParent));
        applicationContext.Categories.Add(new Category("Erotica", "Erotica", romanceParent));
        applicationContext.Categories.Add(new Category("Fantasy", "Fantasy", romanceParent));
        applicationContext.Categories.Add(new Category("Gothic", "Gothic", romanceParent));
        applicationContext.Categories.Add(romanceParent);

        var fantasyParent = new Category("Sci-Fi & Fantasy", "Sci-Fi & Fantasy");
        applicationContext.Categories.Add(new Category("Action", "Action", fantasyParent));
        applicationContext.Categories.Add(new Category("Anthologies", "Anthologies", fantasyParent));
        applicationContext.Categories.Add(new Category("Coming of Age", "Coming of Age", fantasyParent));
        applicationContext.Categories.Add(new Category("Dark Fantasy", "Dark Fantasy", fantasyParent));
        applicationContext.Categories.Add(new Category("Fantasy", "Fantasy", fantasyParent));
        applicationContext.Categories.Add(fantasyParent);

        var scienceAndMathParent = new Category("Science & Math", "Science & Math");
        applicationContext.Categories.Add(new Category("Agricultural Sciences", "Agricultural Sciences", scienceAndMathParent));
        applicationContext.Categories.Add(new Category("Anatomy", "Anatomy", scienceAndMathParent));
        applicationContext.Categories.Add(new Category("Animals", "Animals", scienceAndMathParent));
        applicationContext.Categories.Add(new Category("Astronomy", "Astronomy", scienceAndMathParent));
        applicationContext.Categories.Add(new Category("Biology", "Biology", scienceAndMathParent));
        applicationContext.Categories.Add(scienceAndMathParent);

        var selfHelpParent = new Category("Self-Help", "Self-Help");
        applicationContext.Categories.Add(new Category("Abuse", "Abuse", selfHelpParent));
        applicationContext.Categories.Add(new Category("Addictions", "Addictions", selfHelpParent));
        applicationContext.Categories.Add(new Category("Anger Management", "Anger Management", selfHelpParent));
        applicationContext.Categories.Add(new Category("Anxieties & Phobias", "Anxieties & Phobias", selfHelpParent));
        applicationContext.Categories.Add(new Category("Creativity", "Creativity", selfHelpParent));
        applicationContext.Categories.Add(selfHelpParent);

        var socialSciencesParent = new Category("Social Sciences", "Social Sciences");
        applicationContext.Categories.Add(new Category("Anarchy", "Anarchy", socialSciencesParent));
        applicationContext.Categories.Add(new Category("Civil Rights", "Civil Rights", socialSciencesParent));
        applicationContext.Categories.Add(new Category("Comparative Politics", "Comparative Politics", socialSciencesParent));
        applicationContext.Categories.Add(new Category("Cultural", "Cultural", socialSciencesParent));
        applicationContext.Categories.Add(socialSciencesParent);

        var sportsParent = new Category("Sports", "Sports");
        applicationContext.Categories.Add(new Category("Baseball & Softball", "Baseball & Softball", sportsParent));
        applicationContext.Categories.Add(new Category("Basketball", "Basketball", sportsParent));
        applicationContext.Categories.Add(new Category("Boating & Sailing", "Boating & Sailing", sportsParent));
        applicationContext.Categories.Add(new Category("Camping", "Camping", sportsParent));
        applicationContext.Categories.Add(new Category("Extreme Sports", "Extreme Sports", sportsParent));
        applicationContext.Categories.Add(sportsParent);

        var teenParent = new Category("Teen", "Teen");
        applicationContext.Categories.Add(new Category("Being a Teen", "Being a Teen", teenParent));
        applicationContext.Categories.Add(new Category("Fantasy", "Fantasy", teenParent));
        applicationContext.Categories.Add(new Category("Historical Fiction", "Historical Fiction", teenParent));
        applicationContext.Categories.Add(new Category("Hobbies", "Hobbies", teenParent));
        applicationContext.Categories.Add(new Category("Horror", "Horror", teenParent));
        applicationContext.Categories.Add(teenParent);

        var travelParent = new Category("Travel", "Travel");
        applicationContext.Categories.Add(new Category("Africa", "Africa", travelParent));
        applicationContext.Categories.Add(new Category("Asia", "Asia", travelParent));
        applicationContext.Categories.Add(new Category("America", "America", travelParent));
        applicationContext.Categories.Add(new Category("Europe", "Europe", travelParent));
        applicationContext.Categories.Add(new Category("Oceania", "Oceania", travelParent));
        applicationContext.Categories.Add(travelParent);

        var trueCrimeParent = new Category("True Crime", "True Crime");
        applicationContext.Categories.Add(new Category("Criminal Law", "Criminal Law", trueCrimeParent));
        applicationContext.Categories.Add(new Category("Famous Criminals", "Famous Criminals", trueCrimeParent));
        applicationContext.Categories.Add(new Category("Murder & Mayhem", "Murder & Mayhem", trueCrimeParent));
        applicationContext.Categories.Add(new Category("Organized Crime", "Organized Crime", trueCrimeParent));
        applicationContext.Categories.Add(new Category("Serial Killers", "Serial Killers", trueCrimeParent));
        applicationContext.Categories.Add(trueCrimeParent);

        await applicationContext.SaveChangesAsync();
    }

    private static async Task InsertBooks(ApplicationContext applicationContext)
    {
        var existingBooks = await applicationContext.Books.AnyAsync();
        if (existingBooks)
            return;

        var books = await GetBookFakerList(applicationContext, BooksToGenerate);
        applicationContext.Books.AddRange(books);
        await applicationContext.SaveChangesAsync();
    }

    private static async Task<List<Book>> GetBookFakerList(ApplicationContext applicationContext, int number)
    {
        var bookFaker = new Faker<Book>();
        var booksList = new List<Book>();

        var authors = await applicationContext.Authors.ToListAsync();
        var categories = await applicationContext.Categories.Take(10).ToListAsync();
        var publishers = await applicationContext.Publishers.ToListAsync();

        var i = 1;
        var random = new Random();
        while (i <= number)
        {
            var book = bookFaker.CustomInstantiator(x =>
            {
                var book = new Book(
                    x.Name.JobTitle(),
                    x.Lorem.Paragraph(),
                    x.Date.Past(yearsToGoBack: 10, refDate: DateTime.Today),
                    x.Random.Enum<Format>(),
                    null,
                    null);

                book.AddQuote(new Quote(x.Lorem.Paragraph()));
                book.AddKeyword(new Keyword(x.Lorem.Word()));
                book.AddKeyword(new Keyword(x.Lorem.Word()));
                book.AddKeyword(new Keyword(x.Lorem.Word()));
                book.AddReview(new Review(x.Lorem.Paragraph(), x.Random.Enum<ReviewType>(), x.Name.FullName(), x.Lorem.Sentence(wordCount: 50)));

                return book;
            }).Generate();

            var index = random.Next(AuthorsToGenerate);
            book.SetAuthor(authors[index]);
            book.SetCategory(categories[index]);
            book.SetPublisher(publishers[index]);

            booksList.Add(book);
            i++;
        }

        return booksList;
    }
}