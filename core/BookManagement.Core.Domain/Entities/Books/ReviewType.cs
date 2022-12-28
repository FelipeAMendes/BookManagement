using System.ComponentModel;

namespace BookManagement.Core.Domain.Entities.Books;

public enum ReviewType
{
    [Description("My Review")] MyReview = 1,
    [Description("From the Inside Flap")] InsideFlat,
    [Description("From the Back Cover")] BackCover
}