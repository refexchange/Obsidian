﻿using Obsidian.Foundation.ProcessManagement;
using System;
using System.Collections.Generic;

namespace Obsidian.Application.OAuth20
{
    public class PermissionGrantMessage : Message<OAuth20Result>
    {
        public PermissionGrantMessage(Guid sagaId) : base(sagaId)
        {
        }

        public IList<string> GrantedScopeNames { get; set; }
    }
}