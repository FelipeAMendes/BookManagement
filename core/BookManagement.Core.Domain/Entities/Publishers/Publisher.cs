using BookManagement.Core.Domain.Entities.Publishers.Validations;
using BookManagement.Core.Shared.Entities;

namespace BookManagement.Core.Domain.Entities.Publishers;

public class Publisher : BaseEntity<PublisherValidation>
{
    public Publisher(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public void Edit(string name, string description)
    {
        Name = name;
        Description = description;
        SetUpdatedDate(DateTime.Now);
    }
}