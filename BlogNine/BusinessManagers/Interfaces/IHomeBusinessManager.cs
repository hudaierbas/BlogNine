﻿using BlogNine.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogNine.BusinessManagers.Interfaces
{
    public interface IHomeBusinessManager
    {
        ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString, int? page);
    }
}
