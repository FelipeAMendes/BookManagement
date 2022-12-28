using BookManagement.Core.Shared.Commands.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement.Modules.Web.Models;

public class BasePageModel : PageModel
{
    private ICommandResult _commandResult;

    public ICommandResult CommandResult
    {
        get => _commandResult;
        protected set
        {
            _commandResult = value;
            _commandResult.Errors.ToList().ForEach(err => AddModelStateError(err.PropertyName, err.ErrorMessage));
        }
    }

    public void AddModelStateError(string propertyName, string errorMessage)
    {
        var value = ModelState.FirstOrDefault(x => x.Key.ToLower().Contains(propertyName.ToLower())).Value;
        if (value is not null)
        {
            value.ValidationState = ModelValidationState.Invalid;
            value.Errors.Add(errorMessage);
        }
    }

    public SelectList CreateSelectList(IEnumerable<SelectListModel> items, string dataValueField, string dataTextField,
        object selectedValue = null)
    {
        var selectListModel = items.ToList();
        selectListModel.Insert(0, new SelectListModel(null, "Select"));
        return new SelectList(selectListModel, dataValueField, dataTextField, selectedValue);
    }
}