﻿using Microsoft.Extensions.DependencyInjection;
using Obsidian.Application.OAuth20;
using Obsidian.Foundation.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Obsidian.Application.DependencyInjection
{
    public static class SagaBusServiceCollectionExtensions
    {
        /// <summary>
        /// Register <see cref="SagaBus"/> as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSagaBus(this IServiceCollection services) => services.AddSingleton(sp =>
        {
            var bus = new SagaBus(sp);
            bus.RegisterSagas();
            return bus;
        });

        /// <summary>
        /// Register sagas as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSagas(this IServiceCollection services)

        {
            foreach (var saga in _sagaTypes)
            {
                services.AddTransient(saga);
            }
            services.AddTransient<OAuth20Configuration>().AddTransient<OAuth20Service>();
            return services;
        }

        public static void RegisterSagas(this SagaBus bus)
        {
            foreach (var saga in _sagaTypes)
            {
                bus.Register(saga);
            }
        }

        private static readonly IEnumerable<Type> _sagaTypes = FindSagas();

        private static IEnumerable<Type> FindSagas()
        {
            var assembly = typeof(SagaBusServiceCollectionExtensions).GetTypeInfo().Assembly;
            return assembly.GetTypes()
                .Select(t => t.GetTypeInfo())
                .Where(t => !t.IsAbstract)
                .Where(t => t.HasBaseType(typeof(Saga)))
                .Select(t => t.AsType());
        }

        private static bool HasBaseType(this TypeInfo typeInfo, Type targetType)
        {
            TypeInfo currentType = typeInfo;
            do
            {
                if (currentType.IsEquivalentTo(targetType))
                {
                    return true;
                }
                currentType = currentType.BaseType.GetTypeInfo();
            } while (currentType.BaseType != null);
            return false;
        }
    }
}