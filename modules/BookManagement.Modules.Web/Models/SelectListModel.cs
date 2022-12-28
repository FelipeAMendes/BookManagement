using System;

namespace BookManagement.Modules.Web.Models;

public class SelectListModel
{
    public SelectListModel(Guid? id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid? Id { get; set; }
    public string Name { get; set; }
}