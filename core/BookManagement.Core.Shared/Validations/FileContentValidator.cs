using System.Text;
using BookManagement.Core.Shared.Extensions.StreamExtensions;
using BookManagement.Core.Shared.Validations.Inputs;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;

namespace BookManagement.Core.Shared.Validations;

public class FileContentValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private readonly FileContentValidatorInput[] _formats;

    public FileContentValidator(params FileContentValidatorInput[] formats)
    {
        _formats = formats;
    }

    public override string Name => nameof(FileContentValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value is not IFormFile formFile)
            return true;

        using var stream = formFile.OpenReadStream();
        var imageData = stream.ReadAllBytes();
        var valid = IsFormatValid(imageData);

        return valid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} conteúdo inválido";
    }

    private bool IsFormatValid(byte[] bytes)
    {
        var formats = new List<byte[]>();

        if (_formats.Contains(FileContentValidatorInput.Image))
        {
            var bmp = Encoding.ASCII.GetBytes("BM");
            var gif = Encoding.ASCII.GetBytes("GIF");
            var png = new byte[] {137, 80, 78, 71};
            var tiff = new byte[] {73, 73, 42};
            var tiff2 = new byte[] {77, 77, 42};
            var jpg = new byte[] {255, 216, 255};
            var jpeg = new byte[] {255, 216, 255, 224};
            var jpeg2 = new byte[] {255, 216, 255, 225};

            formats.Add(bmp);
            formats.Add(gif);
            formats.Add(png);
            formats.Add(tiff);
            formats.Add(tiff2);
            formats.Add(jpg);
            formats.Add(jpeg);
            formats.Add(jpeg2);
        }

        if (_formats.Contains(FileContentValidatorInput.Pdf))
        {
            var pdf = Encoding.ASCII.GetBytes("%PDF-");

            formats.Add(pdf);
        }

        return formats.Any(pattern => pattern.SequenceEqual(bytes.Take(pattern.Length)));
    }
}