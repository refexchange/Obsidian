﻿using System.Collections.Generic;

namespace Obsidian.Application.Dto
{
    public class UpdateScopeDto
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public IList<(string Type, string Value)> Claims { get; set; }
    }
}
