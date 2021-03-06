﻿using Obsidian.Foundation.Modeling;
using System;
using System.Collections.Generic;

namespace Obsidian.Domain
{
    public class PermissionScope : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }

        public string ScopeName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IList<(string Type, string Value)> Claims { get; set; }

        public static PermissionScope Create(Guid id, string scopeName, string displayName, string description)
            => new PermissionScope
            {
                Id = id,
                ScopeName = scopeName,
                DisplayName = displayName,
                Description = description,
                Claims = new List<(string, string)>()
            };

        #region Equality

        public override bool Equals(object obj) => this.EntityEquals(obj);

        public override int GetHashCode() => Id.GetHashCode();

        #endregion Equality
    }
}